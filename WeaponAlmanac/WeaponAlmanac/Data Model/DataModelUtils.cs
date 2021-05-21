using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model
{
    public static class DataModelUtils
    {
        public static int InvalidDate => int.MinValue;

        public static string FormatYear(int year)
        {
            var res = string.Empty;
            if (year != InvalidDate)
            {
                var format = (year < 0) ? Properties.Resources.YearBCFormat :
                                          Properties.Resources.YearACFormat;
                res = string.Format(format, Math.Abs(year));
            }

            return res;
        }

        public static int ParseYear(string year)
        {
            var res = InvalidDate;
            
            year = year.Trim();
            if (!string.IsNullOrEmpty(year))
            {
                var values = year.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(values[0], out res))
                {
                    if ((res >= 0) && string.Format(Properties.Resources.YearBCFormat, res).Equals(year, StringComparison.OrdinalIgnoreCase))
                    {
                        res *= -1;
                    }
                }
            }

            return res;
        }
    }
}
