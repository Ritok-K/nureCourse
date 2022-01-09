using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Serializers
{
    static class UserSerializer
    {
        internal static Data.User Load(DataRow row, string prefix = "")
        {
            var res = new Data.User()
            {
                Id = row.Field<int>($"{prefix}{MovieDB.c_UserIdColumn}"),
                FirstName = row.Field<string>($"{prefix}{MovieDB.c_FirstNameColumn}"),
                SecondName = row.Field<string>($"{prefix}{MovieDB.c_SecondNameColumn}"),
                EMail = row.Field<string>($"{prefix}{MovieDB.c_EMailColumn}"),
                Role = GetRole(row.Field<string>($"{prefix}{MovieDB.c_RoleColumn}")),
                PasswordHash = row.Field<string>($"{prefix}{MovieDB.c_PasswordColumn}"),
                Salt = row.Field<string>($"{prefix}{MovieDB.c_SaltColumn}"),
            };

            return res;
        }

        internal static void LoadAggregated(Data.User user, DataRow row)
        {
            if (row.Table.Columns.Contains(MovieDB.c_IncomeColumn))
            {
                var v = row.Field<decimal?>(MovieDB.c_IncomeColumn);
                user.Income = v.HasValue ? (int)v.Value : null;
            }

            if (row.Table.Columns.Contains(MovieDB.c_LastOrderColumn))
            {
                var v = row.Field<DateTime?>(MovieDB.c_LastOrderColumn);
                user.LastOrder = v;
            }
        }

        internal static void LoadId(Data.User user, DataRow row)
        {
            user.Id = row.Field<int>(MovieDB.c_UserIdColumn);
        }

        internal static void Save(Data.User user, DataRow row)
        {
            row[MovieDB.c_FirstNameColumn] = user.FirstName;
            row[MovieDB.c_SecondNameColumn] = user.SecondName;
            row[MovieDB.c_EMailColumn] = user.EMail;
            row[MovieDB.c_RoleColumn] = GetRoleName(user.Role);
            row[MovieDB.c_PasswordColumn] = user.PasswordHash;
            row[MovieDB.c_SaltColumn] = user.Salt;
        }

        internal static void AddColumns(DataTable table)
        {
            table.Columns.Add(MovieDB.c_FirstNameColumn, typeof(string));
            table.Columns.Add(MovieDB.c_SecondNameColumn, typeof(string));
            table.Columns.Add(MovieDB.c_EMailColumn, typeof(string));
            table.Columns.Add(MovieDB.c_RoleColumn, typeof(string));
            table.Columns.Add(MovieDB.c_PasswordColumn, typeof(string));
            table.Columns.Add(MovieDB.c_SaltColumn, typeof(string));
        }

        #region Helper Methods

        static Data.UserRole GetRole(string role)
        {
            var res = Data.UserRole.User;

            switch (role)
            {
                case MovieDB.c_RoleUserValue:
                    res = Data.UserRole.User;
                    break;
                case MovieDB.c_RoleManagerValue:
                    res = Data.UserRole.Manager;
                    break;
                default:
                    Debug.Assert(false, "Unknown role");
                    break;
            }

            return res;
        }

        static string GetRoleName(Data.UserRole role)
        {
            var res = MovieDB.c_RoleUserValue;

            switch (role)
            {
                case Data.UserRole.User:
                    res = MovieDB.c_RoleUserValue;
                    break;
                case Data.UserRole.Manager:
                    res = MovieDB.c_RoleManagerValue;
                    break;
            }

            return res;
        }

        #endregion
    }
}
