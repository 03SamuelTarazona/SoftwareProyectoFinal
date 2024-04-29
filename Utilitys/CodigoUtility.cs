using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Utilitys
{
    public class CodigoUtility
    {
        public int NumeroAleatorio()
        {
            Random r = new Random();
            return r.Next(0, 999999 + 1);
        }
    }
}