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
        IEnumerable<string> FieldsClause { get; set; } = Enumerable.Empty<string>();
        string FromClause { get; set; } = string.Empty;
        IList<string> JoinClauses { get; set; } = new List<string>();
        string WhereClause { get; set; } = string.Empty;
        IList<string> GroupByClauses { get; set; } = new List<string>();
        IList<string> OrderByClauses { get; set; } = new List<string>();
        int? Limit { get; set; } = null;
        int? Offeset { get; set; } = null;
        IDataFilter Filter { get; set; } = null;

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

        internal SelectQueryBuilder GroupBy(string groupBy)
        {
            Debug.Assert(!string.IsNullOrEmpty(groupBy));

            GroupByClauses.Add(groupBy);
            return this;
        }

        internal SelectQueryBuilder OrderBy(string orderBy, bool desc = false)
        {
            Debug.Assert(!string.IsNullOrEmpty(orderBy));

            var direction = desc ? " DESC" : string.Empty;
            OrderByClauses.Add($"{orderBy}{direction}");
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
                var fieldsCollection = new List<string>();
                fieldsCollection.AddRange(FieldsClause ?? Enumerable.Empty<string>());
                fieldsCollection.AddRange(Filter?.GetSelectClauses() ?? Enumerable.Empty<string>());
                fieldsCollection = fieldsCollection.Where(s => !string.IsNullOrEmpty(s)).ToList();

                if (!fieldsCollection.Any())
                {
                    throw new ArgumentException("Invalid SELECT part");
                }

                var select = string.Join(", ", fieldsCollection).Trim(' ');
                res.Append($"SELECT {select}");
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
                joinCollection.AddRange(JoinClauses ?? Enumerable.Empty<string>());
                joinCollection.AddRange(Filter?.GetJoinClauses() ?? Enumerable.Empty<string>());
                joinCollection = joinCollection.Where(s => !string.IsNullOrEmpty(s)).ToList();

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
                whereRules = whereRules.Where(s => !string.IsNullOrEmpty(s)).ToList();

                var where = string.Join(" AND ", whereRules).Trim(' ');
                if (!string.IsNullOrEmpty(where))
                {
                    res.Append($"\nWHERE {where}");
                }
            }

            // group
            {
                var groupRules = new List<string>();
                groupRules.AddRange(GroupByClauses ?? Enumerable.Empty<string>());
                groupRules.AddRange(Filter?.GetGroupByClauses() ?? Enumerable.Empty<string>());
                groupRules = groupRules.Where(s => !string.IsNullOrEmpty(s)).ToList();

                var group = string.Join(", ", groupRules).Trim(' ');
                if (!string.IsNullOrEmpty(group))
                {
                    res.Append($"\nGROUP BY {group}");
                }
            }

            // order
            {
                var orderRules = new List<string>();
                orderRules.AddRange(OrderByClauses ?? Enumerable.Empty<string>());
                orderRules.AddRange(Filter?.GetOrderClauses() ?? Enumerable.Empty<string>());
                orderRules = orderRules.Where(s => !string.IsNullOrEmpty(s)).ToList();

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
