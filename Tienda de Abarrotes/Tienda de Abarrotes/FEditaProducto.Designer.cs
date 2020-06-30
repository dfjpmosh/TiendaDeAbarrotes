namespace Tienda_de_Abarrotes
{
    partial class FEditaProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEditaProducto));
            this.tbNuevaCate = new System.Windows.Forms.TextBox();
            this.btnAgreCategoria = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cbEstadoProd = new System.Windows.Forms.ComboBox();
            this.cbProveedorAgre = new System.Windows.Forms.ComboBox();
            this.cbCategoriaAgre = new System.Windows.Forms.ComboBox();
            this.npdExistencia = new System.Windows.Forms.NumericUpDown();
            this.npdPVenta = new System.Windows.Forms.NumericUpDown();
            this.npdPCosto = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbNombreProd = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbIdProd = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.npdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npdPVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npdPCosto)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNuevaCate
            // 
            this.tbNuevaCate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNuevaCate.Location = new System.Drawing.Point(327, 92);
            this.tbNuevaCate.MaxLength = 29;
            this.tbNuevaCate.Name = "tbNuevaCate";
            this.tbNuevaCate.Size = new System.Drawing.Size(155, 26);
            this.tbNuevaCate.TabIndex = 48;
            this.tbNuevaCate.Text = "Escribir nueva categoria";
            this.tbNuevaCate.Click += new System.EventHandler(this.tbNuevaCate_Click);
            // 
            // btnAgreCategoria
            // 
            this.btnAgreCategoria.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgreCategoria.Location = new System.Drawing.Point(246, 90);
            this.btnAgreCategoria.Name = "btnAgreCategoria";
            this.btnAgreCategoria.Size = new System.Drawing.Size(75, 27);
            this.btnAgreCategoria.TabIndex = 47;
            this.btnAgreCategoria.Text = "Agregar";
            this.btnAgreCategoria.UseVisualStyleBackColor = true;
            this.btnAgreCategoria.Click += new System.EventHandler(this.btnAgreCategoria_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(205, 157);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(13, 13);
            this.label22.TabIndex = 46;
            this.label22.Text = "$";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(205, 126);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(13, 13);
            this.label21.TabIndex = 45;
            this.label21.Text = "$";
            // 
            // cbEstadoProd
            // 
            this.cbEstadoProd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEstadoProd.FormattingEnabled = true;
            this.cbEstadoProd.Items.AddRange(new object[] {
            "Activo",
            "Baja"});
            this.cbEstadoProd.Location = new System.Drawing.Point(119, 246);
            this.cbEstadoProd.Name = "cbEstadoProd";
            this.cbEstadoProd.Size = new System.Drawing.Size(121, 27);
            this.cbEstadoProd.TabIndex = 44;
            this.cbEstadoProd.Text = "Activo";
            // 
            // cbProveedorAgre
            // 
            this.cbProveedorAgre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedorAgre.FormattingEnabled = true;
            this.cbProveedorAgre.Location = new System.Drawing.Point(119, 215);
            this.cbProveedorAgre.Name = "cbProveedorAgre";
            this.cbProveedorAgre.Size = new System.Drawing.Size(363, 27);
            this.cbProveedorAgre.TabIndex = 43;
            this.cbProveedorAgre.Text = "Selecciona un Proveedor";
            // 
            // cbCategoriaAgre
            // 
            this.cbCategoriaAgre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategoriaAgre.FormattingEnabled = true;
            this.cbCategoriaAgre.Location = new System.Drawing.Point(119, 90);
            this.cbCategoriaAgre.Name = "cbCategoriaAgre";
            this.cbCategoriaAgre.Size = new System.Drawing.Size(121, 27);
            this.cbCategoriaAgre.Sorted = true;
            this.cbCategoriaAgre.TabIndex = 42;
            this.cbCategoriaAgre.Text = "-Sin Categoria";
            // 
            // npdExistencia
            // 
            this.npdExistencia.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npdExistencia.Location = new System.Drawing.Point(120, 185);
            this.npdExistencia.Name = "npdExistencia";
            this.npdExistencia.Size = new System.Drawing.Size(120, 26);
            this.npdExistencia.TabIndex = 41;
            // 
            // npdPVenta
            // 
            this.npdPVenta.DecimalPlaces = 2;
            this.npdPVenta.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npdPVenta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.npdPVenta.Location = new System.Drawing.Point(120, 154);
            this.npdPVenta.Name = "npdPVenta";
            this.npdPVenta.Size = new System.Drawing.Size(120, 26);
            this.npdPVenta.TabIndex = 40;
            // 
            // npdPCosto
            // 
            this.npdPCosto.DecimalPlaces = 2;
            this.npdPCosto.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npdPCosto.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.npdPCosto.Location = new System.Drawing.Point(120, 123);
            this.npdPCosto.Name = "npdPCosto";
            this.npdPCosto.Size = new System.Drawing.Size(120, 26);
            this.npdPCosto.TabIndex = 39;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(53, 249);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 19);
            this.label18.TabIndex = 38;
            this.label18.Text = "Estado:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(31, 218);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 19);
            this.label19.TabIndex = 37;
            this.label19.Text = "Proveedor:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(31, 187);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 19);
            this.label20.TabIndex = 36;
            this.label20.Text = "Existencia:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.AutoSize = true;
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Image = global::Tienda_de_Abarrotes.Properties.Resources.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(306, 300);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(51, 51);
            this.btnAceptar.TabIndex = 35;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnModificaProd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 19);
            this.label11.TabIndex = 34;
            this.label11.Text = "Precio Venta:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 19);
            this.label13.TabIndex = 33;
            this.label13.Text = "Precio Costo:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(34, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 19);
            this.label15.TabIndex = 32;
            this.label15.Text = "Categoria:";
            // 
            // tbNombreProd
            // 
            this.tbNombreProd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNombreProd.Location = new System.Drawing.Point(120, 60);
            this.tbNombreProd.MaxLength = 30;
            this.tbNombreProd.Name = "tbNombreProd";
            this.tbNombreProd.Size = new System.Drawing.Size(362, 26);
            this.tbNombreProd.TabIndex = 31;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(46, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 19);
            this.label16.TabIndex = 30;
            this.label16.Text = "Nombre:";
            // 
            // tbIdProd
            // 
            this.tbIdProd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIdProd.Location = new System.Drawing.Point(120, 29);
            this.tbIdProd.MaxLength = 10;
            this.tbIdProd.Name = "tbIdProd";
            this.tbIdProd.ReadOnly = true;
            this.tbIdProd.Size = new System.Drawing.Size(120, 26);
            this.tbIdProd.TabIndex = 29;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(53, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 19);
            this.label17.TabIndex = 28;
            this.label17.Text = "Codigo:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.BackgroundImage = global::Tienda_de_Abarrotes.Properties.Resources.Cancelar;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = global::Tienda_de_Abarrotes.Properties.Resources.Cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(140, 300);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(56, 56);
            this.btnCancelar.TabIndex = 49;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FEditaProducto
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tienda_de_Abarrotes.Properties.Resources.abarrotestr12;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(496, 380);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.tbNuevaCate);
            this.Controls.Add(this.btnAgreCategoria);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cbEstadoProd);
            this.Controls.Add(this.cbProveedorAgre);
            this.Controls.Add(this.cbCategoriaAgre);
            this.Controls.Add(this.npdExistencia);
            this.Controls.Add(this.npdPVenta);
            this.Controls.Add(this.npdPCosto);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbNombreProd);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tbIdProd);
            this.Controls.Add(this.label17);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FEditaProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Producto";
            ((System.ComponentModel.ISupportInitialize)(this.npdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npdPVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npdPCosto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNuevaCate;
        private System.Windows.Forms.Button btnAgreCategoria;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbEstadoProd;
        private System.Windows.Forms.ComboBox cbProveedorAgre;
        private System.Windows.Forms.ComboBox cbCategoriaAgre;
        private System.Windows.Forms.NumericUpDown npdExistencia;
        private System.Windows.Forms.NumericUpDown npdPVenta;
        private System.Windows.Forms.NumericUpDown npdPCosto;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbNombreProd;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbIdProd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnCancelar;
    }
}