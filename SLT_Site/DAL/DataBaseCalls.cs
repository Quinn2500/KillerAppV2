using System;
using System.Collections.Generic;
using System.Data;
using DataModellen;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataBaseCalls
    {
        private MySqlConnection mConn = new MySqlConnection("Server=localhost; database=sltdatabase; UID=root; password=; Sslmode=none;");
        public string TestConnection()
        {
            string response = null;
            try
            {
                mConn.Open();
                response = null;
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            finally
            {
                mConn.Close();
            }
            return response;
        }

        private void SqlCommand(string query, List<MySqlParameter> p )
        {
            mConn.Open();
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            foreach (MySqlParameter param in p)
                cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
            mConn.Close();
        }

        private void Command(string query)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(query, mConn);
            mySqlCommand.ExecuteNonQuery();
        }

        private DataTable Select(string query)
        {

            DataTable dtResult = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            mConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            dtResult.Load(reader);
            mConn.Close();
            return dtResult;
        }

        private string Read(string query)
        {
            string antwoord = "leeg";
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            mConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                antwoord = reader.GetString(0);
            }
            mConn.Close();
            return antwoord;
        }

        private int LastID()
        {
            int antwoord = 0;
            MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", mConn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                antwoord = Convert.ToInt32(reader.GetString(0));
            }

            return antwoord;
        }
        public void CreateAccount(string username, string password, string email, string firstname, string lastname)
        {
            List<MySqlParameter> List = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
                new MySqlParameter("@Password", password),
                new MySqlParameter("@FirstName", firstname),
                new MySqlParameter("@LastName", lastname),
                new MySqlParameter("@Email", email)
            };
            SqlCommand("INSERT INTO gebruiker(Gebruikersnaam, Wachtwoord, Voornaam, Achternaam, Email) VALUES (@Username, @Password, @FirstName,@LastName, @Email)", List);
        }

        public string GetPassword(string username)
        {
            return Read($"SELECT gebruiker.Wachtwoord FROM gebruiker WHERE gebruiker.Gebruikersnaam = '{username}'");

        }

        public DataTable GetAllLijsten(string username)
        {
            return Select($"SELECT * FROM `lijst` WHERE lijst.Gebruikersnaam = '{username}'");
        }

        public DataTable Getlijst(int id)
        {
            return Select($"SELECT * FROM `lijst` WHERE lijst.ID = '{id}'");
        }

        public void InsertLijst(Lijst l)
        {
            mConn.Open();
            Command($"INSERT INTO lijst(Gebruikersnaam, Soort, Datum, Naam, Openbaar) VALUES ('{l.Gebruikersnaam}', '{l.Soort}', '{l.Datum}','{l.Titel}', '{l.isPublic}')");
            int cijfer = LastID();
            mConn.Close();
            foreach (Woord w in l.WoordenLijst)
            {
                mConn.Open();
                Command($"INSERT INTO woorden(Woord, Betekenis) VALUES ('{w.Begrip}', '{w.Betekenis}')");
                int nummer = LastID();
                mConn.Close();
                //SqlCommand($"INSERT INTO woordtolijst(WoordID, LijstID) VALUES ('{nummer}', '{cijfer}')");
            }
            mConn.Close();
        }

        public DataTable GetWoordenFromLijst(int id)
        {
            return Select($"SELECT woorden.Woord, woorden.Betekenis FROM woordtolijst INNER JOIN woorden on woordtolijst.WoordID = woorden.ID WHERE woordtolijst.LijstID = '{id}'");
        }

        public int GetLijstID(string username, string lijstnaam)
        {
            string s = Read($"SELECT lijst.ID FROM `lijst` WHERE lijst.Gebruikersnaam = '{username}' AND lijst.Naam = '{lijstnaam}'");
            return Convert.ToInt32(s);
        }

        public DataTable GetOpenbareLijsten(string username)
        {
            return Select($"SELECT * FROM `lijst` WHERE lijst.Openbaar = 1 && lijst.Gebruikersnaam != '{username}'");
        }
    }
}
