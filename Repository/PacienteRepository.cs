﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;

public class PacienteRepository
{
    public int registroUsuario(PacienteDto paciente)
    {
        int comando = 0;
        try
        {
            ConexionBDUtility conexion = new ConexionBDUtility();
            conexion.Connect();

            string SQL = "INSERT INTO dbo.Persona (id_rol, nombres, apellidos,documento, correo, contrasena, genero,fecha_nacimiento, telefono,seguro_social)"
                        + "VALUES (@id_rol,@nombres, @apellidos,@documento, @correo, @contrasena, @genero,@fecha_nacimiento, @telefono,@seguro_social)";


            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@id_rol", paciente.persona.id_rol);
                command.Parameters.AddWithValue("@nombres", paciente.persona.nombres);
                command.Parameters.AddWithValue("@apellidos", paciente.persona.apellidos);
                command.Parameters.AddWithValue("@documento", paciente.persona.documento);
                command.Parameters.AddWithValue("@correo", paciente.persona.correo);
                command.Parameters.AddWithValue("@contrasena", paciente.persona.contrasena);
                command.Parameters.AddWithValue("@genero", paciente.persona.genero);
                command.Parameters.AddWithValue("@fecha_nacimiento", paciente.persona.fecha_nacimiento);
                command.Parameters.AddWithValue("@telefono", paciente.telefono);
                command.Parameters.AddWithValue("@seguro_social", paciente.seguro_social);
                command.ExecuteNonQuery();
            }
            conexion.Disconnect();
            comando = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return comando;
    }
    public bool buscarPaciente(string correo)
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
}
