using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda_de_Abarrotes
{
    class CConexionBD
    {
        string conexion = "Data Source=localhost; Initial Catalog=MINISUPER; Integrated Security=true;";

        #region CLIENTES
        public void agregarCliente(string IdTelefono, string Contrasena, string Nombre, string Apellidos, string Colonia, string Calle, string Numero)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "INSERT INTO CLIENTES(idTelefono, contrasena, nombre, apellido, dirColonia, dirCalle, dirNumero, estado)  VALUES(@idTelefono, @contrasena, @nombre, @apellidos, @dirColonia, @dirCalle, @dirNumero, @estado)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@idTelefono", IdTelefono);
                cmd.Parameters.AddWithValue("@contrasena", Contrasena);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@apellidos", Apellidos);
                cmd.Parameters.AddWithValue("@dirColonia", Colonia);
                cmd.Parameters.AddWithValue("@dirCalle", Calle);
                cmd.Parameters.AddWithValue("@dirNumero", Numero);
                cmd.Parameters.AddWithValue("@estado", "Activo");
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Cliente Se Agrego Satisfactoriamente: ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al insertar:\n"+e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<CCliente> leerClientes()
        {
            List<CCliente> lC = new List<CCliente>();
            CCliente cliente;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CLIENTES WHERE estado = 'Activo'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente = new CCliente();
                        cliente.idTelefono = reader.GetString(0);
                        cliente.contrasena = reader.GetString(1);
                        cliente.nombre = reader.GetString(2);
                        cliente.apellidos = reader.GetString(3);
                        cliente.colonia = reader.GetString(4);
                        cliente.calle = reader.GetString(5);
                        cliente.numero = reader.GetString(6);
                        cliente.estado = reader.GetString(7);

                        lC.Add(cliente);
                    }
                }
                cnn.Close();
                return lC;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void eliminaCliente(string id)//Eliminacion Logica
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE CLIENTES SET estado = 'Baja' WHERE idTelefono='"+id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Cliente Se Ha Eliminado Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Eliminar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public CCliente buscarCliente(string id)
        {
            CCliente cliente;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CLIENTES WHERE idTelefono = '"+id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente = new CCliente();
                        cliente.idTelefono = reader.GetString(0);
                        cliente.contrasena = reader.GetString(1);
                        cliente.nombre = reader.GetString(2);
                        cliente.apellidos = reader.GetString(3);
                        cliente.colonia = reader.GetString(4);
                        cliente.calle = reader.GetString(5);
                        cliente.numero = reader.GetString(6);
                        cliente.estado = reader.GetString(7);

                        return cliente;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public CCliente buscarClienteNom(string id)
        {
            CCliente cliente;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CLIENTES WHERE nombre = '" + id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente = new CCliente();
                        cliente.idTelefono = reader.GetString(0);
                        cliente.contrasena = reader.GetString(1);
                        cliente.nombre = reader.GetString(2);
                        cliente.apellidos = reader.GetString(3);
                        cliente.colonia = reader.GetString(4);
                        cliente.calle = reader.GetString(5);
                        cliente.numero = reader.GetString(6);
                        cliente.estado = reader.GetString(7);

                        return cliente;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void reactivaCliente(string id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE CLIENTES SET estado = 'Activo' WHERE estado = 'Baja' AND idTelefono='"+id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Cliente Se Reactivo Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Activar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region PROVEEDORES
        public void agregarProveedor(string IdTelefono, string Nombre, string Colonia, string Calle, string Numero)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "INSERT INTO PROVEEDORES(idTelefono, nombre, dirColonia, dirCalle, dirNumero, estado)  VALUES(@idTelefono, @nombre, @dirColonia, @dirCalle, @dirNumero, @estado)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@idTelefono", IdTelefono);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@dirColonia", Colonia);
                cmd.Parameters.AddWithValue("@dirCalle", Calle);
                cmd.Parameters.AddWithValue("@dirNumero", Numero);
                cmd.Parameters.AddWithValue("@estado", "Activo");
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Proveedor Se Agrego Satisfactoriamente: ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al insertar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<CProveedor> leerProveedores()
        {
            List<CProveedor> lP = new List<CProveedor>();
            CProveedor proveedor;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PROVEEDORES WHERE estado = 'Activo'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        proveedor = new CProveedor();
                        proveedor.idTelefono = reader.GetString(0);
                        proveedor.nombre = reader.GetString(1);
                        proveedor.colonia = reader.GetString(2);
                        proveedor.calle = reader.GetString(3);
                        proveedor.numero = reader.GetString(4);
                        proveedor.estado = reader.GetString(5);

                        lP.Add(proveedor);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void eliminaProveedor(string id)//Eliminacion Logica
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PROVEEDORES SET estado = 'Baja' WHERE idTelefono='" + id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Proveedor Se Ha Eliminado Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Eliminar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public CProveedor buscarProveedor(string id)
        {
            CProveedor proveedor;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PROVEEDORES WHERE idTelefono = " + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        proveedor = new CProveedor();
                        proveedor.idTelefono = reader.GetString(0);
                        proveedor.nombre = reader.GetString(1);
                        proveedor.colonia = reader.GetString(2);
                        proveedor.calle = reader.GetString(3);
                        proveedor.numero = reader.GetString(4);
                        proveedor.estado = reader.GetString(5);

                        return proveedor;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public CProveedor buscarProveedorNom(string nom)
        {
            CProveedor proveedor;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PROVEEDORES WHERE nombre = '" + nom + "'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        proveedor = new CProveedor();
                        proveedor.idTelefono = reader.GetString(0);
                        proveedor.nombre = reader.GetString(1);
                        proveedor.colonia = reader.GetString(2);
                        proveedor.calle = reader.GetString(3);
                        proveedor.numero = reader.GetString(4);
                        proveedor.estado = reader.GetString(5);

                        return proveedor;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void reactivaProveedor(string id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PROVEEDORES SET estado = 'Activo' WHERE estado = 'Baja' AND idTelefono=" + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Proveedor Se Reactivo Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Activar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool tieneProductosAlta(string id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE estado = 'Activo' AND idProveedor='"+id+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        #endregion

        #region PRODUCTOS
        public void agregarProducto(string IdProducto, string Nombre, int Categoria, float PrecioC, float PrecioP, int Existencia, string IdProveedor, string Estado)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "INSERT INTO PRODUCTOS(idProducto, nombre, categoria, precioC, precioP, existencia, idProveedor, estado)  VALUES(@idProducto, @nombre, @categoria, @precioC, @precioP, @existencia, @idProveedor, @estado)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@categoria", Categoria);
                cmd.Parameters.AddWithValue("@precioC", PrecioC);
                cmd.Parameters.AddWithValue("@precioP", PrecioP);
                cmd.Parameters.AddWithValue("@existencia", Existencia);
                cmd.Parameters.AddWithValue("@idProveedor", IdProveedor);
                cmd.Parameters.AddWithValue("@estado", Estado);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Producto Se Agrego Satisfactoriamente: ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al insertar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public List<CProducto> leerProductos()
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE estado = 'Activo'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerProductosBaja()
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE estado = 'Baja'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerTodosProductos()
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CCategoria> leerCategorias()
        {
            List<CCategoria> lCa = new List<CCategoria>();
            CCategoria categoria;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CATEGORIAS";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoria = new CCategoria();
                        categoria.idCategoria = reader.GetInt32(0);
                        categoria.nombre = reader.GetString(1);
                        
                        lCa.Add(categoria);
                    }
                }
                cnn.Close();
                return lCa;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public CCategoria buscarCategoria(string nom)
        {
            CCategoria categoria;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CATEGORIAS WHERE nombre = '" + nom + "'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoria = new CCategoria();
                        categoria.idCategoria = reader.GetInt32(0);
                        categoria.nombre = reader.GetString(1);
                        
                        return categoria;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public CCategoria buscarCategoriaId(int id)
        {
            CCategoria categoria;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM CATEGORIAS WHERE idCategoria = " + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoria = new CCategoria();
                        categoria.idCategoria = reader.GetInt32(0);
                        categoria.nombre = reader.GetString(1);

                        return categoria;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void agregarCategoria(string Nombre)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "INSERT INTO CATEGORIAS(nombre)  VALUES(@nombre)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("La Categoria Se Agrego Satisfactoriamente: ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al insertar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void eliminaProducto(string id)//Eliminacion Logica
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PRODUCTOS SET estado = 'Baja' WHERE idProducto=" + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Producto Se Ha Eliminado Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Eliminar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public CProducto buscarProducto(string id)
        {
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE idProducto = " + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        return producto;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
        
        public void reactivaProducto(string id)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PRODUCTOS SET estado = 'Activo' WHERE estado = 'Baja' AND idProducto=" + id;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Producto Se Reactivo Sin Problemas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Activar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void modificarProducto(string IdProducto, string Nombre, int Categoria, float PrecioC, float PrecioP, int Existencia, string IdProveedor, string Estado)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PRODUCTOS SET nombre=@nombre, categoria=@categoria, precioC=@precioC, precioP=@precioP, existencia=@existencia, idProveedor=@idProveedor, estado=@estado WHERE idProducto=@idProducto";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@categoria", Categoria);
                cmd.Parameters.AddWithValue("@precioC", PrecioC);
                cmd.Parameters.AddWithValue("@precioP", PrecioP);
                cmd.Parameters.AddWithValue("@existencia", Existencia);
                cmd.Parameters.AddWithValue("@idProveedor", IdProveedor);
                cmd.Parameters.AddWithValue("@estado", Estado);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("El Producto Se Actualizo Satisfactoriamente: ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Actualizar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region VENTAS
        public int insertaVenta(string id, float tot)
        {
            int folio = 0;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand("sp_INSERTAVENTA", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter pIdCliente = new SqlParameter("@cliente", System.Data.SqlDbType.VarChar);
                pIdCliente.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(pIdCliente);
                pIdCliente.Value = id;

                SqlParameter pTotal = new SqlParameter("@total", System.Data.SqlDbType.Float);
                pTotal.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(pTotal);
                pTotal.Value = tot;

                SqlParameter pFolio = new SqlParameter("@folio", System.Data.SqlDbType.Int);
                pFolio.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(pFolio);
                pFolio.Value = 1;

                cnn.Open();
                cmd.ExecuteNonQuery();
                folio = (int)cmd.Parameters["@folio"].Value;
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Activar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return folio;
        }

        public void agregarDetalleVenta(int Folio, string IdProducto, int Cantidad, float Precio, float Subtotal)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "INSERT INTO DETALLEVENTA(folio,idProducto,cantidad,precio,subtotal) VALUES(@folio,@idProducto,@cantidad,@precio,@subtotal)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@folio", Folio);
                cmd.Parameters.AddWithValue("@idProducto", IdProducto);
                cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@precio", Precio);
                cmd.Parameters.AddWithValue("@subtotal", Subtotal);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al insertar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Reportes
        public List<CVentas> leerVentas(string fIni, string fFin)
        {
            List<CVentas> lV = new List<CVentas>();
            CVentas venta;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM VENTAS WHERE fecha > convert(datetime, '" + fIni + "', 121) AND fecha < convert(datetime, '" + fFin + "', 121) AND idCliente<>'Mostrador'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        venta = new CVentas();
                        venta.folio = reader.GetInt32(0);
                        venta.idCliente = reader.GetString(1);
                        venta.fecha = reader.GetDateTime(2);
                        venta.total = Convert.ToSingle(reader.GetDecimal(3));
                        
                        lV.Add(venta);
                    }
                }
                cnn.Close();
                return lV;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CCliente> leerClientesRep()
        {
            List<CCliente> lC = new List<CCliente>();
            CCliente cliente;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT CLIENTES.idTelefono, CLIENTES.nombre, CLIENTES.apellido, SUM(VENTAS.total) AS SUMA  FROM CLIENTES,VENTAS "+
                                "WHERE CLIENTES.idTelefono=VENTAS.idCliente AND VENTAS.idCliente<>'Mostrador'" +
                                "GROUP BY CLIENTES.idTelefono,CLIENTES.nombre, CLIENTES.apellido";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente = new CCliente();
                        cliente.idTelefono = reader.GetString(0);
                        cliente.nombre = reader.GetString(1);
                        cliente.apellidos = reader.GetString(2);
                        cliente.suma = Convert.ToSingle(reader.GetDecimal(3));

                        lC.Add(cliente);
                    }
                }
                cnn.Close();
                return lC;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerProductosProv(string idProv)
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE idProveedor = '"+idProv+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerProductosCat(int cat)
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE categoria = " + cat;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerProductosMC()
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT p.idProducto, p.nombre, p.categoria, p.precioC, p.precioP, p.existencia, p.idProveedor, SUM(v.cantidad) AS SUMA "+  
                                "FROM PRODUCTOS AS p, DETALLEVENTA AS v "+
                                "WHERE p.idProducto=v.idProducto "+
                                "GROUP BY p.idProducto, p.nombre, p.categoria, p.precioC, p.precioP, p.existencia, p.idProveedor";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.suma = reader.GetInt32(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CProducto> leerProductosEsc()
        {
            List<CProducto> lP = new List<CProducto>();
            CProducto producto;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PRODUCTOS WHERE existencia < 5 ORDER BY existencia";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto = new CProducto();
                        producto.idProducto = reader.GetString(0);
                        producto.nombre = reader.GetString(1);
                        producto.categoria = reader.GetInt32(2);
                        producto.precioC = Convert.ToSingle(reader.GetDecimal(3));
                        producto.precioP = Convert.ToSingle(reader.GetDecimal(4));
                        producto.existencia = reader.GetInt32(5);
                        producto.idProveedor = reader.GetString(6);
                        producto.estado = reader.GetString(7);

                        lP.Add(producto);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CPedido> leerPedidos(string fIni, string fFin)
        {
            List<CPedido> lP = new List<CPedido>();
            CPedido pedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PEDIDOSXCLIENTE WHERE fecha > convert(datetime, '" + fIni + "', 121) AND fecha < convert(datetime, '" + fFin + "', 121)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CPedido();
                        pedido.folio = reader.GetInt32(0);
                        pedido.idCliente = reader.GetString(1);
                        pedido.fecha = reader.GetDateTime(2);
                        pedido.estado = reader.GetString(3);
                        pedido.total = Convert.ToSingle(reader.GetDecimal(4));
                        //pedido.comentario = reader.GetString(5);

                        lP.Add(pedido);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CPedido> leerPedidosCli(string idCli)
        {
            List<CPedido> lP = new List<CPedido>();
            CPedido pedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PEDIDOSXCLIENTE WHERE idCliente = '"+idCli+"'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CPedido();
                        pedido.folio = reader.GetInt32(0);
                        pedido.idCliente = reader.GetString(1);
                        pedido.fecha = reader.GetDateTime(2);
                        pedido.estado = reader.GetString(3);
                        pedido.total = Convert.ToSingle(reader.GetDecimal(4));
                        //pedido.comentario = reader.GetString(5);

                        lP.Add(pedido);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CDetalleVenta> leerDetalleVenta(int folio)
        {
            List<CDetalleVenta> lDV = new List<CDetalleVenta>();
            CDetalleVenta dVenta;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM DETALLEVENTA WHERE folio = " + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dVenta = new CDetalleVenta();
                        dVenta.id = reader.GetInt32(0);
                        dVenta.folio = reader.GetInt32(1);
                        dVenta.idProducto = reader.GetString(2);
                        dVenta.cantidad = reader.GetInt32(3);
                        dVenta.precio = Convert.ToSingle(reader.GetDecimal(4));
                        dVenta.subtotal = Convert.ToSingle(reader.GetDecimal(5));
                        
                        lDV.Add(dVenta);
                    }
                }
                cnn.Close();
                return lDV;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CDetallePedido> leerDetallePedido(int folio)
        {
            List<CDetallePedido> lDP = new List<CDetallePedido>();
            CDetallePedido dPedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM DETALLEPXC WHERE folio = " + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dPedido = new CDetallePedido();
                        dPedido.id = reader.GetInt32(0);
                        dPedido.folio = reader.GetInt32(1);
                        dPedido.idProducto = reader.GetString(2);
                        dPedido.cantidad = reader.GetInt32(3);
                        dPedido.precio = Convert.ToSingle(reader.GetDecimal(4));
                        dPedido.subtotal = Convert.ToSingle(reader.GetDecimal(5));

                        lDP.Add(dPedido);
                    }
                }
                cnn.Close();
                return lDP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
        #endregion

        public List<CPedido> leerPedidosEspera()
        {
            List<CPedido> lP = new List<CPedido>();
            CPedido pedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PEDIDOSXCLIENTE WHERE estado='En Espera'";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CPedido();
                        pedido.folio = reader.GetInt32(0);
                        pedido.idCliente = reader.GetString(1);
                        pedido.fecha = reader.GetDateTime(2);
                        pedido.estado = reader.GetString(3);
                        pedido.total = Convert.ToSingle(reader.GetDecimal(4));
                        //pedido.comentario = reader.GetString(5);

                        lP.Add(pedido);
                    }
                }
                cnn.Close();
                return lP;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void cambiaEstadoPed(int folio)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "UPDATE PEDIDOSXCLIENTE SET estado = 'Realizado' WHERE folio=" + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Activar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public CVentas buscarVenta(int folio)
        {
            CVentas venta;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM VENTAS WHERE folio = " + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        venta = new CVentas();
                        venta.folio = reader.GetInt32(0);
                        venta.idCliente = reader.GetString(1);
                        venta.fecha = reader.GetDateTime(2);
                        venta.total = Convert.ToSingle(reader.GetDecimal(3));

                        return venta;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public CPedido buscarPedido(int folio)
        {
            CPedido pedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                string query = "SELECT * FROM PEDIDOSXCLIENTE WHERE folio = " + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CPedido();
                        pedido.folio = reader.GetInt32(0);
                        pedido.idCliente = reader.GetString(1);
                        pedido.fecha = reader.GetDateTime(2);
                        pedido.estado = reader.GetString(3);
                        pedido.total = Convert.ToSingle(reader.GetDecimal(4));
                        //pedido.comentario = reader.GetString(5);

                        return pedido;
                    }
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Ocurrió un error al Leer:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public List<CDetallePedido> leerDetallePedido(string folio)
        {
            List<CDetallePedido> lPA = new List<CDetallePedido>();
            CDetallePedido pedido;

            try
            {
                SqlConnection cnn = new SqlConnection(conexion);
                cnn.Open();
                string query = "SELECT * FROM DETALLEPXC WHERE folio =" + folio;
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pedido = new CDetallePedido();
                        pedido.id = reader.GetInt32(0);
                        pedido.folio = reader.GetInt32(1);
                        pedido.idProducto = reader.GetString(2);
                        pedido.cantidad = reader.GetInt32(3);
                        pedido.precio = Convert.ToSingle(reader.GetDecimal(4));
                        pedido.subtotal = Convert.ToSingle(reader.GetDecimal(5));

                        lPA.Add(pedido);
                    }
                }
                cnn.Close();
                return lPA;
            }
            catch (SqlException e)
            {

            }

            return null;
        }
    }
}

