using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DB
{
    class DBExecption : Exception
    {
        internal DBExecption(string message) 
            : base(message)
        {
        }
    }

    class UserNotFoundDBExceptiom : DBExecption
    {
        internal UserNotFoundDBExceptiom(string email)
            : base($"User with loging '{email}' has not been found")
        {
        }
    }

    class UserAlreadyExistsDBExceptiom : DBExecption
    {
        internal UserAlreadyExistsDBExceptiom(string email)
            : base($"User with loging '{email}' already exists")
        {
        }
    }

    class InvalidPasswordDBExceptiom : DBExecption
    {
        internal InvalidPasswordDBExceptiom()
            : base($"Invalid password")
        {
        }
    }
}
