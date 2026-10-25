using ProyectoEventos.Forms.Utils;
namespace ProyectoEventos.Forms
{
    partial class FrmPrincipal
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
            pnlMenu = new Panel();
            pnlContenido = new Panel();
            pnlEncabezado = new Panel();
            lblPantalla = new Label();
            lblTitulo = new Label();
            btnInicio = new Button();
            btnLugares = new Button();
            btnClientes = new Button();
            btnServicios = new Button();
            btnProductos = new Button();
            btnEventos = new Button();
            btnNegocio = new Button();
            btnSalir = new Button();
            pnlMenu.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(btnNegocio);
            pnlMenu.Controls.Add(btnSalir);
            pnlMenu.Controls.Add(btnEventos);
            pnlMenu.Controls.Add(btnProductos);
            pnlMenu.Controls.Add(btnServicios);
            pnlMenu.Controls.Add(btnClientes);
            pnlMenu.Controls.Add(btnLugares);
            pnlMenu.Controls.Add(btnInicio);
            pnlMenu.Controls.Add(lblTitulo);
            pnlMenu.BackColor = Tema.Menu;
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Width = 230;
            pnlMenu.Name = "pnlMenu";
            pnlMenu.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitulo.ForeColor = Tema.MenuTexto;
            lblTitulo.Height = 100;
            lblTitulo.Padding = new Padding(20, 0, 10, 0);
            lblTitulo.Text = "Sistema de Presupuestos para Eventos";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Tema.Encabezado;
            pnlEncabezado.Controls.Add(lblPantalla);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Height = 70;
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.TabIndex = 2;
            // 
            // lblPantalla
            // 
            lblPantalla.Dock = DockStyle.Fill;
            lblPantalla.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPantalla.ForeColor = Tema.Texto;
            lblPantalla.Padding = new Padding(30, 0, 0, 0);
            lblPantalla.Text = "Inicio";
            lblPantalla.TextAlign = ContentAlignment.MiddleLeft;
            lblPantalla.Name = "lblPantalla";
            lblPantalla.TabIndex = 0;
            // 
            // pnlContenido
            // 
            pnlContenido.AutoScroll = true;
            pnlContenido.BackColor = Tema.Fondo;
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Name = "pnlContenido";
            pnlContenido.TabIndex = 1;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Tema.Menu;
            btnInicio.Cursor = Cursors.Hand;
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Font = new Font("Segoe UI", 11F);
            btnInicio.ForeColor = Tema.MenuTexto;
            btnInicio.Height = 52;
            btnInicio.Padding = new Padding(20, 0, 0, 0);
            btnInicio.Text = "Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleLeft;
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Name = "btnInicio";
            btnInicio.TabIndex = 2;
            btnInicio.Click += btnInicio_Click;
            // 
            // btnLugares
            // 
            btnLugares.BackColor = Tema.Menu;
            btnLugares.Cursor = Cursors.Hand;
            btnLugares.Dock = DockStyle.Top;
            btnLugares.FlatAppearance.BorderSize = 0;
            btnLugares.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnLugares.FlatStyle = FlatStyle.Flat;
            btnLugares.Font = new Font("Segoe UI", 11F);
            btnLugares.ForeColor = Tema.MenuTexto;
            btnLugares.Height = 52;
            btnLugares.Padding = new Padding(20, 0, 0, 0);
            btnLugares.Text = "Lugares";
            btnLugares.TextAlign = ContentAlignment.MiddleLeft;
            btnLugares.UseVisualStyleBackColor = false;
            btnLugares.Name = "btnLugares";
            btnLugares.TabIndex = 3;
            btnLugares.Click += btnLugares_Click;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Tema.Menu;
            btnClientes.Cursor = Cursors.Hand;
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 11F);
            btnClientes.ForeColor = Tema.MenuTexto;
            btnClientes.Height = 52;
            btnClientes.Padding = new Padding(20, 0, 0, 0);
            btnClientes.Text = "Clientes";
            btnClientes.TextAlign = ContentAlignment.MiddleLeft;
            btnClientes.UseVisualStyleBackColor = false;
            btnClientes.Name = "btnClientes";
            btnClientes.TabIndex = 4;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnServicios
            // 
            btnServicios.BackColor = Tema.Menu;
            btnServicios.Cursor = Cursors.Hand;
            btnServicios.Dock = DockStyle.Top;
            btnServicios.FlatAppearance.BorderSize = 0;
            btnServicios.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnServicios.FlatStyle = FlatStyle.Flat;
            btnServicios.Font = new Font("Segoe UI", 11F);
            btnServicios.ForeColor = Tema.MenuTexto;
            btnServicios.Height = 52;
            btnServicios.Padding = new Padding(20, 0, 0, 0);
            btnServicios.Text = "Servicios";
            btnServicios.TextAlign = ContentAlignment.MiddleLeft;
            btnServicios.UseVisualStyleBackColor = false;
            btnServicios.Name = "btnServicios";
            btnServicios.TabIndex = 5;
            btnServicios.Click += btnServicios_Click;
            // 
            // btnProductos
            // 
            btnProductos.BackColor = Tema.Menu;
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.Dock = DockStyle.Top;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 11F);
            btnProductos.ForeColor = Tema.MenuTexto;
            btnProductos.Height = 52;
            btnProductos.Padding = new Padding(20, 0, 0, 0);
            btnProductos.Text = "Productos";
            btnProductos.TextAlign = ContentAlignment.MiddleLeft;
            btnProductos.UseVisualStyleBackColor = false;
            btnProductos.Name = "btnProductos";
            btnProductos.TabIndex = 6;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnEventos
            // 
            btnEventos.BackColor = Tema.Menu;
            btnEventos.Cursor = Cursors.Hand;
            btnEventos.Dock = DockStyle.Top;
            btnEventos.FlatAppearance.BorderSize = 0;
            btnEventos.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnEventos.FlatStyle = FlatStyle.Flat;
            btnEventos.Font = new Font("Segoe UI", 11F);
            btnEventos.ForeColor = Tema.MenuTexto;
            btnEventos.Height = 52;
            btnEventos.Padding = new Padding(20, 0, 0, 0);
            btnEventos.Text = "Eventos";
            btnEventos.TextAlign = ContentAlignment.MiddleLeft;
            btnEventos.UseVisualStyleBackColor = false;
            btnEventos.Name = "btnEventos";
            btnEventos.TabIndex = 7;
            btnEventos.Click += btnEventos_Click;
            // 
            // btnNegocio
            // 
            btnNegocio.BackColor = Tema.Menu;
            btnNegocio.Cursor = Cursors.Hand;
            btnNegocio.Dock = DockStyle.Bottom;
            btnNegocio.FlatAppearance.BorderSize = 0;
            btnNegocio.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnNegocio.FlatStyle = FlatStyle.Flat;
            btnNegocio.Font = new Font("Segoe UI", 11F);
            btnNegocio.ForeColor = Tema.MenuTexto;
            btnNegocio.Height = 52;
            btnNegocio.Padding = new Padding(20, 0, 0, 0);
            btnNegocio.Text = "Mi negocio";
            btnNegocio.TextAlign = ContentAlignment.MiddleLeft;
            btnNegocio.UseVisualStyleBackColor = false;
            btnNegocio.Name = "btnNegocio";
            btnNegocio.TabIndex = 8;
            btnNegocio.Click += btnNegocio_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Tema.Menu;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Dock = DockStyle.Bottom;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatAppearance.MouseOverBackColor = Tema.MenuHover;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 11F);
            btnSalir.ForeColor = Tema.MenuTexto;
            btnSalir.Height = 52;
            btnSalir.Padding = new Padding(20, 0, 0, 0);
            btnSalir.Text = "Salir";
            btnSalir.TextAlign = ContentAlignment.MiddleLeft;
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Name = "btnSalir";
            btnSalir.TabIndex = 9;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Tema.Fondo;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
            Controls.Add(pnlMenu);
            MinimumSize = new Size(1000, 500);
            Name = "FrmPrincipal";
            Text = "Sistema de Presupuestos para Eventos";
            WindowState = FormWindowState.Maximized;
            Load += FrmPrincipal_Load;
            pnlMenu.ResumeLayout(false);
            pnlEncabezado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private Panel pnlContenido;
        private Panel pnlEncabezado;
        private Label lblPantalla;
        private Label lblTitulo;
        private Button btnInicio;
        private Button btnLugares;
        private Button btnClientes;
        private Button btnServicios;
        private Button btnProductos;
        private Button btnEventos;
        private Button btnNegocio;
        private Button btnSalir;
    }
}
