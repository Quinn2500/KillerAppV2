using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class TestDatabaseCalls : DataBaseCalls
    {
        public void DeleteUser(string username)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@p",username)
            };
            SqlCommand("DELETE FROM gebruiker WHERE gebruiker.gebruikersnaam = @p",p);
        }

        public void DeleteList(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("DELETE FROM lijst WHERE lijst.ID = @ID", p);
        }
    }
}
