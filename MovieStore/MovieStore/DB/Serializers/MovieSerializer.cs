using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Serializers
{
    static class MovieSerializer
    {
        internal static Data.Movie Load(DataRow row, string prefix = "")
        {
            var res = new Data.Movie()
            {
                Id = row.Field<int>($"{prefix}{MovieDB.c_MoviewIdColumn}"),
                Title = row.Field<string>($"{prefix}{MovieDB.c_TitleColumn}"),
                Year = row.Field<DateTime>($"{prefix}{MovieDB.c_MovieYearColumn}"),
                Genre = row.Field<string>($"{prefix}{MovieDB.c_GenreColumn}"),
                Description = row.Field<string>($"{prefix}{MovieDB.c_DescriptionColumn}"),
                Imdb = row.Field<float?>($"{prefix}{MovieDB.c_ImdbColumn}"),
                Country = row.Field<string>($"{prefix}{MovieDB.c_CountryColumn}"),
                Price = row.Field<int>($"{prefix}{MovieDB.c_PriceColumn}"),
                Studio = new Data.Studio() { Id = row.Field<int>($"{prefix}{MovieDB.c_StudioIdColumn}") },
            };

            return res;
        }
    }
}
