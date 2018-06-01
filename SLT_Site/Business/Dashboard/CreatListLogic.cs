using System;
using System.Collections.Generic;
using System.Text;
using DataModellen;
using DAL;

namespace Business.Dashboard
{
    public class CreatListLogic
    {
        DataBaseCalls db = new DataBaseCalls();
        private List<Woord> lijst = new List<Woord>();

        public Lijst CreateList(string s, string username)
        {
            string[] stringArray = s.Split('/');
            string titel = stringArray[0];
            string[] opties = stringArray[1].Split('?');
            string soort = stringArray[2];
            int isPublic = 0;
            if (opties[1].Split('=')[1] == "true")
            {
                isPublic = 1;
            }
            string[] stringLijst = opties[0].Split('|');
            foreach (string t in stringLijst)
            {
                if (t != "")
                {
                    string[] w = t.Split('-');
                    Woord k = new Woord() { Begrip = w[0], Betekenis = w[1] };
                    lijst.Add(k);
                }
            }
            Lijst l = new Lijst{ Gebruikersnaam = username, Datum = String.Format("{0:dd/MM/yyyy}", DateTime.Today), isPublic = isPublic, Soort = soort, Titel = titel, WoordenLijst = lijst };
            db.InsertLijst(l);
            return l;
        }
    }
}
