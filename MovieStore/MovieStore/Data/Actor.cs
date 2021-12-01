using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    public enum ActorFamilyStatus
    {
        Single,
        Married
    }

    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime? BirthDate { get; set; }              // in UTC
        public string Country { get; set; }                   // can be null
        public ActorFamilyStatus? FamilyStatus { get; set; }  // can be null
        public string AwardsDescription { get; set; }         // can be null
    }
}
