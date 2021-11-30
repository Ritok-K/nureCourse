using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    enum ActorFamilyStatus
    {
        Single,
        Married
    }

    class Actor
    {
        internal int Id { get; set; }
        internal string FirstName { get; set; }
        internal string SecondName { get; set; }
        internal DateTime? BirthDate { get; set; }              // in UTC
        internal string Country { get; set; }                   // can be null
        internal ActorFamilyStatus? FamilyStatus { get; set; }  // can be null
        internal string AwardsDescription { get; set; }         // can be null
    }
}
