using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB
{
    class MovieDB
    {
        #region Properties

        internal string ConnectionString => ConfigurationManager.AppSettings.Get("DBConnection");
        internal Data.User CurrentUser { get; set; }
        internal bool IsManagerMode => CurrentUser?.Role == Data.UserRole.Manager;
        internal bool IsAuthorized => CurrentUser != null;

        #endregion

        #region DB Constants

        // tables
        internal const string c_ActorsTable = "Actors";
        internal const string c_MovieActorTable = "MovieActor";
        internal const string c_MovieOrderTable = "MovieOrder";
        internal const string c_MoviesTable = "Movies";
        internal const string c_OrdersTable = "Orders";
        internal const string c_UsersTable = "Users";
        internal const string c_StudioTable = "Studio";

        // primary and foreign keys
        internal const string c_ActorIdColumn = "actorId";
        internal const string c_MovieIdColumn = "movieId";
        internal const string c_OrderIdColumn = "orderId";
        internal const string c_UserIdColumn = "userId";
        internal const string c_StudioIdColumn = "studioId";

        // data columns
        internal const string c_AwardsDescriptionColumn = "awardsDescription";
        internal const string c_BirthDateColumn = "birthDate";
        internal const string c_CountryColumn = "country";
        internal const string c_DateColumn = "date";
        internal const string c_DescriptionColumn = "description";
        internal const string c_EMailColumn = "email";
        internal const string c_FamilyStatusColumn = "familyStatus";
        internal const string c_FirstNameColumn = "firstName";
        internal const string c_FoundationDateColumn = "foundationDate";
        internal const string c_GenreColumn = "genre";
        internal const string c_ImdbColumn = "imdb";
        internal const string c_MovieYearColumn = "movieYear";
        internal const string c_PasswordColumn = "password";
        internal const string c_PriceColumn = "price";
        internal const string c_ProductionColumn = "production";
        internal const string c_RoleColumn = "role";
        internal const string c_SaltColumn = "salt";
        internal const string c_SecondNameColumn = "secondName";
        internal const string c_TitleColumn = "title";

        // virtual columns
        internal const string c_IncomeColumn = "income";
        internal const string c_LastOrderColumn = "lastOrder";

        // enums
        internal const string c_RoleUserValue = "user";
        internal const string c_RoleManagerValue = "manager";
        internal const string c_FamilyStatusSingleValue = "single";
        internal const string c_FamilyStatusMarriedValue = "married";

        #endregion

        #region Methods

        internal void Logout()
        {
            CurrentUser = null;
        }

        internal void Login(string email, string password)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand($"SELECT * FROM {c_UsersTable} WHERE {c_EMailColumn} = {BuildParameterName(c_EMailColumn)} LIMIT 1;", connection))
            {
                command.Parameters.Add(new MySqlParameter(BuildParameterName(c_EMailColumn), email));

                using (var adapter = new MySqlDataAdapter(command))
                {
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    Debug.Assert(ds.Tables.Count > 0);

                    var table = ds.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        var user = Serializers.UserSerializer.Load(table.Rows[0]);
                        if (user.CheckPassword(password))
                        {
                            CurrentUser = user;
                        }
                        else
                        {
                            throw new InvalidPasswordDBException();
                        }
                    }
                    else
                    {
                        throw new UserNotFoundDBException(email);
                    }
                }
            }
        }

        internal void LoginAsNewUser(Data.User user)
        {
            Debug.Assert(!string.IsNullOrEmpty(user.EMail));
            Debug.Assert(!string.IsNullOrEmpty(user.PasswordHash));
            Debug.Assert(!string.IsNullOrEmpty(user.Salt));

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand($"SELECT * FROM {c_UsersTable} WHERE {c_EMailColumn} = LOWER({BuildParameterName(c_EMailColumn)}) LIMIT 1;", connection))
            {
                command.Parameters.Add(new MySqlParameter(BuildParameterName(c_EMailColumn), user.EMail.ToLower()));

                using (var adapter = new MySqlDataAdapter(command))
                {
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    Debug.Assert(ds.Tables.Count > 0);

                    var table = ds.Tables[0];
                    if (table.Rows.Count == 0)
                    {
                        var row = table.NewRow();
                        Serializers.UserSerializer.Save(user, row);
                        table.Rows.Add(row);

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(ds);

                        using (var idCommand = new MySqlCommand($"SELECT {c_UserIdColumn} FROM {c_UsersTable} WHERE {c_EMailColumn} = {BuildParameterName(c_EMailColumn)} LIMIT 1;", connection))
                        {
                            idCommand.Parameters.Add(new MySqlParameter(BuildParameterName(c_EMailColumn), user.EMail));
                            adapter.SelectCommand = idCommand;

                            ds.Clear();
                            adapter.Fill(ds);

                            Serializers.UserSerializer.LoadId(user, ds.Tables[0].Rows[0]);
                            CurrentUser = user;
                        }
                    }
                    else
                    {
                        throw new UserAlreadyExistsDBException(user.EMail);
                    }
                }
            }
        }

        internal IList<Data.Movie> GetMovies(int? limit = null, int? offset = null, IDataFilter filter = null, bool loadActors = false)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Movie>();

            // load movies
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // Movie table fields with aliases
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieIdColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieYearColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_GenreColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_DescriptionColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_ImdbColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_PriceColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_StudioIdColumn),
                                                    // Studio table fields with aliases
                                                    BuildFieldNameWithAliase(c_StudioTable, c_StudioIdColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_FoundationDateColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_ProductionColumn),
                                                  })
                                           .From(c_MoviesTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Left, c_StudioTable, c_StudioIdColumn)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var movie = Serializers.MovieSerializer.Load(r, BuildFiledPrefix(c_MoviesTable));
                                var studio = Serializers.StudioSerializer.Load(r, BuildFiledPrefix(c_StudioTable));
                                movie.Studio = studio;

                                res.Add(movie);
                            }
                        }
                    }
                }
            }

            // load actors
            if (loadActors)
            {
                ExpandActors(res);
            }

            return res;
        }

        internal IList<Data.Movie> GetTopMovies(int? limit = null, int? offset = null, IDataFilter filter = null, bool loadActors = false)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Movie>();

            // load movies
            {
                var sqlBuilder = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // Movie table fields with aliases
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieIdColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieYearColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_GenreColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_DescriptionColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_ImdbColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_PriceColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_StudioIdColumn),
                                                    // Studio table fields with aliases
                                                    BuildFieldNameWithAliase(c_StudioTable, c_StudioIdColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_FoundationDateColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_ProductionColumn),
                                                    // Aggregated fields,
                                                    $"SUM({BuildFieldName(c_MoviesTable, c_PriceColumn)}) AS {c_IncomeColumn}",
                                                  })
                                           .From(c_MoviesTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Left, c_StudioTable, c_StudioIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieOrderTable, c_MovieIdColumn)
                                           .GroupBy(BuildFieldName(c_MoviesTable, c_MovieIdColumn))
                                           .Pagging(limit, offset)
                                           .AddFilter(filter);

                if (!filter?.GetOrderClauses()?.Any() ?? true)
                {
                    sqlBuilder.OrderBy(c_IncomeColumn, true);
                }

                var sql = sqlBuilder.Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var movie = Serializers.MovieSerializer.Load(r, BuildFiledPrefix(c_MoviesTable));
                                var studio = Serializers.StudioSerializer.Load(r, BuildFiledPrefix(c_StudioTable));
                                movie.Studio = studio;

                                Serializers.MovieSerializer.LoadAggregated(movie, r);

                                res.Add(movie);
                            }
                        }
                    }
                }
            }

            // load actors
            if (loadActors)
            {
                ExpandActors(res);
            }

            return res;
        }

        internal IList<Data.Movie> GetAdviceMovies(int userdId, int? limit = null, int? offset = null, IDataFilter filter = null, bool loadActors = false)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Movie>();

            // load movies
            {
                var userIdParam = BuildParameterName(c_UserIdColumn);

                var topUserGenre = $"SELECT {BuildFieldName(c_MoviesTable, c_GenreColumn)} FROM {c_MoviesTable}\n" + 
                                   $"INNER JOIN {c_MovieOrderTable} USING ({c_MovieIdColumn})\n" + 
                                   $"INNER JOIN {c_OrdersTable} USING ({c_OrderIdColumn})\n" +
                                   $"WHERE {c_UserIdColumn} = {userIdParam}\n" +
                                   $"GROUP BY {c_GenreColumn}\n" +
                                   $"ORDER BY COUNT({BuildFieldName(c_MoviesTable, c_GenreColumn)}) DESC";

                var watchedMovies = $"SELECT {BuildFieldName(c_MoviesTable, c_MovieIdColumn)} FROM {c_MoviesTable}\n" + 
                                    $"INNER JOIN {c_MovieOrderTable} USING ({c_MovieIdColumn})\n" +
                                    $"INNER JOIN {c_OrdersTable} using ({c_OrderIdColumn})\n" +
                                    $"WHERE {c_UserIdColumn} = {userIdParam}";

                var whereClause = $"({c_GenreColumn} IN ({topUserGenre}))\nAND\n({c_MovieIdColumn} NOT IN ({watchedMovies}))";

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // Movie table fields with aliases
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieIdColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_MovieYearColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_GenreColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_DescriptionColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_ImdbColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_PriceColumn),
                                                    BuildFieldNameWithAliase(c_MoviesTable, c_StudioIdColumn),
                                                    // Studio table fields with aliases
                                                    BuildFieldNameWithAliase(c_StudioTable, c_StudioIdColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_TitleColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_CountryColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_FoundationDateColumn),
                                                    BuildFieldNameWithAliase(c_StudioTable, c_ProductionColumn),
                                                  })
                                           .From(c_MoviesTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Left, c_StudioTable, c_StudioIdColumn)
                                           .Where(whereClause)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(userIdParam, userdId);
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var movie = Serializers.MovieSerializer.Load(r, BuildFiledPrefix(c_MoviesTable));
                                var studio = Serializers.StudioSerializer.Load(r, BuildFiledPrefix(c_StudioTable));
                                movie.Studio = studio;

                                res.Add(movie);
                            }
                        }
                    }
                }
            }

            // load actors
            if (loadActors)
            {
                ExpandActors(res);
            }

            return res;
        }

        internal IList<Data.Actor> GetActors(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Actor>();

            // load movies
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_ActorsTable)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var actor = Serializers.ActorSerializer.Load(r);
                                res.Add(actor);
                            }
                        }
                    }
                }
            }

            return res;
        }

        internal IList<Data.Studio> GetStudio(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Studio>();

            // load movies
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_StudioTable)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var studio = Serializers.StudioSerializer.Load(r);
                                res.Add(studio);
                            }
                        }
                    }
                }
            }

            return res;
        }
        
        internal IList<Data.Studio> GetTopStudio(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Studio>();

            // load movies
            {
                var sqlBuilder = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // User table fields
                                                    BuildFieldName(c_StudioTable, "*"),
                                                    // Aggregated fields,
                                                    $"SUM({BuildFieldName(c_MoviesTable, c_PriceColumn)}) AS {c_IncomeColumn}",
                                                  })
                                           .From(c_StudioTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MoviesTable, c_StudioIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieOrderTable, c_MovieIdColumn)
                                           .GroupBy(BuildFieldName(c_StudioTable, c_StudioIdColumn))
                                           .Pagging(limit, offset)
                                           .AddFilter(filter);
                
                if (!filter?.GetOrderClauses()?.Any() ?? true)
                {
                    sqlBuilder.OrderBy(c_IncomeColumn, true);
                }

                var sql = sqlBuilder.Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var studio = Serializers.StudioSerializer.Load(r);
                                Serializers.StudioSerializer.LoadAggregated(studio, r);
                             
                                res.Add(studio);
                            }
                        }
                    }
                }
            }

            return res;
        }

        internal IList<Data.Order> GetOrders(int? limit = null, int? offset = null, IDataFilter filter = null, bool loadMovies = false)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Order>();

            // load movies
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // Order table fields with aliases
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_OrderIdColumn),
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_DateColumn),
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_UserIdColumn),
                                                    // User table fields with aliases
                                                    BuildFieldNameWithAliase(c_UsersTable, c_UserIdColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_FirstNameColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_SecondNameColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_EMailColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_RoleColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_PasswordColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_SaltColumn),
                                                  })
                                           .From(c_OrdersTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Left, c_UsersTable, c_UserIdColumn)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var order = Serializers.OrderSerializer.Load(r, BuildFiledPrefix(c_OrdersTable));
                                var user = Serializers.UserSerializer.Load(r, BuildFiledPrefix(c_UsersTable));
                                order.User = user;

                                res.Add(order);
                            }
                        }
                    }
                }
            }

            // load actors
            if (loadMovies)
            {
                ExpandMovies(res);
            }

            return res;
        }

        internal IList<Data.Order> GetTopOrders(int? limit = null, int? offset = null, IDataFilter filter = null, bool loadMovies = false)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Order>();

            // load movies
            {
                var sqlBuilder = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // Order table fields with aliases
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_OrderIdColumn),
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_DateColumn),
                                                    BuildFieldNameWithAliase(c_OrdersTable, c_UserIdColumn),
                                                    // User table fields with aliases
                                                    BuildFieldNameWithAliase(c_UsersTable, c_UserIdColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_FirstNameColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_SecondNameColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_EMailColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_RoleColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_PasswordColumn),
                                                    BuildFieldNameWithAliase(c_UsersTable, c_SaltColumn),
                                                    // Aggregated fields,
                                                    $"SUM({BuildFieldName(c_MoviesTable, c_PriceColumn)}) AS {c_IncomeColumn}",
                                                  })
                                           .From(c_OrdersTable)
                                           .JoinUsing(QueryBuilders.SQLJoin.Left, c_UsersTable, c_UserIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieOrderTable, c_OrderIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MoviesTable, c_MovieIdColumn)
                                           .GroupBy(BuildFieldName(c_OrdersTable, c_OrderIdColumn))
                                           .Pagging(limit, offset)
                                           .AddFilter(filter);

                if (!filter?.GetOrderClauses()?.Any() ?? true)
                {
                    sqlBuilder.OrderBy(c_IncomeColumn, true);
                }

                var sql = sqlBuilder.Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var order = Serializers.OrderSerializer.Load(r, BuildFiledPrefix(c_OrdersTable));
                                var user = Serializers.UserSerializer.Load(r, BuildFiledPrefix(c_UsersTable));
                                order.User = user;

                                Serializers.OrderSerializer.LoadAggregated(order, r);

                                res.Add(order);
                            }
                        }
                    }
                }
            }

            // load actors
            if (loadMovies)
            {
                ExpandMovies(res);
            }

            return res;
        }

        internal IList<Data.User> GetUsers(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.User>();

            // load movies
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_UsersTable)
                                           .Pagging(limit, offset)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var user = Serializers.UserSerializer.Load(r);
                                res.Add(user);
                            }
                        }
                    }
                }
            }

            return res;
        }

        internal IList<Data.User> GetTopUsers(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.User>();

            // load movies
            {
                var sqlBuilder = new QueryBuilders.SelectQueryBuilder()
                                           .Select(new string[] {
                                                    // User table fields
                                                    BuildFieldName(c_UsersTable, "*"),
                                                    // Aggregated fields,
                                                    $"SUM({BuildFieldName(c_MoviesTable, c_PriceColumn)}) AS {c_IncomeColumn}",
                                                    $"MAX({BuildFieldName(c_OrdersTable, c_DateColumn)}) AS {c_LastOrderColumn}",
                                                  })
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_OrdersTable, c_UserIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieOrderTable, c_OrderIdColumn)
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MoviesTable, c_MovieIdColumn)
                                           .From(c_UsersTable)
                                           .GroupBy(BuildFieldName(c_UsersTable, c_UserIdColumn))
                                           .Pagging(limit, offset)
                                           .AddFilter(filter);

                if (!filter?.GetOrderClauses()?.Any() ?? true)
                {
                    sqlBuilder.OrderBy(c_IncomeColumn, true);
                }

                var sql = sqlBuilder.Make();
                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    filter?.AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            foreach (var r in table.Rows.Cast<DataRow>())
                            {
                                var user = Serializers.UserSerializer.Load(r);
                                Serializers.UserSerializer.LoadAggregated(user, r);

                                res.Add(user);
                            }
                        }
                    }
                }
            }

            return res;
        }

        internal void DeleteMovies(IEnumerable<Data.Movie> movies)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var sqlParams = movies.Select(m => Tuple.Create(BuildParameterName(c_MoviesTable, $"{c_MovieIdColumn}{m.Id}"), m.Id))
                                  .ToList();

            var sql = new QueryBuilders.DeleteRequestBuilder()
                                       .Delete(c_MoviesTable)
                                       .Where($"{BuildFieldName(c_MoviesTable, c_MovieIdColumn)} IN ({string.Join(", ", sqlParams.Select(p => p.Item1))})")
                                       .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var p in sqlParams)
                {
                    command.Parameters.AddWithValue(p.Item1, p.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void DeleteActors(IEnumerable<Data.Actor> actors)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var sqlParams = actors.Select(a => Tuple.Create(BuildParameterName(c_ActorsTable, $"{c_ActorIdColumn}{a.Id}"), a.Id))
                                  .ToList();

            var sql = new QueryBuilders.DeleteRequestBuilder()
                                       .Delete(c_ActorsTable)
                                       .Where($"{BuildFieldName(c_ActorsTable, c_ActorIdColumn)} IN ({string.Join(", ", sqlParams.Select(p => p.Item1))})")
                                       .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var p in sqlParams)
                {
                    command.Parameters.AddWithValue(p.Item1, p.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void DeleteStudio(IEnumerable<Data.Studio> studio)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var sqlParams = studio.Select(s => Tuple.Create(BuildParameterName(c_StudioTable, $"{c_StudioIdColumn}{s.Id}"), s.Id))
                                  .ToList();

            var sql = new QueryBuilders.DeleteRequestBuilder()
                                       .Delete(c_StudioTable)
                                       .Where($"{BuildFieldName(c_StudioTable, c_StudioIdColumn)} IN ({string.Join(", ", sqlParams.Select(p => p.Item1))})")
                                       .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var p in sqlParams)
                {
                    command.Parameters.AddWithValue(p.Item1, p.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void DeleteOrders(IEnumerable<Data.Order> orders)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var sqlParams = orders.Select(o => Tuple.Create(BuildParameterName(c_OrdersTable, $"{c_OrderIdColumn}{o.Id}"), o.Id))
                                  .ToList();

            var sql = new QueryBuilders.DeleteRequestBuilder()
                                       .Delete(c_OrdersTable)
                                       .Where($"{BuildFieldName(c_OrdersTable, c_OrderIdColumn)} IN ({string.Join(", ", sqlParams.Select(p => p.Item1))})")
                                       .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var p in sqlParams)
                {
                    command.Parameters.AddWithValue(p.Item1, p.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void DeleteUsers(IEnumerable<Data.User> users)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            if (users.Any(u => u.Id == CurrentUser.Id))
            {
                throw new AttemptToDeleteCurrentUserDBException(CurrentUser.EMail);
            }

            var sqlParams = users.Select(u => Tuple.Create(BuildParameterName(c_UsersTable, $"{c_UserIdColumn}{u.Id}"), u.Id))
                                 .ToList();

            var sql = new QueryBuilders.DeleteRequestBuilder()
                                       .Delete(c_UsersTable)
                                       .Where($"{BuildFieldName(c_UsersTable, c_UserIdColumn)} IN ({string.Join(", ", sqlParams.Select(p => p.Item1))})")
                                       .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var p in sqlParams)
                {
                    command.Parameters.AddWithValue(p.Item1, p.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void AddMovies(IEnumerable<Data.Movie> movies)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // inserting
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_MoviesTable)
                                           .Pagging(1, 0)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var table = new DataTable(c_MoviesTable);
                        Serializers.MovieSerializer.AddColumns(table);

                        foreach (var m in movies)
                        {
                            var r = table.NewRow();
                            Serializers.MovieSerializer.Save(m, r);
                            table.Rows.Add(r);
                        }

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(table);
                    }
                }
            }
        }

        internal void AddActors(IEnumerable<Data.Actor> actors)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // inserting
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_ActorsTable)
                                           .Pagging(1, 0)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var table = new DataTable(c_ActorsTable);
                        Serializers.ActorSerializer.AddColumns(table);

                        foreach (var a in actors)
                        {
                            var r = table.NewRow();
                            Serializers.ActorSerializer.Save(a, r);
                            table.Rows.Add(r);
                        }

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(table);
                    }
                }
            }
        }

        internal void AddStudio(IEnumerable<Data.Studio> studio)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // inserting
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_StudioTable)
                                           .Pagging(1, 0)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var table = new DataTable(c_StudioTable);
                        Serializers.StudioSerializer.AddColumns(table);

                        foreach (var s in studio)
                        {
                            var r = table.NewRow();
                            Serializers.StudioSerializer.Save(s, r);
                            table.Rows.Add(r);
                        }

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(table);
                    }
                }
            }
        }

        internal void AddOrders(IEnumerable<Data.Order> orders)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // inserting
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_OrdersTable)
                                           .Pagging(1, 0)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var table = new DataTable(c_OrdersTable);
                        Serializers.OrderSerializer.AddColumns(table, true);

                        var kvp = new Dictionary<DataRow, Data.Order>();
                        foreach (var o in orders)
                        {
                            var r = table.NewRow();
                            Serializers.OrderSerializer.Save(o, r);

                            kvp.Add(r, o);
                            table.Rows.Add(r);
                        }

                        var commandBuilder = new MySqlCommandBuilder(adapter);

                        adapter.RowUpdated += (o, e) => 
                        {
                            if (e.StatementType == StatementType.Insert)
                            {
                                var id = (int)e.Command.LastInsertedId;

                                e.Row[c_OrderIdColumn] = id;
                                kvp[e.Row].Id = id;
                            }
                        };

                        adapter.Update(table);
                    }
                }
            }

            // updating movies
            {
                foreach(var o in orders)
                {
                    UpdateMovies(o);
                }
            }
        }

        private void Adapter_RowUpdated(object sender, MySqlRowUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void AddUsers(IEnumerable<Data.User> users)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // check on unique emails
            {
                var filter = new Filters.UserFilter();
                filter.WithEmails(users.Select(u => u.EMail));

                var existenUsers = GetUsers(filter: filter);
                if (existenUsers.Count > 0)
                {
                    throw new UserAlreadyExistsDBException(existenUsers.First().EMail);
                }
            }

            // inserting
            {
                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_UsersTable)
                                           .Pagging(1, 0)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var table = new DataTable(c_UsersTable);
                        Serializers.UserSerializer.AddColumns(table);

                        foreach (var u in users)
                        {
                            var r = table.NewRow();
                            Serializers.UserSerializer.Save(u, r);
                            table.Rows.Add(r);
                        }

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(table);
                    }
                }
            }
        }

        internal void UpdateMovies(IEnumerable<Data.Movie> movies)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // updating
            {
                var filter = new Filters.MovieFilter();
                filter.WithIds(movies.Select(u => u.Id));

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_MoviesTable)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    (filter as IDataFilter).AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            // setup pk column (required for Find method)
                            table.PrimaryKey = new DataColumn[] { table.Columns.Cast<DataColumn>().First(c => c.ColumnName == filter.PkColumn) };

                            foreach (var m in movies)
                            {
                                var r = table.Rows.Find(m.Id);
                                if (r != null)
                                {
                                    Serializers.MovieSerializer.Save(m, r);
                                }
                            }

                            var commandBuilder = new MySqlCommandBuilder(adapter);
                            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                            adapter.Update(table);
                        }
                    }
                }
            }
        }

        internal void UpdateActors(IEnumerable<Data.Actor> actors)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // updating
            {
                var filter = new Filters.ActorFilter();
                filter.WithIds(actors.Select(u => u.Id));

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_ActorsTable)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    (filter as IDataFilter).AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            // setup pk column (required for Find method)
                            table.PrimaryKey = new DataColumn[] { table.Columns.Cast<DataColumn>().First(c => c.ColumnName == filter.PkColumn) };

                            foreach (var a in actors)
                            {
                                var r = table.Rows.Find(a.Id);
                                if (r != null)
                                {
                                    Serializers.ActorSerializer.Save(a, r);
                                }
                            }

                            var commandBuilder = new MySqlCommandBuilder(adapter);
                            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                            adapter.Update(table);
                        }
                    }
                }
            }
        }

        internal void UpdateStudio(IEnumerable<Data.Studio> studio)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // updating
            {
                var filter = new Filters.StudioFilter();
                filter.WithIds(studio.Select(u => u.Id));

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_StudioTable)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    (filter as IDataFilter).AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            // setup pk column (required for Find method)
                            table.PrimaryKey = new DataColumn[] { table.Columns.Cast<DataColumn>().First(c => c.ColumnName == filter.PkColumn) };

                            foreach (var s in studio)
                            {
                                var r = table.Rows.Find(s.Id);
                                if (r != null)
                                {
                                    Serializers.StudioSerializer.Save(s, r);
                                }
                            }

                            var commandBuilder = new MySqlCommandBuilder(adapter);
                            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                            adapter.Update(table);
                        }
                    }
                }
            }
        }

        internal void UpdateOrders(IEnumerable<Data.Order> orders)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // updating
            {
                var filter = new Filters.OrderFilter();
                filter.WithIds(orders.Select(u => u.Id));

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_OrdersTable)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    (filter as IDataFilter).AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            // setup pk column (required for Find method)
                            table.PrimaryKey = new DataColumn[] { table.Columns.Cast<DataColumn>().First(c => c.ColumnName == filter.PkColumn) };

                            foreach (var o in orders)
                            {
                                var r = table.Rows.Find(o.Id);
                                if (r != null)
                                {
                                    Serializers.OrderSerializer.Save(o, r);
                                }
                            }

                            var commandBuilder = new MySqlCommandBuilder(adapter);
                            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                            adapter.Update(table);
                        }
                    }
                }
            }

            // updating movies
            {
                foreach (var o in orders)
                {
                    UpdateMovies(o);
                }
            }
        }

        internal void UpdateUsers(IEnumerable<Data.User> users)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            // updating
            {
                var filter = new Filters.UserFilter();
                filter.WithIds(users.Select(u => u.Id));

                var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_UsersTable)
                                           .AddFilter(filter)
                                           .Make();

                using (var connection = new MySqlConnection(ConnectionString))
                using (var command = new MySqlCommand(sql, connection))
                {
                    (filter as IDataFilter).AddCommandParameters(command);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        Debug.Assert(ds.Tables.Count > 0);

                        var table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            // setup pk column (required for Find method)
                            table.PrimaryKey = new DataColumn[] { table.Columns.Cast<DataColumn>().First(c => c.ColumnName == filter.PkColumn) };

                            foreach (var u in users)
                            {
                                var r = table.Rows.Find(u.Id);
                                if (r != null)
                                {
                                    Serializers.UserSerializer.Save(u, r);
                                }
                            }

                            var commandBuilder = new MySqlCommandBuilder(adapter);
                            adapter.Update(table);
                        }
                    }
                }
            }
        }

        #endregion

        #region Utility Methods

        static internal string BuildParameterName(string column)
        {
            return $"@{column}";
        }

        static internal string BuildParameterName(string table, string column)
        {
            return $"@{BuildFiledPrefix(table)}{column}";
        }

        static internal string BuildFieldName(string table, string column)
        {
            return $"{table}.{column}";
        }

        // Return filed name string in format "table.column AS table_column".
        // Useful at joining several tables with same column names, so as to column can be found in request result.
        static internal string BuildFieldNameWithAliase(string table, string column)
        {
            return $"{BuildFieldName(table, column)} AS {BuildFiledPrefix(table)}{column}";
        }

        // Return filed prefix in format "table_".
        // Method should be use in pair with BuildFieldNameWithAliase()
        static internal string BuildFiledPrefix(string table)
        {
            return $"{table}_";
        }

        #endregion

        #region Helper Methods

        void ExpandActors(IList<Data.Movie> movies)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                foreach (var m in movies)
                {
                    var sql = new QueryBuilders.SelectQueryBuilder()
                                               .SelectAll()
                                               .From(c_ActorsTable)
                                               .JoinUsing(QueryBuilders.SQLJoin.Left, c_MovieActorTable, c_ActorIdColumn)
                                               .Where($"{BuildFieldName(c_MovieActorTable, c_MovieIdColumn)} = {BuildParameterName(c_MovieActorTable, c_MovieIdColumn)}")
                                               .Make();

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue(BuildParameterName(c_MovieActorTable, c_MovieIdColumn), m.Id);

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            var ds = new DataSet();
                            adapter.Fill(ds);
                            Debug.Assert(ds.Tables.Count > 0);

                            var table = ds.Tables[0];
                            if (table.Rows.Count > 0)
                            {
                                m.Actors = new List<Data.Actor>();

                                foreach (var r in table.Rows.Cast<DataRow>())
                                {
                                    var actor = Serializers.ActorSerializer.Load(r);
                                    m.Actors.Add(actor);
                                }
                            }
                        }
                    }
                }
            }
        }

        void ExpandMovies(IList<Data.Order> orders)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                foreach (var o in orders)
                {
                    var sql = new QueryBuilders.SelectQueryBuilder()
                                               .SelectAll()
                                               .From(c_MoviesTable)
                                               .JoinUsing(QueryBuilders.SQLJoin.Left, c_MovieOrderTable, c_MovieIdColumn)
                                               .Where($"{BuildFieldName(c_MovieOrderTable, c_OrderIdColumn)} = {BuildParameterName(c_MovieOrderTable, c_OrderIdColumn)}")
                                               .Make();

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue(BuildParameterName(c_MovieOrderTable, c_OrderIdColumn), o.Id);

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            var ds = new DataSet();
                            adapter.Fill(ds);
                            Debug.Assert(ds.Tables.Count > 0);

                            var table = ds.Tables[0];
                            if (table.Rows.Count > 0)
                            {
                                o.Movies = new List<Data.Movie>();

                                foreach (var m in table.Rows.Cast<DataRow>())
                                {
                                    var movie = Serializers.MovieSerializer.Load(m);
                                    o.Movies.Add(movie);
                                }
                            }
                        }
                    }
                }
            }
        }

        void UpdateMovies(Data.Order order)
        {
            if (order.Movies == null)
            {
                return;
            }

            var sql = new QueryBuilders.SelectQueryBuilder()
                                           .SelectAll()
                                           .From(c_MovieOrderTable)
                                           .Where($"{BuildFieldName(c_MovieOrderTable, c_OrderIdColumn)} = {BuildParameterName(c_MovieOrderTable, c_OrderIdColumn)}")
                                           .Make();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue(BuildParameterName(c_MovieOrderTable, c_OrderIdColumn), order.Id);

                using (var adapter = new MySqlDataAdapter(command))
                {

                    var ds = new DataSet();
                    adapter.Fill(ds);
                    Debug.Assert(ds.Tables.Count > 0);

                    var table = ds.Tables[0];

                    // search rows to delete
                    foreach (var r in table.Rows.Cast<DataRow>())
                    {
                        var movieId = r.Field<int>(c_MovieIdColumn);

                        var hasMovieInOrder = order.Movies.Any(m => m.Id == movieId);
                        if (!hasMovieInOrder)
                        {
                            r.Delete();
                        }
                    }

                    // insert new rows
                    foreach(var m in order.Movies)
                    {
                        var movieId = m.Id;

                        var hasMovieInRows = table.Rows.Cast<DataRow>().Any(r => r.Field<int>(c_MovieIdColumn) == movieId);
                        if (!hasMovieInRows)
                        {
                            var r = table.NewRow();
                            r[c_MovieIdColumn] = movieId;
                            r[c_OrderIdColumn] = order.Id;

                            table.Rows.Add(r);
                        }
                    }

                    var commandBuilder = new MySqlCommandBuilder(adapter);
                    adapter.Update(table);
                }
            }
        }

        #endregion
    }
}
