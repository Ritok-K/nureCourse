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

        #endregion

        #region Constants

        const string c_UsersTable = "Users";

        const string c_UserIdColumn = "userId";
        const string c_FirstNameColumn = "firstName";
        const string c_SecondNameColumn = "secondName";
        const string c_NameColumn = "secondName";
        const string c_EMailColumn = "email";
        const string c_RoleColumn = "role";
        const string c_PasswordColumn = "password";
        const string c_SaltColumn = "salt";

        const string c_RoleUserValue = "user";
        const string c_RoleManagerValue = "manager";

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

                    var table = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                    if (table != null && table.Rows.Count > 0)
                    {
                        var user = LoadUser(table.Rows[0]);
                        if (user.CheckPassword(password))
                        {
                            CurrentUser = user;
                        }
                        else
                        {
                            throw new InvalidPasswordDBExceptiom();
                        }
                    }
                    else
                    {
                        throw new UserNotFoundDBExceptiom(email);
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

                    var table = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                    if (table != null && table.Rows.Count == 0)
                    {
                        var row = table.NewRow();
                        SaveUser(user, row);
                        table.Rows.Add(row);

                        var commandBuilder = new MySqlCommandBuilder(adapter);
                        adapter.Update(ds);

                        using (var idCommand = new MySqlCommand($"SELECT {c_UserIdColumn} FROM {c_UsersTable} WHERE {c_EMailColumn} = {BuildParameterName(c_EMailColumn)} LIMIT 1;", connection))
                        {
                            idCommand.Parameters.Add(new MySqlParameter(BuildParameterName(c_EMailColumn), user.EMail));
                            adapter.SelectCommand = idCommand;

                            ds.Clear();
                            adapter.Fill(ds);

                            LoadUserId(user, ds.Tables[0].Rows[0]);
                            CurrentUser = user;
                        }
                    }
                    else
                    {
                        throw new UserAlreadyExistsDBExceptiom(user.EMail);
                    }
                }
            }
        }

        #endregion

        #region Helper Methods

        Data.User LoadUser(DataRow row)
        {
            var res = new Data.User()
            {
                Id = row.Field<int>(c_UserIdColumn),
                FirstName = row.Field<string>(c_FirstNameColumn),
                SecondName = row.Field<string>(c_SecondNameColumn),
                EMail = row.Field<string>(c_EMailColumn),
                Role = GetRole(row.Field<string>(c_RoleColumn)),
                PasswordHash = row.Field<string>(c_PasswordColumn),
                Salt = row.Field<string>(c_SaltColumn),
            };

            return res;
        }

        void LoadUserId(Data.User user, DataRow row)
        {
            user.Id = row.Field<int>(c_UserIdColumn);
        }

        void SaveUser(Data.User user, DataRow row)
        {
            row[c_FirstNameColumn] = user.FirstName;
            row[c_SecondNameColumn] = user.SecondName;
            row[c_EMailColumn] = user.EMail;
            row[c_RoleColumn] = GetRoleName(user.Role);
            row[c_PasswordColumn] = user.PasswordHash;
            row[c_SaltColumn] = user.Salt;
        }

        Data.UserRole GetRole(string role)
        {
            var res = Data.UserRole.User;

            switch(role)
            {
                case c_RoleUserValue:
                    res = Data.UserRole.User;
                    break;
                case c_RoleManagerValue:
                    res = Data.UserRole.Manager;
                    break;
                default:
                    Debug.Assert(false, "Unknown role");
                    break;
            }

            return res;
        }
        string GetRoleName(Data.UserRole role)
        {
            var res = c_RoleUserValue;

            switch(role)
            {
                case Data.UserRole.User:
                    res = c_RoleUserValue;
                    break;
                case Data.UserRole.Manager:
                    res = c_RoleManagerValue;
                    break;
            }

            return res;
        }

        string BuildParameterName(string column)
        {
            return $"@{column}";
        }

        #endregion
    }
}
