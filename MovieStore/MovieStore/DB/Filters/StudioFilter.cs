using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class StudioFilter : PrimaryKeyFilter
    {
        internal StudioFilter()
            : base(MovieDB.c_StudioTable, MovieDB.c_StudioIdColumn)
        { }
    }
}
