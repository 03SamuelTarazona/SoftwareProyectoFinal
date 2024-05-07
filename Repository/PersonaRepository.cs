using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;

public class PersonaRepository
{
    public bool buscarPersona(string correo)
    {
        SqlInyectionUtility reemplazar = new SqlInyectionUtility(); 
        correo = reemplazar.Seguridad(correo);
        ConexionBDUtility conexion = new ConexionBDUtility();
        conexion.Connect();
        string SQL = "SELECT COUNT(*) FROM dbo.Persona WHERE correo = @correo";
        int personaEncontrado = 0;
        using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
        {
            command.Parameters.AddWithValue("@correo", correo);

            personaEncontrado = (int)command.ExecuteScalar();
        }
        conexion.Disconnect();
        if (personaEncontrado > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public PersonaDto IniciarSesion(string correo, string contrasenaIngresada)
    {
        SqlInyectionUtility reemplazar = new SqlInyectionUtility();
        ConexionBDUtility conexion = new ConexionBDUtility();
        PersonaDto persona = null;
        PersonaDto personaResp = new PersonaDto();
        correo = reemplazar.Seguridad(correo);
        contrasenaIngresada = reemplazar.Seguridad(contrasenaIngresada);
        try
        {
            conexion.Connect();
            string SQL = "SELECT id_persona, id_rol, nombres, apellidos, documento, correo, contrasena, genero, fecha_nacimiento FROM dbo.Persona WHERE correo = @correo";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@correo", correo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                       
                        string contrasenaAlmacenada = reader["contrasena"].ToString();

                        
                        if (contrasenaIngresada == contrasenaAlmacenada)
                        {
                            
                            persona = new PersonaDto
                            {
                                id_persona = Convert.ToInt32(reader["id_persona"]),
                                id_rol = Convert.ToInt32(reader["id_rol"]),
                                nombres = reader["nombres"].ToString(),
                                apellidos = reader["apellidos"].ToString(),
                                documento = reader["documento"].ToString(),
                                correo = reader["correo"].ToString(),
                                genero = reader["genero"].ToString(),
                                fecha_nacimiento = reader["fecha_nacimiento"].ToString(),
                                
                            };
                            persona.respuesta = 1;
                            persona.mensaje = "Inicio correcto";
                        }
                        else
                        {
                            
                            personaResp.respuesta = 0;
                            personaResp.mensaje = "Contraseña incorrecta";
                            return personaResp;
                        }
                    }
                    else
                    {
                       
                        personaResp.respuesta = 0;
                        personaResp.mensaje = "Usuario no encontrado";
                        return personaResp;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            personaResp = new PersonaDto
            {
                respuesta = -1,
                mensaje = "Error al iniciar sesión: " + ex.Message
            };
        }
        finally
        {
            conexion.Disconnect();
        }

        return persona;
    }
    public int ActualizarContrasena(string correo, string contrasena)
    {
        SqlInyectionUtility reemplazar = new SqlInyectionUtility();
        int comando = 0;
        ConexionBDUtility conexion = new ConexionBDUtility();
        correo = reemplazar.Seguridad(correo);
        contrasena = reemplazar.Seguridad(contrasena);
        try
        {

            conexion.Connect();
            string SQL = "UPDATE dbo.Persona SET  contrasena=@contrasena " + "WHERE correo = @correo";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@contrasena", contrasena);

                command.ExecuteNonQuery();
            }
            comando = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            conexion.Disconnect();
        }
        return comando;
    }

    public PersonaDto SeleccionarPersona(string correo)
    {
        ConexionBDUtility conexion = new ConexionBDUtility();
        PersonaDto persona = null;
        PersonaDto personaResp = new PersonaDto();
        EncryptUtility encr = new EncryptUtility();

        try
        {
            conexion.Connect();
            string SQL = "SELECT id_persona,correo FROM dbo.Persona WHERE (correo = @correo)";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@correo", correo);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        persona = new PersonaDto
                        {
                            id_persona = Convert.ToInt32(reader["id_persona"]),
                            correo = reader["correo"].ToString()
                        };
                        conexion.Disconnect();
                        persona.respuesta = 1;
                        persona.mensaje = "Inicio correcto";
                        return persona;

                    }
                    else
                    {
                        personaResp.respuesta = 0;
                        personaResp.mensaje = "Inicio Incorrecto";
                        return personaResp;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            persona = new PersonaDto
            {
                respuesta = -1,
                mensaje = "Error al inicio sesión: " + ex.Message
            };
        }
        finally
        {
            conexion.Disconnect();
        }
        return persona;
    }
  

}
