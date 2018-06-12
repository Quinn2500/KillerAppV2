using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class AdminDatabaseCalls : DataBaseCalls
    {
        public void ApproveList(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("UPDATE lijst SET lijst.Goedkgekeurd = 1 WHERE lijst.ID = @ID", p);
        }

        public void RemovePublic(int id)
        {
            List<MySqlParameter> p = new List<MySqlParameter>()
            {
                new MySqlParameter("@ID", id),
            };
            SqlCommand("UPDATE lijst SET lijst.Openbaar = 0 WHERE lijst.ID = @ID", p);
        }
    }
}
