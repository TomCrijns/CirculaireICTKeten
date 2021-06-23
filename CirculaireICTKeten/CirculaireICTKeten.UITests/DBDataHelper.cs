using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculaireICTKeten.UITests
{
    public static class DBDataHelper
    {
        public static string GetSaltFromDB()
        {
            string connString = "Server=tcp:circulaireictketendbserver.database.windows.net,1433;Initial Catalog=CirculaireICTKeten_db;Persist Security Info=False;User ID=test123;Password=groepE-3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT Salt FROM AccountData WHERE ProfileId = 14");
            cmd.Connection = conn;
            conn.Open();
            string salt = (string)cmd.ExecuteScalar();
            conn.Close();
            return salt;
        }
    }
}
