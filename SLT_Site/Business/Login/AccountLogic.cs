using System;
using Business.Login;
using DataModellen;
using DAL;

namespace Business
{
    public class AccountLogic
    {
        DataBaseCalls db = new DataBaseCalls();

        public void RegisterUser(Gebruiker user)
        {
            db.CreateAccount(user.Username,user.Password,user.Email,user.FirstName,user.LastName,user.IsAdmin);
        }

        public void RegisterAdmin(Admin user)
        {
            db.CreateAccount(user.Username, user.Password, user.Email, user.FirstName, user.LastName, user.IsAdmin);
        }
    }
}
