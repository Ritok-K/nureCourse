using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class MovieFilter : IDataFilter
    {
        string TitleField => MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_TitleColumn);
        string IdField => MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_MovieIdColumn);

        List<Tuple<string, string>> TitleParams { get; set; } = new List<Tuple<string, string>>();
        List<Tuple<string, int>> IdParams { get; set; } = new List<Tuple<string, int>>();

        internal void WithTitles(IEnumerable<string> titles)
        {
            Debug.Assert(!titles.Any(e => string.IsNullOrEmpty(e)));

            TitleParams.AddRange(titles.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        internal void WithIds(IEnumerable<int> ids)
        {
            Debug.Assert(ids.Any());

            IdParams.AddRange(ids.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        IEnumerable<string> IDataFilter.GetWhereClauses()
        {
            var titles = TitleParams.Any() ? $"{TitleField} IN ({string.Join(", ", TitleParams.Select(e => $"LOWER({e.Item1})"))})" : string.Empty;
            var ids = IdParams.Any() ? $"{IdField} IN ({string.Join(", ", IdParams.Select(id => $"{id.Item1}"))})" : string.Empty;

            return new string[] { titles, ids };
        }

        void IDataFilter.AddCommandParameters(MySqlCommand command)
        {
            foreach (var e in TitleParams)
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
            return MovieDB.BuildParameterName(MovieDB.c_MoviesTable, Guid.NewGuid().ToString("N"));
        }
    }
}
