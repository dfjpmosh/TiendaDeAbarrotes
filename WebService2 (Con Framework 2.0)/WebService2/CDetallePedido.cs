using System;
using System.Collections.Generic;
using System.Web;

namespace WebService2
{
    public class CDetallePedido
    {
        public string idCliente;
        public string nomProducto;
        public string idProducto;
        public int cantidad;
        public float precio;
        public float subtotal;

        public CDetallePedido()
        {
        }
    }
}