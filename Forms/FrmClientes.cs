using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteController controlador;
        private int idSeleccionado;

        public FrmClientes()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ClienteController();
            idSeleccionado = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = controlador.ObtenerTodos();
            dgvClientes.Columna("Descripcion").Visible = false;

            if (dgvClientes.Rows.Count > 0)
            {
                dgvClientes.ClearSelection();
                dgvClientes.CurrentCell = null;
            }
        }

        private ClienteDTO? ObtenerSeleccionado()
        {
            return dgvClientes.Seleccionado<ClienteDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ClienteDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un cliente de la grilla para editar.");
                return;
            }

            try
            {
                CargarFormulario(controlador.ObtenerPorId(seleccionado.Id));
                idSeleccionado = seleccionado.Id;
            }
            catch (NoEncontradoException ex)
            {
                Mensajes.Advertencia(ex.Message);
                CargarGrilla();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO dto = ArmarDto();
                controlador.Guardar(dto);

                CargarGrilla();
                LimpiarFormulario();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ClienteDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un cliente de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este cliente?"))
            {
                try
                {
                    controlador.Eliminar(seleccionado.Id);
                    CargarGrilla();
                    LimpiarFormulario();
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }

        private ClienteDTO ArmarDto()
        {
            ClienteDTO dto = new ClienteDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.Dni = txtBoxDni.Text;
            dto.Mail = txtBoxMail.Text;
            dto.Telefono = txtBoxTelefono.Text;
            return dto;
        }

        private void CargarFormulario(ClienteDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            txtBoxDni.Text = dto.Dni;
            txtBoxMail.Text = dto.Mail;
            txtBoxTelefono.Text = dto.Telefono;
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            txtBoxDni.Text = string.Empty;
            txtBoxMail.Text = string.Empty;
            txtBoxTelefono.Text = string.Empty;
        }
    }
}
