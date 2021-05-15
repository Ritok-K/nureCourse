using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    public abstract class DataModelObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("B");
    }
}
