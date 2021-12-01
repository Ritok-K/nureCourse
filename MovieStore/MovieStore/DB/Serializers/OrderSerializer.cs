using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Serializers
{
    class OrderSerializer
    {
        internal static Data.Order Load(DataRow row, string prefix = "")
        {
            var res = new Data.Order()
            {
                Id = row.Field<int>($"{prefix}{MovieDB.c_OrderIdColumn}"),
                Date = row.Field<DateTime>($"{prefix}{MovieDB.c_DateColumn}"),
                User = new Data.User() { Id = row.Field<int>($"{prefix}{MovieDB.c_UserIdColumn}") },
            };

            // Do we need it?
            //res.Date = DateTime.SpecifyKind(res.Date, DateTimeKind.Utc);

            return res;
        }

        internal static void Save(Data.Order order, DataRow row)
        {
            if (order.User == null)
            {
                throw new ArgumentException("User.Id should exists.", nameof(Data.Order.User));
            }

            row[MovieDB.c_DateColumn] = order.Date;
            row[MovieDB.c_UserIdColumn] = order.User.Id;
        }

        internal static void AddColumns(DataTable table)
        {
            table.Columns.Add(MovieDB.c_DateColumn, typeof(DateTime));
            table.Columns.Add(MovieDB.c_UserIdColumn, typeof(string));
        }
    }
}
