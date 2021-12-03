using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class MovieFilter : PrimaryKeyFilter
    {
        Tuple<string, string> TitleParams { get; set; } = null;
        Tuple<string, string> StudioTitleParams { get; set; } = null;

        internal MovieFilter()
            : base(MovieDB.c_MoviesTable, MovieDB.c_MovieIdColumn)
        { }

        internal void WithTitleLike(string text)
        {
            Debug.Assert(!string.IsNullOrEmpty(text));

            TitleParams = Tuple.Create(GenerateUniqueParameter(), text);
        }

        internal void WithStudioLike(string text)
        {
            Debug.Assert(!string.IsNullOrEmpty(text));

            StudioTitleParams = Tuple.Create(GenerateUniqueParameter(), text);
        }

        public override IEnumerable<string> GetWhereClauses()
        {
            var res = new List<string>();
            res.AddRange(base.GetWhereClauses());

            var textSearch = new List<string>();
            if (TitleParams != null)
            {
                textSearch.Add($"(LOWER({MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_TitleColumn)}) LIKE {TitleParams.Item1})");
            }

            if (StudioTitleParams != null)
            {
                textSearch.Add($"(LOWER({MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_TitleColumn)}) LIKE {StudioTitleParams.Item1})");
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

            if (TitleParams != null)
            {
                command.Parameters.AddWithValue(TitleParams.Item1, $"%{TitleParams.Item2.ToLower()}%");
            }

            if (StudioTitleParams != null)
            {
                command.Parameters.AddWithValue(StudioTitleParams.Item1, $"%{StudioTitleParams.Item2.ToLower()}%");
            }
        }
    }
}
