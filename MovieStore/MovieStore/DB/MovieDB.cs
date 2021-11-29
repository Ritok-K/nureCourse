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

        const string c_EMailParam = "@email";

        #endregion

        #region Methods

        internal void Login(string email, string password)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand($"SELECT * FROM {c_UsersTable} WHERE {c_EMailColumn} = {c_EMailParam};", connection))
            {
                command.Parameters.Add(new MySqlParameter(c_EMailParam, email));

                using (var adapter = new MySqlDataAdapter(command))
                {
                    var ds = new DataSet();

                    adapter.Fill(ds);

                    var table = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                    if (table != null && table.Rows.Count > 0)
                    {
                        var user = CreateUser(table, table.Rows[0]);
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

        #endregion

        #region Helper Methods

        Data.User CreateUser(DataTable table, DataRow row)
        {
            var res = new Data.User()
            {
                Id = row[table.Columns.IndexOf(c_UserIdColumn)] as string,
                FirstName = row[table.Columns.IndexOf(c_FirstNameColumn)] as string,
                SecondName = row[table.Columns.IndexOf(c_SecondNameColumn)] as string,
                EMail = row[table.Columns.IndexOf(c_EMailColumn)] as string,
                Role = GetRole(row[table.Columns.IndexOf(c_RoleColumn)] as string),
                PasswordHash = row[table.Columns.IndexOf(c_PasswordColumn)] as string,
                Salt = row[table.Columns.IndexOf(c_SaltColumn)] as string,
            };

            return res;
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

        #endregion
    }
}
