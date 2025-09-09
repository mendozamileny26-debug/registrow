using ProyectoBiblioteca.Logica;
using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoBiblioteca.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            Persona ousuario = PersonaLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            // Guardar en sesión lo necesario
            Session["Usuario"] = ousuario;
            Session["IdUsuario"] = ousuario.IdPersona;  // 🔹 ID único del usuario logueado
            Session["IdTipoPersona"] = ousuario.oTipoPersona.IdTipoPersona; // 🔹 Tipo de persona (1=Admin, 2=Usuario)

            return RedirectToAction("Index", "Admin");
        }
    }
}