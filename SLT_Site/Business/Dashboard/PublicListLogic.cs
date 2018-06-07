using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModellen;
using DAL;

namespace Business.Dashboard
{
    public class PublicListLogic
    {
        DataBaseCalls db = new DataBaseCalls();
        public List<Lijst> GetAllPublicLists(string user)
        {
            List<Lijst> antwoord = new List<Lijst>();
            foreach (DataRow dr in db.GetOpenbareLijsten(user).Rows)
            {
                Lijst l = new Lijst();
                l.Titel = dr[4].ToString();
                l.Gebruikersnaam = dr[1].ToString();
                l.Soort = dr[2].ToString();
                l.Datum = dr[3].ToString();
                antwoord.Add(l);
            }

            return antwoord;
        }

        public List<Lijst> GetAllApprovedLists(string user)
        {
            List<Lijst> antwoord = new List<Lijst>();
            foreach (DataRow dr in db.GetApprovedLijsten(user).Rows)
            {
                Lijst l = new Lijst();
                l.Titel = dr[4].ToString();
                l.Gebruikersnaam = dr[1].ToString();
                l.Soort = dr[2].ToString();
                l.Datum = dr[3].ToString();
                antwoord.Add(l);
            }

            return antwoord;
        }
    }
}
