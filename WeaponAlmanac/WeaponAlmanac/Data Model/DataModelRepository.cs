using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    class DataModelRepository
    {
        internal DataModelRepository(string ownCollectionPath, string commonCollectionPath)
        {
            m_ownCollectionPath = ownCollectionPath;
            m_commonCollectionPath = commonCollectionPath;
        }

        internal List<Weapon> GetWeapons()
        {
            return new List<Weapon>();
        }

        internal List<Collector> GetCollectors()
        {
            return new List<Collector>();
        }

        internal List<Weapon> GetOwnWeapons()
        {
            return new List<Weapon>();
        }

        string m_ownCollectionPath;
        string m_commonCollectionPath;
    }
}
