using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace La_tiendita.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string NProducto { get; set; }

        public string Descripcion { get; set; }

        public string FechaV { get; set; }

        public int Existencia { get; set; }

        public Decimal PrecioCompra { get; set; }

        public Double PrecioVenta { get; set; }

        public int idCategoria { get; set; }
        public Categoria categoria { get; set; }


    }
}