using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//--------------Namespace para trabajar con SQLServer--------------
using System.Data.SqlClient;
//------------------------------------------------------------------
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Mvc;

namespace La_tiendita.Models
{
    public class Conexion
    {
        private String cadenaConexion { get; set; }
        //string strcon =
        private SqlConnection conexionSQL;
        public Conexion()
        {
            //Extrae las credenciales del webconfig
            cadenaConexion = @"Data source=DESKTOP-NFDMETJ;Initial Catalog=ProductosDB;Integrated Security=True";

        }
        //Función para realizar conexion a base de datos SQLserver
        public bool conectar()
        {
            try
            {
                //crea objeto SQLConnection y enviar como paramentros las credenciales de conexion
                this.conexionSQL = new SqlConnection(this.cadenaConexion);
                //abre la conexion a la base de datos SQLServer
                this.conexionSQL.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //Metodo que verifica si el estado de la conexion es abierta o cerrada en la aplicación.
        public bool estadoConexion()
        {
            switch (this.conexionSQL.State)
            {
                case System.Data.ConnectionState.Broken:
                    return true;
                case System.Data.ConnectionState.Open:
                    return true;
                default:
                    return false;
            }
        }
        public void desconectar()
        {
            //Cierra la conexion a la base de datos
            this.conexionSQL.Close();
        }
        public List<Producto> listaProducto()
        {
            try
            {
                conectar();
                List<Producto> lista = new List<Producto>();
                //crea objetos para comandos SQL
                SqlCommand comando = new SqlCommand();
                //crea objeto para leer datos de consulta SQL
                SqlDataReader lector;
                //Consulta a la tabla empresa.
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "Select* from Producto  inner join Categoria  on Producto.Id_categoria = Categoria.Id_categoria ";
                //Agrega los parametros para conexion al objeto comando.
                comando.Connection = this.conexionSQL;
                //Ejecuta la instruccion Select
                lector = comando.ExecuteReader();

                //verifica si, se encuentran resultados
                if (lector.HasRows)
                {
                    //Verfica que se pueden leer registros
                    while (lector.Read())
                    {
                        //agrega datos de datareader al Dropdownlist.

                        Categoria oc1 = new Categoria();
                        oc1.idCategoria = Convert.ToInt32(lector["Id_categoria"]);
                        oc1.categoria = lector["categoria"].ToString();


                        lista.Add(new Producto
                        {
                            Id = Convert.ToInt32(lector["Id_producto"]),
                            NProducto = lector["produtco"].ToString(),
                            Descripcion = lector["descripcion_producto"].ToString(),
                            FechaV = lector["fecha_caducidad"].ToString(),
                            Existencia = Convert.ToInt32(lector["existencia_de_producto"]),
                            PrecioCompra = Convert.ToDecimal(lector["precio_compra"]),
                            PrecioVenta = Convert.ToDecimal(lector["precio_venta"]),
                            categoria = oc1
                        });
                    }
                    lector.Close();
                }
                desconectar();
                return lista;
            }
            catch (SqlException error)
            {
                return null;
            }


        }

        public List<Categoria> listaCategoria()
        {
            try
            {
                conectar();
                List<Categoria> lista = new List<Categoria>();
                //crea objetos para comandos SQL
                SqlCommand comando = new SqlCommand();
                //crea objeto para leer datos de consulta SQL
                SqlDataReader lector;
                //Consulta a la tabla empresa.
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "Select * from Categoria";
                //Agrega los parametros para conexion al objeto comando.
                comando.Connection = this.conexionSQL;
                //Ejecuta la instruccion Select
                lector = comando.ExecuteReader();
                //verifica si, se encuentran resultados
                if (lector.HasRows)
                {
                    //Verfica que se pueden leer registros
                    while (lector.Read())
                    {
                        //agrega datos de datareader al Dropdownlist.


                        lista.Add(new Categoria
                        {
                            idCategoria = Convert.ToInt32(lector["Id_categoria"]),
                            categoria = lector["categoria"].ToString()
                        });
                    }
                    lector.Close();
                }
                desconectar();
                return lista;
            }
            catch (SqlException error)
            {
                return null;
            }


        }

        public Producto cargarProducto(int id)
        {
            conectar();
            //objetos comando
            SqlCommand comando = new SqlCommand();//tabla 
            SqlDataReader lector;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "SELECT * FROM Producto WHERE Id_producto =" + id;
            comando.Connection = this.conexionSQL;

            try
            {
                //extra datos de la tabla Ofertas a controles asp y variables string.
                lector = comando.ExecuteReader();
                Producto items = new Producto();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        Categoria oc1 = new Categoria();
                        oc1.idCategoria = Convert.ToInt32(lector["Id_categoria"]);

                        items.Id = Convert.ToInt32(lector["Id_producto"]);
                        items.NProducto = lector["produtco"].ToString();
                        items.Descripcion = lector["descripcion_producto"].ToString();
                        items.FechaV = Convert.ToDateTime(lector["fecha_caducidad"]).ToString("yyyy-MM-dd");
                        items.Existencia = Convert.ToInt32(lector["existencia_de_producto"]);
                        items.PrecioCompra = Convert.ToDecimal(lector["precio_compra"]);
                        items.PrecioVenta = Convert.ToDecimal(lector["precio_venta"]);
                        items.categoria = oc1;

                    }

                }
                lector.Close();
                desconectar();
                return items;
            }
            catch (SqlException error)
            {
                return null;
            }

        }

