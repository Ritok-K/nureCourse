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
        public int ManufacturedStartDate { get; set; } = DataModelUtils.InvalidDate;
        public int ManufacturedEndDate { get; set; } = DataModelUtils.InvalidDate;
        public bool HasManufacturedStartDate => ManufacturedStartDate != DataModelUtils.InvalidDate;
        public bool HasManufacturedEndDate => ManufacturedStartDate != DataModelUtils.InvalidDate;
        public bool HasValidManufacturedDate => HasManufacturedStartDate &&
                                                HasManufacturedEndDate &&
                                               (ManufacturedStartDate <= ManufacturedEndDate);

        public bool IsEmpty => string.IsNullOrEmpty(Name) &&
                               string.IsNullOrEmpty(Description) &&
                               string.IsNullOrEmpty(Country) &&
                               !HasValidManufacturedDate;

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

            if (res && HasValidManufacturedDate)
            {
                res = weapon.ManufactureDate >= ManufacturedStartDate &&
                      weapon.ManufactureDate <= ManufacturedEndDate;
            }

            return res;
        }
    }
}
