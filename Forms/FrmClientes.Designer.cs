namespace ProyectoEventos.Forms
{
    partial class FrmClientes
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
            lblDni = new Label();
            txtBoxDni = new TextBox();
            lblMail = new Label();
            txtBoxMail = new TextBox();
            lblTelefono = new Label();
            txtBoxTelefono = new TextBox();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            lblDgvClientes = new Label();
            dgvClientes = new DataGridView();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
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
            tlpFormulario.ColumnCount = 1;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormulario.Controls.Add(lblModo, 0, 0);
            tlpFormulario.Controls.Add(lblNombre, 0, 1);
            tlpFormulario.Controls.Add(txtBoxNombre, 0, 2);
            tlpFormulario.Controls.Add(lblDni, 0, 3);
            tlpFormulario.Controls.Add(txtBoxDni, 0, 4);
            tlpFormulario.Controls.Add(lblMail, 0, 5);
            tlpFormulario.Controls.Add(txtBoxMail, 0, 6);
            tlpFormulario.Controls.Add(lblTelefono, 0, 7);
            tlpFormulario.Controls.Add(txtBoxTelefono, 0, 8);
            tlpFormulario.Controls.Add(tlpBotones, 0, 9);
            tlpFormulario.Controls.Add(lblAviso, 0, 10);
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
            lblModo.Text = "Nuevo cliente";
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
            tlpGrilla.Controls.Add(lblDgvClientes, 0, 0);
            tlpGrilla.Controls.Add(lblBuscar, 1, 0);
            tlpGrilla.Controls.Add(txtBuscar, 2, 0);
            tlpGrilla.Controls.Add(dgvClientes, 0, 1);
            tlpGrilla.SetColumnSpan(dgvClientes, 3);
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
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Margin = new Padding(3, 8, 3, 3);
            lblDni.Text = "DNI: *";
            lblDni.Name = "lblDni";
            lblDni.TabIndex = 2;
            // 
            // txtBoxDni
            // 
            txtBoxDni.Dock = DockStyle.Fill;
            txtBoxDni.Name = "txtBoxDni";
            txtBoxDni.TabIndex = 3;
            // 
            // lblMail
            // 
            lblMail.AutoSize = true;
            lblMail.Margin = new Padding(3, 8, 3, 3);
            lblMail.Text = "Mail:";
            lblMail.Name = "lblMail";
            lblMail.TabIndex = 4;
            // 
            // txtBoxMail
            // 
            txtBoxMail.Dock = DockStyle.Fill;
            txtBoxMail.Name = "txtBoxMail";
            txtBoxMail.TabIndex = 5;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Margin = new Padding(3, 8, 3, 3);
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Name = "lblTelefono";
            lblTelefono.TabIndex = 6;
            // 
            // txtBoxTelefono
            // 
            txtBoxTelefono.Dock = DockStyle.Fill;
            txtBoxTelefono.Name = "txtBoxTelefono";
            txtBoxTelefono.TabIndex = 7;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = SystemColors.ControlLightLight;
            btnGuardar.Margin = new Padding(0, 0, 0, 8);
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 8;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0);
            btnEditar.Size = new Size(94, 29);
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 9;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(8, 0, 0, 0);
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.TabIndex = 10;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblDgvClientes
            // 
            lblDgvClientes.AutoSize = true;
            lblDgvClientes.Margin = new Padding(3, 12, 3, 3);
            lblDgvClientes.Text = "Clientes";
            lblDgvClientes.Name = "lblDgvClientes";
            lblDgvClientes.TabIndex = 11;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Dock = DockStyle.Fill;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.TabIndex = 12;
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
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            MinimumSize = new Size(1000, 650);
            Name = "FrmClientes";
            Text = "Gestión de clientes";
            WindowState = FormWindowState.Maximized;
            Load += FrmClientes_Load;
            FormClosing += FrmClientes_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
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
        private Label lblDni;
        private TextBox txtBoxDni;
        private Label lblMail;
        private TextBox txtBoxMail;
        private Label lblTelefono;
        private TextBox txtBoxTelefono;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Label lblDgvClientes;
        private DataGridView dgvClientes;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Button btnVolver;
    }
}
