using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using La_tiendita.Models;

namespace La_tiendita.Controllers
{
    public class HomeController : Controller
    {
        Conexion conexion = new Conexion();
        public ActionResult Index()
        {
            ViewBag.categoria = conexion.listaCategoria();

            return View();
        }    
    }
}