using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    class Movie
    {
        internal int Id { get; set; }
        internal string Title { get; set; }
        internal DateTime Year { get; set; }            // in UTC
        internal string Genre { get; set; }
        internal string Description { get; set; }       // can be null
        internal float? Imdb { get; set; }              // can be null
        internal string Country { get; set; }           // can be null
        internal int Price { get; set; }                // in cents
        internal Studio Studio { get; set; }            // either whole Studio obj or Studio.Id only
        internal IList<Actor> Actors { get; set; }      // can be null
    }
}
