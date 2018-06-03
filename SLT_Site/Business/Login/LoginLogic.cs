using System;
using System.Collections.Generic;
using System.Text;
using Business.Login;
using DataModellen;
using DAL;

namespace Business
{
    public class LoginLogic
    {
        DataBaseCalls db = new DataBaseCalls();

        public string Connectie()
        {
            return db.TestConnection();
        }

        public string Login(string username, string password)
        {
            if (Security.MatchHash(db.GetPassword(username),password))
            {
                return username;
            }
            else
            {
                return null;
            }
        }
    }
}
