using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MovieStore.DB.Filters
{
    class PrimaryKeyFilter : EmptyFilter
    {
        internal string Table { get; init; }
        internal string PkColumn { get; init; }

        string IdField => MovieDB.BuildFieldName(Table, PkColumn);

        List<Tuple<string, int>> IdParams { get; set; } = new List<Tuple<string, int>>();

        internal PrimaryKeyFilter(string table, string pkColumn)
        {
            Debug.Assert(!string.IsNullOrEmpty(table));
            Debug.Assert(!string.IsNullOrEmpty(pkColumn));

            Table = table;
            PkColumn = pkColumn;
        }

        internal void WithIds(IEnumerable<int> ids)
        {
            Debug.Assert(ids.Any());

            IdParams.AddRange(ids.Select(e => Tuple.Create(GenerateUniqueParameter(), e)));
        }

        public override IEnumerable<string> GetWhereClauses()
        {
            var ids = IdParams.Any() ? $"{IdField} IN ({string.Join(", ", IdParams.Select(id => $"{id.Item1}"))})" : string.Empty;

            return new string[] { ids };
        }

        public override void AddCommandParameters(MySqlCommand command)
        {
            foreach (var id in IdParams)
            {
                command.Parameters.AddWithValue(id.Item1, id.Item2);
            }
        }

        protected string GenerateUniqueParameter()
        {
            return GenerateUniqueParameter(Table);
        }

        protected static string GenerateUniqueParameter(string table)
        {
            return MovieDB.BuildParameterName(table, Guid.NewGuid().ToString("N"));
        }
    }
}
