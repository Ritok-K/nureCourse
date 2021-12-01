using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    public class Studio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }           // can be null
        public DateTime FoundationDate { get; set; }  // in UTC
        public string Production { get; set; }       // can be null
    }
}
