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
        Encrypt encrypt = new Encrypt();
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
}

