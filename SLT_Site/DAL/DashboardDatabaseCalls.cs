using System;
using System.Collections.Generic;
using System.Data;
using DataModellen;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DashboardDatabaseCalls : DataBaseCalls
    {
        private void Command(string query, List<MySqlParameter> p)
        {
            MySqlCommand cmd = new MySqlCommand(query, Conn);
            foreach (MySqlParameter param in p)
                cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
        }

        private int LastID()
        {
            int antwoord = 0;
            MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", Conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                antwoord = Convert.ToInt32(reader.GetString(0));
            }

            return antwoord;
        }

        public DataTable GetListFromUser(string username)
        {
            MySqlParameter p = new MySqlParameter("@Username", username);
            return Select("SELECT * FROM `lijst` WHERE lijst.Gebruikersnaam = @Username", p);
        }

        public DataTable Getlijst(int id)
        {
            MySqlParameter p = new MySqlParameter("@ID", id);
            return Select("SELECT * FROM `lijst` WHERE lijst.ID = ID", p);
        }

        public void InsertLijst(Lijst l)
        {
            Conn.Open();
            List<MySqlParameter> lijstParameter = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", l.Gebruikersnaam),
                new MySqlParameter("@Soort", l.Soort),
                new MySqlParameter("@Date", l.Datum),
                new MySqlParameter("@Titel", l.Titel),
                new MySqlParameter("@IsPublic", l.IsPublic)
            };
            Command("INSERT INTO lijst(Gebruikersnaam, Soort, Datum, Naam, Openbaar) VALUES (@Username, @Soort, @Date,@Titel, @IsPublic)", lijstParameter);
            int cijfer = LastID();
            Conn.Close();
            foreach (Woord w in l.WoordenLijst)
            {
                List<MySqlParameter> woordParameter = new List<MySqlParameter>()
                {
                    new MySqlParameter("@Begrip", w.Begrip),
                    new MySqlParameter("@Betekenis", w.Betekenis)
                };
                Conn.Open();
                Command("INSERT INTO woorden(Woord, Betekenis) VALUES (@Begrip, @Betekenis)", woordParameter);
                int nummer = LastID();
                Conn.Close();
                List<MySqlParameter> koppelParameter = new List<MySqlParameter>()
                {
                    new MySqlParameter("@Nummer", nummer),
                    new MySqlParameter("@Cijfer", cijfer)
                };
                SqlCommand("INSERT INTO woordtolijst(WoordID, LijstID) VALUES (@Nummer, @Cijfer)", koppelParameter);
            }
            Conn.Close();
        }

        public DataTable GetWoordenFromLijst(int id)
        {
            MySqlParameter p = new MySqlParameter("@ID", id);
            return Select("SELECT woorden.Woord, woorden.Betekenis FROM woordtolijst INNER JOIN woorden on woordtolijst.WoordID = woorden.ID WHERE woordtolijst.LijstID = @ID", p);
        }

        public DataTable GetOpenbareLijsten(string username)
        {
            MySqlParameter p = new MySqlParameter("@Username", username);
            return Select("SELECT * FROM `lijst` WHERE lijst.Openbaar = 1 && lijst.Gebruikersnaam != @Username && lijst.Goedkgekeurd = 0", p);
        }

        public void DeleteList(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("DELETE FROM lijst WHERE lijst.ID = @ID", p);
        }

        public int CheckIfTitelExists(string titel,string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Titel", titel),
                new MySqlParameter("@Username", username)
            };
            return Convert.ToInt32(Read("SELECT COUNT(lijst.Naam) FROM `lijst` WHERE lijst.Naam = @Titel  && lijst.Gebruikersnaam = @Username", p));
        }

        public DataTable GetApprovedLijsten(string username)
        {
            MySqlParameter p = new MySqlParameter("@Username", username);
            return Select("SELECT * FROM `lijst` WHERE lijst.Goedkgekeurd = 1 && lijst.Gebruikersnaam != @Username", p);
        }
    }
}
