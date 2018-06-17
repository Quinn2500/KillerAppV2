using DataModellen;
using DAL;

namespace Business.Login
{
    public class AccountLogic
    {
        LoginDatabaseCalls db = new LoginDatabaseCalls();

        public void RegisterUser(Gebruiker user)
        {
            db.CreateAccount(user.Username,Security.CreateHash(user.Password),user.Email,user.FirstName,user.LastName,user.IsAdmin);
        }

        public void RegisterAdmin(Admin user)
        {
            db.CreateAccount(user.Username, Security.CreateHash(user.Password), user.Email, user.FirstName, user.LastName, user.IsAdmin);
        }

        public string CheckIfUserExitst(string username)
        {
            if (db.CheckIfUsernameExitst(username) == 1)
            {
                return "Deze gebruiker bestaat helaas al, kies een andere gebruikersnaam";
            }
            else
            {
                return null;
            }     
        }
    }
}
