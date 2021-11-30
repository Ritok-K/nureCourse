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
    }
}
