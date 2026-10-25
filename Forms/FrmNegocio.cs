using System;
using System.Windows.Forms;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmNegocio : Form
    {
        private readonly NegocioController controlador;
        private string valoresGuardados = string.Empty;

        public FrmNegocio()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            txtVistaPrevia.Font = new Font("Consolas", 10F);
            txtVistaPrevia.BorderStyle = BorderStyle.None;
            txtVistaPrevia.BackColor = Color.White;
            txtVistaPrevia.Width = TextRenderer.MeasureText(new string('0', TicketPresupuesto.Ancho), txtVistaPrevia.Font).Width
                + SystemInformation.VerticalScrollBarWidth + 12;
            controlador = new NegocioController();
            FijarAltoDeCampos();
        }

        // Sin borde (modo lectura) los campos se achican; con alto fijo el formulario no salta al cambiar de modo.
        private void FijarAltoDeCampos()
        {
            foreach (Control control in tlpFormulario.Controls)
            {
                if (control is TextBox texto)
                {
                    int alto = texto.Height;
                    texto.AutoSize = false;
                    texto.Height = alto;
                }
            }
        }

        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            CargarFormulario(controlador.Obtener());
        }

        // El modo lectura se aplica con el formulario ya en pantalla: antes, las flechas ocultas de los numéricos dejan un recuadro blanco.
        private void FrmNegocio_Shown(object? sender, EventArgs e)
        {
            MostrarModoEdicion(false);
        }

        private void FrmNegocio_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarModoEdicion(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.Guardar(ArmarDto());
                CargarFormulario(controlador.Obtener());
                MostrarModoEdicion(false);
                if (ParentForm is FrmPrincipal principal)
                {
                    principal.MostrarNombreNegocio();
                }
                Aviso.Mostrar(lblAviso, "Datos del negocio guardados.");
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                return;
            }
            CargarFormulario(controlador.Obtener());
            MostrarModoEdicion(false);
        }

        private void Campo_Changed(object? sender, EventArgs e)
        {
            txtVistaPrevia.Text = string.Join(Environment.NewLine, TicketPresupuesto.ArmarVistaPrevia(ArmarDto()));
        }

        private void MostrarModoEdicion(bool editando)
        {
            foreach (Control control in tlpFormulario.Controls)
            {
                if (control is TextBox texto)
                {
                    texto.ReadOnly = !editando;
                    texto.TabStop = editando;
                    texto.BackColor = editando ? Tema.Campo : Tema.Tarjeta;
                    texto.BorderStyle = editando ? BorderStyle.FixedSingle : BorderStyle.None;
                    texto.Select(0, 0);
                }
                else if (control is NumericUpDown numero)
                {
                    numero.ReadOnly = !editando;
                    numero.TabStop = editando;
                    numero.Increment = editando ? 1 : 0;
                    numero.BackColor = editando ? Tema.Campo : Tema.Tarjeta;
                    numero.BorderStyle = editando ? BorderStyle.FixedSingle : BorderStyle.None;
                    // Controls[0] son las flechas y Controls[1] el cuadro de texto interno, que en lectura ocupa todo el ancho.
                    numero.Controls[0].Visible = editando;
                    numero.Controls[1].Dock = editando ? DockStyle.None : DockStyle.Fill;
                    numero.PerformLayout();
                }
            }

            lblModo.Text = editando ? "Editando datos del negocio" : "Datos del negocio";
            btnEditar.Visible = !editando;
            btnGuardar.Visible = editando;
            btnCancelarEdicion.Visible = editando;
            ActiveControl = null;
        }

        private NegocioDTO ArmarDto()
        {
            NegocioDTO dto = new NegocioDTO();
            dto.Nombre = txtBoxNombre.Text;
            dto.Direccion = txtBoxDireccion.Text;
            dto.Telefono = txtBoxTelefono.Text;
            dto.Mail = txtBoxMail.Text;
            dto.Cuit = txtBoxCuit.Text;
            dto.Instagram = txtBoxInstagram.Text;
            dto.MensajePie = txtBoxMensajePie.Text;
            dto.DiasValidez = Convert.ToInt32(numDiasValidez.Value);
            dto.PorcentajeSena = numPorcentajeSena.Value;
            return dto;
        }

        private void CargarFormulario(NegocioDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            txtBoxDireccion.Text = dto.Direccion;
            txtBoxTelefono.Text = dto.Telefono;
            txtBoxMail.Text = dto.Mail;
            txtBoxCuit.Text = dto.Cuit;
            txtBoxInstagram.Text = dto.Instagram;
            txtBoxMensajePie.Text = dto.MensajePie;
            numDiasValidez.Value = dto.DiasValidez;
            numPorcentajeSena.Value = dto.PorcentajeSena;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private bool HayCambiosSinGuardar()
        {
            return Formulario.Valores(tlpFormulario) != valoresGuardados;
        }
    }
}
