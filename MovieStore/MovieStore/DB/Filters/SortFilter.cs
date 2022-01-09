using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Filters
{
    class SortFilter : EmptyFilter
    {
        List<Tuple<string, bool>> OrderColumns { get; set; } = new List<Tuple<string, bool>>();

        internal void SortByColumn(string orderByColumn, bool desc = false)
        {
            OrderColumns.Add(Tuple.Create(orderByColumn, desc));
        }

        internal void Build(Type modelType, string modelField, bool desc)
        {
            var sortList = new List<string>();

            if (modelType == typeof(Data.Movie))
            {
                switch (modelField)
                {
                    case nameof(Data.Movie.Title):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_TitleColumn));
                        break;
                    case nameof(Data.Movie.Year):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_MovieYearColumn));
                        break;
                    case nameof(Data.Movie.Genre):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_GenreColumn));
                        break;
                    case nameof(Data.Movie.Imdb):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_ImdbColumn));
                        break;
                    case nameof(Data.Movie.Studio):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_TitleColumn));
                        break;
                    case nameof(Data.Movie.Country):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_CountryColumn));
                        break;
                    case nameof(Data.Movie.Price):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_MoviesTable, MovieDB.c_PriceColumn));
                        break;
                    case nameof(Data.Movie.Income):
                        sortList.Add(MovieDB.c_IncomeColumn); // aggregated fileds don't have table prefix
                        break;
                }
            }
            else if (modelType == typeof(Data.Actor))
            {
                switch (modelField)
                {
                    case nameof(Data.Actor.FirstName):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_FirstNameColumn));
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_SecondNameColumn));
                        break;
                    case nameof(Data.Actor.SecondName):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_SecondNameColumn));
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_FirstNameColumn));
                        break;
                    case nameof(Data.Actor.BirthDate):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_BirthDateColumn));
                        break;
                    case nameof(Data.Actor.Country):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_CountryColumn));
                        break;
                    case nameof(Data.Actor.FamilyStatus):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_ActorsTable, MovieDB.c_FamilyStatusColumn));
                        break;
                }
            }
            else if (modelType == typeof(Data.Studio))
            {
                switch (modelField)
                {
                    case nameof(Data.Studio.Title):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_TitleColumn));
                        break;
                    case nameof(Data.Studio.Country):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_CountryColumn));
                        break;
                    case nameof(Data.Studio.FoundationDate):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_FoundationDateColumn));
                        break;
                    case nameof(Data.Studio.Production):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_StudioTable, MovieDB.c_ProductionColumn));
                        break;
                    case nameof(Data.Studio.Income):
                        sortList.Add(MovieDB.c_IncomeColumn); // aggregated fileds don't have table prefix
                        break;
                }
            }
            else if (modelType == typeof(Data.Order))
            {
                switch (modelField)
                {
                    case nameof(Data.Order.Id):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_OrdersTable, MovieDB.c_OrderIdColumn));
                        break;
                    case nameof(Data.Order.Date):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_OrdersTable, MovieDB.c_DateColumn));
                        break;
                    case nameof(Data.Order.User):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_FirstNameColumn));
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_SecondNameColumn));
                        break;
                    case nameof(Data.Order.Income):
                        sortList.Add(MovieDB.c_IncomeColumn); // aggregated fileds don't have table prefix
                        break;
                }
            }
            else if (modelType == typeof(Data.User))
            {
                switch (modelField)
                {
                    case nameof(Data.User.FirstName):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_FirstNameColumn));
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_SecondNameColumn));
                        break;
                    case nameof(Data.User.SecondName):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_SecondNameColumn));
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_FirstNameColumn));
                        break;
                    case nameof(Data.User.EMail):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_EMailColumn));
                        break;
                    case nameof(Data.User.Role):
                        sortList.Add(MovieDB.BuildFieldName(MovieDB.c_UsersTable, MovieDB.c_RoleColumn));
                        break;
                    case nameof(Data.User.Income):
                        sortList.Add(MovieDB.c_IncomeColumn); // aggregated fileds don't have table prefix
                        break;
                }
            }

            foreach (var c in sortList)
            {
                SortByColumn(c, desc);
            }
        }
        public override IEnumerable<string> GetOrderClauses()
        {
            return OrderColumns.Select(c =>
            {
                var desc = c.Item2 ? " DESC" : string.Empty;
                return $"{c.Item1}{desc}";
            }).ToList();
        }
    }
}
