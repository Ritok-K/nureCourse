using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class MovieFilter : PrimaryKeyFilter
    {
        internal MovieFilter()
            : base(MovieDB.c_MoviesTable, MovieDB.c_MovieIdColumn)
        { }
    }
}
