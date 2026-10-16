namespace ProyectoEventos.Forms
{
    partial class FrmInicio
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
            lblFechaHoy = new Label();
            lblAlerta = new Label();
            tlpResumen = new TableLayoutPanel();
            tlpTarjetaProximoEvento = new TableLayoutPanel();
            lblResumenTituloProximoEvento = new Label();
            lblResumenValorProximoEvento = new Label();
            lblProximoEventoDetalle = new Label();
            tlpTarjetaProximos = new TableLayoutPanel();
            lblResumenTituloProximos = new Label();
            lblResumenValorProximos = new Label();
            tlpTarjetaEnEspera = new TableLayoutPanel();
            lblResumenTituloEnEspera = new Label();
            lblResumenValorEnEspera = new Label();
            tlpTarjetaMes = new TableLayoutPanel();
            lblResumenTituloMes = new Label();
            lblResumenValorMes = new Label();
            tlpGrilla = new TableLayoutPanel();
            lblProximosEventos = new Label();
            dgvProximosEventos = new DataGridView();
            tlpGrillaPendientes = new TableLayoutPanel();
            lblPendientes = new Label();
            dgvPendientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpResumen.SuspendLayout();
            tlpTarjetaProximoEvento.SuspendLayout();
            tlpTarjetaProximos.SuspendLayout();
            tlpTarjetaEnEspera.SuspendLayout();
            tlpTarjetaMes.SuspendLayout();
            tlpGrilla.SuspendLayout();
            tlpGrillaPendientes.SuspendLayout();
            SuspendLayout();
            //
            // tlpRaiz
            //
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(lblFechaHoy, 0, 0);
            tlpRaiz.Controls.Add(lblAlerta, 0, 1);
            tlpRaiz.Controls.Add(tlpResumen, 0, 2);
            tlpRaiz.Controls.Add(tlpGrilla, 0, 3);
            tlpRaiz.Controls.Add(tlpGrillaPendientes, 0, 4);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 5;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            //
            // lblFechaHoy
            //
            lblFechaHoy.AutoSize = true;
            lblFechaHoy.Margin = new Padding(0, 0, 0, 14);
            lblFechaHoy.Name = "lblFechaHoy";
            lblFechaHoy.TabIndex = 0;
            lblFechaHoy.Text = "Hoy";
            //
            // lblAlerta
            //
            lblAlerta.AutoSize = true;
            lblAlerta.Dock = DockStyle.Fill;
            lblAlerta.Margin = new Padding(0, 0, 0, 16);
            lblAlerta.Name = "lblAlerta";
            lblAlerta.TabIndex = 1;
            lblAlerta.Visible = false;
            //
            // tlpResumen
            //
            tlpResumen.AutoSize = true;
            tlpResumen.ColumnCount = 4;
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            tlpResumen.Controls.Add(tlpTarjetaProximoEvento, 0, 0);
            tlpResumen.Controls.Add(tlpTarjetaProximos, 1, 0);
            tlpResumen.Controls.Add(tlpTarjetaEnEspera, 2, 0);
            tlpResumen.Controls.Add(tlpTarjetaMes, 3, 0);
            tlpResumen.Dock = DockStyle.Fill;
            tlpResumen.Margin = new Padding(0, 0, 0, 20);
            tlpResumen.Name = "tlpResumen";
            tlpResumen.RowCount = 1;
            tlpResumen.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // tlpTarjetaProximoEvento
            //
            tlpTarjetaProximoEvento.AutoSize = true;
            tlpTarjetaProximoEvento.ColumnCount = 1;
            tlpTarjetaProximoEvento.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaProximoEvento.Controls.Add(lblResumenTituloProximoEvento, 0, 0);
            tlpTarjetaProximoEvento.Controls.Add(lblResumenValorProximoEvento, 0, 1);
            tlpTarjetaProximoEvento.Controls.Add(lblProximoEventoDetalle, 0, 2);
            tlpTarjetaProximoEvento.Dock = DockStyle.Fill;
            tlpTarjetaProximoEvento.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaProximoEvento.Name = "tlpTarjetaProximoEvento";
            tlpTarjetaProximoEvento.RowCount = 3;
            tlpTarjetaProximoEvento.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProximoEvento.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProximoEvento.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloProximoEvento
            //
            lblResumenTituloProximoEvento.AutoSize = true;
            lblResumenTituloProximoEvento.Name = "lblResumenTituloProximoEvento";
            lblResumenTituloProximoEvento.TabIndex = 2;
            lblResumenTituloProximoEvento.Text = "Próximo evento";
            //
            // lblResumenValorProximoEvento
            //
            lblResumenValorProximoEvento.AutoSize = true;
            lblResumenValorProximoEvento.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorProximoEvento.Name = "lblResumenValorProximoEvento";
            lblResumenValorProximoEvento.TabIndex = 3;
            lblResumenValorProximoEvento.Text = "-";
            //
            // lblProximoEventoDetalle
            //
            lblProximoEventoDetalle.AutoSize = true;
            lblProximoEventoDetalle.Margin = new Padding(3, 4, 3, 0);
            lblProximoEventoDetalle.Name = "lblProximoEventoDetalle";
            lblProximoEventoDetalle.TabIndex = 4;
            //
            // tlpTarjetaProximos
            //
            tlpTarjetaProximos.AutoSize = true;
            tlpTarjetaProximos.ColumnCount = 1;
            tlpTarjetaProximos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaProximos.Controls.Add(lblResumenTituloProximos, 0, 0);
            tlpTarjetaProximos.Controls.Add(lblResumenValorProximos, 0, 1);
            tlpTarjetaProximos.Dock = DockStyle.Fill;
            tlpTarjetaProximos.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaProximos.Name = "tlpTarjetaProximos";
            tlpTarjetaProximos.RowCount = 2;
            tlpTarjetaProximos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProximos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloProximos
            //
            lblResumenTituloProximos.AutoSize = true;
            lblResumenTituloProximos.Name = "lblResumenTituloProximos";
            lblResumenTituloProximos.TabIndex = 5;
            lblResumenTituloProximos.Text = "Eventos aprobados";
            //
            // lblResumenValorProximos
            //
            lblResumenValorProximos.AutoSize = true;
            lblResumenValorProximos.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorProximos.Name = "lblResumenValorProximos";
            lblResumenValorProximos.TabIndex = 6;
            lblResumenValorProximos.Text = "0";
            //
            // tlpTarjetaEnEspera
            //
            tlpTarjetaEnEspera.AutoSize = true;
            tlpTarjetaEnEspera.ColumnCount = 1;
            tlpTarjetaEnEspera.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaEnEspera.Controls.Add(lblResumenTituloEnEspera, 0, 0);
            tlpTarjetaEnEspera.Controls.Add(lblResumenValorEnEspera, 0, 1);
            tlpTarjetaEnEspera.Dock = DockStyle.Fill;
            tlpTarjetaEnEspera.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaEnEspera.Name = "tlpTarjetaEnEspera";
            tlpTarjetaEnEspera.RowCount = 2;
            tlpTarjetaEnEspera.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaEnEspera.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloEnEspera
            //
            lblResumenTituloEnEspera.AutoSize = true;
            lblResumenTituloEnEspera.Name = "lblResumenTituloEnEspera";
            lblResumenTituloEnEspera.TabIndex = 7;
            lblResumenTituloEnEspera.Text = "Esperando aprobación";
            //
            // lblResumenValorEnEspera
            //
            lblResumenValorEnEspera.AutoSize = true;
            lblResumenValorEnEspera.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorEnEspera.Name = "lblResumenValorEnEspera";
            lblResumenValorEnEspera.TabIndex = 8;
            lblResumenValorEnEspera.Text = "0";
            //
            // tlpTarjetaMes
            //
            tlpTarjetaMes.AutoSize = true;
            tlpTarjetaMes.ColumnCount = 1;
            tlpTarjetaMes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaMes.Controls.Add(lblResumenTituloMes, 0, 0);
            tlpTarjetaMes.Controls.Add(lblResumenValorMes, 0, 1);
            tlpTarjetaMes.Dock = DockStyle.Fill;
            tlpTarjetaMes.Margin = new Padding(0);
            tlpTarjetaMes.Name = "tlpTarjetaMes";
            tlpTarjetaMes.RowCount = 2;
            tlpTarjetaMes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaMes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloMes
            //
            lblResumenTituloMes.AutoSize = true;
            lblResumenTituloMes.Name = "lblResumenTituloMes";
            lblResumenTituloMes.TabIndex = 9;
            lblResumenTituloMes.Text = "Aprobado este mes";
            //
            // lblResumenValorMes
            //
            lblResumenValorMes.AutoSize = true;
            lblResumenValorMes.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorMes.Name = "lblResumenValorMes";
            lblResumenValorMes.TabIndex = 10;
            lblResumenValorMes.Text = "$ 0,00";
            //
            // tlpGrilla
            //
            tlpGrilla.ColumnCount = 1;
            tlpGrilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrilla.Controls.Add(lblProximosEventos, 0, 0);
            tlpGrilla.Controls.Add(dgvProximosEventos, 0, 1);
            tlpGrilla.Dock = DockStyle.Fill;
            tlpGrilla.Name = "tlpGrilla";
            tlpGrilla.RowCount = 2;
            tlpGrilla.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpGrilla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // lblProximosEventos
            //
            lblProximosEventos.AutoSize = true;
            lblProximosEventos.Margin = new Padding(3, 0, 3, 10);
            lblProximosEventos.Text = "Próximos eventos aprobados";
            lblProximosEventos.Name = "lblProximosEventos";
            lblProximosEventos.TabIndex = 11;
            //
            // dgvProximosEventos
            //
            dgvProximosEventos.AllowUserToAddRows = false;
            dgvProximosEventos.AllowUserToDeleteRows = false;
            dgvProximosEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProximosEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProximosEventos.Dock = DockStyle.Fill;
            dgvProximosEventos.MultiSelect = false;
            dgvProximosEventos.ReadOnly = true;
            dgvProximosEventos.RowHeadersWidth = 51;
            dgvProximosEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProximosEventos.Name = "dgvProximosEventos";
            dgvProximosEventos.TabIndex = 12;
            dgvProximosEventos.CellFormatting += dgvProximosEventos_CellFormatting;
            //
            // tlpGrillaPendientes
            //
            tlpGrillaPendientes.ColumnCount = 1;
            tlpGrillaPendientes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGrillaPendientes.Controls.Add(lblPendientes, 0, 0);
            tlpGrillaPendientes.Controls.Add(dgvPendientes, 0, 1);
            tlpGrillaPendientes.Dock = DockStyle.Fill;
            tlpGrillaPendientes.Name = "tlpGrillaPendientes";
            tlpGrillaPendientes.RowCount = 2;
            tlpGrillaPendientes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpGrillaPendientes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // lblPendientes
            //
            lblPendientes.AutoSize = true;
            lblPendientes.Margin = new Padding(3, 0, 3, 10);
            lblPendientes.Text = "Para atender";
            lblPendientes.Name = "lblPendientes";
            lblPendientes.TabIndex = 13;
            //
            // dgvPendientes
            //
            dgvPendientes.AllowUserToAddRows = false;
            dgvPendientes.AllowUserToDeleteRows = false;
            dgvPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPendientes.Dock = DockStyle.Fill;
            dgvPendientes.MultiSelect = false;
            dgvPendientes.ReadOnly = true;
            dgvPendientes.RowHeadersWidth = 51;
            dgvPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPendientes.Name = "dgvPendientes";
            dgvPendientes.TabIndex = 14;
            dgvPendientes.CellFormatting += dgvPendientes_CellFormatting;
            //
            // FrmInicio
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(0, 760);
            ClientSize = new Size(1000, 600);
            Controls.Add(tlpRaiz);
            Name = "FrmInicio";
            Text = "Inicio";
            Load += FrmInicio_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).EndInit();
            tlpTarjetaProximoEvento.ResumeLayout(false);
            tlpTarjetaProximoEvento.PerformLayout();
            tlpTarjetaProximos.ResumeLayout(false);
            tlpTarjetaProximos.PerformLayout();
            tlpTarjetaEnEspera.ResumeLayout(false);
            tlpTarjetaEnEspera.PerformLayout();
            tlpTarjetaMes.ResumeLayout(false);
            tlpTarjetaMes.PerformLayout();
            tlpResumen.ResumeLayout(false);
            tlpResumen.PerformLayout();
            tlpGrilla.ResumeLayout(false);
            tlpGrilla.PerformLayout();
            tlpGrillaPendientes.ResumeLayout(false);
            tlpGrillaPendientes.PerformLayout();
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private Label lblFechaHoy;
        private Label lblAlerta;
        private TableLayoutPanel tlpResumen;
        private TableLayoutPanel tlpTarjetaProximoEvento;
        private Label lblResumenTituloProximoEvento;
        private Label lblResumenValorProximoEvento;
        private Label lblProximoEventoDetalle;
        private TableLayoutPanel tlpTarjetaProximos;
        private Label lblResumenTituloProximos;
        private Label lblResumenValorProximos;
        private TableLayoutPanel tlpTarjetaEnEspera;
        private Label lblResumenTituloEnEspera;
        private Label lblResumenValorEnEspera;
        private TableLayoutPanel tlpTarjetaMes;
        private Label lblResumenTituloMes;
        private Label lblResumenValorMes;
        private TableLayoutPanel tlpGrilla;
        private Label lblProximosEventos;
        private DataGridView dgvProximosEventos;
        private TableLayoutPanel tlpGrillaPendientes;
        private Label lblPendientes;
        private DataGridView dgvPendientes;
    }
}
