namespace ProyectoEventos.Forms
{
    partial class FrmLugares
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
            lblCostoBase = new Label();
            numCosto = new NumericUpDown();
            lblCostoHora = new Label();
            numCostoHora = new NumericUpDown();
            lblCapacidad = new Label();
            numCapacidad = new NumericUpDown();
            lblUbicacion = new Label();
            txtBoxUbicacion = new TextBox();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            lblDgvLugares = new Label();
            dgvLugares = new DataGridView();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)numCosto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCostoHora).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCapacidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLugares).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpFormulario.SuspendLayout();
            tlpBotones.SuspendLayout();
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
            tlpFormulario.ColumnCount = 2;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.Controls.Add(lblModo, 0, 0);
            tlpFormulario.Controls.Add(lblNombre, 0, 1);
            tlpFormulario.Controls.Add(txtBoxNombre, 0, 2);
            tlpFormulario.Controls.Add(lblCostoBase, 0, 3);
            tlpFormulario.Controls.Add(numCosto, 0, 4);
            tlpFormulario.Controls.Add(lblCostoHora, 1, 3);
            tlpFormulario.Controls.Add(numCostoHora, 1, 4);
            tlpFormulario.Controls.Add(lblCapacidad, 0, 5);
            tlpFormulario.Controls.Add(numCapacidad, 0, 6);
            tlpFormulario.Controls.Add(lblUbicacion, 0, 7);
            tlpFormulario.Controls.Add(txtBoxUbicacion, 0, 8);
            tlpFormulario.Controls.Add(tlpBotones, 0, 9);
            tlpFormulario.Controls.Add(lblAviso, 0, 10);
            tlpFormulario.SetColumnSpan(lblModo, 2);
            tlpFormulario.SetColumnSpan(lblNombre, 2);
            tlpFormulario.SetColumnSpan(txtBoxNombre, 2);
            tlpFormulario.SetColumnSpan(lblCapacidad, 2);
            tlpFormulario.SetColumnSpan(numCapacidad, 2);
            tlpFormulario.SetColumnSpan(lblUbicacion, 2);
            tlpFormulario.SetColumnSpan(txtBoxUbicacion, 2);
            tlpFormulario.SetColumnSpan(tlpBotones, 2);
            tlpFormulario.SetColumnSpan(lblAviso, 2);
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
            btnCancelarEdicion.Margin = new Padding(0, 0, 0, 8);
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
            lblModo.Text = "Nuevo lugar";
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
            tlpGrilla.Controls.Add(lblDgvLugares, 0, 0);
            tlpGrilla.Controls.Add(lblBuscar, 1, 0);
            tlpGrilla.Controls.Add(txtBuscar, 2, 0);
            tlpGrilla.Controls.Add(dgvLugares, 0, 1);
            tlpGrilla.SetColumnSpan(dgvLugares, 3);
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
            // lblCostoBase
            // 
            lblCostoBase.AutoSize = true;
            lblCostoBase.Margin = new Padding(3, 8, 3, 3);
            lblCostoBase.Text = "Costo base:";
            lblCostoBase.Name = "lblCostoBase";
            lblCostoBase.TabIndex = 2;
            // 
            // numCosto
            // 
            numCosto.Dock = DockStyle.Fill;
            numCosto.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numCosto.Minimum = new decimal(new int[] { -727379968, 232, 0, -2147483648 });
            numCosto.DecimalPlaces = 2;
            numCosto.Name = "numCosto";
            numCosto.TabIndex = 3;
            // 
            // lblCostoHora
            // 
            lblCostoHora.AutoSize = true;
            lblCostoHora.Margin = new Padding(3, 8, 3, 3);
            lblCostoHora.Text = "Costo por hora:";
            lblCostoHora.Name = "lblCostoHora";
            lblCostoHora.TabIndex = 31;
            // 
            // numCostoHora
            // 
            numCostoHora.Dock = DockStyle.Fill;
            numCostoHora.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numCostoHora.Minimum = new decimal(new int[] { -727379968, 232, 0, -2147483648 });
            numCostoHora.DecimalPlaces = 2;
            numCostoHora.Name = "numCostoHora";
            numCostoHora.TabIndex = 4;
            // 
            // lblCapacidad
            // 
            lblCapacidad.AutoSize = true;
            lblCapacidad.Margin = new Padding(3, 8, 3, 3);
            lblCapacidad.Text = "Capacidad: *";
            lblCapacidad.Name = "lblCapacidad";
            lblCapacidad.TabIndex = 4;
            // 
            // numCapacidad
            // 
            numCapacidad.Dock = DockStyle.Fill;
            numCapacidad.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCapacidad.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numCapacidad.Name = "numCapacidad";
            numCapacidad.TabIndex = 5;
            // 
            // lblUbicacion
            // 
            lblUbicacion.AutoSize = true;
            lblUbicacion.Margin = new Padding(3, 8, 3, 3);
            lblUbicacion.Text = "Ubicación: *";
            lblUbicacion.Name = "lblUbicacion";
            lblUbicacion.TabIndex = 6;
            // 
            // txtBoxUbicacion
            // 
            txtBoxUbicacion.Dock = DockStyle.Fill;
            txtBoxUbicacion.Name = "txtBoxUbicacion";
            txtBoxUbicacion.TabIndex = 7;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = SystemColors.ControlLightLight;
            btnGuardar.Margin = new Padding(0, 0, 8, 8);
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 8;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0, 0, 8, 0);
            btnEditar.Size = new Size(94, 29);
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 9;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(0);
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.TabIndex = 10;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblDgvLugares
            // 
            lblDgvLugares.AutoSize = true;
            lblDgvLugares.Margin = new Padding(3, 12, 3, 3);
            lblDgvLugares.Text = "Lugares";
            lblDgvLugares.Name = "lblDgvLugares";
            lblDgvLugares.TabIndex = 11;
            // 
            // dgvLugares
            // 
            dgvLugares.AllowUserToAddRows = false;
            dgvLugares.AllowUserToDeleteRows = false;
            dgvLugares.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLugares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLugares.Dock = DockStyle.Fill;
            dgvLugares.MultiSelect = false;
            dgvLugares.ReadOnly = true;
            dgvLugares.RowHeadersWidth = 51;
            dgvLugares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLugares.Name = "dgvLugares";
            dgvLugares.TabIndex = 12;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Right;
            btnVolver.Margin = new Padding(3, 3, 3, 10);
            btnVolver.Size = new Size(94, 29);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Name = "btnVolver";
            btnVolver.TabIndex = 13;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmLugares
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            MinimumSize = new Size(1000, 650);
            Name = "FrmLugares";
            Text = "Gestión de lugares";
            WindowState = FormWindowState.Maximized;
            Load += FrmLugares_Load;
            FormClosing += FrmLugares_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numCosto).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCostoHora).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCapacidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLugares).EndInit();
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
        private Label lblCostoBase;
        private NumericUpDown numCosto;
        private Label lblCostoHora;
        private NumericUpDown numCostoHora;
        private Label lblCapacidad;
        private NumericUpDown numCapacidad;
        private Label lblUbicacion;
        private TextBox txtBoxUbicacion;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Label lblDgvLugares;
        private DataGridView dgvLugares;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Button btnVolver;
    }
}
