using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class LoginDatabaseCalls : DataBaseCalls
    {
        public void CreateAccount(string username, string password, string email, string firstname, string lastname, bool isadmin)
        {
            MySqlCommand cmd = new MySqlCommand("AddUser", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@Passw", MySqlDbType.VarChar).Value = password;
            cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = firstname;
            cmd.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;
            cmd.Parameters.AddWithValue("@IsAdmin", isadmin);
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        public string GetPassword(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Read("SELECT gebruiker.Wachtwoord FROM gebruiker WHERE gebruiker.Gebruikersnaam = @Username", p);

        }

        public int CheckIfUsernameExitst(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Convert.ToInt32(Read("SELECT COUNT(gebruiker.Gebruikersnaam) FROM `gebruiker` WHERE gebruiker.Gebruikersnaam = @Username", p));
        }

        public string CheckIfAdmin(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@Username", username),
            };
            return Read("SELECT gebruiker.Admin FROM `gebruiker` WHERE gebruiker.Gebruikersnaam = @Username", p);
        }

    }
}
