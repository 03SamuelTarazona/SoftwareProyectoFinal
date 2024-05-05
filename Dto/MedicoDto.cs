using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Dto
{
    public class MedicoDto
    {
        public PersonaDto persona { get; set; }
        public string especialidad { get; set; }
    }
}