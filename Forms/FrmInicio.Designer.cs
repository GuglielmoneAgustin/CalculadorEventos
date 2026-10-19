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
            lblAlerta = new Label();
            tlpResumen = new TableLayoutPanel();
            tlpTarjetaProximoEvento = new TableLayoutPanel();
            lblResumenTituloProximoEvento = new Label();
            lblResumenValorProximoEvento = new Label();
            lblResumenDetalleProximoEvento = new Label();
            tlpTarjetaSemana = new TableLayoutPanel();
            lblResumenTituloSemana = new Label();
            lblResumenValorSemana = new Label();
            lblResumenDetalleSemana = new Label();
            tlpTarjetaHoy = new TableLayoutPanel();
            lblResumenTituloHoy = new Label();
            lblResumenValorHoy = new Label();
            lblResumenDetalleHoy = new Label();
            tlpTarjetaPendientes = new TableLayoutPanel();
            lblResumenTituloPendientes = new Label();
            lblResumenValorPendientes = new Label();
            lblResumenDetallePendientes = new Label();
            tlpGrilla = new TableLayoutPanel();
            lblProximosEventos = new Label();
            dgvProximosEventos = new DataGridView();
            tlpGrillaPendientes = new TableLayoutPanel();
            lblPendientes = new Label();
            btnVerPresupuestos = new Button();
            dgvPendientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpResumen.SuspendLayout();
            tlpTarjetaProximoEvento.SuspendLayout();
            tlpTarjetaSemana.SuspendLayout();
            tlpTarjetaPendientes.SuspendLayout();
            tlpTarjetaHoy.SuspendLayout();
            tlpGrilla.SuspendLayout();
            tlpGrillaPendientes.SuspendLayout();
            SuspendLayout();
            //
            // tlpRaiz
            //
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(lblAlerta, 0, 0);
            tlpRaiz.Controls.Add(tlpResumen, 0, 1);
            tlpRaiz.Controls.Add(tlpGrilla, 0, 2);
            tlpRaiz.Controls.Add(tlpGrillaPendientes, 0, 3);
            tlpRaiz.Controls.Add(btnVerPresupuestos, 0, 4);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 5;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19F));
            tlpResumen.Controls.Add(tlpTarjetaProximoEvento, 0, 0);
            tlpResumen.Controls.Add(tlpTarjetaSemana, 1, 0);
            tlpResumen.Controls.Add(tlpTarjetaPendientes, 2, 0);
            tlpResumen.Controls.Add(tlpTarjetaHoy, 3, 0);
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
            tlpTarjetaProximoEvento.Controls.Add(lblResumenDetalleProximoEvento, 0, 2);
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
            // lblResumenDetalleProximoEvento
            //
            lblResumenDetalleProximoEvento.AutoSize = true;
            lblResumenDetalleProximoEvento.Margin = new Padding(3, 4, 3, 0);
            lblResumenDetalleProximoEvento.Name = "lblResumenDetalleProximoEvento";
            lblResumenDetalleProximoEvento.TabIndex = 4;
            //
            // tlpTarjetaSemana
            //
            tlpTarjetaSemana.AutoSize = true;
            tlpTarjetaSemana.ColumnCount = 1;
            tlpTarjetaSemana.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaSemana.Controls.Add(lblResumenTituloSemana, 0, 0);
            tlpTarjetaSemana.Controls.Add(lblResumenValorSemana, 0, 1);
            tlpTarjetaSemana.Controls.Add(lblResumenDetalleSemana, 0, 2);
            tlpTarjetaSemana.Dock = DockStyle.Fill;
            tlpTarjetaSemana.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaSemana.Name = "tlpTarjetaSemana";
            tlpTarjetaSemana.RowCount = 3;
            tlpTarjetaSemana.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaSemana.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaSemana.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloSemana
            //
            lblResumenTituloSemana.AutoSize = true;
            lblResumenTituloSemana.Name = "lblResumenTituloSemana";
            lblResumenTituloSemana.TabIndex = 5;
            lblResumenTituloSemana.Text = "Esta semana";
            //
            // lblResumenValorSemana
            //
            lblResumenValorSemana.AutoSize = true;
            lblResumenValorSemana.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorSemana.Name = "lblResumenValorSemana";
            lblResumenValorSemana.TabIndex = 6;
            lblResumenValorSemana.Text = "0";
            //
            // lblResumenDetalleSemana
            //
            lblResumenDetalleSemana.AutoSize = true;
            lblResumenDetalleSemana.Margin = new Padding(3, 4, 3, 0);
            lblResumenDetalleSemana.Name = "lblResumenDetalleSemana";
            lblResumenDetalleSemana.TabIndex = 7;
            //
            // tlpTarjetaPendientes
            //
            tlpTarjetaPendientes.AutoSize = true;
            tlpTarjetaPendientes.ColumnCount = 1;
            tlpTarjetaPendientes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaPendientes.Controls.Add(lblResumenTituloPendientes, 0, 0);
            tlpTarjetaPendientes.Controls.Add(lblResumenValorPendientes, 0, 1);
            tlpTarjetaPendientes.Controls.Add(lblResumenDetallePendientes, 0, 2);
            tlpTarjetaPendientes.Dock = DockStyle.Fill;
            tlpTarjetaPendientes.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaPendientes.Name = "tlpTarjetaPendientes";
            tlpTarjetaPendientes.RowCount = 3;
            tlpTarjetaPendientes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaPendientes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaPendientes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloPendientes
            //
            lblResumenTituloPendientes.AutoSize = true;
            lblResumenTituloPendientes.Name = "lblResumenTituloPendientes";
            lblResumenTituloPendientes.TabIndex = 8;
            lblResumenTituloPendientes.Text = "Para atender";
            //
            // lblResumenValorPendientes
            //
            lblResumenValorPendientes.AutoSize = true;
            lblResumenValorPendientes.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorPendientes.Name = "lblResumenValorPendientes";
            lblResumenValorPendientes.TabIndex = 9;
            lblResumenValorPendientes.Text = "0";
            //
            // lblResumenDetallePendientes
            //
            lblResumenDetallePendientes.AutoSize = true;
            lblResumenDetallePendientes.Margin = new Padding(3, 4, 3, 0);
            lblResumenDetallePendientes.Name = "lblResumenDetallePendientes";
            lblResumenDetallePendientes.TabIndex = 10;
            //
            // tlpTarjetaHoy
            //
            tlpTarjetaHoy.AutoSize = true;
            tlpTarjetaHoy.ColumnCount = 1;
            tlpTarjetaHoy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaHoy.Controls.Add(lblResumenTituloHoy, 0, 0);
            tlpTarjetaHoy.Controls.Add(lblResumenValorHoy, 0, 1);
            tlpTarjetaHoy.Controls.Add(lblResumenDetalleHoy, 0, 2);
            tlpTarjetaHoy.Dock = DockStyle.Fill;
            tlpTarjetaHoy.Margin = new Padding(0);
            tlpTarjetaHoy.Name = "tlpTarjetaHoy";
            tlpTarjetaHoy.RowCount = 3;
            tlpTarjetaHoy.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaHoy.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaHoy.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // lblResumenTituloHoy
            //
            lblResumenTituloHoy.AutoSize = true;
            lblResumenTituloHoy.Name = "lblResumenTituloHoy";
            lblResumenTituloHoy.TabIndex = 17;
            lblResumenTituloHoy.Text = "Hoy";
            //
            // lblResumenValorHoy
            //
            lblResumenValorHoy.AutoSize = true;
            lblResumenValorHoy.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorHoy.Name = "lblResumenValorHoy";
            lblResumenValorHoy.TabIndex = 18;
            lblResumenValorHoy.Text = "-";
            //
            // lblResumenDetalleHoy
            //
            lblResumenDetalleHoy.AutoSize = true;
            lblResumenDetalleHoy.Margin = new Padding(3, 4, 3, 0);
            lblResumenDetalleHoy.Name = "lblResumenDetalleHoy";
            lblResumenDetalleHoy.TabIndex = 19;
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
            lblProximosEventos.Anchor = AnchorStyles.Left;
            lblProximosEventos.AutoSize = true;
            lblProximosEventos.Margin = new Padding(3, 0, 3, 10);
            lblProximosEventos.Name = "lblProximosEventos";
            lblProximosEventos.TabIndex = 11;
            lblProximosEventos.Text = "Próximos eventos aprobados";
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
            dgvProximosEventos.TabIndex = 13;
            dgvProximosEventos.CellFormatting += dgvProximosEventos_CellFormatting;
            dgvProximosEventos.Enter += dgvProximosEventos_Enter;
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
            lblPendientes.Anchor = AnchorStyles.Left;
            lblPendientes.AutoSize = true;
            lblPendientes.Margin = new Padding(3, 0, 3, 10);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.TabIndex = 14;
            lblPendientes.Text = "Para atender";
            //
            // btnVerPresupuestos
            //
            btnVerPresupuestos.Anchor = AnchorStyles.Right;
            btnVerPresupuestos.AutoSize = true;
            btnVerPresupuestos.Margin = new Padding(0, 16, 0, 0);
            btnVerPresupuestos.Name = "btnVerPresupuestos";
            btnVerPresupuestos.Padding = new Padding(16, 0, 16, 0);
            btnVerPresupuestos.TabIndex = 15;
            btnVerPresupuestos.Text = "Ver presupuesto";
            btnVerPresupuestos.Click += btnVerPresupuestos_Click;
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
            dgvPendientes.TabIndex = 16;
            dgvPendientes.CellFormatting += dgvPendientes_CellFormatting;
            dgvPendientes.Enter += dgvPendientes_Enter;
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
            tlpTarjetaSemana.ResumeLayout(false);
            tlpTarjetaSemana.PerformLayout();
            tlpTarjetaPendientes.ResumeLayout(false);
            tlpTarjetaPendientes.PerformLayout();
            tlpTarjetaHoy.ResumeLayout(false);
            tlpTarjetaHoy.PerformLayout();
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
        private Label lblAlerta;
        private TableLayoutPanel tlpResumen;
        private TableLayoutPanel tlpTarjetaProximoEvento;
        private Label lblResumenTituloProximoEvento;
        private Label lblResumenValorProximoEvento;
        private Label lblResumenDetalleProximoEvento;
        private TableLayoutPanel tlpTarjetaSemana;
        private Label lblResumenTituloSemana;
        private Label lblResumenValorSemana;
        private Label lblResumenDetalleSemana;
        private TableLayoutPanel tlpTarjetaPendientes;
        private Label lblResumenTituloPendientes;
        private Label lblResumenValorPendientes;
        private Label lblResumenDetallePendientes;
        private TableLayoutPanel tlpTarjetaHoy;
        private Label lblResumenTituloHoy;
        private Label lblResumenValorHoy;
        private Label lblResumenDetalleHoy;
        private TableLayoutPanel tlpGrilla;
        private Label lblProximosEventos;
        private DataGridView dgvProximosEventos;
        private TableLayoutPanel tlpGrillaPendientes;
        private Label lblPendientes;
        private Button btnVerPresupuestos;
        private DataGridView dgvPendientes;
    }
}
