using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    class Gebruiker : User
    {
        private bool mIsAdmin = false;
        public bool IsAdmin { get { return mIsAdmin; } }
    }
}
