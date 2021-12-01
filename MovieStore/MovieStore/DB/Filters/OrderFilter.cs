using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class OrderFilter : PrimaryKeyFilter
    {
        internal OrderFilter()
            : base(MovieDB.c_OrdersTable, MovieDB.c_OrderIdColumn)
        { }
    }
}
