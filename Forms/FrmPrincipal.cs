using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmPrincipal : Form
    {
        private static readonly Color ColorBotonNormal = Tema.Carob;
        private static readonly Color ColorBotonActivo = Tema.Matcha;

        private Form? pantallaActual;
        private Button? botonActivo;
        private bool descartando;

        private readonly Stack<(Form Pantalla, Action? AlVolver, string Titulo)> historial = new();

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            MostrarNombreNegocio();
            AbrirEnPanel(() => new FrmInicio(), btnInicio);
        }

        public void MostrarNombreNegocio()
        {
            string nombre = new NegocioController().Obtener().Nombre;
            lblTitulo.Text = nombre;
            Text = nombre + " - Presupuestos para eventos";
        }

        private void AbrirEnPanel(Func<Form> crearPantalla, Button boton)
        {
            if (!CerrarPantallas())
            {
                return;
            }

            Form pantalla = crearPantalla();
            foreach (Control volver in pantalla.Controls.Find("btnVolver", true))
            {
                volver.Visible = false;
            }
            Incrustar(pantalla);

            lblPantalla.Text = boton.Text;
            MarcarBotonActivo(boton);
        }

        public void AbrirSubpantalla(Form subpantalla, Action? alVolver = null)
        {
            Form anterior = pantallaActual!;
            historial.Push((anterior, alVolver, lblPantalla.Text));
            anterior.Visible = false;

            subpantalla.FormClosed += (s, e) => VolverAPantallaAnterior(subpantalla);
            subpantalla.TextChanged += (s, e) =>
            {
                if (pantallaActual == subpantalla)
                {
                    lblPantalla.Text = subpantalla.Text;
                }
            };

            Incrustar(subpantalla);
            lblPantalla.Text = subpantalla.Text;
        }

        private bool CerrarPantallas()
        {
            descartando = true;
            if (pantallaActual != null && !IntentarCerrar(pantallaActual))
            {
                descartando = false;
                return false;
            }
            pantallaActual?.Dispose();
            while (historial.Count > 0)
            {
                Form oculta = historial.Pop().Pantalla;
                oculta.Close();
                oculta.Dispose();
            }
            descartando = false;
            return true;
        }

        private void Incrustar(Form pantalla)
        {
            pantalla.TopLevel = false;
            pantalla.FormBorderStyle = FormBorderStyle.None;
            pantalla.Dock = DockStyle.Fill;
            pantallaActual = pantalla;

            pnlContenido.Controls.Add(pantalla);
            pantalla.Show();

            QuitarSeleccionGrillas(pantalla);
            BeginInvoke(() => QuitarSeleccionGrillas(pantalla));
        }

        private void VolverAPantallaAnterior(Form subpantalla)
        {
            if (descartando || historial.Count == 0 || pantallaActual != subpantalla)
            {
                return;
            }
            var previo = historial.Pop();
            pnlContenido.Controls.Remove(subpantalla);
            subpantalla.Dispose();
            pantallaActual = previo.Pantalla;
            previo.Pantalla.Visible = true;
            lblPantalla.Text = previo.Titulo;
            previo.AlVolver?.Invoke();
        }

        private static bool IntentarCerrar(Form pantalla)
        {
            bool cerrada = false;
            FormClosedEventHandler alCerrar = (sender, e) => cerrada = true;
            pantalla.FormClosed += alCerrar;
            pantalla.Close();
            pantalla.FormClosed -= alCerrar;
            return cerrada;
        }

        private void QuitarSeleccionGrillas(Control contenedor)
        {
            if (contenedor.IsDisposed)
            {
                return;
            }
            foreach (Control control in contenedor.Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.QuitarSeleccion();
                }
                else
                {
                    QuitarSeleccionGrillas(control);
                }
            }
        }

        private void MarcarBotonActivo(Button boton)
        {
            if (botonActivo != null)
            {
                botonActivo.BackColor = ColorBotonNormal;
            }
            botonActivo = boton;
            botonActivo.BackColor = ColorBotonActivo;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmInicio(), btnInicio);
        }

        private void btnLugares_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmLugares(), btnLugares);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmClientes(), btnClientes);
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmServiciosAdicionales(), btnServicios);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmProductos(), btnProductos);
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmEventos(), btnEventos);
        }

        private void btnNegocio_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(() => new FrmNegocio(), btnNegocio);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (Mensajes.Confirmar("¿Seguro que desea salir?"))
            {
                Application.Exit();
            }
        }
    }
}
