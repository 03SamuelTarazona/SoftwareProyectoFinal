﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Software_Proyecto.Utilitys;

public class PersonaRepository
{
    public bool buscarPersona(string correo)
    {
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
        ConexionBDUtility conexion = new ConexionBDUtility();
        PersonaDto persona = null;
        PersonaDto personaResp = new PersonaDto();

        try
        {
            conexion.Connect();
            string SQL = "SELECT id_persona, id_rol, nombres, apellidos, documento, correo, contrasena, genero, fecha_nacimiento, estado FROM dbo.Persona WHERE correo = @correo";
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
                                estado = !reader.IsDBNull(reader.GetOrdinal("estado")) ? Convert.ToInt32(reader["estado"]) : 1
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

}
