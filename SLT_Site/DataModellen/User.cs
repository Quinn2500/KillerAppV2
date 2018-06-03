using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    public class User
    {
        private string mUsername;
        private string mPassword;
        private string mFirstName;
        private string mLastName;
        private string mEmail;

        public string Username{ get { return mUsername; } set { mUsername = value; }}
        public string Password { get { return mPassword; } set { mPassword = value; } }
        public string FirstName { get { return mFirstName; } set { mFirstName = value; } }
        public string LastName { get { return mLastName; } set { mLastName = value; } }
        public string Email { get { return mEmail; } set { mEmail = value; } }

    }
}
