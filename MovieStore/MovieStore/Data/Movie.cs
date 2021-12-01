using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }            // in UTC
        public string Genre { get; set; }
        public string Description { get; set; }       // can be null
        public float? Imdb { get; set; }              // can be null
        public string Country { get; set; }           // can be null
        public int Price { get; set; }                // in cents
        public Studio Studio { get; set; }            // either whole Studio obj or Studio.Id only
        public IList<Actor> Actors { get; set; }      // can be null
    }
}
