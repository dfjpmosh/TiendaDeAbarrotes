using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda_de_Abarrotes
{
    class CCliente
    {
        public string idTelefono;
        public string contrasena;
        public string nombre;
        public string apellidos;
        public string colonia;
        public string calle;
        public string numero;
        public string estado;
        public float suma;
        
        public CCliente()
        {
            idTelefono = "";
            contrasena = "";
            nombre = "";
            apellidos = "";
            colonia = "";
            calle = "";
            numero = "";
            estado = "";
        }
    }
}
