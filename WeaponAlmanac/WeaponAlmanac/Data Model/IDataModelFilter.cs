using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    interface IDataModelWeaponFilter
    {
        bool Pass(Weapon weapon);
    }

    interface IDataModelCollectorFilter
    {
        bool Pass(Collector collector);
    }
}
