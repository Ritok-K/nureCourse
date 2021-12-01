using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

        internal string ConnectionString => @"server=192.168.1.43;user id=rita;password=morganalifrey;persistsecurityinfo=True;database=movie";
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
            using (var command = new MySqlCommand($"SELECT * FROM {c_UsersTable} WHERE {c_EMailColumn} = {BuildParameterName(c_EMailColumn)} LIMIT 1;", connection))
            {
                command.Parameters.Add(new MySqlParameter(BuildParameterName(c_EMailColumn), user.EMail));

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
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_StudioTable, c_StudioIdColumn)
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
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    foreach(var m in res)
                    {
                        var sql = new QueryBuilders.SelectQueryBuilder()
                                                   .SelectAll()
                                                   .From(c_ActorsTable)
                                                   .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieActorTable, c_ActorIdColumn)
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
                                           .JoinUsing(QueryBuilders.SQLJoin.Inner, c_UsersTable, c_UserIdColumn)
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
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    foreach (var o in res)
                    {
                        var sql = new QueryBuilders.SelectQueryBuilder()
                                                   .SelectAll()
                                                   .From(c_MoviesTable)
                                                   .JoinUsing(QueryBuilders.SQLJoin.Inner, c_MovieOrderTable, c_MovieIdColumn)
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

            return res;
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
    }
}