        public int actualizarProducto(Producto producto)
        {
            conectar();
            String query = "update Producto set produtco = @p1, descripcion_producto = @p2, fecha_caducidad = @p3, existencia_de_producto = @p4, precio_compra = @p5, precio_venta = @p6, Id_categoria = @p7 where Id_producto =" + producto.Id;
            SqlCommand comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexionSQL;
            try
            {
                //asigna los valores a los parametros utilizados en la consulta.
                comando.Parameters.AddWithValue("@p1", producto.NProducto);
                comando.Parameters.AddWithValue("@p2", producto.Descripcion);
                comando.Parameters.AddWithValue("@p3", Convert.ToDateTime(producto.FechaV));
                comando.Parameters.AddWithValue("@p4", producto.Existencia);
                comando.Parameters.AddWithValue("@p5", producto.PrecioCompra);
                comando.Parameters.AddWithValue("@p6", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@p7", producto.idCategoria);

                //Realizar el actualización a la tabal oferta desde la aplicación.
                return comando.ExecuteNonQuery();
                desconectar();

            }
            catch (SqlException error)
            {
                return 0;
                desconectar();
            }

        }


        public int eliminarProducto(int idproducto)
        {
            conectar();
            String query = "Delete from Producto where Id_producto = @p1";
            SqlCommand comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexionSQL;
            try
            {
                //asigna los valores a los parametros utilizados en la consulta.
                comando.Parameters.AddWithValue("@p1", idproducto);

                //Realizar el actualización a la tabal oferta desde la aplicación.
                return comando.ExecuteNonQuery();
                desconectar();
            }
            catch (SqlException error)
            {
                return 0;
                desconectar();
            }
        }

            public int insertarProducto(Producto producto)
            {
                conectar();
                String query = "Insert into Producto values(@p1 , @p2 , @p3 , @p4 ,@p5, @p6, @p7)";
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = query;
                comando.Connection = this.conexionSQL;
                try
                {
                    //asigna los valores a los parametros utilizados en la consulta.
                    comando.Parameters.AddWithValue("@p1", producto.NProducto);
                    comando.Parameters.AddWithValue("@p2", producto.Descripcion);
                    comando.Parameters.AddWithValue("@p3", Convert.ToDateTime(producto.FechaV));
                    comando.Parameters.AddWithValue("@p4", producto.Existencia);
                    comando.Parameters.AddWithValue("@p5", producto.PrecioCompra);
                    comando.Parameters.AddWithValue("@p6", producto.PrecioVenta);
                    comando.Parameters.AddWithValue("@p7", producto.idCategoria);

                    //Realizar el actualización a la tabal oferta desde la aplicación.
                    return comando.ExecuteNonQuery();
                    desconectar();

                }
                catch (SqlException error)
                {
                    return 0;
                    desconectar();
                }

            }
        //---------------------------------------------------------------------------------------------

        public Categoria cargarCategoria(int id)
        {
            conectar();
            //objetos comando
            SqlCommand comando = new SqlCommand();//tabla 
            SqlDataReader lector;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "SELECT * FROM Categoria WHERE Id_categoria =" + id;
            comando.Connection = this.conexionSQL;

            try
            {
                //extra datos de la tabla Ofertas a controles asp y variables string.
                lector = comando.ExecuteReader();
                Categoria items = new Categoria();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {

                        items.idCategoria = Convert.ToInt32(lector["Id_categoria"]);
                        items.categoria = lector["categoria"].ToString();

                    }

                }
                lector.Close();
                desconectar();
                return items;
            }
            catch (SqlException error)
            {
                return null;
            }

        }

        public int actualizarCategoria(Categoria categoria)
        {
            conectar();
            String query = "update Categoria set categoria = @p1 where Id_categoria =" + categoria.idCategoria;
            SqlCommand comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexionSQL;
            try
            {
                //asigna los valores a los parametros utilizados en la consulta.
                comando.Parameters.AddWithValue("@p1", categoria.categoria);

                //Realizar el actualización a la tabal oferta desde la aplicación.
                return comando.ExecuteNonQuery();
                desconectar();

            }
            catch (SqlException error)
            {
                return 0;
                desconectar();
            }

        }


        public int eliminarCategoria(int idcategoria)
        {
            conectar();
            String query = "Delete from Categoria where Id_categoria = @p1";
            SqlCommand comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexionSQL;
            try
            {
                //asigna los valores a los parametros utilizados en la consulta.
                comando.Parameters.AddWithValue("@p1", idcategoria);

                //Realizar el actualización a la tabal oferta desde la aplicación.
                return comando.ExecuteNonQuery();
                desconectar();
            }
            catch (SqlException error)
            {
                return 0;
                desconectar();
            }
        }

        public int insertarCategoria(Categoria categoria)
        {
            conectar();
            String query = "Insert into Categoria values(@p1)";
            SqlCommand comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = query;
            comando.Connection = this.conexionSQL;
            try
            {
                //asigna los valores a los parametros utilizados en la consulta.
                comando.Parameters.AddWithValue("@p1", categoria.categoria);

                //Realizar el actualización a la tabal oferta desde la aplicación.
                return comando.ExecuteNonQuery();
                desconectar();

            }
            catch (SqlException error)
            {
                return 0;
                desconectar();
            }

        }




    }
}