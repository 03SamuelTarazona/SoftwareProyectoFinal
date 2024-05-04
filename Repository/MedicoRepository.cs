
using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public class MedicoRepository
{
    public int registroMedico(MedicoDto medico)
    {
        int num = 0;
        try
        {
            ConexionBDUtility conexion = new ConexionBDUtility();
            conexion.Connect();

            string SQL = "INSERT INTO dbo.Persona (id_rol, nombres, apellidos,documento, correo, contrasena, genero,fecha_nacimiento, especialidad)"
                        + "VALUES (@id_rol,@nombres, @apellidos,@documento, @correo, @contrasena, @genero,@fecha_nacimiento, @especialidad)";


            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@id_rol", medico.persona.id_rol);
                command.Parameters.AddWithValue("@nombres", medico.persona.nombres);
                command.Parameters.AddWithValue("@apellidos", medico.persona.apellidos);
                command.Parameters.AddWithValue("@documento", medico.persona.documento);
                command.Parameters.AddWithValue("@correo", medico.persona.correo);
                command.Parameters.AddWithValue("@contrasena", medico.persona.contrasena);
                command.Parameters.AddWithValue("@genero", medico.persona.genero);
                command.Parameters.AddWithValue("@fecha_nacimiento", medico.persona.fecha_nacimiento);
                command.Parameters.AddWithValue("@especialidad", medico.especialidad);

                command.ExecuteNonQuery();
            }
            conexion.Disconnect();
            num = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return num;
    }
    public List<MedicoDto> MostrarMedicos()
    {

        ConexionBDUtility conexion = new ConexionBDUtility();
        List<MedicoDto> medicos = new List<MedicoDto>();
        conexion.Connect();

        string SQL = "SELECT  id_persona,nombres, apellidos, correo, genero,especialidad FROM dbo.Persona WHERE id_rol=2";

        using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    {

                        MedicoDto medico = new MedicoDto
                        {
                            persona = new PersonaDto(),
                            especialidad = Convert.ToInt32(reader["especialidad"]) 
                        };

                        medico.persona.id_persona = Convert.ToInt32(reader["id_persona"]);
                        medico.persona.nombres = reader["nombres"].ToString();
                        medico.persona.apellidos = reader["apellidos"].ToString();
                        medico.persona.correo = reader["correo"].ToString();
                        medico.persona.genero = reader["genero"].ToString();

                        medicos.Add(medico);
                    }

                }
            }
        }
        conexion.Disconnect();
        return medicos;
    }
    public int EliminarMedico(int id)
    {
        int eli = 0;
        ConexionBDUtility conexion = new ConexionBDUtility();

        conexion.Connect();
            string SQL = "DELETE FROM dbo.Persona WHERE id_persona = @id_persona";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))

            {
                command.Parameters.AddWithValue("@id_persona", id);
                command.ExecuteNonQuery();
            }
            eli = 1;

            conexion.Disconnect();
        

        return eli;
    }
}