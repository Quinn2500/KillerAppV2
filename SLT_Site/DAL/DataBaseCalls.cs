using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void Command(string query, List<MySqlParameter> p)
        {
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            foreach (MySqlParameter param in p)
                cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
        }

        private DataTable Select(string query, MySqlParameter p)
        {

            DataTable dtResult = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            cmd.Parameters.Add(p);
            mConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            dtResult.Load(reader);
            mConn.Close();
            return dtResult;
        }

        private string Read(string query, List<MySqlParameter> p)
        {
            string antwoord = "leeg";
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            foreach (MySqlParameter param in p)
            {
                cmd.Parameters.Add(param);
            }
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
        public void CreateAccount(string username, string password, string email, string firstname, string lastname, bool isadmin)
        {
            MySqlCommand cmd = new MySqlCommand("AddUser", mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@Passw", MySqlDbType.VarChar).Value = password;
            cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = firstname;
            cmd.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;
            cmd.Parameters.AddWithValue("@IsAdmin", isadmin);
            mConn.Open();
            cmd.ExecuteNonQuery();
            mConn.Close();
        }

        public string GetPassword(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Read("SELECT gebruiker.Wachtwoord FROM gebruiker WHERE gebruiker.Gebruikersnaam = @Username",p);

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
            mConn.Open();
            List<MySqlParameter> LijstParameter = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", l.Gebruikersnaam),
                new MySqlParameter("@Soort", l.Soort),
                new MySqlParameter("@Date", l.Datum),
                new MySqlParameter("@Titel", l.Titel),
                new MySqlParameter("@IsPublic", l.isPublic)
            };
            Command("INSERT INTO lijst(Gebruikersnaam, Soort, Datum, Naam, Openbaar) VALUES (@Username, @Soort, @Date,@Titel, @IsPublic)", LijstParameter);
            int cijfer = LastID();
            mConn.Close();
            foreach (Woord w in l.WoordenLijst)
            {
                List<MySqlParameter> WoordParameter = new List<MySqlParameter>()
                {
                    new MySqlParameter("@Begrip", w.Begrip),
                    new MySqlParameter("@Betekenis", w.Betekenis)
                };
                mConn.Open();
                Command("INSERT INTO woorden(Woord, Betekenis) VALUES (@Begrip, @Betekenis)", WoordParameter);
                int nummer = LastID();
                mConn.Close();
                List<MySqlParameter> KoppelParameter = new List<MySqlParameter>()
                {
                    new MySqlParameter("@Nummer", nummer),
                    new MySqlParameter("@Cijfer", cijfer)
                };
                SqlCommand("INSERT INTO woordtolijst(WoordID, LijstID) VALUES (@Nummer, @Cijfer)",KoppelParameter);
            }
            mConn.Close();
        }

        public DataTable GetWoordenFromLijst(int id)
        {
            MySqlParameter p = new MySqlParameter("@ID", id);
            return Select("SELECT woorden.Woord, woorden.Betekenis FROM woordtolijst INNER JOIN woorden on woordtolijst.WoordID = woorden.ID WHERE woordtolijst.LijstID = @ID",p);
        }

        public int GetLijstID(string username, string lijstnaam)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
                new MySqlParameter("@Lijstnaam", lijstnaam)
            };
            string s = Read("SELECT lijst.ID FROM `lijst` WHERE lijst.Gebruikersnaam = @Username AND lijst.Naam = @Lijstnaam",p);
            return Convert.ToInt32(s);
        }

        public DataTable GetOpenbareLijsten(string username)
        {
            MySqlParameter p = new MySqlParameter("@Username", username);
            return Select("SELECT * FROM `lijst` WHERE lijst.Openbaar = 1 && lijst.Gebruikersnaam != @Username",p);
        }

        public void ApproveList(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("UPDATE lijst SET lijst.Goedkgekeurd = 1 WHERE lijst.ID = @ID",p);
        }

        public void DeleteList(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("DELETE FROM lijst WHERE lijst.ID = @ID", p);
        }

        public int CheckIfUsernameExitst(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Convert.ToInt32(Read("SELECT COUNT(gebruiker.Gebruikersnaam) FROM `gebruiker` WHERE gebruiker.Gebruikersnaam = @Username",p));
        }

        public string CheckIfAdmin(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Read("SELECT gebruiker.Admin FROM `gebruiker` WHERE gebruiker.Gebruikersnaam = @Username", p);            
        }

        public void RemovePublic(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("UPDATE lijst SET lijst.Openbaar = 0 WHERE lijst.ID = @ID", p);
        }

        public int CheckIfTitelExists(string titel)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Titel", titel),
            };
            return Convert.ToInt32(Read("SELECT COUNT(lijst.Naam) FROM `lijst` WHERE lijst.Naam = @Titel", p));
        }

        public DataTable GetApprovedLijsten(string username)
        {
            MySqlParameter p = new MySqlParameter("@Username", username);
            return Select("SELECT * FROM `lijst` WHERE lijst.Goedkgekeurd = 1 && lijst.Gebruikersnaam != @Username", p);
        }
    }
}
