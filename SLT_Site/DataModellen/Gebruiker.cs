using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    public class Gebruiker : User
    {
        private bool mIsAdmin = false;
        public bool IsAdmin { get { return mIsAdmin; } }
    }
}
