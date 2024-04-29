
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
        PersonaDto persona = new PersonaDto();
        CorreoUtility correoUtility = new CorreoUtility();
        CodigoRepository recuperacion = new CodigoRepository();
        PersonaRepository personaRepository = new PersonaRepository();
        CodigoUtility generadorCodigo = new CodigoUtility();

        if (personaRepository.buscarPersona(correo))
        {
            persona = personaRepository.SeleccionarPersona(correo);
            String codigo = generadorCodigo.NumeroAleatorio().ToString();

            
            correoUtility.enviarCorreoCodigo(correo, codigo);

            
            int resultado = recuperacion.registroRecuperacion(persona.id_persona, codigo);

            if (resultado == 1)
            {
                Console.WriteLine("Código registrado con éxito."); 
            }
            else
            {
                Console.WriteLine("Error al registrar el código."); 
            }

            return persona;
        }
        else
        {
            Console.WriteLine("No se encontró la persona."); 
            return persona;
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
        }

        return m;
    }


}

