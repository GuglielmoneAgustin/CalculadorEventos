namespace ProyectoEventos.Forms
{
    partial class FrmEventoDatos
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
            tlpTarjetaDatos = new TableLayoutPanel();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblLugar = new Label();
            cmbLugar = new ComboBox();
            lblFechaHora = new Label();
            lblHorasDuracion = new Label();
            dtpFechaHora = new DateTimePicker();
            numHorasDuracion = new NumericUpDown();
            lblCantidadAdultos = new Label();
            lblCantidadNinos = new Label();
            numCantidadAdultos = new NumericUpDown();
            numCantidadNinos = new NumericUpDown();
            lblInfoLugar = new Label();
            flowBotones = new FlowLayoutPanel();
            btnSiguiente = new Button();
            btnVolver = new Button();
            tlpRaiz.SuspendLayout();
            tlpTarjetaDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numHorasDuracion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadAdultos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadNinos).BeginInit();
            flowBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 3;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 760F));
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRaiz.Controls.Add(tlpTarjetaDatos, 1, 0);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Location = new Point(0, 0);
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle());
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRaiz.Size = new Size(974, 660);
            tlpRaiz.TabIndex = 0;
            // 
            // tlpTarjetaDatos
            // 
            tlpTarjetaDatos.AutoSize = true;
            tlpTarjetaDatos.ColumnCount = 2;
            tlpTarjetaDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetaDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetaDatos.Controls.Add(lblCliente, 0, 0);
            tlpTarjetaDatos.Controls.Add(cmbCliente, 0, 1);
            tlpTarjetaDatos.Controls.Add(lblLugar, 0, 2);
            tlpTarjetaDatos.Controls.Add(cmbLugar, 0, 3);
            tlpTarjetaDatos.Controls.Add(lblFechaHora, 0, 4);
            tlpTarjetaDatos.Controls.Add(lblHorasDuracion, 1, 4);
            tlpTarjetaDatos.Controls.Add(dtpFechaHora, 0, 5);
            tlpTarjetaDatos.Controls.Add(numHorasDuracion, 1, 5);
            tlpTarjetaDatos.Controls.Add(lblCantidadAdultos, 0, 6);
            tlpTarjetaDatos.Controls.Add(lblCantidadNinos, 1, 6);
            tlpTarjetaDatos.Controls.Add(numCantidadAdultos, 0, 7);
            tlpTarjetaDatos.Controls.Add(numCantidadNinos, 1, 7);
            tlpTarjetaDatos.Controls.Add(lblInfoLugar, 0, 8);
            tlpTarjetaDatos.Controls.Add(flowBotones, 0, 9);
            tlpTarjetaDatos.Dock = DockStyle.Fill;
            tlpTarjetaDatos.Location = new Point(110, 3);
            tlpTarjetaDatos.Name = "tlpTarjetaDatos";
            tlpTarjetaDatos.RowCount = 10;
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.RowStyles.Add(new RowStyle());
            tlpTarjetaDatos.Size = new Size(754, 364);
            tlpTarjetaDatos.TabIndex = 0;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            tlpTarjetaDatos.SetColumnSpan(lblCliente, 2);
            lblCliente.Location = new Point(3, 0);
            lblCliente.Margin = new Padding(3, 0, 3, 3);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(68, 20);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente: *";
            // 
            // cmbCliente
            // 
            tlpTarjetaDatos.SetColumnSpan(cmbCliente, 2);
            cmbCliente.Dock = DockStyle.Fill;
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(3, 26);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(748, 28);
            cmbCliente.TabIndex = 1;
            // 
            // lblLugar
            // 
            lblLugar.AutoSize = true;
            tlpTarjetaDatos.SetColumnSpan(lblLugar, 2);
            lblLugar.Location = new Point(3, 71);
            lblLugar.Margin = new Padding(3, 14, 3, 3);
            lblLugar.Name = "lblLugar";
            lblLugar.Size = new Size(59, 20);
            lblLugar.TabIndex = 2;
            lblLugar.Text = "Lugar: *";
            // 
            // cmbLugar
            // 
            tlpTarjetaDatos.SetColumnSpan(cmbLugar, 2);
            cmbLugar.Dock = DockStyle.Fill;
            cmbLugar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLugar.FormattingEnabled = true;
            cmbLugar.Location = new Point(3, 97);
            cmbLugar.Name = "cmbLugar";
            cmbLugar.Size = new Size(748, 28);
            cmbLugar.TabIndex = 3;
            cmbLugar.SelectedIndexChanged += DatosDelLugar_Changed;
            // 
            // lblFechaHora
            // 
            lblFechaHora.AutoSize = true;
            lblFechaHora.Location = new Point(3, 142);
            lblFechaHora.Margin = new Padding(3, 14, 3, 3);
            lblFechaHora.Name = "lblFechaHora";
            lblFechaHora.Size = new Size(95, 20);
            lblFechaHora.TabIndex = 4;
            lblFechaHora.Text = "Fecha y hora:";
            // 
            // lblHorasDuracion
            // 
            lblHorasDuracion.AutoSize = true;
            lblHorasDuracion.Location = new Point(380, 142);
            lblHorasDuracion.Margin = new Padding(3, 14, 3, 3);
            lblHorasDuracion.Name = "lblHorasDuracion";
            lblHorasDuracion.Size = new Size(110, 20);
            lblHorasDuracion.TabIndex = 6;
            lblHorasDuracion.Text = "Duración (hs): *";
            // 
            // dtpFechaHora
            // 
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaHora.Dock = DockStyle.Fill;
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.Location = new Point(3, 168);
            dtpFechaHora.Name = "dtpFechaHora";
            dtpFechaHora.ShowUpDown = true;
            dtpFechaHora.Size = new Size(371, 27);
            dtpFechaHora.TabIndex = 5;
            dtpFechaHora.ValueChanged += DatosDelLugar_Changed;
            // 
            // numHorasDuracion
            // 
            numHorasDuracion.DecimalPlaces = 2;
            numHorasDuracion.Dock = DockStyle.Fill;
            numHorasDuracion.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numHorasDuracion.Location = new Point(380, 168);
            numHorasDuracion.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numHorasDuracion.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numHorasDuracion.Name = "numHorasDuracion";
            numHorasDuracion.Size = new Size(371, 27);
            numHorasDuracion.TabIndex = 7;
            numHorasDuracion.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numHorasDuracion.ValueChanged += DatosDelLugar_Changed;
            // 
            // lblCantidadAdultos
            // 
            lblCantidadAdultos.AutoSize = true;
            lblCantidadAdultos.Location = new Point(3, 212);
            lblCantidadAdultos.Margin = new Padding(3, 14, 3, 3);
            lblCantidadAdultos.Name = "lblCantidadAdultos";
            lblCantidadAdultos.Size = new Size(125, 20);
            lblCantidadAdultos.TabIndex = 8;
            lblCantidadAdultos.Text = "Cantidad adultos:";
            // 
            // lblCantidadNinos
            // 
            lblCantidadNinos.AutoSize = true;
            lblCantidadNinos.Location = new Point(380, 212);
            lblCantidadNinos.Margin = new Padding(3, 14, 3, 3);
            lblCantidadNinos.Name = "lblCantidadNinos";
            lblCantidadNinos.Size = new Size(111, 20);
            lblCantidadNinos.TabIndex = 10;
            lblCantidadNinos.Text = "Cantidad niños:";
            // 
            // numCantidadAdultos
            // 
            numCantidadAdultos.Dock = DockStyle.Fill;
            numCantidadAdultos.Location = new Point(3, 238);
            numCantidadAdultos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantidadAdultos.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numCantidadAdultos.Name = "numCantidadAdultos";
            numCantidadAdultos.Size = new Size(371, 27);
            numCantidadAdultos.TabIndex = 9;
            numCantidadAdultos.ValueChanged += DatosDelLugar_Changed;
            // 
            // numCantidadNinos
            // 
            numCantidadNinos.Dock = DockStyle.Fill;
            numCantidadNinos.Location = new Point(380, 238);
            numCantidadNinos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantidadNinos.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numCantidadNinos.Name = "numCantidadNinos";
            numCantidadNinos.Size = new Size(371, 27);
            numCantidadNinos.TabIndex = 11;
            numCantidadNinos.ValueChanged += DatosDelLugar_Changed;
            // 
            // lblInfoLugar
            // 
            lblInfoLugar.AutoSize = true;
            tlpTarjetaDatos.SetColumnSpan(lblInfoLugar, 2);
            lblInfoLugar.Location = new Point(3, 284);
            lblInfoLugar.Margin = new Padding(3, 16, 3, 0);
            lblInfoLugar.Name = "lblInfoLugar";
            lblInfoLugar.Size = new Size(0, 20);
            lblInfoLugar.TabIndex = 14;
            // 
            // flowBotones
            // 
            flowBotones.AutoSize = true;
            tlpTarjetaDatos.SetColumnSpan(flowBotones, 2);
            flowBotones.Controls.Add(btnSiguiente);
            flowBotones.Controls.Add(btnVolver);
            flowBotones.Dock = DockStyle.Fill;
            flowBotones.FlowDirection = FlowDirection.RightToLeft;
            flowBotones.Location = new Point(3, 324);
            flowBotones.Margin = new Padding(3, 20, 3, 0);
            flowBotones.Name = "flowBotones";
            flowBotones.Size = new Size(748, 40);
            flowBotones.TabIndex = 15;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(598, 0);
            btnSiguiente.Margin = new Padding(0);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(150, 40);
            btnSiguiente.TabIndex = 13;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(448, 0);
            btnVolver.Margin = new Padding(0, 0, 12, 0);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(150, 38);
            btnVolver.TabIndex = 12;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmEventoDatos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(840, 0);
            ClientSize = new Size(974, 660);
            Controls.Add(tlpRaiz);
            Name = "FrmEventoDatos";
            Text = "Datos del evento";
            Load += FrmEventoDatos_Load;
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            tlpTarjetaDatos.ResumeLayout(false);
            tlpTarjetaDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numHorasDuracion).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadAdultos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadNinos).EndInit();
            flowBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpTarjetaDatos;
        private FlowLayoutPanel flowBotones;
        private Label lblCliente;
        private ComboBox cmbCliente;
        private Label lblLugar;
        private ComboBox cmbLugar;
        private Label lblFechaHora;
        private DateTimePicker dtpFechaHora;
        private Label lblHorasDuracion;
        private NumericUpDown numHorasDuracion;
        private Label lblCantidadAdultos;
        private NumericUpDown numCantidadAdultos;
        private Label lblCantidadNinos;
        private NumericUpDown numCantidadNinos;
        private Button btnVolver;
        private Button btnSiguiente;
        private Label lblInfoLugar;
    }
}
