using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    public abstract class DataModelObject
    {
        static public DateTime InvalidDate => new DateTime(9999, 1, 1);

        public string Id { get; set; } = Guid.NewGuid().ToString("B");
    }
}
