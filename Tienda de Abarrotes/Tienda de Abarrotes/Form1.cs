using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Printing;
using System.IO;

namespace Tienda_de_Abarrotes
{
    public partial class Form1 : Form
    {
        CConexionBD conexion;
        private Font Fuente;
        private StreamReader streamParaImp;
        bool ebd = true;

        public Form1()
        {
            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start();
            Thread.Sleep(12000);
            t.Abort();
            InitializeComponent();
            reloj.Start();
        }

        public void SplashScreen()
        {
            Application.Run(new FPreload());
        }

        private void tcPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcPrincipal.SelectedIndex)
            {
                case 1: tsbAgregarCli_Click(sender, e); break;
                case 2: tsbAgregarProv_Click(sender, e); break;
                case 3: tsbAgregarProd_Click(sender, e); break;
                case 4: limpiaYocultaDGV(); break;
            }
        }

        private void reloj_Tick(object sender, EventArgs e)
        {
            lbFyH.Text = DateTime.Now.ToString();
            int modulo = DateTime.Now.Second % 10;

            if (modulo == 0 && ebd)
            {
                //ebd = false;
                escanea();
            }

        }

        public void escanea()
        {
            List<CPedido> lP = new List<CPedido>();
            conexion = new CConexionBD();

            lP = conexion.leerPedidosEspera();

            if (lP.Count > 0)
                btnChecaPedidos.BackColor = Color.Red;
            else
                btnChecaPedidos.BackColor = Color.Transparent;
            //MessageBox.Show("Rifado");
            ebd = true;
        }

        #region CLIENTES
        private void tsbAgregarCli_Click(object sender, EventArgs e)
        {
            tbIdTelefono.Text = "";
            tbContrasena.Text = "";
            tbNombre.Text = "";
            tbApellidos.Text = "";
            tbColonia.Text = "";
            tbCalle.Text = "";
            tbNumero.Text = "";
            gbAgregaCli.Visible = true;
            gbEliminaCli.Visible = false;
        }

        private void btnAgregarCli_Click(object sender, EventArgs e)
        {
            CCliente cliente = new CCliente();
            if (validaAgregarCliente())
            {
                conexion = new CConexionBD();
                if ((cliente = conexion.buscarCliente(tbIdTelefono.Text)) == null)
                {
                    conexion.agregarCliente(tbIdTelefono.Text, tbContrasena.Text, tbNombre.Text, tbApellidos.Text, tbColonia.Text, tbCalle.Text, tbNumero.Text);
                }
                else
                {
                    if (cliente.estado == "Baja")
                    {
                        if (MessageBox.Show("El Cliente ya Existe\nEsta en Baja Temporal\n¿Desea Reactivarlo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            conexion.reactivaCliente(tbIdTelefono.Text);
                        }
                    }
                    else
                        MessageBox.Show("El Cliente ya existe");
                }

                tbIdTelefono.Text = "";
                tbContrasena.Text = "";
                tbNombre.Text = "";
                tbApellidos.Text = "";
                tbColonia.Text = "";
                tbCalle.Text = "";
                tbNumero.Text = "";
            }
        }

        public bool validaAgregarCliente()
        {
            if (!esNumero(tbIdTelefono.Text))
            {
                MessageBox.Show("El Campo Telefono Debe Ser Numerico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbIdTelefono.Text = "";
                tbIdTelefono.Focus();
                return false;
            }
            if (tbContrasena.TextLength != 8)
            {
                MessageBox.Show("La Contraseña Debe Tener 8 Caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbContrasena.Text = "";
                tbContrasena.Focus();
                return false;
            }
            if (tbNombre.TextLength < 1)
            {
                MessageBox.Show("El Campo Nombre Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNombre.Focus();
                return false;
            }
            if (tbApellidos.TextLength < 1)
            {
                MessageBox.Show("El Campo Apellidos Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbApellidos.Focus();
                return false;
            }
            if (tbColonia.TextLength < 1)
            {
                MessageBox.Show("El Campo Colonia Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbColonia.Focus();
                return false;
            }
            if (tbCalle.TextLength < 1)
            {
                MessageBox.Show("El Campo Calle Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCalle.Focus();
                return false;
            }
            if (tbNumero.TextLength < 1)
            {
                MessageBox.Show("El Campo Numero Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNumero.Focus();
                return false;
            }

            return true;
        }

        private void tsbEliminaCli_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            List<CCliente> lC = new List<CCliente>();
            lC = conexion.leerClientes();
            dgvClientes.Rows.Clear();

            if (lC.Count > 0)
                foreach (CCliente c in lC)
                    dgvClientes.Rows.Add(c.idTelefono, "*********", c.nombre, c.apellidos, c.calle + " #" + c.numero + " Col. " + c.colonia);

            gbAgregaCli.Visible = false;
            gbEliminaCli.Visible = true;
        }

        private void dgvClientes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvClientes.Columns[e.ColumnIndex].Name == "Telefono" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvClientes.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvClientes.Columns[e.ColumnIndex].Name == "Contrasena" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvClientes.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvClientes.Columns[e.ColumnIndex].Name == "Eliminar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvClientes.Rows[e.RowIndex].Cells["Eliminar"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(@"D:\dfjpmosh\Documents\Visual Studio 2010\Projects\Tienda de Abarrotes\Tienda de Abarrotes\Resources\Eliminar.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvClientes.Rows[e.RowIndex].Height = icoAtomico.Height + 5;
                this.dgvClientes.Columns[e.ColumnIndex].Width = icoAtomico.Width + 5;

                e.Handled = true;
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvClientes.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if (MessageBox.Show("Realmente Desea Eliminar El Cliente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    conexion = new CConexionBD();
                    //Checar si el cliente tiene ventas, si es el caso evitar que se elimine
                    conexion.eliminaCliente(dgvClientes[0, dgvClientes.CurrentRow.Index].Value.ToString());
                    tsbEliminaCli_Click(sender, null);
                }
            }
        }

        private void btnBuscarCli_Click(object sender, EventArgs e)
        {
            string clave = "";
            int i;
            bool b = false;

            if (tbCliente.Text.Length > 0)
                clave = tbCliente.Text;

            for (i = 0; i < dgvClientes.RowCount; i++)
            {
                if (clave == dgvClientes[0, i].Value.ToString())
                {
                    dgvClientes.CurrentCell = dgvClientes[0, i];
                    b = true;
                    break;
                }
            }
            if (!b)
                MessageBox.Show("No se Encontro el Cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscarCli_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region PROVEEDORES
        private void tsbAgregarProv_Click(object sender, EventArgs e)
        {
            tbIdTelefonoProv.Text = "";
            tbNombreProv.Text = "";
            tbColoniaProv.Text = "";
            tbCalleProv.Text = "";
            tbNumeroProv.Text = "";
            gbAgregaProv.Visible = true;
            gbEliminaProv.Visible = false;
            gbEditaProv.Visible = false;
        }

        private void btnAgregarProv_Click(object sender, EventArgs e)
        {
            CProveedor proveedor = new CProveedor();
            if (validaAgregarProv())
            {
                conexion = new CConexionBD();
                if ((proveedor = conexion.buscarProveedor(tbIdTelefonoProv.Text)) == null)
                {
                    conexion.agregarProveedor(tbIdTelefonoProv.Text, tbNombreProv.Text, tbColoniaProv.Text, tbCalleProv.Text, tbNumeroProv.Text);
                }
                else
                {
                    if (proveedor.estado == "Baja")
                    {
                        if (MessageBox.Show("El Proveedor ya Existe\nEsta en Baja Temporal\n¿Desea Reactivarlo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            conexion.reactivaProveedor(tbIdTelefonoProv.Text);
                        }
                    }
                    else
                        MessageBox.Show("El Proveedor ya existe");
                }

                tbIdTelefonoProv.Text = "";
                tbNombreProv.Text = "";
                tbColoniaProv.Text = "";
                tbCalleProv.Text = "";
                tbNumeroProv.Text = "";
            }
        }

        public bool validaAgregarProv()
        {
            if (!esNumero(tbIdTelefonoProv.Text))
            {
                MessageBox.Show("El Campo Telefono Debe Ser Numerico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbIdTelefonoProv.Text = "";
                tbIdTelefonoProv.Focus();
                return false;
            }
            if (tbNombreProv.TextLength < 1)
            {
                MessageBox.Show("El Campo Nombre Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNombreProv.Focus();
                return false;
            }
            if (tbColoniaProv.TextLength < 1)
            {
                MessageBox.Show("El Campo Colonia Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbColoniaProv.Focus();
                return false;
            }
            if (tbCalleProv.TextLength < 1)
            {
                MessageBox.Show("El Campo Calle Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCalleProv.Focus();
                return false;
            }
            if (tbNumeroProv.TextLength < 1)
            {
                MessageBox.Show("El Campo Numero Esta Vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNumeroProv.Focus();
                return false;
            }

            return true;
        }

        private void tsbEliminaProv_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            List<CProveedor> lP = new List<CProveedor>();
            lP = conexion.leerProveedores();
            dgvProveedores.Rows.Clear();

            if (lP.Count > 0)
                foreach (CProveedor p in lP)
                    dgvProveedores.Rows.Add(p.idTelefono, p.nombre, p.calle + " " + p.numero + " " + p.colonia);

            gbAgregaProv.Visible = false;
            gbEditaProv.Visible = false;
            gbEliminaProv.Visible = true;
        }

        private void dgvProveedores_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvProveedores.Columns[e.ColumnIndex].Name == "TelefonoProv" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProveedores.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProveedores.Columns[e.ColumnIndex].Name == "EliminarProv" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvProveedores.Rows[e.RowIndex].Cells["EliminarProv"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(@"D:\dfjpmosh\Documents\Visual Studio 2010\Projects\Tienda de Abarrotes\Tienda de Abarrotes\Resources\Eliminar.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvProveedores.Rows[e.RowIndex].Height = icoAtomico.Height + 5;
                this.dgvProveedores.Columns[e.ColumnIndex].Width = icoAtomico.Width + 5;

                e.Handled = true;
            }
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvProveedores.Columns[e.ColumnIndex].Name == "EliminarProv")
            {
                if (MessageBox.Show("Realmente Desea Eliminar El Proveedor?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    conexion = new CConexionBD();
                    if (!conexion.tieneProductosAlta(dgvProveedores[0, dgvProveedores.CurrentRow.Index].Value.ToString()))
                    {
                        conexion.eliminaProveedor(dgvProveedores[0, dgvProveedores.CurrentRow.Index].Value.ToString());
                        tsbEliminaProv_Click(sender, null);
                    }
                    else
                        MessageBox.Show("Primero debe eliminar los productos del proveedor,\n e intente nuevamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBuscarProv_Click(object sender, EventArgs e)
        {
            string clave = "";
            int i;
            bool b = false;

            if (tbProveedor.Text.Length > 0)
                clave = tbProveedor.Text;

            for (i = 0; i < dgvProveedores.RowCount; i++)
            {
                if (clave == dgvProveedores[0, i].Value.ToString())
                {
                    dgvProveedores.CurrentCell = dgvProveedores[0, i];
                    b = true;
                    break;
                }
            }
            if (!b)
                MessageBox.Show("No se Encontro el Proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscarProv_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void tsbEditaProv_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            List<CProveedor> lP = new List<CProveedor>();
            lP = conexion.leerProveedores();
            dgvProveedoresEdit.Rows.Clear();

            if (lP.Count > 0)
                foreach (CProveedor p in lP)
                    dgvProveedoresEdit.Rows.Add(p.idTelefono, p.nombre, p.calle + " " + p.numero + " " + p.colonia);

            gbAgregaProv.Visible = false;
            gbEliminaProv.Visible = false;
            gbEditaProv.Visible = true;
        }

        private void dgvProveedoresEdit_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvProveedoresEdit.Columns[e.ColumnIndex].Name == "TelefonoProvEdit" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProveedoresEdit.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProveedoresEdit.Columns[e.ColumnIndex].Name == "EditarProv" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvProveedoresEdit.Rows[e.RowIndex].Cells["EditarProv"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(@"D:\dfjpmosh\Documents\Visual Studio 2010\Projects\Tienda de Abarrotes\Tienda de Abarrotes\Resources\Eliminar.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvProveedoresEdit.Rows[e.RowIndex].Height = icoAtomico.Height + 5;
                this.dgvProveedoresEdit.Columns[e.ColumnIndex].Width = icoAtomico.Width + 5;

                e.Handled = true;
            }
        }

        private void dgvProveedoresEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvProveedoresEdit.Columns[e.ColumnIndex].Name == "EditarProv")
            {
                /*if (MessageBox.Show("Realmente Desea Eliminar El Proveedor?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    conexion = new CConexionBD();
                    conexion.eliminaProveedor(dgvProveedores[0, dgvProveedores.CurrentRow.Index].Value.ToString());
                    tsbEliminaProv_Click(sender, null);
                }*/
                MessageBox.Show("No se puede modificar");
            }
        }

        private void btnBuscarProvEdit_Click(object sender, EventArgs e)
        {
            string clave = "";
            int i;
            bool b = false;

            if (tbEditProv.Text.Length > 0)
                clave = tbEditProv.Text;

            for (i = 0; i < dgvProveedoresEdit.RowCount; i++)
            {
                if (clave == dgvProveedoresEdit[0, i].Value.ToString())
                {
                    dgvProveedoresEdit.CurrentCell = dgvProveedoresEdit[0, i];
                    b = true;
                    break;
                }
            }
            if (!b)
                MessageBox.Show("No se Encontro el Proveedor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbProveedorEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscarProvEdit_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region PRODUCTOS
        private void tsbAgregarProd_Click(object sender, EventArgs e)
        {
            inicializacombos();
            tbIdProd.Text = "";
            tbNombreProd.Text = "";
            cbCategoriaAgre.Text = "-Sin Categoria";
            npdPCosto.Value = 0;
            npdPVenta.Value = 0;
            npdExistencia.Value = 0;
            cbProveedorAgre.Text = "";
            cbEstadoProd.Text = "Activo";
            gbEliminaProd.Visible = false;
            gbEditaProd.Visible = false;
            gbAgregaProd.Visible = true;

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

        private void btnAgregarProd_Click(object sender, EventArgs e)
        {
            CProducto producto = new CProducto();
            if (validaAgregarProd())
            {
                conexion = new CConexionBD();
                if ((producto = conexion.buscarProducto(tbIdProd.Text)) == null)
                {
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

                    conexion.agregarProducto(id, nom, ca, cc, cv, ex, pp, es);
                }
                else
                {
                    if (producto.estado == "Baja")
                    {
                        if (MessageBox.Show("El Producto ya Existe\nEsta en Baja Temporal\n¿Desea Reactivarlo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            conexion.reactivaProducto(producto.idProducto);
                        }
                    }
                    else
                        MessageBox.Show("El Producto ya existe");
                }

                tbIdProd.Text = "";
                tbNombreProd.Text = "";
                cbCategoriaAgre.Text = "-Sin Categoria";
                npdPCosto.Value = 0;
                npdPVenta.Value = 0;
                npdExistencia.Value = 0;
                cbProveedorAgre.Text = "";
                cbEstadoProd.Text = "Activo";
            }
        }

        public bool validaAgregarProd()
        {
            if (!esNumero(tbIdProd.Text))
            {
                MessageBox.Show("El Campo Codigo Debe Ser Numerico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbIdProd.Text = "";
                tbIdProd.Focus();
                return false;
            }
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

        private void tsbEliminaProd_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            List<CProducto> lP = new List<CProducto>();
            lP = conexion.leerProductos();
            dgvProductosElim.Rows.Clear();

            if (lP.Count > 0)
                foreach (CProducto p in lP)
                {
                    CCategoria cat = conexion.buscarCategoriaId(p.categoria);
                    CProveedor pro = conexion.buscarProveedor(p.idProveedor);
                    dgvProductosElim.Rows.Add(p.idProducto, p.nombre, cat.nombre, p.precioC.ToString("0.00") + "$", p.precioP.ToString("0.00") + "$", p.existencia, pro.nombre, p.estado);
                }

            gbAgregaProd.Visible = false;
            gbEditaProd.Visible = false;
            gbEliminaProd.Visible = true;
        }

        private void dgvProductosElim_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "IdProducto" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 50;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "Categoria" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "PCosto" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 60;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "PVenta" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 60;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "ExistenciaProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 75;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "IdProveedorProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 150;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "EstadoProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = 57;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosElim.Columns[e.ColumnIndex].Name == "EliminarProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvProductosElim.Rows[e.RowIndex].Cells["EliminarProd"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(@"D:\dfjpmosh\Documents\Visual Studio 2010\Projects\Tienda de Abarrotes\Tienda de Abarrotes\Resources\Eliminar.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvProductosElim.Rows[e.RowIndex].Height = icoAtomico.Height + 5;
                this.dgvProductosElim.Columns[e.ColumnIndex].Width = icoAtomico.Width + 5;

                e.Handled = true;
            }
        }

        private void dgvProductosElim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvProductosElim.Columns[e.ColumnIndex].Name == "EliminarProd")
            {
                if (MessageBox.Show("Realmente Desea Eliminar El Producto?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    conexion = new CConexionBD();
                    conexion.eliminaProducto(dgvProductosElim[0, dgvProductosElim.CurrentRow.Index].Value.ToString());
                    tsbEliminaProd_Click(sender, null);
                }
            }
        }

        private void btnBuscarProdElim_Click(object sender, EventArgs e)
        {
            string clave = "";
            int i;
            bool b = false;

            if (tbProdElim.Text.Length > 0)
                clave = tbProdElim.Text;

            for (i = 0; i < dgvProductosElim.RowCount; i++)
            {
                if (clave == dgvProductosElim[0, i].Value.ToString())
                {
                    dgvProductosElim.CurrentCell = dgvProductosElim[0, i];
                    b = true;
                    break;
                }
            }
            if (!b)
                MessageBox.Show("No se Encontro el Producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbProdElim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscarProdElim_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void tsbEditaProd_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            List<CProducto> lP = new List<CProducto>();
            lP = conexion.leerTodosProductos();
            dgvProductosEdit.Rows.Clear();

            if (lP.Count > 0)
                foreach (CProducto p in lP)
                {
                    CCategoria cat = conexion.buscarCategoriaId(p.categoria);
                    CProveedor pro = conexion.buscarProveedor(p.idProveedor);
                    dgvProductosEdit.Rows.Add(p.idProducto, p.nombre, cat.nombre, p.precioC.ToString("0.00") + "$", p.precioP.ToString("0.00") + "$", p.existencia, pro.nombre, p.estado);
                }

            gbAgregaProd.Visible = false;
            gbEliminaProd.Visible = false;
            gbEditaProd.Visible = true;
        }

        private void dgvProductosEdit_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "IdEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 50;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "CategoriaEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 100;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "PCostoEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 60;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "PVentaEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 60;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "ExistenciaEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 75;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "ProveedorEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 150;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "EstadoEditProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = 57;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "EditarProd" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvProductosEdit.Rows[e.RowIndex].Cells["EditarProd"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(@"D:\dfjpmosh\Documents\Visual Studio 2010\Projects\Tienda de Abarrotes\Tienda de Abarrotes\Resources\Editar.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvProductosEdit.Rows[e.RowIndex].Height = icoAtomico.Height + 5;
                this.dgvProductosEdit.Columns[e.ColumnIndex].Width = icoAtomico.Width + 5;

                e.Handled = true;
            }
        }

        private void dgvProductosEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvProductosEdit.Columns[e.ColumnIndex].Name == "EditarProd")
            {
                if (MessageBox.Show("Realmente Desea Modificar El Producto?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    FEditaProducto fedita = new FEditaProducto(dgvProductosEdit[0, e.RowIndex].Value.ToString());
                    if (fedita.ShowDialog() == DialogResult.OK)
                        tsbEditaProd_Click(sender, null);
                }
            }
        }

        private void btnBuscarProdEdit_Click(object sender, EventArgs e)
        {
            string clave = "";
            int i;
            bool b = false;

            if (tbProdEdit.Text.Length > 0)
                clave = tbProdEdit.Text;

            for (i = 0; i < dgvProductosEdit.RowCount; i++)
            {
                if (clave == dgvProductosEdit[0, i].Value.ToString())
                {
                    dgvProductosEdit.CurrentCell = dgvProductosEdit[0, i];
                    b = true;
                    break;
                }
            }
            if (!b)
                MessageBox.Show("No se Encontro el Producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbProdEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscarProdEdit_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region VENTAS
        public bool esNumero(string numero)
        {
            int i;
            for (i = 0; i < numero.Length; i++)
            {
                if (!Char.IsNumber(numero.ElementAt(i)))
                    return false;
            }

            return true;
        }

        private void btnBuscaClienVenta_Click(object sender, EventArgs e)
        {
            CCliente cliente = new CCliente();
            conexion = new CConexionBD();

            if ((cliente = conexion.buscarCliente(tbIdClienteVenta.Text)) != null)
            {
                tbNomClienteVenta.Text = cliente.nombre + " " + cliente.apellidos;
                tbDirecClienVenta.Text = cliente.calle + " #" + cliente.numero + " " + cliente.colonia;
                tbInsertaCodigo.Focus();
            }
            else
            {
                MessageBox.Show("El Cliente No Existe");
                tbIdClienteVenta.Text = "";
                tbIdClienteVenta.Focus();
            }
        }

        private void tbIdClienteVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnBuscaClienVenta_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnInsProducto_Click(object sender, EventArgs e)
        {
            CProducto producto = new CProducto();
            conexion = new CConexionBD();

            if (tbInsertaCodigo.TextLength > 0 && (producto = conexion.buscarProducto(tbInsertaCodigo.Text)) != null)
            {
                if (buscarProductodgvVentas(producto.idProducto, producto.precioC))
                {
                    dgvVentas.Rows.Add(producto.idProducto, producto.nombre, producto.precioC.ToString("0.00") + "$", "1", producto.precioC.ToString("0.00") + "$");
                    dgvVentas.CurrentCell = dgvVentas[0, dgvVentas.RowCount - 1];
                }
                sumaTotal();
            }
            else
                MessageBox.Show("El Producto No Existe");

            tbInsertaCodigo.Focus();
            tbInsertaCodigo.Text = "";
        }

        public bool buscarProductodgvVentas(string id, float precio)
        {
            int i;

            for (i = 0; i < dgvVentas.RowCount; i++)
            {
                if (id == dgvVentas[0, i].Value.ToString())
                {
                    int c = Convert.ToInt32(dgvVentas[3, i].Value.ToString()) + 1;
                    dgvVentas[3, i].Value = c;
                    dgvVentas[4, i].Value = (c * precio).ToString("0.00") + "$";
                    return false;
                }
            }

            return true;
        }

        private void tbInsertaCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                btnInsProducto_Click(null, null);
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                    e.Handled = false;
                else
                {
                    if (!Char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Solo Debe Contener Numeros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void sumaTotal()
        {
            int i;
            float sub, total = 0;
            string s;

            for (i = 0; i < dgvVentas.RowCount; i++)
            {
                s = dgvVentas[4, i].Value.ToString();
                sub = Convert.ToSingle(s.Substring(0, s.Length - 1));
                total += sub;
            }

            tbTotalVenta.Text = total.ToString("0.00") + "$";
        }

        private void btnInsCantidad_Click(object sender, EventArgs e)
        {
            int i, c;
            string p;
            float precio;

            FInCantidad FC = new FInCantidad();

            if (FC.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    i = dgvVentas.CurrentRow.Index;
                    c = Convert.ToInt32(FC.npdCantidad.Value);
                    p = dgvVentas[2, i].Value.ToString();
                    precio = Convert.ToSingle(p.Substring(0, p.Length - 1));
                    dgvVentas[3, i].Value = c;
                    dgvVentas[4, i].Value = (c * precio).ToString("0.00") + "$";
                    sumaTotal();
                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione un Producto");
                }
            }
        }

        private void btnElimProducto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Decea quitar de la lista el Producto seleccionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    dgvVentas.Rows.RemoveAt(dgvVentas.CurrentRow.Index);
                    sumaTotal();
                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione un Producto");
                }
            }
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            tbIdClienteVenta.Text = "";
            tbNomClienteVenta.Text = "Mostrador";
            tbDirecClienVenta.Text = "";
            dgvVentas.Rows.Clear();
            tbTotalVenta.Text = "0.00$";
        }

        private void btnInsVenta_Click(object sender, EventArgs e)
        {
            conexion = new CConexionBD();
            int folio;
            string total = tbTotalVenta.Text;
            total = total.Substring(0, total.Length - 1);

            if (dgvVentas.RowCount > 0)
            {
                if (tbIdClienteVenta.TextLength > 1)
                    folio = conexion.insertaVenta(tbIdClienteVenta.Text, Convert.ToSingle(total));
                else
                    folio = conexion.insertaVenta("Mostrador", Convert.ToSingle(total));

                if (folio > 0)
                {
                    insDetalleVenta(folio);
                    btnCancelarVenta_Click(null, null); //Solo limpia el formulario
                    MessageBox.Show("La Venta Se Registro Satisfactoriamente");
                }
            }
            else
                MessageBox.Show("No hay Productos en la lista");
        }

        public void insDetalleVenta(int folio)
        {
            conexion = new CConexionBD();
            int i, Cantidad;
            string IdProducto, p, s;
            float Precio, Subtotal;

            for (i = 0; i < dgvVentas.RowCount; i++)
            {
                p = dgvVentas[2, i].Value.ToString();
                p = p.Substring(0, p.Length - 1);
                s = dgvVentas[4, i].Value.ToString();
                s = s.Substring(0, s.Length - 1);
                IdProducto = dgvVentas[0, i].Value.ToString();
                Precio = Convert.ToSingle(p);
                Cantidad = Convert.ToInt32(dgvVentas[3, i].Value);
                Subtotal = Convert.ToSingle(s);

                conexion.agregarDetalleVenta(folio, IdProducto, Cantidad, Precio, Subtotal);
            }
        }
        #endregion

        #region REPORTES
        public void limpiaYocultaDGV()
        {
            dgvRepClientes.Rows.Clear();
            dgvRepVentas.Rows.Clear();
            dgvRepProv.Rows.Clear();
            dgvRepProd.Rows.Clear();
            dgvRepPed.Rows.Clear();

            gbRepCli.Visible = false;
            gbRepVen.Visible = false;
            gbRepProv.Visible = false;
            gbRepProd.Visible = false;
            gbRepPed.Visible = false;

            //Ocultar todo los gb... es funcion se llama desde el switch de paginas
        }

        private void tsmiReporteHoy_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepVen();
            List<CVentas> lV = new List<CVentas>();
            conexion = new CConexionBD();
            //dgvRepVentas.Rows.Clear();

            string dia = DateTime.Today.Day.ToString();
            if (dia.Length == 1)
                dia = "0" + dia;

            string mes = DateTime.Today.Month.ToString();
            if (mes.Length == 1)
                mes = "0" + mes;

            string fIni = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 00:00:00";
            string fFin = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 23:59:59";

            lV = conexion.leerVentas(fIni, fFin);

            foreach (CVentas v in lV)
                dgvRepVentas.Rows.Add(v.folio, v.idCliente, "$"+v.total.ToString("0.00"), v.fecha);
                        
        }

        private void tsmiFePer_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepVen();
            FSelecFecha fsf = new FSelecFecha();
            List<CVentas> lV = new List<CVentas>();
            conexion = new CConexionBD();
            //dgvRepVentas.Rows.Clear();

            if (fsf.ShowDialog() == DialogResult.OK)
            {
                DateTime ini = fsf.dtpFIni.Value;
                DateTime fin = fsf.dtpFFin.Value;

                string dia = ini.Day.ToString();
                if (dia.Length == 1)
                    dia = "0" + dia;

                string mes = ini.Month.ToString();
                if (mes.Length == 1)
                    mes = "0" + mes;

                string fIni = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 00:00:00";

                dia = fin.Day.ToString();
                if (dia.Length == 1)
                    dia = "0" + dia;

                mes = fin.Month.ToString();
                if (mes.Length == 1)
                    mes = "0" + mes;

                string fFin = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 23:59:59";

                lV = conexion.leerVentas(fIni, fFin);

                CCliente cli = new CCliente();

                foreach (CVentas v in lV)
                {
                    cli = conexion.buscarCliente(v.idCliente);
                    dgvRepVentas.Rows.Add(v.folio, cli.nombre+" "+cli.apellidos, "$"+v.total.ToString("0.00"), v.fecha);
                }

                
            }
        }

        public void hazVisiblegbRepVen()
        {
            limpiaYocultaDGV();
            gbRepVen.Visible = true;
        }

        private void tsmiTodosCli_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepCli();
            List<CCliente> lC = new List<CCliente>();
            conexion = new CConexionBD();
            dgvRepClientes.Rows.Clear();
            dgvRepClientes.Columns[2].HeaderText = "Direccion";

            lC = conexion.leerClientes();

            foreach (CCliente c in lC)
                dgvRepClientes.Rows.Add(c.idTelefono, c.nombre + " " + c.apellidos, c.calle + " " + c.numero + " " + c.colonia);
          
        }

        private void tsmiMejoresCli_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepCli();
            List<CCliente> lC = new List<CCliente>();
            conexion = new CConexionBD();
            dgvRepClientes.Rows.Clear();
            dgvRepClientes.Columns[2].HeaderText = "Total Comprado";

            lC = conexion.leerClientesRep();
            lC.Sort(delegate(CCliente c1, CCliente c2)
            {
                return c2.suma.CompareTo(c1.suma);
            });



            foreach (CCliente c in lC)
                dgvRepClientes.Rows.Add(c.idTelefono, c.nombre + " " + c.apellidos, "$"+c.suma.ToString("0.00"));

            
        }

        public void hazVisiblegbRepCli()
        {
            limpiaYocultaDGV();
            gbRepCli.Visible = true;
        }

        private void tsmiProvedores_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProv();
            List<CProveedor> lP = new List<CProveedor>();
            conexion = new CConexionBD();
            dgvRepProv.Rows.Clear();

            lP = conexion.leerProveedores();

            foreach (CProveedor p in lP)
                dgvRepProv.Rows.Add(p.idTelefono, p.nombre, p.calle + " " + p.numero + " " + p.colonia);

            
        }

        public void hazVisiblegbRepProv()
        {
            limpiaYocultaDGV();
            gbRepProv.Visible = true;
        }

        private void tsmiTodosRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            List<CProducto> lP = new List<CProducto>();
            CProveedor proveedor = new CProveedor();
            CCategoria categoria = new CCategoria();
            conexion = new CConexionBD();
            dgvRepProd.Rows.Clear();

            lP = conexion.leerTodosProductos();

            foreach (CProducto p in lP)
            {
                proveedor = conexion.buscarProveedor(p.idProveedor);
                categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
            }

            
        }

        private void tsmiActivosRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            List<CProducto> lP = new List<CProducto>();
            CProveedor proveedor = new CProveedor();
            CCategoria categoria = new CCategoria();
            conexion = new CConexionBD();
            dgvRepProd.Rows.Clear();

            lP = conexion.leerProductos();

            foreach (CProducto p in lP)
            {
                proveedor = conexion.buscarProveedor(p.idProveedor);
                categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
            }

            
        }

        private void tsmiBajaRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            List<CProducto> lP = new List<CProducto>();
            CProveedor proveedor = new CProveedor();
            CCategoria categoria = new CCategoria();
            conexion = new CConexionBD();
            dgvRepProd.Rows.Clear();

            lP = conexion.leerProductosBaja();

            foreach (CProducto p in lP)
            {
                proveedor = conexion.buscarProveedor(p.idProveedor);
                categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
            }

            
        }

        private void tsmiProvedorRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            CProveedor prov = new CProveedor();
            string pc;
            conexion = new CConexionBD();

            FSelecPoC fPC = new FSelecPoC(1);

            if (fPC.ShowDialog() == DialogResult.OK)
            {
                pc = fPC.cbProvCat.Text;
                if (pc != "Selecciona Proveedor" && pc != "-Sin Categoria")
                {
                    prov = conexion.buscarProveedorNom(pc);
                    List<CProducto> lP = new List<CProducto>();
                    CProveedor proveedor = new CProveedor();
                    CCategoria categoria = new CCategoria();
                    conexion = new CConexionBD();
                    dgvRepProd.Rows.Clear();

                    lP = conexion.leerProductosProv(prov.idTelefono);

                    foreach (CProducto p in lP)
                    {
                        proveedor = conexion.buscarProveedor(p.idProveedor);
                        categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                        dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
                    }

                    
                }
            }
        }

        private void tsmiCategoriaRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            CCategoria cat = new CCategoria();
            string pc;
            conexion = new CConexionBD();

            FSelecPoC fPC = new FSelecPoC(2);

            if (fPC.ShowDialog() == DialogResult.OK)
            {
                pc = fPC.cbProvCat.Text;
                if (pc != "Selecciona Proveedor" && pc != "-Sin Categoria")
                {
                    cat = conexion.buscarCategoria(pc);
                    List<CProducto> lP = new List<CProducto>();
                    CProveedor proveedor = new CProveedor();
                    CCategoria categoria = new CCategoria();
                    conexion = new CConexionBD();
                    dgvRepProd.Rows.Clear();

                    lP = conexion.leerProductosCat(cat.idCategoria);

                    foreach (CProducto p in lP)
                    {
                        proveedor = conexion.buscarProveedor(p.idProveedor);
                        categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                        dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
                    }

                    
                }
            }
        }

        private void tsmiVendidosRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            List<CProducto> lP = new List<CProducto>();
            CProveedor proveedor = new CProveedor();
            CCategoria categoria = new CCategoria();
            conexion = new CConexionBD();
            dgvRepProd.Rows.Clear();
            dgvRepProd.Columns[7].HeaderText = "Total Comprado";

            lP = conexion.leerProductosMC();
            lP.Sort(delegate(CProducto c1, CProducto c2)
            {
                return c2.suma.CompareTo(c1.suma);
            });



            foreach (CProducto p in lP)
            {
                proveedor = conexion.buscarProveedor(p.idProveedor);
                categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.suma);
            }

            
        }

        private void tsmiEscasosRepProd_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepProd();
            List<CProducto> lP = new List<CProducto>();
            CProveedor proveedor = new CProveedor();
            CCategoria categoria = new CCategoria();
            conexion = new CConexionBD();
            dgvRepProd.Rows.Clear();

            lP = conexion.leerProductosEsc();

            foreach (CProducto p in lP)
            {
                proveedor = conexion.buscarProveedor(p.idProveedor);
                categoria = conexion.buscarCategoriaId(Convert.ToInt32(p.categoria));
                dgvRepProd.Rows.Add(p.idProducto, p.nombre, categoria.nombre, "$" + p.precioC.ToString("0.00"), "$" + p.precioP.ToString("0.00"), p.existencia, proveedor.nombre, p.estado);
            }

            
        }

        public void hazVisiblegbRepProd()
        {
            limpiaYocultaDGV();
            gbRepProd.Visible = true;
        }

        private void tsmiFechaRepPed_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepPed();
            FSelecFecha fsf = new FSelecFecha();
            List<CPedido> lP = new List<CPedido>();
            conexion = new CConexionBD();
            //dgvRepVentas.Rows.Clear();

            if (fsf.ShowDialog() == DialogResult.OK)
            {
                DateTime ini = fsf.dtpFIni.Value;
                DateTime fin = fsf.dtpFFin.Value;

                string dia = ini.Day.ToString();
                if (dia.Length == 1)
                    dia = "0" + dia;

                string mes = ini.Month.ToString();
                if (mes.Length == 1)
                    mes = "0" + mes;

                string fIni = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 00:00:00";

                dia = fin.Day.ToString();
                if (dia.Length == 1)
                    dia = "0" + dia;

                mes = fin.Month.ToString();
                if (mes.Length == 1)
                    mes = "0" + mes;

                string fFin = DateTime.Today.Year.ToString() + "/" + mes + "/" + dia + " 23:59:59";

                lP = conexion.leerPedidos(fIni, fFin);

                CCliente cli = new CCliente();

                foreach (CPedido p in lP)
                {
                    cli = conexion.buscarCliente(p.idCliente);
                    dgvRepPed.Rows.Add(p.folio, cli.nombre + " " + cli.apellidos, "$" + p.total.ToString("0.00"), p.fecha, p.estado);
                }


            }
        }

        private void tsmiClienteRepPed_Click(object sender, EventArgs e)
        {
            hazVisiblegbRepPed();
            CCliente cli = new CCliente();
            string pc;
            conexion = new CConexionBD();

            FSelecPoC fPC = new FSelecPoC(3);

            if (fPC.ShowDialog() == DialogResult.OK)
            {
                pc = fPC.cbProvCat.Text;
                if (pc != "Selecciona Cliente" && pc != "Selecciona Proveedor" && pc != "-Sin Categoria")
                {
                    cli = conexion.buscarClienteNom(pc);
                    List<CPedido> lP = new List<CPedido>();
                    conexion = new CConexionBD();
                    dgvRepPed.Rows.Clear();

                    lP = conexion.leerPedidosCli(cli.idTelefono);

                    foreach (CPedido p in lP)
                    {
                        dgvRepPed.Rows.Add(p.folio, cli.nombre + " " + cli.apellidos, "$" + p.total.ToString("0.00"), p.fecha, p.estado);
                    }
                }
            }
        }

        public void hazVisiblegbRepPed()
        {
            limpiaYocultaDGV();
            gbRepPed.Visible = true;
        }

        private void btnVerDetalleVenta_Click(object sender, EventArgs e)
        {
            List<CDetalleVenta> lDV = new List<CDetalleVenta>();
            int folio = Convert.ToInt32(dgvRepVentas[0, dgvRepVentas.CurrentRow.Index].Value.ToString());
            conexion = new CConexionBD();

            lDV = conexion.leerDetalleVenta(folio);
            CProducto p = new CProducto();

            string detalle = "Folio: "+folio + "\n";

            foreach (CDetalleVenta dv in lDV)
            {
                p = conexion.buscarProducto(dv.idProducto);
                detalle += "Producto: " + p.nombre + " Cantidad: " + dv.cantidad + " Precio: $" + dv.precio.ToString("0.00") + " Subtotal: $" + dv.precio.ToString("0.00") + "\n";
            }

            MessageBox.Show(detalle);
        }

        private void btnVerDetallePedido_Click(object sender, EventArgs e)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            int folio = Convert.ToInt32(dgvRepPed[0, dgvRepPed.CurrentRow.Index].Value.ToString());
            conexion = new CConexionBD();

            lDP = conexion.leerDetallePedido(folio);
            CProducto p = new CProducto();

            string detalle = "Folio: "+folio + "\n";

            foreach (CDetallePedido dp in lDP)
            {
                p = conexion.buscarProducto(dp.idProducto);
                detalle += "Producto: " + p.nombre + " Cantidad: " + dp.cantidad + "\n";
            }

            MessageBox.Show(detalle);
        }
        #endregion

        private void btnChecaPedidos_Click(object sender, EventArgs e)
        {
            FPedidos fp = new FPedidos();

            fp.ShowDialog();
            escanea();
        }

        private void btnImprimirVenta_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog vistaPrevia = new PrintPreviewDialog();
            PrintDocument pd = new PrintDocument();

            if (dgvRepVentas.RowCount > 0)
            {
                int folio = Convert.ToInt32(dgvRepVentas[0, dgvRepVentas.CurrentRow.Index].Value.ToString());
                crearArchivoVentas(folio);

                try
                {
                    streamParaImp = new StreamReader("C:\\temporalSW\\ejemplo.txt");
                    try
                    {
                        Fuente = new Font("Lucida Console", 10);
                        pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        vistaPrevia.Document = pd;

                        Form f = vistaPrevia as Form;
                        Control[] ts = vistaPrevia.Controls.Find("toolStrip1", true);
                        ToolStrip to = ts[0] as ToolStrip;
                        to.Items.RemoveAt(0);
                        f.WindowState = FormWindowState.Maximized;
                        f.ShowDialog();
                        streamParaImp.Close();

                        streamParaImp = new StreamReader("C:\\temporalSW\\ejemplo.txt");
                        pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        pd.Print();
                    }
                    finally
                    {
                        streamParaImp.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se Puede Imprimir\n" + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    File.Delete("C:\\temporalSW\\ejemplo.txt");
                }
            }
        }

        private void crearArchivoVentas(int folio)
        {
            List<CDetalleVenta> lDV = new List<CDetalleVenta>();
            CProducto p = new CProducto();
            conexion = new CConexionBD();
            CVentas ven = new CVentas();
            CCliente cli = new CCliente();

            ven = conexion.buscarVenta(folio);
            cli = conexion.buscarCliente(ven.idCliente);
            lDV = conexion.leerDetalleVenta(folio);

            try
            {
                StreamWriter sw = new StreamWriter("C:\\temporalSW\\ejemplo.txt", true, Encoding.ASCII);

                sw.WriteLine("                          NOTA DE VENTA");
                sw.WriteLine("");
                sw.WriteLine("Fecha: " + DateTime.Now.ToLongDateString());
                sw.WriteLine("");
                sw.WriteLine("Id Cliente: " + cli.idTelefono);
                sw.WriteLine("Nombre Cliente: " + cli.nombre +" "+cli.apellidos);
                sw.WriteLine("Direccion Cliente: " + cli.calle +" #"+cli.numero+" Col. "+cli.colonia);
                sw.WriteLine("");
                sw.WriteLine("Folio: " + ven.folio);
                sw.WriteLine("Fecha de la Venta: " + ven.fecha);
                sw.WriteLine("");
                sw.WriteLine("========================================================================");
                sw.WriteLine("[Id]      " + "[Producto]             " + "[Cantidad] " + "   [P. Unit.]" + "   [Subtotal]");
                sw.WriteLine("========================================================================");
                sw.WriteLine("");
                foreach (CDetalleVenta dv in lDV)
                {
                    p = conexion.buscarProducto(dv.idProducto);
                    sw.WriteLine(dv.idProducto +"       "+ p.nombre+calculaEspacio(p.nombre,25) +"      " + dv.cantidad +"         $"+ dv.precio.ToString("0.00") +"        $"+ dv.subtotal.ToString("0.00"));
                }
                sw.WriteLine("");
                sw.WriteLine("                                                       TOTAL = $" + ven.total.ToString("0.00"));

                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se Puede Imprimir\n" + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float lineasPorPagina = 0;
            float yPos = 0;
            int count = 0;
            float margenIzquierda = ev.MarginBounds.Left;
            float margenArriba = ev.MarginBounds.Top;
            string linea = null;

            lineasPorPagina = ev.MarginBounds.Height / Fuente.GetHeight(ev.Graphics);

            while (count < lineasPorPagina && ((linea = streamParaImp.ReadLine()) != null))
            {
                yPos = margenArriba + (count * Fuente.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(linea, Fuente, Brushes.Black, margenIzquierda, yPos, new StringFormat());
                count++;
            }

            if (linea != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private string calculaEspacio(string c, int t)
        {
            int x;
            int s = c.Length;
            int es = t - s;
            string cad = "";
            for (x = 0; x < es; x++)
                cad += " ";
            return cad;
        }

        private void btnImpPedido_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog vistaPrevia = new PrintPreviewDialog();
            PrintDocument pd = new PrintDocument();

            if (dgvRepPed.RowCount > 0)
            {
                int folio = Convert.ToInt32(dgvRepPed[0, dgvRepPed.CurrentRow.Index].Value.ToString());
                crearArchivoPedido(folio);

                try
                {
                    streamParaImp = new StreamReader("C:\\temporalSW\\ejemplo.txt");
                    try
                    {
                        Fuente = new Font("Lucida Console", 10);
                        pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        vistaPrevia.Document = pd;

                        Form f = vistaPrevia as Form;
                        Control[] ts = vistaPrevia.Controls.Find("toolStrip1", true);
                        ToolStrip to = ts[0] as ToolStrip;
                        to.Items.RemoveAt(0);
                        f.WindowState = FormWindowState.Maximized;
                        f.ShowDialog();
                        streamParaImp.Close();

                        streamParaImp = new StreamReader("C:\\temporalSW\\ejemplo.txt");
                        pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        pd.Print();
                    }
                    finally
                    {
                        streamParaImp.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se Puede Imprimir\n" + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    File.Delete("C:\\temporalSW\\ejemplo.txt");
                }
            }
        }

        private void crearArchivoPedido(int folio)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            CProducto p = new CProducto();
            conexion = new CConexionBD();
            CPedido ped = new CPedido();
            CCliente cli = new CCliente();

            ped = conexion.buscarPedido(folio);
            cli = conexion.buscarCliente(ped.idCliente);
            lDP = conexion.leerDetallePedido(folio);

            try
            {
                StreamWriter sw = new StreamWriter("C:\\temporalSW\\ejemplo.txt", true, Encoding.ASCII);

                sw.WriteLine("                              PEDIDO");
                sw.WriteLine("");
                sw.WriteLine("Fecha: " + DateTime.Now.ToLongDateString());
                sw.WriteLine("");
                sw.WriteLine("Id Cliente: " + cli.idTelefono);
                sw.WriteLine("Nombre Cliente: " + cli.nombre + " " + cli.apellidos);
                sw.WriteLine("Direccion Cliente: " + cli.calle + " #" + cli.numero + " Col. " + cli.colonia);
                sw.WriteLine("");
                sw.WriteLine("Folio: " + ped.folio);
                sw.WriteLine("Fecha de la Venta: " + ped.fecha);
                sw.WriteLine("");
                sw.WriteLine("========================================================================");
                sw.WriteLine("[Id]      " + "[Producto]             " + "[Cantidad] " + "   [P. Unit.]" + "   [Subtotal]");
                sw.WriteLine("========================================================================");
                sw.WriteLine("");
                foreach (CDetallePedido dp in lDP)
                {
                    p = conexion.buscarProducto(dp.idProducto);
                    sw.WriteLine(dp.idProducto + "       " + p.nombre + calculaEspacio(p.nombre, 25) + "      " + dp.cantidad + "         $" + dp.precio.ToString("0.00") + "        $" + dp.subtotal.ToString("0.00"));
                }
                sw.WriteLine("");
                sw.WriteLine("                                                       TOTAL = $" + ped.total.ToString("0.00"));

                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se Puede Imprimir\n" + e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
