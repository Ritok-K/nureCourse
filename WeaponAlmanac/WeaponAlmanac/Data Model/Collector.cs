using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    [Serializable]
    public class Collector : DataModelObject
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public List<string> RareIds { get; set; } = new List<string>();
    }
}
