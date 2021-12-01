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

            // Do we need it?
            //res.FoundationDate = DateTime.SpecifyKind(res.FoundationDate, DateTimeKind.Utc);

            return res;
        }

        internal static void Save(Data.Studio studio, DataRow row)
        {
            row[MovieDB.c_TitleColumn] = studio.Title;
            row[MovieDB.c_CountryColumn] = studio.Country;
            row[MovieDB.c_FoundationDateColumn] = studio.FoundationDate;
            row[MovieDB.c_ProductionColumn] = studio.Production;
        }

        internal static void AddColumns(DataTable table)
        {
            table.Columns.Add(MovieDB.c_TitleColumn, typeof(string));
            table.Columns.Add(MovieDB.c_CountryColumn, typeof(string));
            table.Columns.Add(MovieDB.c_FoundationDateColumn, typeof(DateTime));
            table.Columns.Add(MovieDB.c_ProductionColumn, typeof(string));
        }
    }
}
