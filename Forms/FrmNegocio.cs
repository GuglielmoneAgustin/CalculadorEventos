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
            controlador = new NegocioController();
        }

        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            CargarFormulario(controlador.Obtener());
        }

        private void FrmNegocio_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.Guardar(ArmarDto());
                CargarFormulario(controlador.Obtener());
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

        private void Campo_Changed(object? sender, EventArgs e)
        {
            txtVistaPrevia.Text = string.Join(Environment.NewLine, TicketPresupuesto.ArmarVistaPrevia(ArmarDto()));
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
