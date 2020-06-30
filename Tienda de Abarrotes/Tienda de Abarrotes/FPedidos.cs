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
    public partial class FPedidos : Form
    {
        CConexionBD conexion;

        public FPedidos()
        {
            InitializeComponent();
            cargaPedidos();
        }

        public void cargaPedidos()
        {
            dgvPedidos.Rows.Clear();
            List<CPedido> lP = new List<CPedido>();
            CCliente cli = new CCliente();
            conexion = new CConexionBD();

            lP = conexion.leerPedidosEspera();

            foreach (CPedido p in lP)
            {
                cli = conexion.buscarCliente(p.idCliente);
                dgvPedidos.Rows.Add(p.folio, cli.nombre + " " + cli.apellidos, "$"+p.total.ToString("0.00"), p.fecha);
            }
        }

        private void btnVerDetallePedido_Click(object sender, EventArgs e)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            if (dgvPedidos.RowCount > 0)
            {
                int folio = Convert.ToInt32(dgvPedidos[0, dgvPedidos.CurrentRow.Index].Value.ToString());
                conexion = new CConexionBD();

                lDP = conexion.leerDetallePedido(folio);
                CProducto p = new CProducto();

                string detalle = "Folio: " + folio + "\n";

                foreach (CDetallePedido dp in lDP)
                {
                    p = conexion.buscarProducto(dp.idProducto);
                    detalle += "Producto: " + p.nombre + " Cantidad: " + dp.cantidad + "\n";
                }

                MessageBox.Show(detalle);                
            }
        }

        private void btnPedidoRealizado_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.RowCount > 0)
            {
                List<CDetallePedido> lPA = new List<CDetallePedido>();
                CPedido pedido = new CPedido();
                CProducto producto;
                bool noHay = false;
                int fVenta;
                float total=0;
                int folio = Convert.ToInt32(dgvPedidos[0, dgvPedidos.CurrentRow.Index].Value.ToString());
                conexion = new CConexionBD();

                pedido = conexion.buscarPedido(folio);
                lPA = conexion.leerDetallePedido(folio);

                foreach (CDetallePedido pd in lPA)
                {
                    total += pd.subtotal;
                    producto = new CProducto();
                    producto = conexion.buscarProducto(pd.idProducto);
                    if (producto.existencia < pd.cantidad)
                    {
                        noHay = true;
                        MessageBox.Show("Uno o Mas Productos no se puede surtir por falta de Existencia");
                        break;
                    }
                }

                if (noHay == false)
                {
                    fVenta = conexion.insertaVenta(pedido.idCliente, total);

                    foreach (CDetallePedido pd in lPA)
                        conexion.agregarDetalleVenta(fVenta, pd.idProducto, pd.cantidad, pd.precio, pd.subtotal);

                    conexion.cambiaEstadoPed(folio);

                    cargaPedidos();
                }
            }
        }
    }
}
