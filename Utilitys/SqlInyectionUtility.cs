using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software_Proyecto.Utilitys
{
    public class SqlInyectionUtility
    {
        public string Seguridad(string input)
        {
            if (input == null) return string.Empty;
            input = input.Replace("'", "#");
            return input;
        }

    }
}