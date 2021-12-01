using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }            // in UTC
        public User User { get; set; }                // either whole User obj or User.Id only
        public IList<Movie> Movies { get; set; }      // can be null
    }
}
