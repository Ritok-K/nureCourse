using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    public enum UserRole
    {
        User,
        Manager,
    }

    public class User
    {
        #region Model properties

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string EMail { get; set; }
        public UserRole Role { get; set; } = Data.UserRole.User;
        public string PasswordHash { get; set; }
        public string Salt { get; init; } = Utility.Seсurity.GenerateSalt();

        #endregion

        #region Aggregated properties

        public int? Income { get; set; }              // can be null

        #endregion

        #region Methods

        public void SetPassword(string password)
        {
            var hash = Utility.Seсurity.GetSHA256Hash($"{password}{Salt}");
            PasswordHash = hash;
        }

        public bool CheckPassword(string password)
        {
            var hash = Utility.Seсurity.GetSHA256Hash($"{password}{Salt}");

            var res = PasswordHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
            return res;
        }

        #endregion
    }
}
