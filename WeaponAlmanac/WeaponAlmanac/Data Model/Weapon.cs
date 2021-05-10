using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    [Serializable]
    public class Weapon : DataModelObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public DateTime ManufactureDate { get; set; }
        public uint IssuedNumber { get; set; } = 0;
        public string Material { get; set; }
        public bool IsRare { get; set; } = false;
    }
}
