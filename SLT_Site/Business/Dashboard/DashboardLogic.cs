using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;

namespace Business.Dashboard
{
    public class DashboardLogic
    {
        DataBaseCalls db = new DataBaseCalls();

        public List<string> ListNames(string username)
        {
            List<string> namen = new List<string>();
            foreach (DataRow dr in db.GetListFromUser(username).Rows)
            {
                namen.Add(dr[4].ToString());
            }

            return namen;
        }

        public void DeleteList(string id, string username)
        {
            db.DeleteList(db.GetLijstID(username, id));
        }
    }
}
