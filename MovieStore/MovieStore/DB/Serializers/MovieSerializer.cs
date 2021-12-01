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
                Id = row.Field<int>($"{prefix}{MovieDB.c_MovieIdColumn}"),
                Title = row.Field<string>($"{prefix}{MovieDB.c_TitleColumn}"),
                Year = row.Field<DateTime>($"{prefix}{MovieDB.c_MovieYearColumn}"),
                Genre = row.Field<string>($"{prefix}{MovieDB.c_GenreColumn}"),
                Description = row.Field<string>($"{prefix}{MovieDB.c_DescriptionColumn}"),
                Imdb = row.Field<float?>($"{prefix}{MovieDB.c_ImdbColumn}"),
                Country = row.Field<string>($"{prefix}{MovieDB.c_CountryColumn}"),
                Price = row.Field<int>($"{prefix}{MovieDB.c_PriceColumn}"),
                Studio = new Data.Studio() { Id = row.Field<int>($"{prefix}{MovieDB.c_StudioIdColumn}") },
            };

            // Do we need it?
            //res.Year = DateTime.SpecifyKind(res.Year, DateTimeKind.Utc);

            return res;
        }

        internal static void Save(Data.Movie movie, DataRow row)
        {
            if (movie.Studio == null)
            {
                throw new ArgumentException("Studio.Id should exists.", nameof(Data.Movie.Studio));
            }

            row[MovieDB.c_TitleColumn] = movie.Title;
            row[MovieDB.c_MovieYearColumn] = movie.Year;
            row[MovieDB.c_GenreColumn] = movie.Genre;
            row[MovieDB.c_DescriptionColumn] = movie.Description;
            row[MovieDB.c_ImdbColumn] = movie.Imdb;
            row[MovieDB.c_CountryColumn] = movie.Country;
            row[MovieDB.c_PriceColumn] = movie.Price;
            row[MovieDB.c_StudioIdColumn] = movie.Studio.Id;
        }

        internal static void AddColumns(DataTable table)
        {
            table.Columns.Add(MovieDB.c_TitleColumn, typeof(string));
            table.Columns.Add(MovieDB.c_MovieYearColumn, typeof(DateTime));
            table.Columns.Add(MovieDB.c_GenreColumn, typeof(string));
            table.Columns.Add(MovieDB.c_DescriptionColumn, typeof(string));
            table.Columns.Add(MovieDB.c_ImdbColumn, typeof(float));
            table.Columns.Add(MovieDB.c_CountryColumn, typeof(string));
            table.Columns.Add(MovieDB.c_PriceColumn, typeof(int));
            table.Columns.Add(MovieDB.c_StudioIdColumn, typeof(int));
        }
    }
}
