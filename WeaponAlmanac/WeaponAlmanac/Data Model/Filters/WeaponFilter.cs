using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model.Filters
{
    public class WeaponFilter : IDataModelWeaponFilter
    {
        public List<string> Ids { get; set; }
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
        public bool? IsRare { get; set; } = null;

        public bool IsEmpty => ((Ids == null) || (Ids.Count == 0)) && 
                               string.IsNullOrEmpty(Name) &&
                               string.IsNullOrEmpty(Description) &&
                               string.IsNullOrEmpty(Country) &&
                               !HasValidManufacturedDate &&
                               !IsRare.HasValue;

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

        public bool PassDataModelObject(Weapon weapon)
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

            if (res && IsRare.HasValue)
            {
                res = weapon.IsRare == IsRare.Value;
            }

            return res;
        }
    }
}
