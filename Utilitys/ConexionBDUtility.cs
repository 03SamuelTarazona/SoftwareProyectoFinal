using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Software_Proyecto.Utilitys
{
    public class ConexionBDUtility
    {
        static string SERVER = "SAMUEL";
        static string DB_NAME = "CentroMedico_Software";
        static string DB_USER = "hospital";
        static string DB_PASSWORD = "123";

        static string Conn = "server=" + SERVER + ";database=" + DB_NAME + ";user id=" + DB_USER + ";password=" + DB_PASSWORD + ";MultipleActiveResultSets=true";

        SqlConnection Con = new SqlConnection(Conn);
        public void Connect()
        {
            try
            {
                Con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Disconnect()
        {
            Con.Close();
        }

        public SqlConnection Conexion()
        {
            return Con;
        }
    }
}
