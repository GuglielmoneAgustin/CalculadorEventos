namespace ProyectoEventos.Forms
{
    partial class FrmServiciosAdicionales
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
            lblDescripcion = new Label();
            txtBoxDescripcion = new TextBox();
            lblMonto = new Label();
            numMonto = new NumericUpDown();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            lbldgvServiciosAdicionales = new Label();
            dgvServicios = new DataGridView();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)numMonto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).BeginInit();
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
            tlpFormulario.Controls.Add(lblDescripcion, 0, 2);
            tlpFormulario.Controls.Add(txtBoxDescripcion, 0, 3);
            tlpFormulario.Controls.Add(lblMonto, 0, 4);
            tlpFormulario.Controls.Add(numMonto, 0, 5);
            tlpFormulario.Controls.Add(flowBotones, 0, 6);
            tlpFormulario.Dock = DockStyle.Fill;
            tlpFormulario.Margin = new Padding(0, 0, 40, 0);
            tlpFormulario.Name = "tlpFormulario";
            tlpFormulario.RowCount = 8;
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
            tlpGrilla.ColumnCount = 1;
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrilla.Controls.Add(lbldgvServiciosAdicionales, 0, 0);
            tlpGrilla.Controls.Add(dgvServicios, 0, 1);
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
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Margin = new Padding(3, 12, 3, 3);
            lblDescripcion.Text = "Descripción :";
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.TabIndex = 2;
            // 
            // txtBoxDescripcion
            // 
            txtBoxDescripcion.Dock = DockStyle.Fill;
            txtBoxDescripcion.Multiline = true;
            txtBoxDescripcion.MinimumSize = new Size(0, 65);
            txtBoxDescripcion.ScrollBars = ScrollBars.Vertical;
            txtBoxDescripcion.Name = "txtBoxDescripcion";
            txtBoxDescripcion.TabIndex = 3;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Margin = new Padding(3, 12, 3, 3);
            lblMonto.Text = "Monto :";
            lblMonto.Name = "lblMonto";
            lblMonto.TabIndex = 4;
            // 
            // numMonto
            // 
            numMonto.Dock = DockStyle.Fill;
            numMonto.Maximum = new decimal(new int[] { 1410065408, 2, 0, 0 });
            numMonto.Name = "numMonto";
            numMonto.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Margin = new Padding(0, 0, 10, 0);
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 6;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Margin = new Padding(0, 0, 10, 0);
            btnEditar.Size = new Size(94, 29);
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 7;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Margin = new Padding(0, 0, 10, 0);
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.TabIndex = 8;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lbldgvServiciosAdicionales
            // 
            lbldgvServiciosAdicionales.AutoSize = true;
            lbldgvServiciosAdicionales.Margin = new Padding(3, 12, 3, 3);
            lbldgvServiciosAdicionales.Text = "Servicios Adicionales";
            lbldgvServiciosAdicionales.Name = "lbldgvServiciosAdicionales";
            lbldgvServiciosAdicionales.TabIndex = 9;
            // 
            // dgvServicios
            // 
            dgvServicios.AllowUserToAddRows = false;
            dgvServicios.AllowUserToDeleteRows = false;
            dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServicios.Dock = DockStyle.Fill;
            dgvServicios.MultiSelect = false;
            dgvServicios.ReadOnly = true;
            dgvServicios.RowHeadersWidth = 51;
            dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicios.Name = "dgvServicios";
            dgvServicios.TabIndex = 10;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Right;
            btnVolver.Margin = new Padding(3, 3, 3, 10);
            btnVolver.Size = new Size(94, 29);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Name = "btnVolver";
            btnVolver.TabIndex = 11;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmServiciosAdicionales
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            MinimumSize = new Size(1000, 650);
            Name = "FrmServiciosAdicionales";
            Text = "Gestion de Servicios Adicionales";
            WindowState = FormWindowState.Maximized;
            Load += FrmServiciosAdicionales_Load;
            ((System.ComponentModel.ISupportInitialize)numMonto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).EndInit();
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
        private Label lblDescripcion;
        private TextBox txtBoxDescripcion;
        private Label lblMonto;
        private NumericUpDown numMonto;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Label lbldgvServiciosAdicionales;
        private DataGridView dgvServicios;
        private Button btnVolver;
    }
}
