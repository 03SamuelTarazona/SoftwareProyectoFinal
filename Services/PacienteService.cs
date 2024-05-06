using Software_Proyecto.Dto;
using Software_Proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



public class PacienteService
{
    public PacienteDto registroUsuario(PacienteDto paciente)
    {
        EncryptUtility encrypt = new EncryptUtility();
        PacienteDto pacienteResp = new PacienteDto();
        pacienteResp.persona = new PersonaDto();

        PacienteRepository pacienteRepository = new PacienteRepository();



        if (pacienteRepository.buscarPaciente(paciente.persona.correo))
        {
            pacienteResp.persona.respuesta = 0;
            pacienteResp.persona.mensaje = "Ya existe el usuario";
        }
        else
        {

            paciente.persona.id_rol = 1;
            paciente.persona.estado = 1;
            paciente.persona.contrasena = encrypt.encriptarSHA512(paciente.persona.contrasena);
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

        int id = persona.id_persona;
        pacienteRepository.registroCita(id,agenda);
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
}
