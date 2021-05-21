using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model.Filters
{
    public class WeaponFilter : IDataModelWeaponFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int ManufactureStartDate { get; set; } = DataModelUtils.InvalidDate;
        public int ManufactureEndDate { get; set; } = DataModelUtils.InvalidDate;

        public bool Pass(Weapon weapon)
        {
            bool res = true;

            if (res && !string.IsNullOrEmpty(Name))
            {
                res = weapon.Name.Contains(Name, StringComparison.OrdinalIgnoreCase);
            }

            if (res && !string.IsNullOrEmpty(Description))
            {
                res = weapon.Description.Contains(Description, StringComparison.OrdinalIgnoreCase);
            }

            if (res && !string.IsNullOrEmpty(Country))
            {
                res = weapon.Country.Contains(Country, StringComparison.OrdinalIgnoreCase);
            }

            if (res && (ManufactureStartDate != DataModelUtils.InvalidDate) &&
                       (ManufactureEndDate != DataModelUtils.InvalidDate))
            {
                res = weapon.ManufactureDate >= ManufactureStartDate &&
                      weapon.ManufactureDate <= ManufactureEndDate;
            }

            return res;
        }
    }
}
