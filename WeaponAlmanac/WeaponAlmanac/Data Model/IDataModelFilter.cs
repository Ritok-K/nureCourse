using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    interface IDataModelWeaponFilter
    {
        bool PassId(string id);

        bool PassDataModelObject(Weapon weapon);
    }

    interface IDataModelCollectorFilter
    {
        bool PassId(string id);

        bool PassDataModelObject(Collector collector);
    }
}
