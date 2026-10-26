namespace ProyectoEventos.Forms
{
    partial class FrmProductos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tlpRaiz = new TableLayoutPanel();
            tlpFormulario = new TableLayoutPanel();
            tlpBotones = new TableLayoutPanel();
            btnCancelarEdicion = new Button();
            lblModo = new Label();
            lblAviso = new Label();
            tlpGrilla = new TableLayoutPanel();
            lblNombre = new Label();
            txtBoxNombre = new TextBox();
            lblPrecioAdulto = new Label();
            numPrecioAdulto = new NumericUpDown();
            lblPrecioNino = new Label();
            numPrecioNino = new NumericUpDown();
            lblEnumCategoria = new Label();
            cmbEnumCategoria = new ComboBox();
            lblRestriccion = new Label();
            cmbRestriccion = new ComboBox();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            lblDgvProductos = new Label();
            dgvProductos = new DataGridView();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            btnVolver = new Button();
            tlpRestricciones = new TableLayoutPanel();
            lblRestricciones = new Label();
            lblAyudaRestricciones = new Label();
            txtNuevaRestriccion = new TextBox();
            btnAgregarRestriccion = new Button();
            btnQuitarRestriccion = new Button();
            cmbRestriccionesLista = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numPrecioAdulto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioNino).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpFormulario.SuspendLayout();
            tlpBotones.SuspendLayout();
            tlpGrilla.SuspendLayout();
            tlpRestricciones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 2;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            tlpRaiz.Controls.Add(tlpFormulario, 0, 1);
            tlpRaiz.Controls.Add(tlpGrilla, 1, 1);
            tlpRaiz.Controls.Add(btnVolver, 0, 0);
            tlpRaiz.SetColumnSpan(btnVolver, 2);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Padding = new Padding(40, 10, 40, 30);
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // tlpFormulario
            // 
            tlpFormulario.ColumnCount = 2;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.Controls.Add(lblModo, 0, 0);
            tlpFormulario.SetColumnSpan(lblModo, 2);
            tlpFormulario.Controls.Add(lblNombre, 0, 1);
            tlpFormulario.SetColumnSpan(lblNombre, 2);
            tlpFormulario.Controls.Add(txtBoxNombre, 0, 2);
            tlpFormulario.SetColumnSpan(txtBoxNombre, 2);
            tlpFormulario.Controls.Add(lblPrecioAdulto, 0, 3);
            tlpFormulario.Controls.Add(lblPrecioNino, 1, 3);
            tlpFormulario.Controls.Add(numPrecioAdulto, 0, 4);
            tlpFormulario.Controls.Add(numPrecioNino, 1, 4);
            tlpFormulario.Controls.Add(lblEnumCategoria, 0, 5);
            tlpFormulario.Controls.Add(lblRestriccion, 1, 5);
            tlpFormulario.Controls.Add(cmbEnumCategoria, 0, 6);
            tlpFormulario.Controls.Add(cmbRestriccion, 1, 6);
            tlpFormulario.Controls.Add(tlpBotones, 0, 7);
            tlpFormulario.SetColumnSpan(tlpBotones, 2);
            tlpFormulario.Controls.Add(lblAviso, 0, 8);
            tlpFormulario.SetColumnSpan(lblAviso, 2);
            tlpFormulario.Controls.Add(tlpRestricciones, 0, 9);
            tlpFormulario.SetColumnSpan(tlpRestricciones, 2);
            tlpFormulario.Dock = DockStyle.Fill;
            tlpFormulario.Margin = new Padding(0, 0, 40, 0);
            tlpFormulario.Name = "tlpFormulario";
            tlpFormulario.RowCount = 10;
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // tlpBotones
            // 
            tlpBotones.AutoSize = true;
            tlpBotones.ColumnCount = 2;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.Controls.Add(btnGuardar, 0, 0);
            tlpBotones.SetColumnSpan(btnGuardar, 2);
            tlpBotones.Controls.Add(btnEditar, 0, 1);
            tlpBotones.Controls.Add(btnEliminar, 1, 1);
            tlpBotones.Controls.Add(btnCancelarEdicion, 1, 0);
            tlpBotones.Dock = DockStyle.Top;
            tlpBotones.Margin = new Padding(3, 16, 3, 3);
            tlpBotones.Name = "tlpBotones";
            tlpBotones.RowCount = 2;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            btnGuardar.Dock = DockStyle.Fill;
            btnEditar.Dock = DockStyle.Fill;
            btnEliminar.Dock = DockStyle.Fill;
            // 
            // btnCancelarEdicion
            // 
            btnCancelarEdicion.Dock = DockStyle.Fill;
            btnCancelarEdicion.Margin = new Padding(8, 0, 0, 8);
            btnCancelarEdicion.Name = "btnCancelarEdicion";
            btnCancelarEdicion.Size = new Size(94, 38);
            btnCancelarEdicion.TabIndex = 30;
            btnCancelarEdicion.Text = "Cancelar";
            btnCancelarEdicion.UseVisualStyleBackColor = true;
            btnCancelarEdicion.Visible = false;
            btnCancelarEdicion.Click += btnCancelarEdicion_Click;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Margin = new Padding(3, 0, 3, 2);
            lblModo.Name = "lblModo";
            lblModo.TabIndex = 31;
            lblModo.Text = "Nuevo producto";
            // 
            // lblAviso
            // 
            lblAviso.AutoSize = true;
            lblAviso.Margin = new Padding(3, 6, 3, 0);
            lblAviso.Name = "lblAviso";
            lblAviso.TabIndex = 32;
            // 
            // tlpGrilla
            // 
            tlpGrilla.ColumnCount = 3;
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tlpGrilla.Controls.Add(lblDgvProductos, 0, 0);
            tlpGrilla.Controls.Add(lblBuscar, 1, 0);
            tlpGrilla.Controls.Add(txtBuscar, 2, 0);
            tlpGrilla.Controls.Add(dgvProductos, 0, 1);
            tlpGrilla.SetColumnSpan(dgvProductos, 3);
            // 
            // lblBuscar
            // 
            lblBuscar.Anchor = AnchorStyles.Right;
            lblBuscar.AutoSize = true;
            lblBuscar.Margin = new Padding(3, 12, 6, 3);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.TabIndex = 19;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Margin = new Padding(3, 12, 3, 3);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar...";
            txtBuscar.TabIndex = 20;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            tlpGrilla.Dock = DockStyle.Fill;
            tlpGrilla.Name = "tlpGrilla";
            tlpGrilla.RowCount = 2;
            tlpGrilla.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpGrilla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Margin = new Padding(3, 8, 3, 3);
            lblNombre.Text = "Nombre: *";
            lblNombre.Name = "lblNombre";
            lblNombre.TabIndex = 0;
            // 
            // txtBoxNombre
            // 
            txtBoxNombre.Dock = DockStyle.Fill;
            txtBoxNombre.Name = "txtBoxNombre";
            txtBoxNombre.TabIndex = 1;
            // 
            // lblPrecioAdulto
            // 
            lblPrecioAdulto.AutoSize = true;
            lblPrecioAdulto.Margin = new Padding(3, 8, 3, 3);
            lblPrecioAdulto.Text = "Precio adulto: *";
            lblPrecioAdulto.Name = "lblPrecioAdulto";
            lblPrecioAdulto.TabIndex = 2;
            // 
            // numPrecioAdulto
            // 
            numPrecioAdulto.Dock = DockStyle.Fill;
            numPrecioAdulto.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numPrecioAdulto.Minimum = new decimal(new int[] { -727379968, 232, 0, -2147483648 });
            numPrecioAdulto.DecimalPlaces = 2;
            numPrecioAdulto.Name = "numPrecioAdulto";
            numPrecioAdulto.TabIndex = 3;
            // 
            // lblPrecioNino
            // 
            lblPrecioNino.AutoSize = true;
            lblPrecioNino.Margin = new Padding(3, 8, 3, 3);
            lblPrecioNino.Text = "Precio niño: *";
            lblPrecioNino.Name = "lblPrecioNino";
            lblPrecioNino.TabIndex = 4;
            // 
            // numPrecioNino
            // 
            numPrecioNino.Dock = DockStyle.Fill;
            numPrecioNino.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numPrecioNino.Minimum = new decimal(new int[] { -727379968, 232, 0, -2147483648 });
            numPrecioNino.DecimalPlaces = 2;
            numPrecioNino.Name = "numPrecioNino";
            numPrecioNino.TabIndex = 5;
            // 
            // lblEnumCategoria
            // 
            lblEnumCategoria.AutoSize = true;
            lblEnumCategoria.Margin = new Padding(3, 8, 3, 3);
            lblEnumCategoria.Text = "Categoría:";
            lblEnumCategoria.Name = "lblEnumCategoria";
            lblEnumCategoria.TabIndex = 6;
            // 
            // cmbEnumCategoria
            // 
            cmbEnumCategoria.Dock = DockStyle.Fill;
            cmbEnumCategoria.FormattingEnabled = true;
            cmbEnumCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEnumCategoria.Name = "cmbEnumCategoria";
            cmbEnumCategoria.TabIndex = 7;
            // 
            // lblRestriccion
            // 
            lblRestriccion.AutoSize = true;
            lblRestriccion.Margin = new Padding(3, 8, 3, 3);
            lblRestriccion.Text = "Restricción:";
            lblRestriccion.Name = "lblRestriccion";
            lblRestriccion.TabIndex = 8;
            // 
            // cmbRestriccion
            // 
            cmbRestriccion.Dock = DockStyle.Fill;
            cmbRestriccion.FormattingEnabled = true;
            cmbRestriccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRestriccion.Name = "cmbRestriccion";
            cmbRestriccion.TabIndex = 9;
            // 
            // btnGuardar
            // 
            btnGuardar.Margin = new Padding(0, 0, 0, 8);
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 10;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0);
            btnEditar.Size = new Size(94, 29);
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 11;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(8, 0, 0, 0);
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.TabIndex = 12;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblDgvProductos
            // 
            lblDgvProductos.AutoSize = true;
            lblDgvProductos.Margin = new Padding(3, 12, 3, 3);
            lblDgvProductos.Text = "Productos";
            lblDgvProductos.Name = "lblDgvProductos";
            lblDgvProductos.TabIndex = 13;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.TabIndex = 14;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Right;
            btnVolver.Margin = new Padding(3, 3, 3, 10);
            btnVolver.Size = new Size(94, 29);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Name = "btnVolver";
            btnVolver.TabIndex = 15;
            btnVolver.Click += btnVolver_Click;
            //
            // tlpRestricciones
            //
            tlpRestricciones.AutoSize = true;
            tlpRestricciones.ColumnCount = 2;
            tlpRestricciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRestricciones.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRestricciones.Controls.Add(lblRestricciones, 0, 0);
            tlpRestricciones.SetColumnSpan(lblRestricciones, 2);
            tlpRestricciones.Controls.Add(lblAyudaRestricciones, 0, 1);
            tlpRestricciones.SetColumnSpan(lblAyudaRestricciones, 2);
            tlpRestricciones.Controls.Add(txtNuevaRestriccion, 0, 2);
            tlpRestricciones.Controls.Add(btnAgregarRestriccion, 1, 2);
            tlpRestricciones.Controls.Add(cmbRestriccionesLista, 0, 3);
            tlpRestricciones.Controls.Add(btnQuitarRestriccion, 1, 3);
            tlpRestricciones.Dock = DockStyle.Top;
            tlpRestricciones.Margin = new Padding(0, 24, 0, 0);
            tlpRestricciones.Name = "tlpRestricciones";
            tlpRestricciones.RowCount = 4;
            tlpRestricciones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRestricciones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRestricciones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRestricciones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRestricciones.Tag = Utils.Formulario.NoEsDelFormulario;
            //
            // lblRestricciones
            //
            lblRestricciones.AutoSize = true;
            lblRestricciones.Margin = new Padding(3, 0, 3, 4);
            lblRestricciones.Name = "lblRestricciones";
            lblRestricciones.Text = "Restricciones alimenticias";
            //
            // lblAyudaRestricciones
            //
            lblAyudaRestricciones.AutoSize = true;
            lblAyudaRestricciones.Margin = new Padding(3, 0, 3, 8);
            lblAyudaRestricciones.MaximumSize = new Size(460, 0);
            lblAyudaRestricciones.Name = "lblAyudaRestricciones";
            lblAyudaRestricciones.Text = "Las que agregue aparecen en el combo Restricción del producto.";
            //
            // txtNuevaRestriccion
            //
            txtNuevaRestriccion.Dock = DockStyle.Fill;
            txtNuevaRestriccion.Margin = new Padding(3, 3, 8, 8);
            txtNuevaRestriccion.MaxLength = 40;
            txtNuevaRestriccion.Name = "txtNuevaRestriccion";
            txtNuevaRestriccion.PlaceholderText = "Nueva restricción (por ej. Vegano)";
            txtNuevaRestriccion.TabIndex = 40;
            //
            // btnAgregarRestriccion
            //
            btnAgregarRestriccion.Dock = DockStyle.Fill;
            btnAgregarRestriccion.Margin = new Padding(0, 0, 3, 8);
            btnAgregarRestriccion.Name = "btnAgregarRestriccion";
            btnAgregarRestriccion.TabIndex = 41;
            btnAgregarRestriccion.Text = "Agregar";
            btnAgregarRestriccion.UseVisualStyleBackColor = true;
            btnAgregarRestriccion.Click += btnAgregarRestriccion_Click;
            //
            // btnQuitarRestriccion
            //
            btnQuitarRestriccion.Dock = DockStyle.Fill;
            btnQuitarRestriccion.Margin = new Padding(0, 0, 3, 0);
            btnQuitarRestriccion.Name = "btnQuitarRestriccion";
            btnQuitarRestriccion.TabIndex = 43;
            btnQuitarRestriccion.Text = "Quitar";
            btnQuitarRestriccion.UseVisualStyleBackColor = true;
            btnQuitarRestriccion.Click += btnQuitarRestriccion_Click;
            //
            // cmbRestriccionesLista
            //
            cmbRestriccionesLista.Dock = DockStyle.Fill;
            cmbRestriccionesLista.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRestriccionesLista.FormattingEnabled = true;
            cmbRestriccionesLista.Margin = new Padding(3, 3, 8, 0);
            cmbRestriccionesLista.Name = "cmbRestriccionesLista";
            cmbRestriccionesLista.TabIndex = 42;
            //
            // FrmProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            MinimumSize = new Size(1000, 650);
            Name = "FrmProductos";
            Text = "Gestión de productos";
            WindowState = FormWindowState.Maximized;
            Load += FrmProductos_Load;
            FormClosing += FrmProductos_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numPrecioAdulto).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioNino).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tlpRestricciones.ResumeLayout(false);
            tlpRestricciones.PerformLayout();
            tlpGrilla.ResumeLayout(false);
            tlpGrilla.PerformLayout();
            tlpBotones.ResumeLayout(false);
            tlpBotones.PerformLayout();
            tlpFormulario.ResumeLayout(false);
            tlpFormulario.PerformLayout();
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpFormulario;
        private TableLayoutPanel tlpBotones;
        private Button btnCancelarEdicion;
        private Label lblModo;
        private Label lblAviso;
        private TableLayoutPanel tlpGrilla;
        private Label lblNombre;
        private TextBox txtBoxNombre;
        private Label lblPrecioAdulto;
        private NumericUpDown numPrecioAdulto;
        private Label lblPrecioNino;
        private NumericUpDown numPrecioNino;
        private Label lblEnumCategoria;
        private ComboBox cmbEnumCategoria;
        private Label lblRestriccion;
        private ComboBox cmbRestriccion;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Label lblDgvProductos;
        private DataGridView dgvProductos;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Button btnVolver;
        private TableLayoutPanel tlpRestricciones;
        private Label lblRestricciones;
        private Label lblAyudaRestricciones;
        private TextBox txtNuevaRestriccion;
        private Button btnAgregarRestriccion;
        private Button btnQuitarRestriccion;
        private ComboBox cmbRestriccionesLista;
    }
}
