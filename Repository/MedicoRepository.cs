
using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;
using System.Data.SqlClient;
using System;

public class MedicoRepository
{
    public int registroUsuario(MedicoDto medico)
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
                command.Parameters.AddWithValue("@id_rol", medico.persona.id_rol);
                command.Parameters.AddWithValue("@nombres", medico.persona.nombres);
                command.Parameters.AddWithValue("@apellidos", medico.persona.apellidos);
                command.Parameters.AddWithValue("@documento", medico.persona.documento);
                command.Parameters.AddWithValue("@correo", medico.persona.correo);
                command.Parameters.AddWithValue("@contrasena", medico.persona.contrasena);
                command.Parameters.AddWithValue("@genero", medico.persona.genero);
                command.Parameters.AddWithValue("@fecha_nacimiento", medico.persona.fecha_nacimiento);
                command.Parameters.AddWithValue("@telefono", medico.especialidad);
              //  command.Parameters.AddWithValue("@seguro_social", paciente.seguro_social);
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
}