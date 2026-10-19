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
            flowBotones = new FlowLayoutPanel();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            lblLugar = new Label();
            cmbLugar = new ComboBox();
            lblFechaHora = new Label();
            dtpFechaHora = new DateTimePicker();
            lblHorasDuracion = new Label();
            numHorasDuracion = new NumericUpDown();
            lblCantidadAdultos = new Label();
            numCantidadAdultos = new NumericUpDown();
            lblCantidadNinos = new Label();
            numCantidadNinos = new NumericUpDown();
            btnVolver = new Button();
            btnSiguiente = new Button();
            lblInfoLugar = new Label();
            ((System.ComponentModel.ISupportInitialize)numHorasDuracion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadAdultos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadNinos).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpTarjetaDatos.SuspendLayout();
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
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // tlpTarjetaDatos
            // 
            tlpTarjetaDatos.AutoSize = true;
            tlpTarjetaDatos.ColumnCount = 2;
            tlpTarjetaDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetaDatos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetaDatos.Controls.Add(lblCliente, 0, 0);
            tlpTarjetaDatos.SetColumnSpan(lblCliente, 2);
            tlpTarjetaDatos.Controls.Add(cmbCliente, 0, 1);
            tlpTarjetaDatos.SetColumnSpan(cmbCliente, 2);
            tlpTarjetaDatos.Controls.Add(lblLugar, 0, 2);
            tlpTarjetaDatos.SetColumnSpan(lblLugar, 2);
            tlpTarjetaDatos.Controls.Add(cmbLugar, 0, 3);
            tlpTarjetaDatos.SetColumnSpan(cmbLugar, 2);
            tlpTarjetaDatos.Controls.Add(lblFechaHora, 0, 4);
            tlpTarjetaDatos.Controls.Add(lblHorasDuracion, 1, 4);
            tlpTarjetaDatos.Controls.Add(dtpFechaHora, 0, 5);
            tlpTarjetaDatos.Controls.Add(numHorasDuracion, 1, 5);
            tlpTarjetaDatos.Controls.Add(lblCantidadAdultos, 0, 6);
            tlpTarjetaDatos.Controls.Add(lblCantidadNinos, 1, 6);
            tlpTarjetaDatos.Controls.Add(numCantidadAdultos, 0, 7);
            tlpTarjetaDatos.Controls.Add(numCantidadNinos, 1, 7);
            tlpTarjetaDatos.Controls.Add(lblInfoLugar, 0, 8);
            tlpTarjetaDatos.SetColumnSpan(lblInfoLugar, 2);
            tlpTarjetaDatos.Controls.Add(flowBotones, 0, 9);
            tlpTarjetaDatos.SetColumnSpan(flowBotones, 2);
            tlpTarjetaDatos.Dock = DockStyle.Fill;
            tlpTarjetaDatos.Name = "tlpTarjetaDatos";
            tlpTarjetaDatos.RowCount = 10;
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaDatos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Margin = new Padding(3, 0, 3, 3);
            lblCliente.Name = "lblCliente";
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente: *";
            // 
            // cmbCliente
            // 
            cmbCliente.Dock = DockStyle.Fill;
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Name = "cmbCliente";
            cmbCliente.TabIndex = 1;
            // 
            // lblLugar
            // 
            lblLugar.AutoSize = true;
            lblLugar.Margin = new Padding(3, 14, 3, 3);
            lblLugar.Name = "lblLugar";
            lblLugar.TabIndex = 2;
            lblLugar.Text = "Lugar: *";
            // 
            // cmbLugar
            // 
            cmbLugar.Dock = DockStyle.Fill;
            cmbLugar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLugar.FormattingEnabled = true;
            cmbLugar.Name = "cmbLugar";
            cmbLugar.TabIndex = 3;
            // 
            // lblFechaHora
            // 
            lblFechaHora.AutoSize = true;
            lblFechaHora.Margin = new Padding(3, 14, 3, 3);
            lblFechaHora.Name = "lblFechaHora";
            lblFechaHora.TabIndex = 4;
            lblFechaHora.Text = "Fecha y hora:";
            // 
            // dtpFechaHora
            // 
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaHora.Dock = DockStyle.Fill;
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.Name = "dtpFechaHora";
            dtpFechaHora.ShowUpDown = true;
            dtpFechaHora.TabIndex = 5;
            // 
            // lblHorasDuracion
            // 
            lblHorasDuracion.AutoSize = true;
            lblHorasDuracion.Margin = new Padding(3, 14, 3, 3);
            lblHorasDuracion.Name = "lblHorasDuracion";
            lblHorasDuracion.TabIndex = 6;
            lblHorasDuracion.Text = "Duración (hs): *";
            // 
            // numHorasDuracion
            // 
            numHorasDuracion.Dock = DockStyle.Fill;
            numHorasDuracion.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numHorasDuracion.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numHorasDuracion.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numHorasDuracion.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numHorasDuracion.DecimalPlaces = 2;
            numHorasDuracion.Name = "numHorasDuracion";
            numHorasDuracion.TabIndex = 7;
            // 
            // lblCantidadAdultos
            // 
            lblCantidadAdultos.AutoSize = true;
            lblCantidadAdultos.Margin = new Padding(3, 14, 3, 3);
            lblCantidadAdultos.Name = "lblCantidadAdultos";
            lblCantidadAdultos.TabIndex = 8;
            lblCantidadAdultos.Text = "Cantidad adultos:";
            // 
            // numCantidadAdultos
            // 
            numCantidadAdultos.Dock = DockStyle.Fill;
            numCantidadAdultos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantidadAdultos.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numCantidadAdultos.Name = "numCantidadAdultos";
            numCantidadAdultos.TabIndex = 9;
            // 
            // lblCantidadNinos
            // 
            lblCantidadNinos.AutoSize = true;
            lblCantidadNinos.Margin = new Padding(3, 14, 3, 3);
            lblCantidadNinos.Name = "lblCantidadNinos";
            lblCantidadNinos.TabIndex = 10;
            lblCantidadNinos.Text = "Cantidad niños:";
            // 
            // numCantidadNinos
            // 
            numCantidadNinos.Dock = DockStyle.Fill;
            numCantidadNinos.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantidadNinos.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numCantidadNinos.Name = "numCantidadNinos";
            numCantidadNinos.TabIndex = 11;
            // 
            // lblInfoLugar
            // 
            lblInfoLugar.AutoSize = true;
            lblInfoLugar.Margin = new Padding(3, 16, 3, 0);
            lblInfoLugar.Name = "lblInfoLugar";
            lblInfoLugar.TabIndex = 14;
            cmbLugar.SelectedIndexChanged += DatosDelLugar_Changed;
            dtpFechaHora.ValueChanged += DatosDelLugar_Changed;
            numHorasDuracion.ValueChanged += DatosDelLugar_Changed;
            numCantidadAdultos.ValueChanged += DatosDelLugar_Changed;
            numCantidadNinos.ValueChanged += DatosDelLugar_Changed;
            // 
            // flowBotones
            // 
            flowBotones.Controls.Add(btnSiguiente);
            flowBotones.Controls.Add(btnVolver);
            flowBotones.AutoSize = true;
            flowBotones.Dock = DockStyle.Fill;
            flowBotones.FlowDirection = FlowDirection.RightToLeft;
            flowBotones.Margin = new Padding(3, 20, 3, 0);
            flowBotones.Name = "flowBotones";
            // 
            // btnVolver
            // 
            btnVolver.Margin = new Padding(10, 0, 0, 0);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(150, 38);
            btnVolver.TabIndex = 12;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Margin = new Padding(0);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(150, 40);
            btnSiguiente.TabIndex = 13;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // FrmEventoDatos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(840, 0);
            ClientSize = new Size(600, 660);
            Controls.Add(tlpRaiz);
            Name = "FrmEventoDatos";
            Text = "Datos del evento";
            Load += FrmEventoDatos_Load;
            ((System.ComponentModel.ISupportInitialize)numHorasDuracion).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadAdultos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidadNinos).EndInit();
            flowBotones.ResumeLayout(false);
            tlpTarjetaDatos.ResumeLayout(false);
            tlpTarjetaDatos.PerformLayout();
            tlpRaiz.ResumeLayout(false);
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
