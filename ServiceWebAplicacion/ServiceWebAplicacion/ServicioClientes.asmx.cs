using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;

namespace ServiceWebAplicacion
{

   

    /// <summary>
    /// Descripción breve de ServicioClientes
    /// </summary>
    [WebService(Namespace = "http://suarpe.com/")]   //puedes cambiar esta direccion
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]


        

    public class ServicioClientes : System.Web.Services.WebService
    {
        //hace referencia a la clase conexion, ahi esta la cadena de conexion y nuestros metodos
        Conexion con = new Conexion();

       
        [WebMethod]
        public string HelloWorld()
        {

            return "Hello World";
        }
         

        [WebMethod]
        public String LoginUsuario(string user, String password)
        {
            string msje = "";
            msje = con.InicioSesion(user,password);

            return msje;
        }

        [WebMethod]
        public List<CCategoria> LeerCategorias()
        {
            List<CCategoria> lCa = new List<CCategoria>();
            lCa = con.leerCategorias();
            

            return lCa;
        }

        [WebMethod]
        public List<CProducto> LeerProductos(string Cat)
        {
            List<CProducto> lP = new List<CProducto>();
            lP = con.leerProductos(Cat);

            return lP;
        }

        [WebMethod]
        public void insPedidoAndroid(string idCliente, string nomProducto, string idProducto, int cantidad)
        {
            CProducto producto = new CProducto();
            producto = con.buscarProducto(idProducto);

            con.insPedidoAndroid(idCliente, nomProducto, idProducto, cantidad, producto.precioP, producto.precioP * cantidad);
        }

        [WebMethod]
        public List<CDetallePedido> LeerPedidosAndroid(string idCliente)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            lDP = con.leerPedidosAndroid(idCliente);

            return lDP;
        }

        [WebMethod]
        public void insPedido(string idCliente)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            float total = 0;
            int folio;

            lDP = con.leerPedidosAndroid(idCliente);

            foreach (CDetallePedido p in lDP)
                total += p.subtotal;

            folio = con.insertaPedido(idCliente, total);

            foreach (CDetallePedido p in lDP)
                con.agregarDetallePedido(folio, p.idProducto, p.cantidad, p.precio, p.subtotal);

            con.ElimPedidoAndroid(idCliente);
        }

        
    }
}
