using System;
using System.Collections.Generic;
using System.Text;
using DataModellen;
using DAL;

namespace Business.Dashboard
{
    public class AdminLogic
    {
        AdminDatabaseCalls db = new AdminDatabaseCalls();
        public void Approve(string ListName, string ListUser)
        {
            db.ApproveList(db.GetLijstID(ListUser, ListName));
        }

        public void RemovePublic(string ListName, string ListUser)
        {
            db.RemovePublic(db.GetLijstID(ListUser, ListName));
        }
    }
}
