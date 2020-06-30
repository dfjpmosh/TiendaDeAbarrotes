using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tienda_de_Abarrotes
{
    public partial class FSelecPoC : Form
    {
        public FSelecPoC(int num)
        {
            InitializeComponent();

            inicializacombos(num);
        }

        public void inicializacombos(int n)
        {
            CConexionBD conexion = new CConexionBD();
            List<CCategoria> lCa = new List<CCategoria>();
            List<CProveedor> lP = new List<CProveedor>();
            List<CCliente> lCli = new List<CCliente>();
            conexion = new CConexionBD();

            cbProvCat.Items.Clear();

            switch(n)
            {
                case 1:
                    lEtiqueta.Text = "Proveedores";
                    cbProvCat.Text = "Selecciona Proveedor";
                    lP = conexion.leerProveedores();
                    foreach (CProveedor p in lP)
                        cbProvCat.Items.Add(p.nombre);
                break;
                case 2:
                    lEtiqueta.Text = "Categorias";
                    lCa = conexion.leerCategorias();
                    cbProvCat.Text = "-Sin Categoria";
                    foreach (CCategoria c in lCa)
                        cbProvCat.Items.Add(c.nombre);
                break;
                case 3:
                    lEtiqueta.Text = "Clientes";
                    lCli = conexion.leerClientes();
                    cbProvCat.Text = "Selecciona Cliente";
                    foreach (CCliente c in lCli)
                        cbProvCat.Items.Add(c.nombre);
                break;
            }
                        
        }
    }
}
