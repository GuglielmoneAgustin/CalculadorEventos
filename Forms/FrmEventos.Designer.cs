namespace ProyectoEventos.Forms
{
    partial class FrmEventos
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
            flowBotones = new FlowLayoutPanel();
            tlpGrilla = new TableLayoutPanel();
            lblDgvEventos = new Label();
            dgvEventos = new DataGridView();
            txtBuscar = new TextBox();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnMenu = new Button();
            btnPresupuestos = new Button();
            btnEliminar = new Button();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEventos).BeginInit();
            tlpRaiz.SuspendLayout();
            flowBotones.SuspendLayout();
            tlpGrilla.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(flowBotones, 0, 0);
            tlpRaiz.Controls.Add(tlpGrilla, 0, 1);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // flowBotones
            // 
            flowBotones.Controls.Add(btnNuevo);
            flowBotones.Controls.Add(btnEditar);
            flowBotones.Controls.Add(btnMenu);
            flowBotones.Controls.Add(btnPresupuestos);
            flowBotones.Controls.Add(btnEliminar);
            flowBotones.Controls.Add(btnVolver);
            flowBotones.AutoSize = true;
            flowBotones.Dock = DockStyle.Top;
            flowBotones.Margin = new Padding(0, 0, 0, 16);
            flowBotones.WrapContents = false;
            flowBotones.Name = "flowBotones";
            // 
            // tlpGrilla
            // 
            tlpGrilla.ColumnCount = 2;
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tlpGrilla.Controls.Add(lblDgvEventos, 0, 0);
            tlpGrilla.Controls.Add(txtBuscar, 1, 0);
            tlpGrilla.Controls.Add(dgvEventos, 0, 1);
            tlpGrilla.SetColumnSpan(dgvEventos, 2);
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Margin = new Padding(3, 0, 3, 10);
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
            // lblDgvEventos
            // 
            lblDgvEventos.AutoSize = true;
            lblDgvEventos.Margin = new Padding(3, 0, 3, 10);
            lblDgvEventos.Name = "lblDgvEventos";
            lblDgvEventos.TabIndex = 0;
            lblDgvEventos.Text = "Eventos";
            // 
            // dgvEventos
            // 
            dgvEventos.AllowUserToAddRows = false;
            dgvEventos.AllowUserToDeleteRows = false;
            dgvEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEventos.Dock = DockStyle.Fill;
            dgvEventos.MultiSelect = false;
            dgvEventos.Name = "dgvEventos";
            dgvEventos.CellFormatting += dgvEventos_CellFormatting;
            dgvEventos.ReadOnly = true;
            dgvEventos.RowHeadersWidth = 51;
            dgvEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventos.TabIndex = 1;
            // 
            // btnNuevo
            // 
            btnNuevo.Margin = new Padding(0, 0, 12, 0);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(150, 38);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nuevo evento";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0, 0, 12, 0);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(150, 38);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar datos";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnMenu
            // 
            btnMenu.Margin = new Padding(0, 0, 12, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(150, 38);
            btnMenu.TabIndex = 4;
            btnMenu.Text = "Editar menú";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            // 
            // btnPresupuestos
            // 
            btnPresupuestos.Margin = new Padding(0, 0, 12, 0);
            btnPresupuestos.Name = "btnPresupuestos";
            btnPresupuestos.Size = new Size(160, 38);
            btnPresupuestos.TabIndex = 5;
            btnPresupuestos.Text = "Ver presupuestos";
            btnPresupuestos.UseVisualStyleBackColor = true;
            btnPresupuestos.Click += btnPresupuestos_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(0, 0, 12, 0);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(150, 38);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Margin = new Padding(0, 0, 12, 0);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(150, 38);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmEventos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 620);
            Controls.Add(tlpRaiz);
            Name = "FrmEventos";
            Text = "Gestión de eventos";
            WindowState = FormWindowState.Maximized;
            Load += FrmEventos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEventos).EndInit();
            tlpGrilla.ResumeLayout(false);
            tlpGrilla.PerformLayout();
            flowBotones.ResumeLayout(false);
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private FlowLayoutPanel flowBotones;
        private TableLayoutPanel tlpGrilla;
        private Label lblDgvEventos;
        private DataGridView dgvEventos;
        private TextBox txtBuscar;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnMenu;
        private Button btnPresupuestos;
        private Button btnEliminar;
        private Button btnVolver;
    }
}
