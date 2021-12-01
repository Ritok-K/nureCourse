using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class UserFilter : IDataFilter
    {
        string EmailField => MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_EMailColumn);
        string IdField => MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_UserIdColumn);

        List<Tuple<string, string>> EmailParams { get; set; } = new List<Tuple<string, string>>();
        List<Tuple<string, int>> IdParams { get; set; } = new List<Tuple<string, int>>();

        internal void WithEmails(IEnumerable<string> emails)
        {
            Debug.Assert(!emails.Any(e => string.IsNullOrEmpty(e)));

            EmailParams.AddRange(emails.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        internal void WithIds(IEnumerable<int> ids)
        {
            Debug.Assert(ids.Any());

            IdParams.AddRange(ids.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        IEnumerable<string> IDataFilter.GetWhereClauses()
        {
            var emails = EmailParams.Any() ? $"{EmailField} IN ({string.Join(", ", EmailParams.Select(e => $"LOWER({e.Item1})"))})" : string.Empty;
            var ids = IdParams.Any() ? $"{IdField} IN ({string.Join(", ", IdParams.Select(id => $"{id.Item1}"))})" : string.Empty;

            return new string[] { emails, ids };
        }

        void IDataFilter.AddCommandParameters(MySqlCommand command)
        {
            foreach (var e in EmailParams)
            {
                command.Parameters.AddWithValue(e.Item1, e.Item2.ToLower());
            }

            foreach (var id in IdParams)
            {
                command.Parameters.AddWithValue(id.Item1, id.Item2);
            }
        }

        static string GenerateUniqueParameter()
        {
            return MovieDB.BuildParameterName(MovieDB.c_UsersTable, Guid.NewGuid().ToString("N"));
        }
    }
}
