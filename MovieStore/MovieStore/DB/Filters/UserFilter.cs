using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class UserFilter : PrimaryKeyFilter
    {
        string EmailField => MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_EMailColumn);

        List<Tuple<string, string>> EmailParams { get; set; } = new List<Tuple<string, string>>();

        internal UserFilter()
            : base(MovieDB.c_UsersTable, MovieDB.c_UserIdColumn)
        {
        }

        internal void WithEmails(IEnumerable<string> emails)
        {
            Debug.Assert(!emails.Any(e => string.IsNullOrEmpty(e)));

            EmailParams.AddRange(emails.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        public override IEnumerable<string> GetWhereClauses()
        {
            var res = new List<string>();

            var emails = EmailParams.Any() ? $"{EmailField} IN ({string.Join(", ", EmailParams.Select(e => $"LOWER({e.Item1})"))})" : string.Empty;
            res.Add(emails);
            res.AddRange(base.GetWhereClauses());

            return res;
        }

        public override void AddCommandParameters(MySqlCommand command)
        {
            base.AddCommandParameters(command);

            foreach (var e in EmailParams)
            {
                command.Parameters.AddWithValue(e.Item1, e.Item2.ToLower());
            }
        }
    }
}
