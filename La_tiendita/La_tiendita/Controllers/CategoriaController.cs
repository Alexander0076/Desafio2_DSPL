using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using La_tiendita.Models;

namespace La_tiendita.Controllers
{
    public class CategoriaController : Controller
    {
        
        
        Conexion conexion = new Conexion();
        public ActionResult Index()
        {
            ViewBag.categoria = conexion.listaCategoria();

            return View();
        }


        public ActionResult ModificarCategoria(int id)
        {

            List<Producto> lista = conexion.listaProducto();
            List<SelectListItem> listaO = new List<SelectListItem>();
            Categoria categoria = new Categoria();
            categoria = conexion.cargarCategoria(id);
            Producto producto = new Producto();

            foreach (Producto item in lista)
            {
                if (item.categoria.idCategoria == categoria.idCategoria)
                {
                    var producto1 = item.Id;
                    listaO.Add(new SelectListItem { Text = categoria.categoria, Value = item.categoria.idCategoria.ToString(), Selected = true });


                }
                else
                {
                    var producto1 = item.Id;
                    listaO.Add(new SelectListItem { Text = categoria.categoria, Value = item.categoria.idCategoria.ToString() });


                }
            }

            ViewBag.listaCategoria = listaO;
            return View(conexion.cargarCategoria(id));
        }

        public ActionResult EliminarCategoria(int id)
        {

            conexion.eliminarCategoria(id);
            ViewBag.categoria = conexion.listaCategoria();
            ViewBag.mensaje = "Categoria eliminada exitosamente";
            return View("Index");
        }

        public ActionResult InsertarCategoria()
        {
            return View();
        }

        [ActionName("AgregarCategoria")]
        public ActionResult insert(String categoria)
        {
            Categoria categorias = new Categoria();
            categorias.categoria = categoria;

            conexion.insertarCategoria(categorias);
            ViewBag.categoria = conexion.listaCategoria();
            ViewBag.mensaje = "Categoria agregada exitosamente";
            return View("Index");
        }


        [ActionName("ActualizarCategoria")]
        public ActionResult update(int idCategoria, String categoria)
        {

            Categoria categorias = new Categoria();
            categorias.idCategoria = idCategoria;
            categorias.categoria = categoria;

            conexion.actualizarCategoria(categorias);
            ViewBag.categoria = conexion.listaCategoria();
            ViewBag.mensaje = "Categoria modificada exitosamente";
            return View("Index");
        }




    }
}
