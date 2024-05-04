using Software_Proyecto.Dto;
using Software_Proyecto.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
           else if (personalogueo.id_rol == 3)
            {
                if (personalogueo.respuesta != 0)
                {
                    Session["UserLogged"] = personalogueo;
                    return View("ListaMedicos");
                }
            }
            return View();
        }
        public ActionResult ListaPacientes()
        {
            GerenteService gerenteService = new GerenteService();
            List<PacienteDto>paciente=gerenteService.Lista_Pacientes();
            ViewData["paciente"] = paciente;
            return View("ListaPacientes");
        }
        public ActionResult ListaMedicos()
        {
            GerenteService gerenteService = new GerenteService();
            List<MedicoDto> medico = gerenteService.Lista_Medicos();
            ViewData["medico"] = medico;
            return View("ListaMedicos");

            
        }
        public ActionResult AgregarMedico()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarMedico(MedicoDto medico)
        {
            GerenteService gerenteService=new GerenteService();
            MedicoDto medicoDto = new MedicoDto();
            medicoDto=gerenteService.registrarMedico(medico);
            
            
                return ListaMedicos();
          
        }
        [HttpPost]
        public ActionResult DescargarPdfMedicos()
        {
            try
            {
                GerenteService gerenteService = new GerenteService();

                string tempFilePath = Path.Combine(Path.GetTempPath(), "Lista_Medicos.pdf");
                gerenteService.CrearPdfMedicos();


                Response.Clear(); 
                Response.ContentType = "application/pdf"; 
                Response.AddHeader("Content-Disposition", "attachment; filename=Lista_Medicos.pdf"); 

             
                Response.WriteFile(tempFilePath);

                Response.Flush();

              
                return new EmptyResult(); 
            }
            catch (Exception ex)
            {
               
                ViewData["Mensaje"] = "Error al generar el PDF: " + ex.Message;
                return RedirectToAction("ListaMedicos");
            }
        }

        [HttpPost]
        public ActionResult DescargarPdfPacientes()
        {
            try
            {
                GerenteService gerenteService = new GerenteService();

                string tempFilePath = Path.Combine(Path.GetTempPath(), "Lista_Pacientes.pdf");
                gerenteService.CrearPdfPacientes();


                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Lista_Pacientes.pdf");


                Response.WriteFile(tempFilePath);

                Response.Flush();


                return new EmptyResult();
            }
            catch (Exception ex)
            {

                ViewData["Mensaje"] = "Error al generar el PDF: " + ex.Message;
                return RedirectToAction("ListaPacientes");
            }
        }

    }
}