using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model.Filters
{
    public class CollectorFilter : IDataModelCollectorFilter
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public bool? HasRareWeapon { get; set; } = null;

        public bool IsEmpty => string.IsNullOrEmpty(Name) &&
                               string.IsNullOrEmpty(Country) &&
                               !HasRareWeapon.HasValue;

        public bool Pass(Collector collector)
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
                res = collector.RareIds.Count>0 == HasRareWeapon.Value;
            }

            return res;
        }
    }
}
