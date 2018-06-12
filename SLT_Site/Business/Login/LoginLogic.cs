using DAL;

namespace Business.Login
{
    public class LoginLogic
    {
        LoginDatabaseCalls db = new LoginDatabaseCalls();

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

        public bool CheckIfAdmin(string username)
        {
            if (db.CheckIfAdmin(username) == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
