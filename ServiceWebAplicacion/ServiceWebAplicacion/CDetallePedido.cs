using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceWebAplicacion
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