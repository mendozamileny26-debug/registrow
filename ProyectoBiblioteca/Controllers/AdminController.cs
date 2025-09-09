using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoBiblioteca.Controllers
{
    public class AdminController : Controller
    {
        private static Persona oPesona;
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            oPesona = (Persona)Session["Usuario"];

            // Si es tipo 3, solo debe ver Libros
            if (oPesona.oTipoPersona != null && oPesona.oTipoPersona.IdTipoPersona == 3)
            {
                return RedirectToAction("Libros", "Biblioteca");
            }

            return View(); // para admin u otros roles
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}