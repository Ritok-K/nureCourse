using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.QueryBuilders
{
    class DeleteRequestBuilder
    {
        string FromTable { get; set; } = string.Empty;

        IList<string> WhereClause { get; set; } = new List<string>();


        internal DeleteRequestBuilder Delete(string table)
        {
            Debug.Assert(!string.IsNullOrEmpty(table));

            FromTable = table;
            return this;
        }

        internal DeleteRequestBuilder Where(string where)
        {
            Debug.Assert(!string.IsNullOrEmpty(where));

            WhereClause.Add(where);
            return this;
        }

        internal string Make()
        {
            var res = new StringBuilder();

            // from
            {
                if (string.IsNullOrEmpty(FromTable))
                {
                    throw new ArgumentException("Invalid FROM part");
                }

                res.Append($"DELETE FROM {FromTable}");
            }

            // where
            {
                var whereRules = new List<string>();
                whereRules.AddRange(WhereClause);
                whereRules = whereRules.Where(s => !string.IsNullOrEmpty(s)).ToList();

                var where = string.Join(" AND ", whereRules).Trim(' ');
                if (string.IsNullOrEmpty(where))
                {
                    throw new ArgumentException("Invalid WHERE part");
                }

                res.Append($"\nWHERE {where}");
            }

            return res.ToString();
        }
    }
}
