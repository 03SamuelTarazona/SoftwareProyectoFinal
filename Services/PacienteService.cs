using Software_Proyecto.Dto;
using Software_Proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Software_Proyecto.Utilitys;



public class PacienteService
{
    public PacienteDto registroUsuario(PacienteDto paciente)
    {
        SqlInyectionUtility reemplazar = new SqlInyectionUtility();
        
        EncryptUtility encrypt = new EncryptUtility();
        PacienteDto pacienteResp = new PacienteDto();
        pacienteResp.persona = new PersonaDto();

        PacienteRepository pacienteRepository = new PacienteRepository();
        paciente.persona.nombres = reemplazar.Seguridad(paciente.persona.nombres);
        paciente.persona.apellidos = reemplazar.Seguridad(paciente.persona.apellidos);
        paciente.persona.documento = reemplazar.Seguridad(paciente.persona.documento);
        paciente.persona.correo = reemplazar.Seguridad(paciente.persona.correo);
        paciente.telefono= reemplazar.Seguridad(paciente.telefono);
        paciente.seguro_social= reemplazar.Seguridad(paciente.seguro_social);

        if (pacienteRepository.buscarPaciente(paciente.persona.correo))
        {
            pacienteResp.persona.respuesta = 0;
            pacienteResp.persona.mensaje = "Ya existe el usuario";
        }
        else
        {


            paciente.persona.id_rol = 1;
            paciente.persona.contrasena=encrypt.encriptarSHA512(paciente.persona.contrasena);
           
            int resultadoRegistro = pacienteRepository.registroUsuario(paciente);

            if (resultadoRegistro != 0)
            {
                pacienteResp.persona.respuesta = 1;
                pacienteResp.persona.mensaje = "Se ha registrado el usuario correctamente";
            }
            else
            {
                pacienteResp.persona.respuesta = 0;
                pacienteResp.persona.mensaje = "Error en el registro del usuario";
            }
        }

        return pacienteResp;
    }
    public AgendaDto ReservarCita(string correo, AgendaDto agenda)
    {
        PacienteRepository pacienteRepository = new PacienteRepository();
        PersonaRepository personaRepository = new PersonaRepository();
        PersonaDto persona = new PersonaDto();
        persona = personaRepository.SeleccionarPersona(correo);
        CorreoUtility correoUtility = new CorreoUtility();
        string nombre = persona.nombres;
        int id = persona.id_persona;
        pacienteRepository.registroCita(id, agenda);
        correoUtility.enviarCorreoCita(correo, nombre);
        return agenda;
    }
    public PacienteDto mapeo(PersonaDto pers)
    {
        PacienteDto paciente = new PacienteDto();
        paciente.persona = new PersonaDto();
        paciente.persona.id_persona = pers.id_persona;
        paciente.persona.id_rol = pers.id_rol;
        paciente.persona.nombres = pers.nombres;
        paciente.persona.apellidos = pers.apellidos;
        paciente.persona.fecha_nacimiento = pers.fecha_nacimiento;
        paciente.persona.correo = pers.correo;
        paciente.persona.contrasena = pers.contrasena;
        paciente.persona.genero = pers.genero;
        paciente.persona.respuesta = pers.respuesta;
        paciente.persona.mensaje = pers.mensaje;

        return paciente;
    }
    public List<AgendaDto> Historial(int id_persona)
    {
        PacienteRepository pacienteRepository = new PacienteRepository();
        var Lista = pacienteRepository.MostrarCitas(id_persona);
        return Lista;
    }
    public int Eliminar(int id_persona)
    {
        int c = 0;
        PacienteRepository pacienteRepository = new PacienteRepository();
        pacienteRepository.EliminarPaciente(id_persona);
        return c;
    }
}
