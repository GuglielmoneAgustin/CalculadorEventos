namespace ProyectoEventos.Forms
{
    partial class FrmPresupuestos
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
            lblInfoEvento = new Label();
            tlpGrillaPresupuestos = new TableLayoutPanel();
            lblDgvPresupuestos = new Label();
            dgvPresupuestos = new DataGridView();
            lblEstadoPago = new Label();
            flowDatosPago = new FlowLayoutPanel();
            lblDetallePago = new Label();
            lblMontoPago = new Label();
            numPago = new NumericUpDown();
            tlpBotones = new TableLayoutPanel();
            flowPago = new FlowLayoutPanel();
            btnRegistrarPago = new Button();
            btnMarcarPagado = new Button();
            btnComprobante = new Button();
            flowBotones = new FlowLayoutPanel();
            btnVolver = new Button();
            btnCancelar = new Button();
            btnAprobar = new Button();
            btnImprimir = new Button();
            btnActualizar = new Button();
            tlpRaiz.SuspendLayout();
            tlpGrillaPresupuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPresupuestos).BeginInit();
            flowDatosPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPago).BeginInit();
            tlpBotones.SuspendLayout();
            flowPago.SuspendLayout();
            flowBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(lblInfoEvento, 0, 0);
            tlpRaiz.Controls.Add(tlpGrillaPresupuestos, 0, 1);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Location = new Point(0, 0);
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle());
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRaiz.Size = new Size(1305, 699);
            tlpRaiz.TabIndex = 0;
            // 
            // lblInfoEvento
            // 
            lblInfoEvento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblInfoEvento.AutoSize = true;
            lblInfoEvento.Location = new Point(0, 0);
            lblInfoEvento.Margin = new Padding(0, 0, 0, 16);
            lblInfoEvento.Name = "lblInfoEvento";
            lblInfoEvento.Size = new Size(1305, 20);
            lblInfoEvento.TabIndex = 0;
            lblInfoEvento.Text = "Datos del evento";
            // 
            // tlpGrillaPresupuestos
            // 
            tlpGrillaPresupuestos.ColumnCount = 1;
            tlpGrillaPresupuestos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrillaPresupuestos.Controls.Add(lblDgvPresupuestos, 0, 0);
            tlpGrillaPresupuestos.Controls.Add(dgvPresupuestos, 0, 1);
            tlpGrillaPresupuestos.Controls.Add(lblEstadoPago, 0, 2);
            tlpGrillaPresupuestos.Controls.Add(lblDetallePago, 0, 3);
            tlpGrillaPresupuestos.Controls.Add(flowDatosPago, 0, 4);
            tlpGrillaPresupuestos.Controls.Add(tlpBotones, 0, 5);
            tlpGrillaPresupuestos.Dock = DockStyle.Fill;
            tlpGrillaPresupuestos.Location = new Point(3, 39);
            tlpGrillaPresupuestos.Name = "tlpGrillaPresupuestos";
            tlpGrillaPresupuestos.RowCount = 6;
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle());
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle());
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle());
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle());
            tlpGrillaPresupuestos.RowStyles.Add(new RowStyle());
            tlpGrillaPresupuestos.Size = new Size(1299, 657);
            tlpGrillaPresupuestos.TabIndex = 1;
            // 
            // lblDgvPresupuestos
            // 
            lblDgvPresupuestos.AutoSize = true;
            lblDgvPresupuestos.Location = new Point(3, 0);
            lblDgvPresupuestos.Margin = new Padding(3, 0, 3, 10);
            lblDgvPresupuestos.Name = "lblDgvPresupuestos";
            lblDgvPresupuestos.Size = new Size(169, 20);
            lblDgvPresupuestos.TabIndex = 1;
            lblDgvPresupuestos.Text = "Presupuestos del evento";
            // 
            // dgvPresupuestos
            // 
            dgvPresupuestos.AllowUserToAddRows = false;
            dgvPresupuestos.AllowUserToDeleteRows = false;
            dgvPresupuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPresupuestos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPresupuestos.Dock = DockStyle.Fill;
            dgvPresupuestos.Location = new Point(3, 33);
            dgvPresupuestos.MultiSelect = false;
            dgvPresupuestos.Name = "dgvPresupuestos";
            dgvPresupuestos.ReadOnly = true;
            dgvPresupuestos.RowHeadersWidth = 51;
            dgvPresupuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPresupuestos.Size = new Size(1293, 502);
            dgvPresupuestos.TabIndex = 2;
            dgvPresupuestos.CellFormatting += dgvPresupuestos_CellFormatting;
            // 
            // lblEstadoPago
            // 
            lblEstadoPago.AutoSize = true;
            lblEstadoPago.Location = new Point(3, 554);
            lblEstadoPago.Margin = new Padding(3, 16, 3, 0);
            lblEstadoPago.Name = "lblEstadoPago";
            lblEstadoPago.Size = new Size(50, 20);
            lblEstadoPago.TabIndex = 3;
            lblEstadoPago.Text = "Cobro";
            // 
            // flowDatosPago
            // 
            flowDatosPago.AutoSize = true;
            flowDatosPago.Controls.Add(lblMontoPago);
            flowDatosPago.Controls.Add(numPago);
            flowDatosPago.Dock = DockStyle.Fill;
            flowDatosPago.Location = new Point(3, 580);
            flowDatosPago.Margin = new Padding(3, 10, 3, 0);
            flowDatosPago.Name = "flowDatosPago";
            flowDatosPago.Size = new Size(1293, 27);
            flowDatosPago.TabIndex = 4;
            flowDatosPago.WrapContents = false;
            // 
            // lblDetallePago
            // 
            lblDetallePago.AutoSize = true;
            lblDetallePago.Location = new Point(0, 4);
            lblDetallePago.Margin = new Padding(3, 6, 3, 0);
            lblDetallePago.Name = "lblDetallePago";
            lblDetallePago.Size = new Size(0, 20);
            lblDetallePago.TabIndex = 0;
            // 
            // lblMontoPago
            // 
            lblMontoPago.AutoSize = true;
            lblMontoPago.Location = new Point(24, 4);
            lblMontoPago.Margin = new Padding(0, 4, 6, 0);
            lblMontoPago.Name = "lblMontoPago";
            lblMontoPago.Size = new Size(44, 20);
            lblMontoPago.TabIndex = 1;
            lblMontoPago.Text = "Monto:";
            // 
            // numPago
            // 
            numPago.DecimalPlaces = 2;
            numPago.Location = new Point(74, 0);
            numPago.Margin = new Padding(0);
            numPago.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numPago.Minimum = new decimal(new int[] { -727379968, 232, 0, int.MinValue });
            numPago.Name = "numPago";
            numPago.Size = new Size(135, 27);
            numPago.TabIndex = 3;
            numPago.ThousandsSeparator = true;
            // 
            // tlpBotones
            // 
            tlpBotones.AutoSize = true;
            tlpBotones.ColumnCount = 2;
            tlpBotones.ColumnStyles.Add(new ColumnStyle());
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBotones.Controls.Add(flowPago, 0, 0);
            tlpBotones.Controls.Add(flowBotones, 1, 0);
            tlpBotones.Dock = DockStyle.Fill;
            tlpBotones.Location = new Point(3, 619);
            tlpBotones.Margin = new Padding(3, 12, 3, 0);
            tlpBotones.Name = "tlpBotones";
            tlpBotones.RowCount = 1;
            tlpBotones.RowStyles.Add(new RowStyle());
            tlpBotones.Size = new Size(1293, 38);
            tlpBotones.TabIndex = 5;
            // 
            // flowPago
            // 
            flowPago.AutoSize = true;
            flowPago.Controls.Add(btnRegistrarPago);
            flowPago.Controls.Add(btnMarcarPagado);
            flowPago.Controls.Add(btnComprobante);
            flowPago.Location = new Point(0, 0);
            flowPago.Margin = new Padding(0);
            flowPago.Name = "flowPago";
            flowPago.Size = new Size(430, 38);
            flowPago.TabIndex = 0;
            flowPago.WrapContents = false;
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(0, 0);
            btnRegistrarPago.Margin = new Padding(0, 0, 10, 0);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(130, 38);
            btnRegistrarPago.TabIndex = 4;
            btnRegistrarPago.Text = "Registrar pago";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            btnRegistrarPago.Click += btnRegistrarPago_Click;
            // 
            // btnMarcarPagado
            // 
            btnMarcarPagado.Location = new Point(140, 0);
            btnMarcarPagado.Margin = new Padding(0, 0, 10, 0);
            btnMarcarPagado.Name = "btnMarcarPagado";
            btnMarcarPagado.Size = new Size(150, 38);
            btnMarcarPagado.TabIndex = 5;
            btnMarcarPagado.Text = "Marcar como pagado";
            btnMarcarPagado.UseVisualStyleBackColor = true;
            btnMarcarPagado.Click += btnMarcarPagado_Click;
            // 
            // btnComprobante
            // 
            btnComprobante.Location = new Point(300, 0);
            btnComprobante.Margin = new Padding(0);
            btnComprobante.Name = "btnComprobante";
            btnComprobante.Size = new Size(130, 38);
            btnComprobante.TabIndex = 6;
            btnComprobante.Text = "Ver comprobante";
            btnComprobante.UseVisualStyleBackColor = true;
            btnComprobante.Click += btnComprobante_Click;
            // 
            // flowBotones
            // 
            flowBotones.AutoSize = true;
            flowBotones.Controls.Add(btnVolver);
            flowBotones.Controls.Add(btnCancelar);
            flowBotones.Controls.Add(btnAprobar);
            flowBotones.Controls.Add(btnImprimir);
            flowBotones.Controls.Add(btnActualizar);
            flowBotones.Dock = DockStyle.Fill;
            flowBotones.FlowDirection = FlowDirection.RightToLeft;
            flowBotones.Location = new Point(450, 0);
            flowBotones.Margin = new Padding(20, 0, 0, 0);
            flowBotones.Name = "flowBotones";
            flowBotones.Size = new Size(843, 38);
            flowBotones.TabIndex = 1;
            flowBotones.WrapContents = false;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(723, 0);
            btnVolver.Margin = new Padding(20, 0, 0, 0);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(120, 38);
            btnVolver.TabIndex = 11;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(583, 0);
            btnCancelar.Margin = new Padding(10, 0, 0, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 38);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAprobar
            // 
            btnAprobar.Location = new Point(453, 0);
            btnAprobar.Margin = new Padding(10, 0, 0, 0);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(120, 38);
            btnAprobar.TabIndex = 9;
            btnAprobar.Text = "Aprobar";
            btnAprobar.UseVisualStyleBackColor = true;
            btnAprobar.Click += btnAprobar_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Location = new Point(323, 0);
            btnImprimir.Margin = new Padding(10, 0, 0, 0);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(120, 38);
            btnImprimir.TabIndex = 8;
            btnImprimir.Text = "Ver ticket";
            btnImprimir.UseVisualStyleBackColor = true;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(123, 0);
            btnActualizar.Margin = new Padding(0);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(190, 38);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar presupuesto";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // FrmPresupuestos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(1305, 560);
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            Name = "FrmPresupuestos";
            Text = "Presupuestos del evento";
            Load += FrmPresupuestos_Load;
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            tlpGrillaPresupuestos.ResumeLayout(false);
            tlpGrillaPresupuestos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPresupuestos).EndInit();
            flowDatosPago.ResumeLayout(false);
            flowDatosPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPago).EndInit();
            tlpBotones.ResumeLayout(false);
            tlpBotones.PerformLayout();
            flowPago.ResumeLayout(false);
            flowBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private Label lblInfoEvento;
        private TableLayoutPanel tlpGrillaPresupuestos;
        private Label lblDgvPresupuestos;
        private DataGridView dgvPresupuestos;
        private FlowLayoutPanel flowDatosPago;
        private Label lblEstadoPago;
        private Label lblDetallePago;
        private Label lblMontoPago;
        private NumericUpDown numPago;
        private TableLayoutPanel tlpBotones;
        private FlowLayoutPanel flowPago;
        private Button btnRegistrarPago;
        private Button btnMarcarPagado;
        private Button btnComprobante;
        private FlowLayoutPanel flowBotones;
        private Button btnVolver;
        private Button btnCancelar;
        private Button btnAprobar;
        private Button btnImprimir;
        private Button btnActualizar;
    }
}
