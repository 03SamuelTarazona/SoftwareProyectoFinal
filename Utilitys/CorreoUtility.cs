using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Messaging;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using Software_Proyecto.Utilitys;
using Software_Proyecto.Dto;


public class CorreoUtility
    {
        private SmtpClient cliente;
        private MailMessage email;
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "2024centromedico@gmail.com";
        private string Password = "nbmpsuioienejrzc";
        private bool EnabledSSL = true;
        public CorreoUtility()
        {
            cliente = new SmtpClient(Host, Port)
            {
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }
        public void EnviarCorreo(string destinatario, string asunto, string mensajeCorreo, bool esHtlm = true)
        {
            email = new MailMessage(User, destinatario, asunto, mensajeCorreo);
            email.IsBodyHtml = esHtlm;
            cliente.Send(email);

        }

        public string enviarCorreoCodigo(string destinatario,string codigo)
        {

            CodigoUtility codigoUtility = new CodigoUtility();  
            PersonaDto persona = new PersonaDto();
           
            String mensajeCorreo = mensajeCon(codigo);
            EnviarCorreo(destinatario, "Cambiar Contraseña", mensajeCorreo, true);
            return codigo;

        }

    public string enviarCorreoCita(string destinatario,string nombre)
    {

        CodigoUtility codigoUtility = new CodigoUtility();
        PersonaDto persona = new PersonaDto();

        String mensajeCorreo = mensajeConN(nombre);
        EnviarCorreo(destinatario, "Cambiar Contraseña", mensajeCorreo, true);
        return nombre;

    }


    public string mensajeCon(string codigo)
        {
            string mensajeCon = "<html>" +
                "<head>" +
               
                "</head>" +
                "<body>" +

                "<div class='container'>" +
                "<div class='header'>" +
                "<h1>Cambio de Contraseña</h1>" +
                "</div>" +

                "<div class='content'>" +
                "<h2>Buen dia" + "</h2>" +
                "<p>Esperamos que te ecuentres bien.</p>" +
                "<p>Este correo responde a una solicitud para el cambio de tu contraseña.</p>" +
                "<p>Este es tu codigo para cambiar tu contraseña:"+"</p>" +
                "<p class=´codigo´>" + codigo + "</p>" +
                "<p>Este codigo es de un unico uso.</p>" +
                "</body>" +
                "</html>";

            return mensajeCon;
        }
    public string mensajeConN(string nombre)
    {
        string mensajeConN = "<html>" +
            "<head>" +

            "</head>" +
            "<body>" +

            "<div class='container'>" +
            "<div class='header'>" +
            "<h1>Reserva</h1>" +
            "</div>" +

            "<div class='content'>" +
            "<h2>Buen dia" + nombre+"</h2>" +
            "<p>Esperamos que te ecuentres bien.</p>" +
            "<p>Este correo responde a una reserva de cita.</p>" +
          
            "</body>" +
            "</html>";

        return mensajeConN;
    }
}

    