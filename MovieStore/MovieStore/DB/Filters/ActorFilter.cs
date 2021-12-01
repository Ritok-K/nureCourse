using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class ActorFilter : PrimaryKeyFilter
    {
        internal ActorFilter()
            : base(MovieDB.c_ActorsTable, MovieDB.c_ActorIdColumn)
        { }
    }
}
