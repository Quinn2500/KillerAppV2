using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DataModellen;
using MySql.Data.MySqlClient;

namespace DAL
{
    public abstract class DataBaseCalls
    {
        private MySqlConnection mConn = new MySqlConnection("Server=localhost; database=sltdatabase; UID=root; password=; Sslmode=none;");
        public MySqlConnection Conn
        {
            get { return mConn; }
        }

        public string TestConnection()
        {
            string response = null;
            try
            {
                mConn.Open();
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

        public void SqlCommand(string query, List<MySqlParameter> p )
        {
            mConn.Open();
            MySqlCommand cmd = new MySqlCommand(query, mConn);
            foreach (MySqlParameter param in p)
                cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            mConn.Close();
        }

        public DataTable Select(string query, MySqlParameter p)
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

        public string Read(string query, List<MySqlParameter> p)
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

        public int GetLijstID(string username, string lijstnaam)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
                new MySqlParameter("@Lijstnaam", lijstnaam)
            };
            string s = Read("SELECT lijst.ID FROM `lijst` WHERE lijst.Gebruikersnaam = @Username AND lijst.Naam = @Lijstnaam", p);
            return Convert.ToInt32(s);
        }
    }
}
