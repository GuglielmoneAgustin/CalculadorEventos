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
            flowBotones = new FlowLayoutPanel();
            lblInfoEvento = new Label();
            lblDgvPresupuestos = new Label();
            dgvPresupuestos = new DataGridView();
            btnAprobar = new Button();
            btnCancelar = new Button();
            btnImprimir = new Button();
            btnActualizar = new Button();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPresupuestos).BeginInit();
            tlpRaiz.SuspendLayout();
            flowBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 2;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRaiz.Controls.Add(lblInfoEvento, 0, 0);
            tlpRaiz.Controls.Add(lblDgvPresupuestos, 0, 1);
            tlpRaiz.SetColumnSpan(lblDgvPresupuestos, 2);
            tlpRaiz.Controls.Add(dgvPresupuestos, 0, 2);
            tlpRaiz.SetColumnSpan(dgvPresupuestos, 2);
            tlpRaiz.Controls.Add(flowBotones, 0, 3);
            tlpRaiz.SetColumnSpan(flowBotones, 2);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Padding = new Padding(50, 20, 50, 40);
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 4;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // flowBotones
            // 
            flowBotones.Controls.Add(btnAprobar);
            flowBotones.Controls.Add(btnActualizar);
            flowBotones.Controls.Add(btnCancelar);
            flowBotones.Controls.Add(btnImprimir);
            flowBotones.Controls.Add(btnVolver);
            flowBotones.AutoSize = true;
            flowBotones.Dock = DockStyle.Fill;
            flowBotones.FlowDirection = FlowDirection.RightToLeft;
            flowBotones.Margin = new Padding(3, 15, 3, 3);
            flowBotones.WrapContents = false;
            flowBotones.Name = "flowBotones";
            // 
            // lblInfoEvento
            // 
            lblInfoEvento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblInfoEvento.AutoSize = true;
            lblInfoEvento.Text = "Datos del evento";
            lblInfoEvento.Name = "lblInfoEvento";
            lblInfoEvento.TabIndex = 0;
            // 
            // lblDgvPresupuestos
            // 
            lblDgvPresupuestos.AutoSize = true;
            lblDgvPresupuestos.Text = "Presupuestos del evento";
            lblDgvPresupuestos.Margin = new Padding(3, 25, 3, 3);
            lblDgvPresupuestos.Name = "lblDgvPresupuestos";
            lblDgvPresupuestos.TabIndex = 1;
            // 
            // dgvPresupuestos
            // 
            dgvPresupuestos.AllowUserToAddRows = false;
            dgvPresupuestos.AllowUserToDeleteRows = false;
            dgvPresupuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPresupuestos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPresupuestos.Dock = DockStyle.Fill;
            dgvPresupuestos.MultiSelect = false;
            dgvPresupuestos.ReadOnly = true;
            dgvPresupuestos.CellFormatting += dgvPresupuestos_CellFormatting;
            dgvPresupuestos.RowHeadersWidth = 51;
            dgvPresupuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPresupuestos.Name = "dgvPresupuestos";
            dgvPresupuestos.TabIndex = 2;
            // 
            // btnAprobar
            // 
            btnAprobar.Margin = new Padding(0);
            btnAprobar.Size = new Size(160, 38);
            btnAprobar.Text = "Aprobar";
            btnAprobar.UseVisualStyleBackColor = true;
            btnAprobar.Name = "btnAprobar";
            btnAprobar.TabIndex = 3;
            btnAprobar.Click += btnAprobar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Margin = new Padding(10, 0, 0, 0);
            btnCancelar.Size = new Size(160, 38);
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.TabIndex = 4;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Margin = new Padding(10, 0, 10, 0);
            btnActualizar.Size = new Size(200, 38);
            btnActualizar.Text = "Actualizar presupuesto";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.TabIndex = 7;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Margin = new Padding(10, 0, 0, 0);
            btnImprimir.Size = new Size(160, 38);
            btnImprimir.Text = "Imprimir ticket";
            btnImprimir.UseVisualStyleBackColor = true;
            btnImprimir.Name = "btnImprimir";
            btnImprimir.TabIndex = 5;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // btnVolver
            // 
            btnVolver.Margin = new Padding(10, 0, 0, 0);
            btnVolver.Size = new Size(160, 38);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Name = "btnVolver";
            btnVolver.TabIndex = 6;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmPresupuestos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(900, 500);
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            Name = "FrmPresupuestos";
            Text = "Presupuestos del evento";
            Load += FrmPresupuestos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPresupuestos).EndInit();
            flowBotones.ResumeLayout(false);
            flowBotones.PerformLayout();
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private FlowLayoutPanel flowBotones;
        private Label lblInfoEvento;
        private Label lblDgvPresupuestos;
        private DataGridView dgvPresupuestos;
        private Button btnAprobar;
        private Button btnCancelar;
        private Button btnImprimir;
        private Button btnActualizar;
        private Button btnVolver;
    }
}
