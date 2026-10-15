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
            tlpGrilla = new TableLayoutPanel();
            lblProximosEventos = new Label();
            dgvProximosEventos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvProximosEventos).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpGrilla.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(tlpGrilla, 0, 0);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 1;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            lblProximosEventos.TabIndex = 0;
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
            dgvProximosEventos.TabIndex = 1;
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
            tlpGrilla.ResumeLayout(false);
            tlpGrilla.PerformLayout();
            tlpRaiz.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpGrilla;
        private Label lblProximosEventos;
        private DataGridView dgvProximosEventos;
    }
}
