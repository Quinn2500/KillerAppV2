using DAL;

namespace Business.Dashboard
{
    public class AdminLogic
    {
        AdminDatabaseCalls db = new AdminDatabaseCalls();
        public void Approve(string listName, string listUser)
        {
            db.ApproveList(db.GetLijstID(listUser, listName));
        }

        public void RemovePublic(string listName, string listUser)
        {
            db.RemovePublic(db.GetLijstID(listUser, listName));
        }
    }
}
