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

    class NotAuthorizedDBException : DBExecption
    {
        internal NotAuthorizedDBException()
            : base($"Login is required")
        {
        }
    }

    class UserNotFoundDBException : DBExecption
    {
        internal UserNotFoundDBException(string email)
            : base($"User with loging '{email}' has not been found")
        {
        }
    }

    class UserAlreadyExistsDBException : DBExecption
    {
        internal UserAlreadyExistsDBException(string email)
            : base($"User with e-mail '{email}' already exists")
        {
        }
    }

    class AttemptToDeleteCurrentUserDBException : DBExecption
    {
        internal AttemptToDeleteCurrentUserDBException(string email)
            : base($"You can not delete the currently logged in user '{email}'!")
        {
        }
    }

    class InvalidPasswordDBException : DBExecption
    {
        internal InvalidPasswordDBException()
            : base($"Invalid password")
        {
        }
    }
}
