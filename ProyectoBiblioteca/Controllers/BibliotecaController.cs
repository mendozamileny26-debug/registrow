using Newtonsoft.Json;
using ProyectoBiblioteca.Logica;
using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoBiblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        // GET: Biblioteca
        public ActionResult Libros()
        {
            var objeto = (ProyectoBiblioteca.Models.Persona)Session["Usuario"];

            // Pasamos el tipo de persona a la vista
            ViewBag.TipoUsuario = objeto.oTipoPersona.IdTipoPersona;
            return View();
        }

        public ActionResult Area()
        {
            return View();
        }

        public ActionResult Partner()
        {
            return View();
        }

        public ActionResult Tipocuadrilla()
        {
            return View();
        }

        public ActionResult Tiposolicitud()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipocuadrilla()
        {
            List<Tipocuadrilla> oLista = new List<Tipocuadrilla>();
            oLista = TipocuadrillaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarTipocuadrilla(Tipocuadrilla objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdTipocuadrilla == 0) ? TipocuadrillaLogica.Instancia.Registrar(objeto) : TipocuadrillaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarTipocuadrilla(int id)
        {
            bool respuesta = false;
            respuesta = TipocuadrillaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        //CODIGO ZONA
       
        [HttpGet]
        public JsonResult ListarZona()
        {
            List<Zona> oLista = new List<Zona>();
            oLista = ZonaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarZona(Zona objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdZona == 0) ? ZonaLogica.Instancia.Registrar(objeto) : ZonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarZona(int id)
        {
            bool respuesta = false;
            respuesta = ZonaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        //TERMINO DE CODIGO

        //codigo tiposolicitud

        [HttpGet]
        public JsonResult ListarTiposolicitud()
        {
            List<Tsolicitud> oLista = new List<Tsolicitud>();
            oLista = TsolicitudLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarTiposolicitud(Tsolicitud objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdTiposolicitud == 0) ? TsolicitudLogica.Instancia.Registrar(objeto) : TsolicitudLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarTiposolicitud(int id)
        {
            bool respuesta = false;
            respuesta = TsolicitudLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        //termino el codigo

        [HttpGet]
        public JsonResult ListarPartner()
        {
            List<Partner> oLista = new List<Partner>();
            oLista = PartnerLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPartner(Partner objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdPartner == 0) ? PartnerLogica.Instancia.Registrar(objeto) : PartnerLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPartner(int id)
        {
            bool respuesta = false;
            respuesta = PartnerLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarSolicitud()
        {
            List<Tsolicitud> oLista = new List<Tsolicitud>();
            oLista = TsolicitudLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarSolicitud(Tsolicitud objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdTiposolicitud == 0) ? TsolicitudLogica.Instancia.Registrar(objeto) : TsolicitudLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarSolicitud(int id)
        {
            bool respuesta = false;
            respuesta = TsolicitudLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarArea()
        {
            List<Area> oLista = new List<Area>();
            oLista = AreaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarArea(Area objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdArea == 0) ? AreaLogica.Instancia.Registrar(objeto) : AreaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarArea(int id)
        {
            bool respuesta = false;
            respuesta = AreaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult ListarLibro()
        {
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"]);       // quién está logueado
            int IdTipoPersona = Convert.ToInt32(Session["IdTipoPersona"]); // tipo 1=admin, 2=usuario

            List<Libro> oLista = LibroLogica.Instancia.Listar(IdUsuario, IdTipoPersona);

            if (oLista == null || oLista.Count == 0)
            {
                return Json(new { data = new List<Libro>
                {
                    new Libro { oTsolicitud = new Tsolicitud { Descripcion = "SIN DATOS" } }
                }}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarLibro(string objeto, HttpPostedFileBase filelibro,
        HttpPostedFileBase filedapoyo,
        HttpPostedFileBase filevehiculo,
        HttpPostedFileBase filesoat,
        HttpPostedFileBase filelconducir,
        HttpPostedFileBase filenpoliza)
        {

            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Libro oSolicitud = new Libro();
                oSolicitud = JsonConvert.DeserializeObject<Libro>(objeto);

                // 🔸 Aquí obtenemos el usuario logueado y lo asignamos
                Persona personaLogueada = (Persona)Session["usuario"];
                oSolicitud.IdUsuario = personaLogueada.IdPersona;      // <-- Aquí guardamos quién registró
                oSolicitud.UsuarioRegistro = personaLogueada.Nombre + " " + personaLogueada.Apellido;

                string rutaBase = ConfigurationManager.AppSettings["ruta_imagenes_libros"];

                if (!Directory.Exists(rutaBase))
                    Directory.CreateDirectory(rutaBase);


                int indicador = LibroLogica.Instancia.Validacion(oSolicitud);
                string nuevoestado = "";
                if (indicador == 1)
                {
                     nuevoestado = "Aprobado";

                }
                else
                {
                    nuevoestado = "No aprobado";
                }

                if (oSolicitud.IdSolicitud == 0)
                {
                    int id = LibroLogica.Instancia.Registrar(oSolicitud,nuevoestado);
                    oresponse.resultado = id > 0;
                    oSolicitud.IdSolicitud = id;
                    if (id == 0)
                    {
                        oresponse.resultado = false;
                        oresponse.mensaje = "Error al registrar la solicitud (ID = 0)";
                    }
                    else
                    {
                        oresponse.resultado = true;
                        oresponse.mensaje = "Registro exitoso con ID = " + id;
                    }
                }

                // 🔹 Guardar archivos si existen
                if (oSolicitud.IdSolicitud > 0)
                {
                    if (filelibro != null) oSolicitud.RutaPortada = GuardarArchivo(filelibro, rutaBase, oSolicitud.IdSolicitud, "DNI_LIDER");
                    if (filedapoyo != null) oSolicitud.RutaDocApoyo = GuardarArchivo(filedapoyo, rutaBase, oSolicitud.IdSolicitud, "DNI_APOYO");
                    if (filevehiculo != null) oSolicitud.RutaVehiculo = GuardarArchivo(filevehiculo, rutaBase, oSolicitud.IdSolicitud, "VEHICULO");
                    if (filesoat != null) oSolicitud.RutaSoat = GuardarArchivo(filesoat, rutaBase, oSolicitud.IdSolicitud, "SOAT");
                    if (filelconducir != null) oSolicitud.RutaLicencia = GuardarArchivo(filelconducir, rutaBase, oSolicitud.IdSolicitud, "LICENCIA");
                    if (filenpoliza != null) oSolicitud.RutaPoliza = GuardarArchivo(filenpoliza, rutaBase, oSolicitud.IdSolicitud, "POLIZA");

                    // 🔹 Actualizamos en BD los paths guardados
                    LibroLogica.Instancia.ActualizarRutasEvidencias(oSolicitud);
                }

            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }

        //nuevo codigo
        private string GuardarArchivo(HttpPostedFileBase archivo, string rutaBase, int idSolicitud, string tipo)
        {
            string extension = Path.GetExtension(archivo.FileName);
            string nombreArchivo = $"{idSolicitud}_{tipo}{extension}";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

            archivo.SaveAs(rutaCompleta);

            // 🔹 Guardamos la URL relativa para BD (ejemplo: /Uploads/123_SOAT.pdf)
            return "/Uploads/" + nombreArchivo;
        }

        //NUEVO CODIGO INGRESADO 
        [HttpPost]
        public JsonResult GuardarNombreCuadrilla(int IdSolicitud, string nombreCuadrilla)
        {
            bool respuesta = false;
            string mensaje = "";

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand(
               "UPDATE SOLICITUD SET NombreCuadrillaPhoenix = @nombre, Estado = @estado WHERE IdSolicitud = @id",
               oConexion
           );

                    cmd.Parameters.AddWithValue("@nombre", nombreCuadrilla);
                    cmd.Parameters.AddWithValue("@estado", "Creado"); // 🔹 aquí fijamos el nuevo estado
                    cmd.Parameters.AddWithValue("@id", IdSolicitud);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return Json(new { success = respuesta, message = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarLibro(int id)
        {
            bool respuesta = false;
            respuesta = LibroLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarTipoPersona()
        {
            List<TipoPersona> oLista = new List<TipoPersona>();
            oLista = TipoPersonaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarPersona()
        {
            List<Persona> oLista = new List<Persona>();

            oLista = PersonaLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPersona(Persona objeto)
        {
            bool respuesta = false;
            objeto.Clave = objeto.Clave == null ? "" : objeto.Clave;
            respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPersona(int id)
        {
            bool respuesta = false;
            respuesta = PersonaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        


    }
    public class Response
    {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
}