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
        Tuple<string, string> FirstNameParams { get; set; } = null;
        Tuple<string, string> SecondNameParams { get; set; } = null;

        internal UserFilter()
            : base(MovieDB.c_UsersTable, MovieDB.c_UserIdColumn)
        {
        }

        internal void WithEmails(IEnumerable<string> emails)
        {
            Debug.Assert(!emails.Any(e => string.IsNullOrEmpty(e)));

            EmailParams.AddRange(emails.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        internal void WithFirstNameLike(string text)
        {
            Debug.Assert(!string.IsNullOrEmpty(text));

            FirstNameParams = Tuple.Create(GenerateUniqueParameter(), text);
        }

        internal void WithSecondNameLike(string text)
        {
            Debug.Assert(!string.IsNullOrEmpty(text));

            SecondNameParams = Tuple.Create(GenerateUniqueParameter(), text);
        }

        public override IEnumerable<string> GetWhereClauses()
        {
            var res = new List<string>();
            res.AddRange(base.GetWhereClauses());

            var emails = EmailParams.Any() ? $"{EmailField} IN ({string.Join(", ", EmailParams.Select(e => $"LOWER({e.Item1})"))})" : string.Empty;
            res.Add(emails);

            var textSearch = new List<string>();
            if (FirstNameParams != null)
            {
                textSearch.Add($"(LOWER({MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_FirstNameColumn)}) LIKE {FirstNameParams.Item1})");
            }

            if (SecondNameParams != null)
            {
                textSearch.Add($"(LOWER({MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_SecondNameColumn)}) LIKE {SecondNameParams.Item1})");
            }

            if (textSearch.Any())
            {
                var textWhereClause = string.Join(" OR ", textSearch);
                res.Add(textWhereClause);
            }

            return res;
        }

        public override void AddCommandParameters(MySqlCommand command)
        {
            base.AddCommandParameters(command);

            foreach (var e in EmailParams)
            {
                command.Parameters.AddWithValue(e.Item1, e.Item2.ToLower());
            }

            if (FirstNameParams != null)
            {
                command.Parameters.AddWithValue(FirstNameParams.Item1, $"%{FirstNameParams.Item2.ToLower()}%");
            }

            if (SecondNameParams != null)
            {
                command.Parameters.AddWithValue(SecondNameParams.Item1, $"%{SecondNameParams.Item2.ToLower()}%");
            }
        }
    }
}
