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
    public partial class FEditaProducto : Form
    {
        CConexionBD conexion;
        CProducto producto;

        public FEditaProducto(string id)
        {
            InitializeComponent();
            conexion = new CConexionBD();
            producto = conexion.buscarProducto(id);
            inicializacombos();            
            inicializaDatos();
        }

        public void inicializacombos()
        {
            List<CCategoria> lCa = new List<CCategoria>();
            List<CProveedor> lP = new List<CProveedor>();
            conexion = new CConexionBD();

            lCa = conexion.leerCategorias();
            lP = conexion.leerProveedores();
            cbCategoriaAgre.Items.Clear();
            cbProveedorAgre.Items.Clear();

            foreach (CCategoria c in lCa)
                cbCategoriaAgre.Items.Add(c.nombre);

            foreach (CProveedor p in lP)
                cbProveedorAgre.Items.Add(p.nombre);
        }

        public void inicializaDatos()
        {
            CCategoria cat = conexion.buscarCategoriaId(producto.categoria);
            CProveedor pro = conexion.buscarProveedor(producto.idProveedor);

            tbIdProd.Text = producto.idProducto;
            tbNombreProd.Text = producto.nombre;
            cbCategoriaAgre.Text = cat.nombre;
            npdPCosto.Value = Convert.ToDecimal(producto.precioC);
            npdPVenta.Value = Convert.ToDecimal(producto.precioP);
            npdExistencia.Value = producto.existencia;
            cbProveedorAgre.Text = pro.nombre;
            cbEstadoProd.Text = producto.estado;
        }

        private void tbNuevaCate_Click(object sender, EventArgs e)
        {
            tbNuevaCate.Text = "";
        }

        private void btnAgreCategoria_Click(object sender, EventArgs e)
        {
            CCategoria categoria = new CCategoria();
            if (cbCategoriaAgre.Text.Length > 3)
            {
                conexion = new CConexionBD();
                if ((categoria = conexion.buscarCategoria(tbNuevaCate.Text)) == null)
                {
                    conexion.agregarCategoria(tbNuevaCate.Text);
                    inicializacombos();
                    cbCategoriaAgre.Focus();
                    tbNuevaCate.Text = "Escribe Nueva Categoria";
                }
                else
                {
                    MessageBox.Show("La Categoria ya existe");
                }
            }
            else
                MessageBox.Show("El Nombre de la Categoria debe ser mas largo");
        }

        private void btnModificaProd_Click(object sender, EventArgs e)
        {
            CProducto producto = new CProducto();
            if (validaAgregarProd())
            {
                conexion = new CConexionBD();
                string id = tbIdProd.Text;
                string nom = tbNombreProd.Text;
                CCategoria categoria = conexion.buscarCategoria(cbCategoriaAgre.Text);
                int ca = categoria.idCategoria;
                float cc = Convert.ToSingle(npdPCosto.Value.ToString());
                float cv = Convert.ToSingle(npdPVenta.Value.ToString());
                int ex = Convert.ToInt32(npdExistencia.Value.ToString());
                CProveedor proveedor = conexion.buscarProveedorNom(cbProveedorAgre.Text);
                string pp = proveedor.idTelefono;
                string es = cbEstadoProd.Text;
            
                conexion.modificarProducto(id, nom, ca, cc, cv, ex, pp, es);
            }
        }

        public bool validaAgregarProd()
        {
            if (tbNombreProd.TextLength < 1)
            {
                MessageBox.Show("El Campo Nombre Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNombreProd.Focus();
                return false;
            }
            if (cbProveedorAgre.Text == "" || cbProveedorAgre.Text == "Selecciona un Proveedor")
            {
                MessageBox.Show("Seleccione un Proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbProveedorAgre.Focus();
                return false;
            }

            return true;
        }

    }
}
