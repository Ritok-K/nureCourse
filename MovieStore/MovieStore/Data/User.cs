using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data
{
    enum UserRole
    {
        User,
        Manager,
    }

    class User
    {
        internal int Id { get; set; }
        internal string FirstName { get; set; }
        internal string SecondName { get; set; }
        internal string EMail { get; set; }
        internal UserRole Role { get; set; } = Data.UserRole.User;
        internal string PasswordHash { get; set; }
        internal string Salt { get; init; } = Utility.Seсurity.GenerateSalt();

        internal void SetPassword(string password)
        {
            var hash = Utility.Seсurity.GetSHA256Hash($"{password}{Salt}");
            PasswordHash = hash;
        }

        internal bool CheckPassword(string password)
        {
            var hash = Utility.Seсurity.GetSHA256Hash($"{password}{Salt}");

            var res = PasswordHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
            return res;
        }
    }
}
