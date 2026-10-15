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
            tlpResumen = new TableLayoutPanel();
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
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpResumen.SuspendLayout();
            tlpTarjetaProximos.SuspendLayout();
            tlpTarjetaEnEspera.SuspendLayout();
            tlpTarjetaMes.SuspendLayout();
            tlpGrilla.SuspendLayout();
            SuspendLayout();
            //
            // tlpRaiz
            //
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(tlpResumen, 0, 0);
            tlpRaiz.Controls.Add(tlpGrilla, 0, 1);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // tlpResumen
            //
            tlpResumen.AutoSize = true;
            tlpResumen.ColumnCount = 3;
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tlpResumen.Controls.Add(tlpTarjetaProximos, 0, 0);
            tlpResumen.Controls.Add(tlpTarjetaEnEspera, 1, 0);
            tlpResumen.Controls.Add(tlpTarjetaMes, 2, 0);
            tlpResumen.Dock = DockStyle.Fill;
            tlpResumen.Margin = new Padding(0, 0, 0, 20);
            tlpResumen.Name = "tlpResumen";
            tlpResumen.RowCount = 1;
            tlpResumen.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
            lblResumenTituloProximos.TabIndex = 0;
            lblResumenTituloProximos.Text = "Próximos eventos";
            //
            // lblResumenValorProximos
            //
            lblResumenValorProximos.AutoSize = true;
            lblResumenValorProximos.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorProximos.Name = "lblResumenValorProximos";
            lblResumenValorProximos.TabIndex = 1;
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
            lblResumenTituloEnEspera.TabIndex = 2;
            lblResumenTituloEnEspera.Text = "Eventos esperando aprobación";
            //
            // lblResumenValorEnEspera
            //
            lblResumenValorEnEspera.AutoSize = true;
            lblResumenValorEnEspera.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorEnEspera.Name = "lblResumenValorEnEspera";
            lblResumenValorEnEspera.TabIndex = 3;
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
            lblResumenTituloMes.TabIndex = 4;
            lblResumenTituloMes.Text = "Aprobado este mes";
            //
            // lblResumenValorMes
            //
            lblResumenValorMes.AutoSize = true;
            lblResumenValorMes.Margin = new Padding(3, 6, 3, 0);
            lblResumenValorMes.Name = "lblResumenValorMes";
            lblResumenValorMes.TabIndex = 5;
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
            lblProximosEventos.TabIndex = 6;
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
            dgvProximosEventos.TabIndex = 7;
            //
            // FrmInicio
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(tlpRaiz);
            Name = "FrmInicio";
            Text = "Inicio";
            Load += FrmInicio_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).EndInit();
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
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpResumen;
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
    }
}
