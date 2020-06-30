using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda_de_Abarrotes
{
    class CVentas
    {
        public int folio;
        public string idCliente;
        public DateTime fecha;
        public float total;

        public CVentas()
        {
            folio = 0;
            idCliente = "";
            fecha = new DateTime();
            total = 0;
        }
    }
}
