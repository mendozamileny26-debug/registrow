using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Models
{
    public class Libro
    {
        public int IdSolicitud { get; set; }
        public Tsolicitud oTsolicitud { get; set; }
        public string RutaPortada { get; set; }
        public string NombrePortada { get; set; }
        
        public Area oArea { get; set; }
               
        public Tipocuadrilla oTipocuadrilla { get; set; }
        public string Ubicacion { get; set; }
        public string DniL { get; set; }
        public string DniO { get; set; }
        public string NombreL { get; set; }
        public string NombreO { get; set; }
        public string CelularL { get; set; }
        public string CelularO { get; set; }
        public string CorreoL { get; set; }
        public string CorreoO { get; set; }
        public string ModeloV { get; set; }

        public string PlacaV { get; set; }
        public bool SCTR { get; set; }

        public string MensajeValidacion { get; set; }
        public string Estado { get; set; }

        public string base64 { get; set; }
        public string extension { get; set; }

        public string UsuarioRegistro { get; set; }

        public Zona oZona { get; set; }

        public bool TD1 { get; set; }

        public bool TD2 { get; set; }

        public string NombreCuadrillaPhoenix { get; set; }

        // 🔹 Nuevas propiedades para las evidencias
        public string RutaDocApoyo { get; set; }
        public string RutaVehiculo { get; set; }
        public string RutaSoat { get; set; }
        public string RutaLicencia { get; set; }
        public string RutaPoliza { get; set; }

        public int IdUsuario { get; set; }
    }
}