using System;
using System.Collections.Generic;
using System.Web;

namespace WebService2
{
    public class CProducto
    {
        public string idProducto;
        public string nombre;
        public int categoria;
        public float precioC;
        public float precioP;
        public int existencia;
        public string idProveedor;
        public string estado;
        public int suma;

        public CProducto()
        {
            idProducto = "";
            nombre = "";
            categoria = 0;
            precioC = 0;
            precioP = 0;
            existencia = 0;
            idProveedor = "";
            estado = "";
        }
    }
}