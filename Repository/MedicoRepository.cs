using Software_Proyecto.Dto;
using Software_Proyecto.Utilitys;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public class MedicoRepository
{
    public int actualizarAgenda(string estado, TimeSpan hora_fin)
    {
        int comando = 0;
        ConexionBDUtility conexion = new ConexionBDUtility();
        try
        {

            conexion.Connect();
            string SQL = "UPDATE dbo.Agenda SET  estado=@estado, hora_fin=@hora_fin " + "WHERE id_agenda = @id_agenda";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@hora_fin", hora_fin);
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
                          

                    };

                        medico.persona.id_persona = Convert.ToInt32(reader["id_persona"]);
                        medico.persona.nombres = reader["nombres"].ToString();
                        medico.persona.apellidos = reader["apellidos"].ToString();
                        medico.persona.correo = reader["correo"].ToString();
                        medico.persona.genero = reader["genero"].ToString();
                        medico.especialidad = reader["especialidad"].ToString();
                        


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
    public List<AgendaDto> MostrarCitas()
    {

        ConexionBDUtility conexion = new ConexionBDUtility();
        List<AgendaDto> agenda1 = new List<AgendaDto>();
        conexion.Connect();

        string SQL = "SELECT  id_agenda,id_persona, fecha, hora_inicio FROM dbo.Agenda WHERE estado='Reservado'";

        using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AgendaDto agenda = new AgendaDto();

                    {
                        agenda.id_agenda = Convert.ToInt32(reader["id_agenda"]);
                        agenda.id_persona = Convert.ToInt32(reader["id_persona"]);

                        agenda.fecha = Convert.ToDateTime(reader["fecha"]);
                        agenda.hora_inicio = TimeSpan.Parse(reader["hora_inicio"].ToString());
                    };

                    agenda1.Add(agenda);
                }
            }
        }
        conexion.Disconnect();
        return agenda1;
    }
    public int Detalles(AgendaDto agenda)
    {
        int com = 0;
       ConexionBDUtility conexion = new ConexionBDUtility();

        conexion.Connect();
            string SQL = "update dbo.Agenda set hora_fin = @hora_fin,  estado = @estado, descripcion = @descripcion WHERE id_agenda = @id_agenda;";
            using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
            {
                command.Parameters.AddWithValue("@hora_fin", agenda.hora_fin);
                command.Parameters.AddWithValue("@estado",agenda.estado);
                command.Parameters.AddWithValue("@descripcion",agenda.descripcion);
            command.Parameters.AddWithValue("@id_agenda", agenda.id_agenda);
            command.ExecuteNonQuery();
                com = 1;
            }
        conexion.Disconnect();

        return com; 
    }
    public List<AgendaDto> MostrarPersonasAtendidas()
    {

        ConexionBDUtility conexion = new ConexionBDUtility();
        List<AgendaDto> agenda1 = new List<AgendaDto>();
        conexion.Connect();

        string SQL = "SELECT  id_agenda,id_persona, fecha, hora_inicio,hora_fin,descripcion FROM dbo.Agenda WHERE estado='Revisado'";

        using (SqlCommand command = new SqlCommand(SQL, conexion.Conexion()))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AgendaDto agenda = new AgendaDto();

                    {
                        agenda.id_agenda = Convert.ToInt32(reader["id_agenda"]);
                        agenda.id_persona = Convert.ToInt32(reader["id_persona"]);

                        agenda.fecha = Convert.ToDateTime(reader["fecha"]);
                        agenda.hora_inicio = TimeSpan.Parse(reader["hora_inicio"].ToString());
                        agenda.hora_fin = TimeSpan.Parse(reader["hora_fin"].ToString());
                        agenda.descripcion = reader["descripcion"].ToString();
                    };

                    agenda1.Add(agenda);
                }
            }
        }
        conexion.Disconnect();
        return agenda1;
    }
}