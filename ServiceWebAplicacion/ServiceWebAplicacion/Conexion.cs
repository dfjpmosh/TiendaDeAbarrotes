using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

namespace ServiceWebAplicacion
{
   public class Conexion
    {
       SqlConnection con;

       public Conexion()
        {
            if (con == null)
                con = new SqlConnection("Data Source=.;DataBase=MINISUPER;Integrated Security=true");
             //con = new SqlConnection("Server=NombreServidor;DataBase=ejemplo;User Id=ejem;password=ejem");

        }

        public void Abrir()
        {
            if (con.State == ConnectionState.Closed) con.Open();
        }

        public void Cerrar()
        {
            if (con.State == ConnectionState.Open) con.Close();
        }

       // METODOS
        public String InicioSesion(String nic, String clav)
        {
            String msje = "";
            SqlCommand cmd;
            try
            { 
                Abrir();
                cmd = new SqlCommand("INICIOSESION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", nic);
                cmd.Parameters.AddWithValue("@clave", clav);
                cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msje = cmd.Parameters["@msje"].Value.ToString();
                Cerrar();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return msje;
        }

        public List<CCategoria> leerCategorias()
        {
            List<CCategoria> lCa = new List<CCategoria>();
            CCategoria c;

            try
            {
                Abrir();
                string query = "SELECT * FROM CATEGORIAS WHERE nombre<>'-Sin Categoria' ORDER BY nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c = new CCategoria();
                        c.idCategoria = reader.GetInt32(0);
                        c.nombre = reader.GetString(1);

                        lCa.Add(c);
                    }
                }
                Cerrar();
                return lCa;
            }
            catch (SqlException e)
            {
                
            }

            return null;
        }

        public List<CProducto> leerProductos(string Cat)
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto p;

            try
            {
                Abrir();
                string query = "SELECT * FROM PRODUCTOS as P, CATEGORIAS as C WHERE P.categoria=C.idCategoria AND P.estado = 'Activo' AND C.nombre='"+Cat+"' ORDER BY P.nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        p = new CProducto();
                        p.idProducto = reader.GetString(0);
                        p.nombre = reader.GetString(1);
                        p.categoria = reader.GetInt32(2);
                        p.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        p.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        p.existencia = reader.GetInt32(5);
                        p.idProveedor = reader.GetString(6);
                        p.estado = reader.GetString(7);

                        lP.Add(p);
                    }
                }
                Cerrar();
                return lP;
            }
            catch (SqlException e)
            {

            }

            return null;
        }

        public void insPedidoAndroid(string idCliente, string nomProducto, string idProducto, int cantidad, float precio, float subtotal)
        {
            try
            {
                Abrir();
                string query = "INSERT INTO DPANDROID(idCliente,nomProducto,idProducto,cantidad,precio,subtotal) VALUES(@idCliente,@nomProducto,@idProducto,@cantidad,@precio,@subtotal)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@nomProducto", nomProducto);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                cmd.ExecuteNonQuery();
                Cerrar();
            }
            catch (SqlException e)
            {
                
            }            
        }

        public CProducto buscarProducto(string id)
        {
            CProducto producto;

            try
            {
                Abrir();
                string query = "SELECT * FROM PRODUCTOS WHERE idProducto = " + id;
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);
                        Cerrar();
                        return producto;
                    }
                }
                
            }
            catch (SqlException e)
            {
                
            }
            Cerrar();
            return null;
        }

        public List<CDetallePedido> leerPedidosAndroid(string id)
        {
            List<CDetallePedido> lPA = new List<CDetallePedido>();
            CDetallePedido pedido;

            try
            {
                Abrir();
                string query = "SELECT * FROM DPANDROID WHERE idCliente ='"+id+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CDetallePedido();
                        pedido.idCliente = reader.GetString(0);
                        pedido.nomProducto = reader.GetString(1);
                        pedido.idProducto = reader.GetString(2);
                        pedido.cantidad = reader.GetInt32(3);
                        pedido.precio = Convert.ToSingle(reader.GetDecimal(4));
                        pedido.subtotal = Convert.ToSingle(reader.GetDecimal(5));
                        
                        lPA.Add(pedido);
                    }
                }
                Cerrar();
                return lPA;
            }
            catch (SqlException e)
            {
                
            }

            return null;
        }

        public int insertaPedido(string id, float tot)
        {
            int folio = 0;

            try
            {
                Abrir();
                SqlCommand cmd = new SqlCommand("sp_INSERTAPEDIDO", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter pIdCliente = new SqlParameter("@cliente", System.Data.SqlDbType.VarChar);
                pIdCliente.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(pIdCliente);
                pIdCliente.Value = id;

                SqlParameter pTotal = new SqlParameter("@total", System.Data.SqlDbType.Float);
                pTotal.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(pTotal);
                pTotal.Value = tot;

                SqlParameter pFolio = new SqlParameter("@folio", System.Data.SqlDbType.Int);
                pFolio.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(pFolio);
                pFolio.Value = 1;

                cmd.ExecuteNonQuery();
                folio = (int)cmd.Parameters["@folio"].Value;
                Cerrar();
            }
            catch (SqlException e)
            {
                
            }

            return folio;
        }

        public void agregarDetallePedido(int Folio, string IdProducto, int Cantidad, float Precio, float Subtotal)
        {
            try
            {
                Abrir();
                string query = "INSERT INTO DETALLEPXC(folio,idProducto,cantidad,precio,subtotal) VALUES(@folio,@idProducto,@cantidad,@precio,@subtotal)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@folio", Folio);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@precio", Precio);
                cmd.Parameters.AddWithValue("@subtotal", Subtotal);
                
                cmd.ExecuteNonQuery();
                Cerrar();
            }
            catch (SqlException e)
            {
                
            }
        }

        public void ElimPedidoAndroid(string idCliente)
        {
            try
            {
                Abrir();
                string query = "DELETE FROM DPANDROID WHERE idCliente = '"+idCliente+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Cerrar();
            }
            catch (SqlException e)
            {

            }
        }
    }
}