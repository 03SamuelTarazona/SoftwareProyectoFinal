using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Dto
{
    public class PacienteDto
    {
        public PersonaDto persona { get; set; }
        public string telefono { get; set; }
        public string seguro_social { get; set; }
    }
}