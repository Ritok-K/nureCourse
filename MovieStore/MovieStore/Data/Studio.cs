using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    class Studio
    {
        internal int Id { get; set; }
        internal string Title { get; set; }
        internal string Country { get; set; }           // can be null
        internal DateTime FoundationDate { get; set; }  // in UTC
        internal string Production { get; set; }       // can be null
    }
}
