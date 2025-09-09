using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class Conexion
    {
        public static string CN = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
    }
}