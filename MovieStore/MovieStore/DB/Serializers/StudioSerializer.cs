using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Serializers
{
    class StudioSerializer
    {
        internal static Data.Studio Load(DataRow row, string prefix = "")
        {
            var res = new Data.Studio()
            {
                Id = row.Field<int>($"{prefix}{MovieDB.c_StudioIdColumn}"),
                Title = row.Field<string>($"{prefix}{MovieDB.c_TitleColumn}"),
                Country = row.Field<string>($"{prefix}{MovieDB.c_CountryColumn}"),
                FoundationDate = row.Field<DateTime>($"{prefix}{MovieDB.c_FoundationDateColumn}"),
                Production = row.Field<string>($"{prefix}{MovieDB.c_ProductionColumn}"),
            };

            return res;
        }
    }
}
