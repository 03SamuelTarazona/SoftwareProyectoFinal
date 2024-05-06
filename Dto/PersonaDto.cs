using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Dto
{
    public class PersonaDto
    {
        public int id_persona { get; set; }
        public int id_rol { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documento { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string genero { get; set; }
        public string fecha_nacimiento { get; set; }
        public int estado { get; set; }
        public int respuesta { get; set; }
        public string mensaje { get; set; }
    }
}