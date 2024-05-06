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
        
        string SQL = "INSERT INTO dbo.NumRecuperacion (id_persona, codigo) " +
                             "VALUES (@id_persona, @codigo)";
        using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
        {
            command.Parameters.AddWithValue("@id_persona", id_persona);
            command.Parameters.AddWithValue("@codigo", codigo);
            command.ExecuteNonQuery();
        }
        conexion.Disconnect();
        comando = 1;
        return comando;
    }


    public CodigoDto SeleccionarCodigo(int id_persona)
    {
        ConexionBDUtility conexion = new ConexionBDUtility();
        CodigoDto codigo = null;
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
                        codigo = new CodigoDto
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
            codigo = new CodigoDto
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

    public int eliminarCodigo(int id_persona)
    {
        {
            ConexionBDUtility conexion = new ConexionBDUtility();
            int codigo = 1;
            try
            {
                conexion.Connect();
                string SQL = "DELETE  FROM dbo.NumRecuperacion WHERE (id_persona = @id_persona)";
                using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
                {
                    command.Parameters.AddWithValue("@id_persona", id_persona);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        return codigo;
                    }
                }

            }
            catch (Exception ex)
            {
                codigo = 0;


            }
            finally
            {
                conexion.Disconnect();
            }
            return codigo;
        }




    }
}   
          


    
