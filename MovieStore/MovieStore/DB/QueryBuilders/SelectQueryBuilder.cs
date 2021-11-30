using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.QueryBuilders
{
    enum SQLJoin
    {
        Inner,
        Left,
        Right,
        Outer,
    }

    class SelectQueryBuilder
    {
        IEnumerable<string> FieldsClause { get; set; }
        string FromClause { get; set; }
        IList<string> JoinClauses { get; set; } = new List<string>();
        string WhereClause { get; set; }
        int? Limit { get; set; } = null;
        int? Offeset { get; set; } = null;
        IDataFilter Filter { get; set; }

        internal SelectQueryBuilder Select(IEnumerable<string> fields)
        {
            Debug.Assert(fields?.Any() ?? false);

            FieldsClause = fields;
            return this;
        }

        internal SelectQueryBuilder SelectAll()
        {
            return Select(new string[] { "*" });
        }

        internal SelectQueryBuilder From(string from)
        {
            Debug.Assert(!string.IsNullOrEmpty(from));

            FromClause = from;
            return this;
        }

        internal SelectQueryBuilder JoinOn(SQLJoin type, string table, string on)
        {
            Debug.Assert(!string.IsNullOrEmpty(table));
            Debug.Assert(!string.IsNullOrEmpty(on));

            JoinClauses.Add($"{GetJoinTypeName(type)} JOIN {table} ON ({on})");
            return this;
        }

        internal SelectQueryBuilder JoinUsing(SQLJoin type, string table, string column)
        {
            Debug.Assert(!string.IsNullOrEmpty(table));
            Debug.Assert(!string.IsNullOrEmpty(column));

            JoinClauses.Add($"{GetJoinTypeName(type)} JOIN {table} USING ({column})");
            return this;
        }

        internal SelectQueryBuilder Where(string where)
        {
            Debug.Assert(!string.IsNullOrEmpty(where));

            WhereClause = where;
            return this;
        }

        internal SelectQueryBuilder Pagging(int? limit, int? offset)
        {
            Debug.Assert(!offset.HasValue || limit.HasValue);

            Limit = limit;
            Offeset = offset;
            return this;
        }

        internal SelectQueryBuilder AddFilter(IDataFilter filter)
        {
            Filter = filter;
            return this;
        }

        internal string Make(string finalizing = ";")
        {
            var res = new StringBuilder();

            // select
            {
                if (!(FieldsClause?.Any() ?? false))
                {
                    throw new ArgumentException("Invalid SELECT part");
                }

                var fields = string.Join(", ", FieldsClause).Trim(' ');
                res.Append($"SELECT {fields}");
            }

            // from
            {
                var fromCollection = new List<string>();
                fromCollection.Add(FromClause);

                var from = string.Join(' ', fromCollection).Trim(' ');
                if (string.IsNullOrEmpty(from))
                {
                    throw new ArgumentException("Invalid FROM part");
                }
                res.Append($"\nFROM {from}");
            }

            // join
            {
                var joinCollection = new List<string>();
                joinCollection.AddRange(JoinClauses);
                joinCollection.AddRange(Filter?.GetJoinClauses() ?? Enumerable.Empty<string>());

                var join = string.Join('\n', joinCollection).Trim(' ');
                if (!string.IsNullOrEmpty(join))
                {
                    res.Append($"\n{join}");
                }
            }

            // where
            {
                var whereRules = new List<string>();
                whereRules.Add(WhereClause);
                whereRules.AddRange(Filter?.GetWhereClauses() ?? Enumerable.Empty<string>());

                var where = string.Join(" AND ", whereRules).Trim(' ');
                if (!string.IsNullOrEmpty(where))
                {
                    res.Append($"\nWHERE {where}");
                }
            }

            // group
            {
                var groupRules = new List<string>();
                groupRules.AddRange(Filter?.GetGroupByClauses() ?? Enumerable.Empty<string>());

                var group = string.Join(", ", groupRules).Trim(' ');
                if (!string.IsNullOrEmpty(group))
                {
                    res.Append($"\nGROUP BY {group}");
                }
            }

            // order
            {
                var orderRules = new List<string>();
                orderRules.AddRange(Filter?.GetOrderClauses() ?? Enumerable.Empty<string>());

                var order = string.Join(", ", orderRules).Trim(' ');
                if (!string.IsNullOrEmpty(order))
                {
                    res.Append($"\nORDER BY {order}");
                }
            }

            // limit & offset
            {
                if (Limit.HasValue)
                {
                    res.Append($"\nLIMIT {Limit.Value}");

                    if (Offeset.HasValue)
                    {
                        res.Append($" OFFSET {Offeset.Value}");
                    }
                }
            }

            res.Append(finalizing);

            return res.ToString();
        }

        static string GetJoinTypeName(SQLJoin type)
        {
            var res = string.Empty;

            switch(type)
            {
                case SQLJoin.Inner:
                    res = "INNER";
                    break;
                case SQLJoin.Left:
                    res = "LEFT";
                    break;
                case SQLJoin.Right:
                    res = "Right";
                    break;
                case SQLJoin.Outer:
                    res = "FULL OUTER";
                    break;
            }

            return res;
        }
    }
}
