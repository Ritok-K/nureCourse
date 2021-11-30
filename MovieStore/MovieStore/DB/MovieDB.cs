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
        internal const string c_MoviesTable = "Movies";
        internal const string c_UsersTable = "Users";
        internal const string c_StudioTable = "Studio";

        // primary and foreign keys
        internal const string c_ActorIdColumn = "actorId";
        internal const string c_MoviewIdColumn = "movieId";
        internal const string c_UserIdColumn = "userId";
        internal const string c_StudioIdColumn = "studioId";

        // data columns
        internal const string c_AwardsDescriptionColumn = "awardsDescription";
        internal const string c_BirthDateColumn = "birthDate";
        internal const string c_CountryColumn = "country";
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

        internal IList<Data.Movie> GetMovies(int? limit = null, int? offset = null, IDataFilter filter = null)
        {
            if (!IsAuthorized)
            {
                throw new NotAuthorizedDBException();
            }

            var res = new List<Data.Movie>();

            var sql = new QueryBuilders.SelectQueryBuilder()
                                       .Select(new string[] {
                                                // Movie table fields with aliases
                                                BuildFieldNameWithAliase(c_MoviesTable, c_MoviewIdColumn),
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

            return res;
        }

        #endregion

        #region Utility Methods

        static internal string BuildParameterName(string column)
        {
            return $"@{column}";
        }

        // Return filed name string in format "table.column AS table_column".
        // Useful at joining several tables with same column names, so as to column can be found in request result.
        static internal string BuildFieldNameWithAliase(string table, string column)
        {
            return $"{table}.{column} AS {BuildFiledPrefix(table)}{column}";
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
