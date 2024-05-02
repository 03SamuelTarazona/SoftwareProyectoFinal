using Software_Proyecto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Software_Proyecto.Controllers { 
    public class HomeController : Controller
    {
        public ActionResult RegistroPaciente()
        {
            return View();
        }
      public ActionResult BuscarCorreo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BuscarCorreo(string correo)
        {
            PersonaService personaService = new PersonaService();
            personaService.EnviarCodigo(correo);
            ViewData["Correo"] = correo;
            return View("CambiarContrasena");
        }
        public ActionResult CambiarContrasena()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CambiarContrasena(string correo, string codigo, string contrasena)
       {
            PersonaService personaService = new PersonaService();
            if (personaService.actualizarContrasena(correo,codigo,contrasena) != 0) { 
            
                return View("IniciarSesion");
            }
            else
            {
                return View("Index");
            }

        }


        [HttpPost]
        public ActionResult RegistroPaciente(PacienteDto paciente) {

         
            PacienteService pacienteService = new PacienteService();
            PacienteDto resultado = pacienteService.registroUsuario(paciente);


            if (resultado.persona.respuesta != 0)
            {
                return View("Index", resultado);
            }
            else
            {
                return View(resultado);
            }
        }
  public ActionResult IniciarSesion()
        {
            return View();
        }
        public ActionResult VistaPaciente()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarSesion(PersonaDto persona,string contrasena)
        {
            PersonaService personaService = new PersonaService();
            
            PersonaDto personalogueo = personaService.iniciarSesion(persona,contrasena);

            if (personalogueo.id_rol == 1)
            {
                if (personalogueo.respuesta != 0)
                {
                    Session["UserLogged"] = personalogueo;
                    return View("VistaPaciente");
                }
            }  

            return View();
        }
    } }