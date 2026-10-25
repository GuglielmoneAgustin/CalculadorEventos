namespace ProyectoEventos.Forms
{
    partial class FrmNegocio
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
            lblModo = new Label();
            lblNombre = new Label();
            txtBoxNombre = new TextBox();
            lblDireccion = new Label();
            txtBoxDireccion = new TextBox();
            lblTelefono = new Label();
            txtBoxTelefono = new TextBox();
            lblMail = new Label();
            txtBoxMail = new TextBox();
            lblCuit = new Label();
            txtBoxCuit = new TextBox();
            lblInstagram = new Label();
            txtBoxInstagram = new TextBox();
            lblMensajePie = new Label();
            txtBoxMensajePie = new TextBox();
            lblDiasValidez = new Label();
            numDiasValidez = new NumericUpDown();
            lblPorcentajeSena = new Label();
            numPorcentajeSena = new NumericUpDown();
            lblAyuda = new Label();
            tlpBotones = new TableLayoutPanel();
            btnEditar = new Button();
            btnGuardar = new Button();
            btnCancelarEdicion = new Button();
            lblAviso = new Label();
            tlpTarjetaVistaPrevia = new TableLayoutPanel();
            lblVistaPrevia = new Label();
            txtVistaPrevia = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numDiasValidez).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPorcentajeSena).BeginInit();
            tlpRaiz.SuspendLayout();
            tlpFormulario.SuspendLayout();
            tlpBotones.SuspendLayout();
            tlpTarjetaVistaPrevia.SuspendLayout();
            SuspendLayout();
            //
            // tlpRaiz
            //
            tlpRaiz.ColumnCount = 2;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpRaiz.Controls.Add(tlpFormulario, 0, 0);
            tlpRaiz.Controls.Add(tlpTarjetaVistaPrevia, 1, 0);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 1;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // tlpFormulario
            //
            tlpFormulario.ColumnCount = 2;
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFormulario.Controls.Add(lblModo, 0, 0);
            tlpFormulario.Controls.Add(lblNombre, 0, 1);
            tlpFormulario.Controls.Add(txtBoxNombre, 0, 2);
            tlpFormulario.Controls.Add(lblDireccion, 0, 3);
            tlpFormulario.Controls.Add(txtBoxDireccion, 0, 4);
            tlpFormulario.Controls.Add(lblTelefono, 0, 5);
            tlpFormulario.Controls.Add(lblMail, 1, 5);
            tlpFormulario.Controls.Add(txtBoxTelefono, 0, 6);
            tlpFormulario.Controls.Add(txtBoxMail, 1, 6);
            tlpFormulario.Controls.Add(lblCuit, 0, 7);
            tlpFormulario.Controls.Add(lblInstagram, 1, 7);
            tlpFormulario.Controls.Add(txtBoxCuit, 0, 8);
            tlpFormulario.Controls.Add(txtBoxInstagram, 1, 8);
            tlpFormulario.Controls.Add(lblMensajePie, 0, 9);
            tlpFormulario.Controls.Add(txtBoxMensajePie, 0, 10);
            tlpFormulario.Controls.Add(lblDiasValidez, 0, 11);
            tlpFormulario.Controls.Add(lblPorcentajeSena, 1, 11);
            tlpFormulario.Controls.Add(numDiasValidez, 0, 12);
            tlpFormulario.Controls.Add(numPorcentajeSena, 1, 12);
            tlpFormulario.Controls.Add(lblAyuda, 0, 13);
            tlpFormulario.Controls.Add(tlpBotones, 0, 14);
            tlpFormulario.Controls.Add(lblAviso, 0, 15);
            tlpFormulario.SetColumnSpan(lblModo, 2);
            tlpFormulario.SetColumnSpan(lblNombre, 2);
            tlpFormulario.SetColumnSpan(txtBoxNombre, 2);
            tlpFormulario.SetColumnSpan(lblDireccion, 2);
            tlpFormulario.SetColumnSpan(txtBoxDireccion, 2);
            tlpFormulario.SetColumnSpan(lblMensajePie, 2);
            tlpFormulario.SetColumnSpan(txtBoxMensajePie, 2);
            tlpFormulario.SetColumnSpan(lblAyuda, 2);
            tlpFormulario.SetColumnSpan(tlpBotones, 2);
            tlpFormulario.SetColumnSpan(lblAviso, 2);
            tlpFormulario.Dock = DockStyle.Fill;
            tlpFormulario.Name = "tlpFormulario";
            tlpFormulario.RowCount = 17;
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // lblModo
            //
            lblModo.AutoSize = true;
            lblModo.Margin = new Padding(3, 0, 3, 2);
            lblModo.Name = "lblModo";
            lblModo.TabIndex = 0;
            lblModo.Text = "Datos del negocio";
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Margin = new Padding(3, 8, 3, 3);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre del local: *";
            //
            // txtBoxNombre
            //
            txtBoxNombre.Dock = DockStyle.Fill;
            txtBoxNombre.Name = "txtBoxNombre";
            txtBoxNombre.TabIndex = 1;
            txtBoxNombre.TextChanged += Campo_Changed;
            //
            // lblDireccion
            //
            lblDireccion.AutoSize = true;
            lblDireccion.Margin = new Padding(3, 8, 3, 3);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Text = "Dirección:";
            //
            // txtBoxDireccion
            //
            txtBoxDireccion.Dock = DockStyle.Fill;
            txtBoxDireccion.Name = "txtBoxDireccion";
            txtBoxDireccion.TabIndex = 2;
            txtBoxDireccion.TextChanged += Campo_Changed;
            //
            // lblTelefono
            //
            lblTelefono.AutoSize = true;
            lblTelefono.Margin = new Padding(3, 8, 3, 3);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Text = "Teléfono / WhatsApp:";
            //
            // txtBoxTelefono
            //
            txtBoxTelefono.Dock = DockStyle.Fill;
            txtBoxTelefono.Name = "txtBoxTelefono";
            txtBoxTelefono.TabIndex = 3;
            txtBoxTelefono.TextChanged += Campo_Changed;
            //
            // lblMail
            //
            lblMail.AutoSize = true;
            lblMail.Margin = new Padding(3, 8, 3, 3);
            lblMail.Name = "lblMail";
            lblMail.Text = "Mail:";
            //
            // txtBoxMail
            //
            txtBoxMail.Dock = DockStyle.Fill;
            txtBoxMail.Name = "txtBoxMail";
            txtBoxMail.TabIndex = 4;
            txtBoxMail.TextChanged += Campo_Changed;
            //
            // lblCuit
            //
            lblCuit.AutoSize = true;
            lblCuit.Margin = new Padding(3, 8, 3, 3);
            lblCuit.Name = "lblCuit";
            lblCuit.Text = "CUIT:";
            //
            // txtBoxCuit
            //
            txtBoxCuit.Dock = DockStyle.Fill;
            txtBoxCuit.Name = "txtBoxCuit";
            txtBoxCuit.PlaceholderText = "20-12345678-9";
            txtBoxCuit.TabIndex = 5;
            txtBoxCuit.TextChanged += Campo_Changed;
            //
            // lblInstagram
            //
            lblInstagram.AutoSize = true;
            lblInstagram.Margin = new Padding(3, 8, 3, 3);
            lblInstagram.Name = "lblInstagram";
            lblInstagram.Text = "Instagram:";
            //
            // txtBoxInstagram
            //
            txtBoxInstagram.Dock = DockStyle.Fill;
            txtBoxInstagram.Name = "txtBoxInstagram";
            txtBoxInstagram.PlaceholderText = "@minegocio";
            txtBoxInstagram.TabIndex = 6;
            txtBoxInstagram.TextChanged += Campo_Changed;
            //
            // lblMensajePie
            //
            lblMensajePie.AutoSize = true;
            lblMensajePie.Margin = new Padding(3, 8, 3, 3);
            lblMensajePie.Name = "lblMensajePie";
            lblMensajePie.Text = "Mensaje al pie del ticket:";
            //
            // txtBoxMensajePie
            //
            txtBoxMensajePie.Dock = DockStyle.Fill;
            txtBoxMensajePie.Name = "txtBoxMensajePie";
            txtBoxMensajePie.TabIndex = 7;
            txtBoxMensajePie.TextChanged += Campo_Changed;
            //
            // lblDiasValidez
            //
            lblDiasValidez.AutoSize = true;
            lblDiasValidez.Margin = new Padding(3, 8, 3, 3);
            lblDiasValidez.Name = "lblDiasValidez";
            lblDiasValidez.Text = "Validez del presupuesto (días):";
            //
            // numDiasValidez
            //
            numDiasValidez.Dock = DockStyle.Fill;
            numDiasValidez.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numDiasValidez.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numDiasValidez.Name = "numDiasValidez";
            numDiasValidez.TabIndex = 8;
            numDiasValidez.ValueChanged += Campo_Changed;
            //
            // lblPorcentajeSena
            //
            lblPorcentajeSena.AutoSize = true;
            lblPorcentajeSena.Margin = new Padding(3, 8, 3, 3);
            lblPorcentajeSena.Name = "lblPorcentajeSena";
            lblPorcentajeSena.Text = "Seña sugerida (%):";
            //
            // numPorcentajeSena
            //
            numPorcentajeSena.Dock = DockStyle.Fill;
            numPorcentajeSena.DecimalPlaces = 2;
            numPorcentajeSena.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPorcentajeSena.Minimum = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            numPorcentajeSena.Name = "numPorcentajeSena";
            numPorcentajeSena.TabIndex = 9;
            numPorcentajeSena.ValueChanged += Campo_Changed;
            //
            // lblAyuda
            //
            lblAyuda.AutoSize = true;
            lblAyuda.Margin = new Padding(3, 8, 3, 3);
            lblAyuda.MaximumSize = new Size(560, 0);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Text = "Con validez 0 el ticket no muestra hasta cuándo vale el presupuesto. La seña sugerida completa el campo Seña al cobrar un presupuesto aprobado.";
            //
            // tlpBotones
            //
            tlpBotones.AutoSize = true;
            tlpBotones.ColumnCount = 2;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBotones.Controls.Add(btnGuardar, 0, 0);
            tlpBotones.Controls.Add(btnCancelarEdicion, 1, 0);
            tlpBotones.Controls.Add(btnEditar, 0, 1);
            tlpBotones.SetColumnSpan(btnEditar, 2);
            tlpBotones.Dock = DockStyle.Top;
            tlpBotones.Margin = new Padding(3, 16, 3, 3);
            tlpBotones.Name = "tlpBotones";
            tlpBotones.RowCount = 2;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // btnEditar
            //
            btnEditar.Dock = DockStyle.Fill;
            btnEditar.Margin = new Padding(0);
            btnEditar.Name = "btnEditar";
            btnEditar.TabIndex = 10;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            //
            // btnGuardar
            //
            btnGuardar.Dock = DockStyle.Fill;
            btnGuardar.Margin = new Padding(0);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnCancelarEdicion
            //
            btnCancelarEdicion.Dock = DockStyle.Fill;
            btnCancelarEdicion.Margin = new Padding(8, 0, 0, 0);
            btnCancelarEdicion.Name = "btnCancelarEdicion";
            btnCancelarEdicion.TabIndex = 12;
            btnCancelarEdicion.Text = "Cancelar";
            btnCancelarEdicion.Click += btnCancelarEdicion_Click;
            //
            // lblAviso
            //
            lblAviso.AutoSize = true;
            lblAviso.Margin = new Padding(3, 6, 3, 0);
            lblAviso.Name = "lblAviso";
            //
            // tlpTarjetaVistaPrevia
            //
            tlpTarjetaVistaPrevia.ColumnCount = 1;
            tlpTarjetaVistaPrevia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTarjetaVistaPrevia.Controls.Add(lblVistaPrevia, 0, 0);
            tlpTarjetaVistaPrevia.Controls.Add(txtVistaPrevia, 0, 1);
            tlpTarjetaVistaPrevia.Dock = DockStyle.Fill;
            tlpTarjetaVistaPrevia.Name = "tlpTarjetaVistaPrevia";
            tlpTarjetaVistaPrevia.RowCount = 2;
            tlpTarjetaVistaPrevia.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTarjetaVistaPrevia.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // lblVistaPrevia
            //
            lblVistaPrevia.AutoSize = true;
            lblVistaPrevia.Margin = new Padding(3, 0, 3, 8);
            lblVistaPrevia.Name = "lblVistaPrevia";
            lblVistaPrevia.Text = "Vista previa del ticket";
            //
            // txtVistaPrevia
            //
            txtVistaPrevia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            txtVistaPrevia.Multiline = true;
            txtVistaPrevia.Name = "txtVistaPrevia";
            txtVistaPrevia.ReadOnly = true;
            txtVistaPrevia.ScrollBars = ScrollBars.Vertical;
            txtVistaPrevia.TabStop = false;
            //
            // FrmNegocio
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(tlpRaiz);
            MinimumSize = new Size(1000, 650);
            Name = "FrmNegocio";
            Text = "Mi negocio";
            WindowState = FormWindowState.Maximized;
            Load += FrmNegocio_Load;
            Shown += FrmNegocio_Shown;
            FormClosing += FrmNegocio_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numDiasValidez).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPorcentajeSena).EndInit();
            tlpTarjetaVistaPrevia.ResumeLayout(false);
            tlpTarjetaVistaPrevia.PerformLayout();
            tlpBotones.ResumeLayout(false);
            tlpFormulario.ResumeLayout(false);
            tlpFormulario.PerformLayout();
            tlpRaiz.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRaiz;
        private TableLayoutPanel tlpFormulario;
        private Label lblModo;
        private Label lblNombre;
        private TextBox txtBoxNombre;
        private Label lblDireccion;
        private TextBox txtBoxDireccion;
        private Label lblTelefono;
        private TextBox txtBoxTelefono;
        private Label lblMail;
        private TextBox txtBoxMail;
        private Label lblCuit;
        private TextBox txtBoxCuit;
        private Label lblInstagram;
        private TextBox txtBoxInstagram;
        private Label lblMensajePie;
        private TextBox txtBoxMensajePie;
        private Label lblDiasValidez;
        private NumericUpDown numDiasValidez;
        private Label lblPorcentajeSena;
        private NumericUpDown numPorcentajeSena;
        private Label lblAyuda;
        private TableLayoutPanel tlpBotones;
        private Button btnEditar;
        private Button btnGuardar;
        private Button btnCancelarEdicion;
        private Label lblAviso;
        private TableLayoutPanel tlpTarjetaVistaPrevia;
        private Label lblVistaPrevia;
        private TextBox txtVistaPrevia;
    }
}
