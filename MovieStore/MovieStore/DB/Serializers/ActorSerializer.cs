using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB.Serializers
{
    class ActorSerializer
    {
        internal static Data.Actor Load(DataRow row, string prefix = "")
        {
            var res = new Data.Actor()
            {
                Id = row.Field<int>($"{prefix}{MovieDB.c_ActorIdColumn}"),
                FirstName = row.Field<string>($"{prefix}{MovieDB.c_FirstNameColumn}"),
                SecondName = row.Field<string>($"{prefix}{MovieDB.c_SecondNameColumn}"),
                BirthDate = row.Field<DateTime?>($"{prefix}{MovieDB.c_BirthDateColumn}"),
                Country = row.Field<string>($"{prefix}{MovieDB.c_CountryColumn}"),
                FamilyStatus = GetFamilyStatus(row.Field<string>($"{prefix}{MovieDB.c_FamilyStatusColumn}")),
                AwardsDescription = row.Field<string>($"{prefix}{MovieDB.c_AwardsDescriptionColumn}"),
            };

            // Do we need it?
            //if (res.BirthDate.HasValue)
            //{
            //    res.BirthDate = DateTime.SpecifyKind(res.BirthDate.Value, DateTimeKind.Utc);
            //}

            return res;
        }

        #region Helper Methods

        static Data.ActorFamilyStatus? GetFamilyStatus(string status)
        {
            var res = (Data.ActorFamilyStatus?)(null);

            switch (status)
            {
                case MovieDB.c_FamilyStatusSingleValue:
                    res = Data.ActorFamilyStatus.Single;
                    break;
                case MovieDB.c_FamilyStatusMarriedValue:
                    res = Data.ActorFamilyStatus.Married;
                    break;
                default:
                    res = null;
                    break;
            }

            return res;
        }

        static string GetFamilyStatusName(Data.ActorFamilyStatus? status)
        {
            var res = (string)(null);

            if (status.HasValue)
            {
                switch (status.Value)
                {
                    case Data.ActorFamilyStatus.Single:
                        res = MovieDB.c_FamilyStatusSingleValue;
                        break;
                    case Data.ActorFamilyStatus.Married:
                        res = MovieDB.c_FamilyStatusMarriedValue;
                        break;
                }
            }

            return res;
        }

        #endregion
    }
}
