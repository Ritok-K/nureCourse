using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class OrderFilter : PrimaryKeyFilter
    {
        string DateField => MovieDB.BuildFieldName(MovieDB.c_OrdersTable, MovieDB.c_DateColumn);


        Tuple<string, DateTime> FromDate { get; set; }
        Tuple<string, DateTime> ToDate { get; set; }

        internal OrderFilter()
            : base(MovieDB.c_OrdersTable, MovieDB.c_OrderIdColumn)
        { }

        internal void WithDatePeriod(DateTime fromDate, DateTime toDate)
        {
            FromDate = Tuple.Create(GenerateUniqueParameter(), fromDate);
            ToDate = Tuple.Create(GenerateUniqueParameter(), toDate);
        }

        public override IEnumerable<string> GetWhereClauses()
        {
            var res = new List<string>();
            res.AddRange(base.GetWhereClauses());

            if (FromDate != null && ToDate != null)
            {
                var emails = $"({DateField} >= {FromDate.Item1} AND {DateField} <= {ToDate.Item1})";
                res.Add(emails);
            }

            return res;
        }

        public override void AddCommandParameters(MySqlCommand command)
        {
            base.AddCommandParameters(command);

            if (FromDate != null && ToDate != null)
            {
                command.Parameters.AddWithValue(FromDate.Item1, FromDate.Item2);
                command.Parameters.AddWithValue(ToDate.Item1, ToDate.Item2);
            }
        }

    }
}
