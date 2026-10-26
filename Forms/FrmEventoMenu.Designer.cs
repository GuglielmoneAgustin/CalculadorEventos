namespace ProyectoEventos.Forms
{
    partial class FrmEventoMenu
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
            tlpContenido = new TableLayoutPanel();
            tlpTarjetaProductos = new TableLayoutPanel();
            tlpTarjetaMenu = new TableLayoutPanel();
            tlpTarjetaServicios = new TableLayoutPanel();
            flowAccionesMenu = new FlowLayoutPanel();
            flowBotones = new FlowLayoutPanel();
            lblInfoEvento = new Label();
            lblComponentes = new Label();
            lblDgvItemsMenu = new Label();
            lvProductos = new ListView();
            lblFiltroRestriccion = new Label();
            cmbFiltroRestriccion = new ComboBox();
            lblSubtotalPlato = new Label();
            lblCantAdultosItem = new Label();
            numCantAdultosItem = new NumericUpDown();
            lblCantNinosItem = new Label();
            numCantNinosItem = new NumericUpDown();
            btnAgregarItem = new Button();
            btnCancelarEdicion = new Button();
            dgvItemsMenu = new DataGridView();
            btnQuitarItem = new Button();
            btnEditarItem = new Button();
            lblTotalMenu = new Label();
            lblServicios = new Label();
            clbServicios = new CheckedListBox();
            btnAgregarServicio = new Button();
            dgvServiciosEvento = new DataGridView();
            btnQuitarServicio = new Button();
            btnVolver = new Button();
            btnFinalizar = new Button();
            ((System.ComponentModel.ISupportInitialize)numCantAdultosItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantNinosItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvItemsMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvServiciosEvento).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpContenido.SuspendLayout();
            tlpTarjetaProductos.SuspendLayout();
            tlpTarjetaMenu.SuspendLayout();
            tlpTarjetaServicios.SuspendLayout();
            flowAccionesMenu.SuspendLayout();
            flowBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(lblInfoEvento, 0, 0);
            tlpRaiz.Controls.Add(tlpContenido, 0, 1);
            tlpRaiz.Controls.Add(flowBotones, 0, 2);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 3;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // lblInfoEvento
            // 
            lblInfoEvento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblInfoEvento.AutoSize = true;
            lblInfoEvento.Margin = new Padding(3, 0, 3, 14);
            lblInfoEvento.Name = "lblInfoEvento";
            lblInfoEvento.TabIndex = 0;
            // 
            // tlpContenido
            // 
            tlpContenido.ColumnCount = 3;
            tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpContenido.Controls.Add(tlpTarjetaProductos, 0, 0);
            tlpContenido.Controls.Add(tlpTarjetaMenu, 1, 0);
            tlpContenido.Controls.Add(tlpTarjetaServicios, 2, 0);
            tlpContenido.Dock = DockStyle.Fill;
            tlpContenido.Margin = new Padding(0);
            tlpContenido.Name = "tlpContenido";
            tlpContenido.RowCount = 1;
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // tlpTarjetaProductos
            // 
            tlpTarjetaProductos.ColumnCount = 2;
            tlpTarjetaProductos.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpTarjetaProductos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaProductos.Controls.Add(lblComponentes, 0, 0);
            tlpTarjetaProductos.SetColumnSpan(lblComponentes, 2);
            tlpTarjetaProductos.Controls.Add(lblFiltroRestriccion, 0, 1);
            tlpTarjetaProductos.Controls.Add(cmbFiltroRestriccion, 1, 1);
            tlpTarjetaProductos.Controls.Add(lvProductos, 0, 2);
            tlpTarjetaProductos.SetColumnSpan(lvProductos, 2);
            tlpTarjetaProductos.Controls.Add(lblCantAdultosItem, 0, 3);
            tlpTarjetaProductos.Controls.Add(numCantAdultosItem, 1, 3);
            tlpTarjetaProductos.Controls.Add(lblCantNinosItem, 0, 4);
            tlpTarjetaProductos.Controls.Add(numCantNinosItem, 1, 4);
            tlpTarjetaProductos.Controls.Add(lblSubtotalPlato, 0, 5);
            tlpTarjetaProductos.SetColumnSpan(lblSubtotalPlato, 2);
            tlpTarjetaProductos.Controls.Add(btnAgregarItem, 0, 6);
            tlpTarjetaProductos.SetColumnSpan(btnAgregarItem, 2);
            tlpTarjetaProductos.Controls.Add(btnCancelarEdicion, 0, 7);
            tlpTarjetaProductos.SetColumnSpan(btnCancelarEdicion, 2);
            tlpTarjetaProductos.Dock = DockStyle.Fill;
            tlpTarjetaProductos.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaProductos.Name = "tlpTarjetaProductos";
            tlpTarjetaProductos.RowCount = 8;
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaProductos.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // tlpTarjetaMenu
            // 
            tlpTarjetaMenu.ColumnCount = 1;
            tlpTarjetaMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaMenu.Controls.Add(lblDgvItemsMenu, 0, 0);
            tlpTarjetaMenu.Controls.Add(dgvItemsMenu, 0, 1);
            tlpTarjetaMenu.Controls.Add(flowAccionesMenu, 0, 2);
            tlpTarjetaMenu.Controls.Add(lblTotalMenu, 0, 3);
            tlpTarjetaMenu.Dock = DockStyle.Fill;
            tlpTarjetaMenu.Margin = new Padding(0, 0, 20, 0);
            tlpTarjetaMenu.Name = "tlpTarjetaMenu";
            tlpTarjetaMenu.RowCount = 4;
            tlpTarjetaMenu.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTarjetaMenu.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaMenu.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // tlpTarjetaServicios
            // 
            tlpTarjetaServicios.ColumnCount = 1;
            tlpTarjetaServicios.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaServicios.Controls.Add(lblServicios, 0, 0);
            tlpTarjetaServicios.Controls.Add(clbServicios, 0, 1);
            tlpTarjetaServicios.Controls.Add(btnAgregarServicio, 0, 2);
            tlpTarjetaServicios.Controls.Add(dgvServiciosEvento, 0, 3);
            tlpTarjetaServicios.Controls.Add(btnQuitarServicio, 0, 4);
            tlpTarjetaServicios.Dock = DockStyle.Fill;
            tlpTarjetaServicios.Margin = new Padding(0);
            tlpTarjetaServicios.Name = "tlpTarjetaServicios";
            tlpTarjetaServicios.RowCount = 5;
            tlpTarjetaServicios.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaServicios.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlpTarjetaServicios.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaServicios.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tlpTarjetaServicios.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // 
            // flowAccionesMenu
            // 
            flowAccionesMenu.Controls.Add(btnQuitarItem);
            flowAccionesMenu.Controls.Add(btnEditarItem);
            flowAccionesMenu.AutoSize = true;
            flowAccionesMenu.Dock = DockStyle.Fill;
            flowAccionesMenu.Margin = new Padding(0, 12, 0, 0);
            flowAccionesMenu.WrapContents = false;
            flowAccionesMenu.Name = "flowAccionesMenu";
            // 
            // flowBotones
            // 
            flowBotones.Controls.Add(btnFinalizar);
            flowBotones.Controls.Add(btnVolver);
            flowBotones.AutoSize = true;
            flowBotones.Dock = DockStyle.Fill;
            flowBotones.FlowDirection = FlowDirection.RightToLeft;
            flowBotones.Margin = new Padding(3, 16, 3, 0);
            flowBotones.Name = "flowBotones";
            // 
            // lblDgvItemsMenu
            lblDgvItemsMenu.AutoSize = true;
            lblDgvItemsMenu.Margin = new Padding(3, 0, 3, 10);
            lblDgvItemsMenu.Name = "lblDgvItemsMenu";
            lblDgvItemsMenu.TabIndex = 18;
            lblDgvItemsMenu.Text = "Menú del evento";
            // lblComponentes
            // 
            lblComponentes.AutoSize = true;
            lblComponentes.Margin = new Padding(3, 0, 3, 10);
            lblComponentes.Name = "lblComponentes";
            lblComponentes.TabIndex = 1;
            lblComponentes.Text = "Productos disponibles";
            // 
            // lblFiltroRestriccion
            // 
            lblFiltroRestriccion.Anchor = AnchorStyles.Left;
            lblFiltroRestriccion.AutoSize = true;
            lblFiltroRestriccion.Margin = new Padding(3, 0, 12, 8);
            lblFiltroRestriccion.Name = "lblFiltroRestriccion";
            lblFiltroRestriccion.TabIndex = 21;
            lblFiltroRestriccion.Text = "Apto para:";
            // 
            // cmbFiltroRestriccion
            // 
            cmbFiltroRestriccion.Dock = DockStyle.Fill;
            cmbFiltroRestriccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroRestriccion.FormattingEnabled = true;
            cmbFiltroRestriccion.Margin = new Padding(3, 0, 3, 8);
            cmbFiltroRestriccion.Name = "cmbFiltroRestriccion";
            cmbFiltroRestriccion.TabIndex = 22;
            cmbFiltroRestriccion.SelectedIndexChanged += cmbFiltroRestriccion_SelectedIndexChanged;
            // 
            // lvProductos
            // 
            lvProductos.CheckBoxes = true;
            lvProductos.Dock = DockStyle.Fill;
            lvProductos.MinimumSize = new Size(0, 150);
            lvProductos.FullRowSelect = true;
            lvProductos.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvProductos.MultiSelect = false;
            lvProductos.Name = "lvProductos";
            lvProductos.ShowGroups = true;
            lvProductos.TabIndex = 2;
            lvProductos.UseCompatibleStateImageBehavior = false;
            lvProductos.View = View.Details;
            lvProductos.ItemChecked += lvProductos_ItemChecked;
            lvProductos.Resize += lvProductos_Resize;
            // 
            // lblSubtotalPlato
            // 
            lblSubtotalPlato.AutoSize = true;
            lblSubtotalPlato.Margin = new Padding(3, 10, 3, 0);
            lblSubtotalPlato.Name = "lblSubtotalPlato";
            lblSubtotalPlato.TabIndex = 23;
            // 
            // lblCantAdultosItem
            // 
            lblCantAdultosItem.Anchor = AnchorStyles.Left;
            lblCantAdultosItem.AutoSize = true;
            lblCantAdultosItem.Margin = new Padding(3, 12, 12, 3);
            lblCantAdultosItem.Name = "lblCantAdultosItem";
            lblCantAdultosItem.TabIndex = 3;
            lblCantAdultosItem.Text = "Cant. adultos";
            // 
            // numCantAdultosItem
            // 
            numCantAdultosItem.Dock = DockStyle.Fill;
            numCantAdultosItem.Margin = new Padding(3, 12, 3, 3);
            numCantAdultosItem.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantAdultosItem.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numCantAdultosItem.Name = "numCantAdultosItem";
            numCantAdultosItem.TabIndex = 4;
            numCantAdultosItem.ValueChanged += CantidadesItem_ValueChanged;
            // 
            // lblCantNinosItem
            // 
            lblCantNinosItem.Anchor = AnchorStyles.Left;
            lblCantNinosItem.AutoSize = true;
            lblCantNinosItem.Margin = new Padding(3, 3, 12, 3);
            lblCantNinosItem.Name = "lblCantNinosItem";
            lblCantNinosItem.TabIndex = 5;
            lblCantNinosItem.Text = "Cant. niños";
            // 
            // numCantNinosItem
            // 
            numCantNinosItem.Dock = DockStyle.Fill;
            numCantNinosItem.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCantNinosItem.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numCantNinosItem.Name = "numCantNinosItem";
            numCantNinosItem.TabIndex = 6;
            numCantNinosItem.ValueChanged += CantidadesItem_ValueChanged;
            // 
            // btnAgregarItem
            // 
            btnAgregarItem.Dock = DockStyle.Fill;
            btnAgregarItem.Margin = new Padding(3, 14, 3, 0);
            btnAgregarItem.Name = "btnAgregarItem";
            btnAgregarItem.Size = new Size(260, 38);
            btnAgregarItem.TabIndex = 7;
            btnAgregarItem.Text = "Agregar plato";
            btnAgregarItem.UseVisualStyleBackColor = true;
            btnAgregarItem.Click += btnAgregarItem_Click;
            //
            // btnCancelarEdicion
            //
            btnCancelarEdicion.Dock = DockStyle.Fill;
            btnCancelarEdicion.Margin = new Padding(3, 8, 3, 0);
            btnCancelarEdicion.Name = "btnCancelarEdicion";
            btnCancelarEdicion.Size = new Size(260, 38);
            btnCancelarEdicion.TabIndex = 20;
            btnCancelarEdicion.Text = "Cancelar edición";
            btnCancelarEdicion.UseVisualStyleBackColor = true;
            btnCancelarEdicion.Visible = false;
            btnCancelarEdicion.Click += btnCancelarEdicion_Click;
            // 
            // dgvItemsMenu
            // 
            dgvItemsMenu.AllowUserToAddRows = false;
            dgvItemsMenu.AllowUserToDeleteRows = false;
            dgvItemsMenu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItemsMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItemsMenu.Dock = DockStyle.Fill;
            dgvItemsMenu.MultiSelect = false;
            dgvItemsMenu.Name = "dgvItemsMenu";
            dgvItemsMenu.ReadOnly = true;
            dgvItemsMenu.RowHeadersWidth = 51;
            dgvItemsMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItemsMenu.TabIndex = 8;
            //
            // btnEditarItem
            //
            btnEditarItem.Margin = new Padding(10, 0, 0, 0);
            btnEditarItem.Name = "btnEditarItem";
            btnEditarItem.Size = new Size(160, 38);
            btnEditarItem.TabIndex = 19;
            btnEditarItem.Text = "Editar plato";
            btnEditarItem.UseVisualStyleBackColor = true;
            btnEditarItem.Click += btnEditarItem_Click;
            // 
            // btnQuitarItem
            // 
            btnQuitarItem.Margin = new Padding(0);
            btnQuitarItem.Name = "btnQuitarItem";
            btnQuitarItem.Size = new Size(160, 38);
            btnQuitarItem.TabIndex = 9;
            btnQuitarItem.Text = "Quitar plato";
            btnQuitarItem.UseVisualStyleBackColor = true;
            btnQuitarItem.Click += btnQuitarItem_Click;
            // 
            // lblTotalMenu
            // 
            lblTotalMenu.AutoSize = true;
            lblTotalMenu.Margin = new Padding(3, 12, 3, 0);
            lblTotalMenu.Name = "lblTotalMenu";
            lblTotalMenu.TabIndex = 10;
            lblTotalMenu.Text = "Total menú: $0";
            // 
            // lblServicios
            // 
            lblServicios.AutoSize = true;
            lblServicios.Margin = new Padding(3, 0, 3, 10);
            lblServicios.Name = "lblServicios";
            lblServicios.TabIndex = 11;
            lblServicios.Text = "Servicios adicionales";
            // 
            // clbServicios
            // 
            clbServicios.CheckOnClick = true;
            clbServicios.Dock = DockStyle.Fill;
            clbServicios.Name = "clbServicios";
            clbServicios.TabIndex = 12;
            // 
            // btnAgregarServicio
            // 
            btnAgregarServicio.Anchor = AnchorStyles.Left;
            btnAgregarServicio.Margin = new Padding(3, 12, 3, 12);
            btnAgregarServicio.Name = "btnAgregarServicio";
            btnAgregarServicio.Size = new Size(170, 38);
            btnAgregarServicio.TabIndex = 13;
            btnAgregarServicio.Text = "Agregar servicio";
            btnAgregarServicio.UseVisualStyleBackColor = true;
            btnAgregarServicio.Click += btnAgregarServicio_Click;
            // 
            // dgvServiciosEvento
            // 
            dgvServiciosEvento.AllowUserToAddRows = false;
            dgvServiciosEvento.AllowUserToDeleteRows = false;
            dgvServiciosEvento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServiciosEvento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServiciosEvento.Dock = DockStyle.Fill;
            dgvServiciosEvento.MultiSelect = false;
            dgvServiciosEvento.Name = "dgvServiciosEvento";
            dgvServiciosEvento.ReadOnly = true;
            dgvServiciosEvento.RowHeadersWidth = 51;
            dgvServiciosEvento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServiciosEvento.TabIndex = 14;
            // 
            // btnQuitarServicio
            // 
            btnQuitarServicio.Anchor = AnchorStyles.Left;
            btnQuitarServicio.Margin = new Padding(3, 12, 3, 0);
            btnQuitarServicio.Name = "btnQuitarServicio";
            btnQuitarServicio.Size = new Size(170, 38);
            btnQuitarServicio.TabIndex = 15;
            btnQuitarServicio.Text = "Quitar servicio";
            btnQuitarServicio.UseVisualStyleBackColor = true;
            btnQuitarServicio.Click += btnQuitarServicio_Click;
            // 
            // btnVolver
            // 
            btnVolver.Margin = new Padding(0, 0, 12, 0);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(160, 38);
            btnVolver.TabIndex = 16;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnFinalizar
            // 
            btnFinalizar.Margin = new Padding(0);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(160, 38);
            btnFinalizar.TabIndex = 17;
            btnFinalizar.Text = "Finalizar";
            btnFinalizar.UseVisualStyleBackColor = true;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // FrmEventoMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(880, 540);
            ClientSize = new Size(1240, 740);
            Controls.Add(tlpRaiz);
            Name = "FrmEventoMenu";
            Text = "Menú y servicios del evento";
            Load += FrmEventoMenu_Load;
            ((System.ComponentModel.ISupportInitialize)numCantAdultosItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantNinosItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvItemsMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvServiciosEvento).EndInit();
            flowBotones.ResumeLayout(false);
            flowAccionesMenu.ResumeLayout(false);
            flowAccionesMenu.PerformLayout();
            tlpTarjetaServicios.ResumeLayout(false);
            tlpTarjetaServicios.PerformLayout();
            tlpTarjetaMenu.ResumeLayout(false);
            tlpTarjetaMenu.PerformLayout();
            tlpTarjetaProductos.ResumeLayout(false);
            tlpTarjetaProductos.PerformLayout();
            tlpContenido.ResumeLayout(false);
            tlpRaiz.ResumeLayout(false);
            tlpRaiz.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpContenido;
        private TableLayoutPanel tlpTarjetaProductos;
        private TableLayoutPanel tlpTarjetaMenu;
        private TableLayoutPanel tlpTarjetaServicios;
        private FlowLayoutPanel flowAccionesMenu;
        private FlowLayoutPanel flowBotones;
        private Label lblInfoEvento;
        private Label lblComponentes;
        private Label lblDgvItemsMenu;
        private ListView lvProductos;
        private Label lblFiltroRestriccion;
        private ComboBox cmbFiltroRestriccion;
        private Label lblSubtotalPlato;
        private Label lblCantAdultosItem;
        private NumericUpDown numCantAdultosItem;
        private Label lblCantNinosItem;
        private NumericUpDown numCantNinosItem;
        private Button btnAgregarItem;
        private Button btnCancelarEdicion;
        private DataGridView dgvItemsMenu;
        private Button btnQuitarItem;
        private Button btnEditarItem;
        private Label lblTotalMenu;
        private Label lblServicios;
        private CheckedListBox clbServicios;
        private Button btnAgregarServicio;
        private DataGridView dgvServiciosEvento;
        private Button btnQuitarServicio;
        private Button btnVolver;
        private Button btnFinalizar;
    }
}
