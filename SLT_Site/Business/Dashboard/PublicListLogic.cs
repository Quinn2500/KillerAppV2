using System.Collections.Generic;
using System.Data;
using DataModellen;
using DAL;

namespace Business.Dashboard
{
    public class PublicListLogic
    {
        DashboardDatabaseCalls db = new DashboardDatabaseCalls();
        public List<Lijst> GetAllPublicLists(string user)
        {
            List<Lijst> antwoord = new List<Lijst>();
            foreach (DataRow dr in db.GetOpenbareLijsten(user).Rows)
            {
                Lijst l = new Lijst
                {
                    Titel = dr[4].ToString(),
                    Gebruikersnaam = dr[1].ToString(),
                    Soort = dr[2].ToString(),
                    Datum = dr[3].ToString()
                };
                antwoord.Add(l);
            }

            return antwoord;
        }

        public List<Lijst> GetAllApprovedLists(string user)
        {
            List<Lijst> antwoord = new List<Lijst>();
            foreach (DataRow dr in db.GetApprovedLijsten(user).Rows)
            {
                Lijst l = new Lijst
                {
                    Titel = dr[4].ToString(),
                    Gebruikersnaam = dr[1].ToString(),
                    Soort = dr[2].ToString(),
                    Datum = dr[3].ToString()
                };
                antwoord.Add(l);
            }

            return antwoord;
        }
    }
}
