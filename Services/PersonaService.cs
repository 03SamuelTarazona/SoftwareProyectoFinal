using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Services
{
    public class PersonaService
    {

        public PersonaDto iniciarSesion(PersonaDto persona,String contrasena)
        {
            Encrypt encrypt = new Encrypt(); 
            PersonaRepository personaRepository = new PersonaRepository();
            PersonaDto personaResp = new PersonaDto();

            string contrasenaEncript = encrypt.encriptarSHA512(contrasena);
            personaResp = personaRepository.IniciarSesion(persona.correo, contrasenaEncript);

            return personaResp;
            
        }

    }
}