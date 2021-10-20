using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using La_tiendita.Models;

namespace La_tiendita.Controllers
{
    public class ProductoController : Controller
    {
        Conexion conexion = new Conexion();
        public ActionResult Index()
        {
            ViewBag.producto = conexion.listaProducto();

            return View();
        }
       
        public ActionResult Modificar(int id)
        {

            List<Categoria> lista = conexion.listaCategoria();
            List<SelectListItem> listaO = new List<SelectListItem>();
            Producto producto = new Producto();
            producto = conexion.cargarProducto(id);
            foreach (Categoria item in lista)
            {
                if (item.idCategoria == producto.categoria.idCategoria)
                {
                    listaO.Add(new SelectListItem { Text = item.categoria, Value = item.idCategoria.ToString(), Selected = true });

                }
                else
                {
                    listaO.Add(new SelectListItem { Text = item.categoria, Value = item.idCategoria.ToString() });

                }

            }
            ViewBag.listaCategoria = listaO;
            return View(conexion.cargarProducto(id));
        }

        public ActionResult Eliminar(int id)
        {

            conexion.eliminarProducto(id);
            ViewBag.producto = conexion.listaProducto();
            ViewBag.mensaje = "Producto eliminado exitosamente";
            return View("Index");
        }

        public ActionResult Insertar()
        {

            List<Categoria> lista = conexion.listaCategoria();
            List<SelectListItem> listaO = new List<SelectListItem>();
            foreach (Categoria item in lista)
            {
                listaO.Add(new SelectListItem { Text = item.categoria, Value = item.idCategoria.ToString() });
            }
            ViewBag.listaCategoria = listaO;
            return View();
        }
        [ActionName("Agregar")]
        public ActionResult insert(int listaCategoria, String NProducto, String Descripcion, String FechaV, int Existencia, Decimal PrecioCompra, double PrecioVenta)
        {
            Producto producto = new Producto();
            producto.NProducto = NProducto;
            producto.Descripcion = Descripcion;
            producto.FechaV = FechaV;
            producto.Existencia = Existencia;
            producto.PrecioCompra = PrecioCompra;
            producto.PrecioVenta = PrecioVenta;
            producto.idCategoria = listaCategoria;

            conexion.insertarProducto(producto);
            ViewBag.producto = conexion.listaProducto();
            ViewBag.mensaje = "Producto agregado exitosamente";
            return View("Index");
        }


        [ActionName("Actualizar")]
        public ActionResult update(int Id, String NProducto, String Descripcion, String FechaV, int Existencia, Decimal PrecioCompra, double PrecioVenta, int listaCategoria)
        {

            Producto producto = new Producto();
            producto.Id = Id;
            producto.NProducto = NProducto;
            producto.Descripcion = Descripcion;
            producto.FechaV = FechaV;
            producto.Existencia = Existencia;
            producto.PrecioCompra = PrecioCompra;
            producto.PrecioVenta = PrecioVenta;
            producto.idCategoria = listaCategoria;

            conexion.actualizarProducto(producto);
            ViewBag.producto = conexion.listaProducto();
            ViewBag.mensaje = "Producto modificado exitosamente";
            return View("Index");
        }
    }
}