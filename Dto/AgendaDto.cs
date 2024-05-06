using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Software_Proyecto.Dto
{
    public class AgendaDto
    {
        public int id_agenda { get; set; }
        public int id_persona { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fin { get; set; }
        public string estado { get; set; }
        public string descripcion { get; set; }

    }
}