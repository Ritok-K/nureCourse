using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model.Filters
{
    public class CollectorFilter : IDataModelCollectorFilter
    {
        public List<string> Ids { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool? HasRareWeapon { get; set; } = null;

        public bool IsEmpty => ((Ids==null) || (Ids.Count==0)) &&
                               string.IsNullOrEmpty(Name) &&
                               string.IsNullOrEmpty(Country) &&
                               !HasRareWeapon.HasValue;

        public bool PassId(string id)
        {
            bool res = Ids == null;
            if (!res)
            {
                var idIndex = Ids.FindIndex(i => i == id);
                res = idIndex >= 0;
            }

            return res;
        }

        public bool PassDataModelObject(Collector collector)
        {
            bool res = true;

            if (res && !string.IsNullOrEmpty(Name))
            {
                res = collector.Name.Contains(Name, StringComparison.OrdinalIgnoreCase);
            }

            if (res && !string.IsNullOrEmpty(Country))
            {
                res = collector.Country.Contains(Country, StringComparison.OrdinalIgnoreCase);
            }

            if (res && HasRareWeapon.HasValue)
            {
                res = collector.OwnIds.Count>0 == HasRareWeapon.Value;
            }

            return res;
        }
    }
}
