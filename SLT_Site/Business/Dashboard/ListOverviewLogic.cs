﻿using System;
using System.Collections.Generic;
using System.Data;
using DataModellen;
using DAL;

namespace Business.Dashboard
{
    public class ListOverviewLogic
    {
        DashboardDatabaseCalls db = new DashboardDatabaseCalls();

        public Lijst GiveList(string lijstnaam, string username)
        {
            int id = db.GetLijstID(username, lijstnaam);
            DataTable data = db.Getlijst(id);
            List<Woord> lijst = new List<Woord>();
            foreach (DataRow dr in db.GetWoordenFromLijst(id).Rows)
            {
                Woord w = new Woord()
                {
                    Begrip = dr[0].ToString(),
                    Betekenis = dr[1].ToString()
                };
                lijst.Add(w);
            }

            Lijst l = new Lijst()
            {
                Datum = data.Rows[0][3].ToString(),
                Gebruikersnaam = data.Rows[0][1].ToString(),
                IsPublic = Convert.ToInt32(data.Rows[0][6]),
                Soort = data.Rows[0][2].ToString(),
                Titel = lijstnaam,
                WoordenLijst = lijst

            };
            return l;
        }

    }
}
