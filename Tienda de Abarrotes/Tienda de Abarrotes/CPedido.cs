using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda_de_Abarrotes
{
    class CPedido
    {
        public int folio;
        public string idCliente;
        public DateTime fecha;
        public string estado;
        public float total;
        public string comentario;

        public CPedido()
        {
            folio = 0;
            idCliente = "";
            fecha = new DateTime();
            estado = "";
            total = 0;
            comentario = "";
        }
    }
}
