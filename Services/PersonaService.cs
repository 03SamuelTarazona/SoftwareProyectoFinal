
using Software_Proyecto.Utilitys;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PersonaService
{

    public PersonaDto iniciarSesion(PersonaDto persona, String contrasena)
    {
        Encrypt encrypt = new Encrypt();
        PersonaRepository personaRepository = new PersonaRepository();
        PersonaDto personaResp = new PersonaDto();

        string contrasenaEncript = encrypt.encriptarSHA512(contrasena);
        personaResp = personaRepository.IniciarSesion(persona.correo, contrasenaEncript);

        return personaResp;

    }
    public PersonaDto EnviarCodigo(String correo)
    {
        PersonaDto persona1 = new PersonaDto();
        CorreoUtility correoUtility = new CorreoUtility();
        CodigoRepository recuperacion = new CodigoRepository();
        PersonaRepository personaRepository = new PersonaRepository();
        CodigoUtility generadorCodigo = new CodigoUtility();
        CodigoRepository codigoRepository = new CodigoRepository();

        if (personaRepository.buscarPersona(correo))
        {
            persona1 = personaRepository.SeleccionarPersona(correo);

            string codigo = generadorCodigo.NumeroAleatorio().ToString();

            codigoRepository.eliminarCodigo(persona1.id_persona);
            correoUtility.enviarCorreoCodigo(correo, codigo);
            

            
            int resultado = recuperacion.registroRecuperacion(persona1.id_persona, codigo);

            if (resultado == 1)
            {
                Console.WriteLine("Código registrado con éxito."); 
            }
            else
            {
                Console.WriteLine("Error al registrar el código."); 
            }

            return persona1;
        }
        else
        {
            Console.WriteLine("No se encontró la persona."); 
            return persona1;
        }
    }

    public int actualizarContrasena(string correo, string codigo, string contrasena) 
    {
        int m = 0;
        PersonaRepository personaRepository = new PersonaRepository();
        PersonaDto personaDto = new PersonaDto();   
        Codigo codigo1 = new Codigo();
        Encrypt encrypt = new Encrypt();    
        CodigoRepository codigoRepository = new CodigoRepository();

        personaDto = personaRepository.SeleccionarPersona(correo);
       codigo1= codigoRepository.SeleccionarCodigo(personaDto.id_persona);
        if (codigo == codigo1.codigo)
        {
            string contrasenaencryp=encrypt.encriptarSHA512(contrasena);
            personaRepository.ActualizarContrasena(correo, contrasenaencryp);
            codigoRepository.eliminarCodigo(personaDto.id_persona);
        }

        return m;
    }


}

