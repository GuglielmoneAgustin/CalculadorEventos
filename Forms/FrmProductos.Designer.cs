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
            flowBotones = new FlowLayoutPanel();
            tlpGrilla = new TableLayoutPanel();
            lblNombre = new Label();
            txtBoxNombre = new TextBox();
            lblPrecioAdulto = new Label();
            numPrecioAdulto = new NumericUpDown();
            lblPrecioNino = new Label();
            numPrecioNino = new NumericUpDown();
            lblEnumCategoria = new Label();
            cmbEnumCategoria = new ComboBox();
            lblEnumRestriccion = new Label();
            cmbEnumRestriccion = new ComboBox();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            lblDgvProductos = new Label();
            dgvProductos = new DataGridView();
            txtBuscar = new TextBox();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrecioAdulto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioNino).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpFormulario.SuspendLayout();
            flowBotones.SuspendLayout();
            tlpGrilla.SuspendLayout();
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
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormulario.Controls.Add(lblNombre, 0, 0);
            tlpFormulario.Controls.Add(txtBoxNombre, 0, 1);
            tlpFormulario.Controls.Add(lblPrecioAdulto, 0, 2);
            tlpFormulario.Controls.Add(numPrecioAdulto, 0, 3);
            tlpFormulario.Controls.Add(lblPrecioNino, 0, 4);
            tlpFormulario.Controls.Add(numPrecioNino, 0, 5);
            tlpFormulario.Controls.Add(lblEnumCategoria, 0, 6);
            tlpFormulario.Controls.Add(cmbEnumCategoria, 0, 7);
            tlpFormulario.Controls.Add(lblEnumRestriccion, 0, 8);
            tlpFormulario.Controls.Add(cmbEnumRestriccion, 0, 9);
            tlpFormulario.Controls.Add(flowBotones, 0, 10);
            tlpFormulario.Dock = DockStyle.Fill;
            tlpFormulario.Margin = new Padding(0, 0, 40, 0);
            tlpFormulario.Name = "tlpFormulario";
            tlpFormulario.RowCount = 12;
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
            // flowBotones
            // 
            flowBotones.Controls.Add(btnGuardar);
            flowBotones.Controls.Add(btnEditar);
            flowBotones.Controls.Add(btnEliminar);
            flowBotones.AutoSize = true;
            flowBotones.Dock = DockStyle.Top;
            flowBotones.Margin = new Padding(3, 25, 3, 3);
            flowBotones.WrapContents = false;
            flowBotones.Name = "flowBotones";
            // 
            // tlpGrilla
            // 
            tlpGrilla.ColumnCount = 2;
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tlpGrilla.Controls.Add(lblDgvProductos, 0, 0);
            tlpGrilla.Controls.Add(txtBuscar, 1, 0);
            tlpGrilla.Controls.Add(dgvProductos, 0, 1);
            tlpGrilla.SetColumnSpan(dgvProductos, 2);
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
            lblNombre.Margin = new Padding(3, 12, 3, 3);
            lblNombre.Text = "Nombre :";
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
            lblPrecioAdulto.Margin = new Padding(3, 12, 3, 3);
            lblPrecioAdulto.Text = "Precio adulto :";
            lblPrecioAdulto.Name = "lblPrecioAdulto";
            lblPrecioAdulto.TabIndex = 2;
            // 
            // numPrecioAdulto
            // 
            numPrecioAdulto.Dock = DockStyle.Fill;
            numPrecioAdulto.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numPrecioAdulto.Name = "numPrecioAdulto";
            numPrecioAdulto.TabIndex = 3;
            // 
            // lblPrecioNino
            // 
            lblPrecioNino.AutoSize = true;
            lblPrecioNino.Margin = new Padding(3, 12, 3, 3);
            lblPrecioNino.Text = "Precio niño :";
            lblPrecioNino.Name = "lblPrecioNino";
            lblPrecioNino.TabIndex = 4;
            // 
            // numPrecioNino
            // 
            numPrecioNino.Dock = DockStyle.Fill;
            numPrecioNino.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numPrecioNino.Name = "numPrecioNino";
            numPrecioNino.TabIndex = 5;
            // 
            // lblEnumCategoria
            // 
            lblEnumCategoria.AutoSize = true;
            lblEnumCategoria.Margin = new Padding(3, 12, 3, 3);
            lblEnumCategoria.Text = "Categoría :";
            lblEnumCategoria.Name = "lblEnumCategoria";
            lblEnumCategoria.TabIndex = 6;
            // 
            // cmbEnumCategoria
            // 
            cmbEnumCategoria.Dock = DockStyle.Fill;
            cmbEnumCategoria.FormattingEnabled = true;
            cmbEnumCategoria.Name = "cmbEnumCategoria";
            cmbEnumCategoria.TabIndex = 7;
            // 
            // lblEnumRestriccion
            // 
            lblEnumRestriccion.AutoSize = true;
            lblEnumRestriccion.Margin = new Padding(3, 12, 3, 3);
            lblEnumRestriccion.Text = "Restricción :";
            lblEnumRestriccion.Name = "lblEnumRestriccion";
            lblEnumRestriccion.TabIndex = 8;
            // 
            // cmbEnumRestriccion
            // 
            cmbEnumRestriccion.Dock = DockStyle.Fill;
            cmbEnumRestriccion.FormattingEnabled = true;
            cmbEnumRestriccion.Name = "cmbEnumRestriccion";
            cmbEnumRestriccion.TabIndex = 9;
            // 
            // btnGuardar
            // 
            btnGuardar.Margin = new Padding(0, 0, 10, 0);
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 10;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0, 0, 10, 0);
            btnEditar.Size = new Size(94, 29);
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 11;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(0, 0, 10, 0);
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
            ((System.ComponentModel.ISupportInitialize)numPrecioAdulto).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioNino).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tlpGrilla.ResumeLayout(false);
            tlpGrilla.PerformLayout();
            flowBotones.ResumeLayout(false);
            flowBotones.PerformLayout();
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
        private FlowLayoutPanel flowBotones;
        private TableLayoutPanel tlpGrilla;
        private Label lblNombre;
        private TextBox txtBoxNombre;
        private Label lblPrecioAdulto;
        private NumericUpDown numPrecioAdulto;
        private Label lblPrecioNino;
        private NumericUpDown numPrecioNino;
        private Label lblEnumCategoria;
        private ComboBox cmbEnumCategoria;
        private Label lblEnumRestriccion;
        private ComboBox cmbEnumRestriccion;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Label lblDgvProductos;
        private DataGridView dgvProductos;
        private TextBox txtBuscar;
        private Button btnVolver;
    }
}
