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
        private List<ClienteDTO> clientes = new List<ClienteDTO>();
        private int idSeleccionado;
        // Valores del formulario al cargarlo o limpiarlo: si cambian, hay datos sin guardar
        private string valoresGuardados = string.Empty;

        public FrmClientes()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ClienteController();
            idSeleccionado = 0;
            dgvClientes.MensajeSinFilas(() => clientes.Count == 0
                ? "Todavía no hay clientes. Complete el formulario y toque Guardar."
                : "Ningún cliente coincide con la búsqueda.");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarFormulario();
        }

        private void FrmClientes_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
        }

        private void CargarGrilla()
        {
            clientes = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        // Muestra los clientes que coinciden con el texto de búsqueda (sin volver a pedirlos al controlador)
        private void MostrarGrilla()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = Busqueda.Filtrar(clientes, txtBuscar.Text, c => new[] { c.Nombre, c.Dni, c.Mail, c.Telefono });
            dgvClientes.Columna("Id").Visible = false;
            dgvClientes.Columna("Dni").HeaderText = "DNI";
            dgvClientes.Columna("Telefono").HeaderText = "Teléfono";
            dgvClientes.Columna("CantidadEventos").HeaderText = "Eventos";
            dgvClientes.Columna("CantidadEventos").FillWeight = 50;
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
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                return;
            }

            try
            {
                ClienteDTO cliente = controlador.ObtenerPorId(seleccionado.Id);
                CargarFormulario(cliente);
                idSeleccionado = cliente.Id;
                MostrarModoEdicion(cliente.Nombre);
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
                bool eraEdicion = idSeleccionado != 0;
                controlador.Guardar(dto);

                CargarGrilla();
                LimpiarFormulario();
                string dni = dto.Dni.Trim();
                dgvClientes.SeleccionarFila<ClienteDTO>(c => c.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase));
                Aviso.Mostrar(lblAviso, eraEdicion ? "Cliente actualizado." : "Cliente guardado.");
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
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
                    if (seleccionado.Id == idSeleccionado)
                    {
                        LimpiarFormulario();
                    }
                    Aviso.Mostrar(lblAviso, "Cliente eliminado.");
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

        // El título de la tarjeta indica (y Cancelar permite descartar) que se está modificando un cliente existente
        private void MostrarModoEdicion(string nombre)
        {
            lblModo.Text = "Editando: " + nombre;
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 1);
            btnCancelarEdicion.Visible = true;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            txtBoxDni.Text = string.Empty;
            txtBoxMail.Text = string.Empty;
            txtBoxTelefono.Text = string.Empty;

            lblModo.Text = "Nuevo cliente";
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 2);
            btnCancelarEdicion.Visible = false;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private bool HayCambiosSinGuardar()
        {
            return Formulario.Valores(tlpFormulario) != valoresGuardados;
        }
    }
}
