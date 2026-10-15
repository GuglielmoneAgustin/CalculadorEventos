using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmServiciosAdicionales : Form
    {
        private readonly ServicioAdicionalController controlador;
        private List<ServicioAdicionalDTO> servicios = new List<ServicioAdicionalDTO>();
        private int idSeleccionado;

        public FrmServiciosAdicionales()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ServicioAdicionalController();
            idSeleccionado = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmServiciosAdicionales_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            servicios = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        // Muestra los servicios que coinciden con el texto de búsqueda (sin volver a pedirlos al controlador)
        private void MostrarGrilla()
        {
            dgvServicios.DataSource = null;
            dgvServicios.DataSource = Busqueda.Filtrar(servicios, txtBuscar.Text, s => new[] { s.Nombre, s.Descripcion });
            dgvServicios.Columna("Monto").DefaultCellStyle.Format = Formato.Moneda;

            if (dgvServicios.Rows.Count > 0)
            {
                dgvServicios.ClearSelection();
                dgvServicios.CurrentCell = null;
            }
        }

        private ServicioAdicionalDTO? ObtenerSeleccionado()
        {
            return dgvServicios.Seleccionado<ServicioAdicionalDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un servicio de la grilla para editar.");
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
                ServicioAdicionalDTO dto = ArmarDto();
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
            ServicioAdicionalDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un servicio de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este servicio?"))
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

        private ServicioAdicionalDTO ArmarDto()
        {
            ServicioAdicionalDTO dto = new ServicioAdicionalDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.Descripcion = txtBoxDescripcion.Text;
            dto.Monto = numMonto.Value;
            return dto;
        }

        private void CargarFormulario(ServicioAdicionalDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            txtBoxDescripcion.Text = dto.Descripcion;
            numMonto.Value = dto.Monto;
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            txtBoxDescripcion.Text = string.Empty;
            numMonto.Value = 0;
        }
    }
}
