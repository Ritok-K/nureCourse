using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    class Order
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }            // in UTC
        internal User User { get; set; }                // either whole User obj or User.Id only
        internal IList<Movie> Movies { get; set; }      // can be null
    }
}
