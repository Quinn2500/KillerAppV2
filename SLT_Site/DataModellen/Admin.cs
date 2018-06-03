using System;
using System.Collections.Generic;
using System.Text;

namespace DataModellen
{
    public class Admin : User
    {
        private bool mIsAdmin = true;
        public bool IsAdmin { get { return mIsAdmin; } }
    }
}
