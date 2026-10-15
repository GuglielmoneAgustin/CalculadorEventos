using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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

        // Pantallas que quedaron ocultas detras de una subpantalla (ej: Eventos -> Presupuestos)
        private readonly Stack<(Form Pantalla, Action? AlVolver, string Titulo)> historial = new();

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmInicio(), btnInicio);
        }

        // Carga un formulario dentro de pnlContenido (reemplaza al anterior)
        private void AbrirEnPanel(Form pantalla, Button boton)
        {
            // Al cambiar de seccion se descarta toda la pila de subpantallas
            descartando = true;
            while (historial.Count > 0)
            {
                Form oculta = historial.Pop().Pantalla;
                oculta.Close();
                oculta.Dispose();
            }
            if (pantallaActual != null)
            {
                pantallaActual.Close();
                pantallaActual.Dispose();
            }
            descartando = false;
            pantallaActual = pantalla;

            pantalla.TopLevel = false;
            pantalla.FormBorderStyle = FormBorderStyle.None;
            pantalla.Dock = DockStyle.Fill;

            // El boton "Volver" no tiene sentido cuando la pantalla esta incrustada
            foreach (Control volver in pantalla.Controls.Find("btnVolver", true))
            {
                volver.Visible = false;
            }

            pnlContenido.Controls.Add(pantalla);
            pantalla.Show();

            // Al mostrarse, las grillas seleccionan la primera fila: la deseleccionamos
            QuitarSeleccionGrillas(pantalla);
            BeginInvoke(() => QuitarSeleccionGrillas(pantalla));

            lblPantalla.Text = boton.Text;
            MarcarBotonActivo(boton);
        }

        // Abre una subpantalla en el mismo panel, encima de la actual. Al cerrarse (Close) se
        // vuelve a la anterior y se ejecuta alVolver (por ejemplo, para refrescar una grilla).
        public void AbrirSubpantalla(Form subpantalla, Action? alVolver = null)
        {
            Form anterior = pantallaActual!;
            historial.Push((anterior, alVolver, lblPantalla.Text));
            anterior.Visible = false;

            subpantalla.TopLevel = false;
            subpantalla.FormBorderStyle = FormBorderStyle.None;
            subpantalla.Dock = DockStyle.Fill;
            pantallaActual = subpantalla;

            subpantalla.FormClosed += (s, e) =>
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
            };
            subpantalla.TextChanged += (s, e) =>
            {
                if (pantallaActual == subpantalla)
                {
                    lblPantalla.Text = subpantalla.Text;
                }
            };

            pnlContenido.Controls.Add(subpantalla);
            subpantalla.Show();
            lblPantalla.Text = subpantalla.Text;

            QuitarSeleccionGrillas(subpantalla);
            BeginInvoke(() => QuitarSeleccionGrillas(subpantalla));
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
                    grilla.ClearSelection();
                    grilla.CurrentCell = null;
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
            AbrirEnPanel(new FrmInicio(), btnInicio);
        }

        private void btnLugares_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmLugares(), btnLugares);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmClientes(), btnClientes);
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmServiciosAdicionales(), btnServicios);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmProductos(), btnProductos);
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmEventos(), btnEventos);
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
