using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final_5to_Semestre_Web
{
    public static class Sesion
    {
        public static String U
        {
            set
            {
                HttpContext.Current.Session["U"] = value;
            }
            get
            {
                return (String)HttpContext.Current.Session["U"];
            }
        }
        public static String NombrePagina
        {
            set
            {
                HttpContext.Current.Session["N"] = value;
            }
            get
            {
                return (String)HttpContext.Current.Session["N"];
            }
        }
    }
}