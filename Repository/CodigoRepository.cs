using Software_Proyecto.Utilitys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class CodigoRepository
{
        public int registroRecuperacion(int id_persona, string codigo)
        {
            int comando = 0;
            
                ConexionBDUtility conexion = new ConexionBDUtility();
                conexion.Connect();
                string horaActual = DateTime.Now.ToString("hh:mm:ss");
                var parseHoraActual = DateTime.Parse(horaActual);
        string SQL = "INSERT INTO dbo.NumRecuperacion (id_persona, codigo, hora_envio) " +
                             "VALUES (@id_persona, @codigo, @hora_envio)";
                using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
                {
                    command.Parameters.AddWithValue("@id_persona", id_persona);
                    command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@hora_envio", parseHoraActual);
            command.ExecuteNonQuery();
                }
            conexion.Disconnect();
            comando = 1;
            return comando;
        }


        public Codigo SeleccionarCodigo(int id_persona)
        {
        ConexionBDUtility conexion = new ConexionBDUtility();
        Codigo codigo = null;
        try
                    {
            conexion.Connect();
            string SQL = "SELECT id_persona,codigo FROM dbo.NumRecuperacion WHERE (id_persona = @id_persona)";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@id_persona", id_persona);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        codigo = new Codigo
                        {
                            id_persona = Convert.ToInt32(reader["id_persona"]),
                            codigo = reader["codigo"].ToString()
                        };
                        conexion.Disconnect();
                        return codigo;
                    }
                    else
                    {
                        return codigo;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            codigo = new Codigo
            {
                mensaje = "Error al traer la informacion: " + ex.Message
            };
        }
        finally
        {
            conexion.Disconnect();
        }
        return codigo;
        }

    public void eliminarCodigo(int id_codigo)
    {
        Codigo codigo = null;
        ConexionBDUtility conexion = new ConexionBDUtility();
        conexion.Connect();
        try
        {
            SqlCommand comando = new SqlCommand("EliminarCodigo_SP", conexion.Conexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.Add("@id_codigo", System.Data.SqlDbType.Int).Value = id_codigo;
            comando.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            codigo = new Codigo
            {
                mensaje = "No se pudo eliminar el codigo: " + e.Message
            };
        }
        conexion.Disconnect();
    }


}   
          


    
