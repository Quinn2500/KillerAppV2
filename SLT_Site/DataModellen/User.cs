using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    public abstract class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; }

        protected User(bool isadmin)
        {
            IsAdmin = isadmin;
        }

    }
}
