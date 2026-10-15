using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmLugares : Form
    {
        private readonly LugarController controlador;
        private List<LugarDTO> lugares = new List<LugarDTO>();
        private int idSeleccionado;

        public FrmLugares()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new LugarController();
            idSeleccionado = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmLugares_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            lugares = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        // Muestra los lugares que coinciden con el texto de búsqueda (sin volver a pedirlos al controlador)
        private void MostrarGrilla()
        {
            dgvLugares.DataSource = null;
            dgvLugares.DataSource = Busqueda.Filtrar(lugares, txtBuscar.Text, l => new[] { l.Nombre, l.Ubicacion });
            dgvLugares.Columna("CostoBase").DefaultCellStyle.Format = Formato.Moneda;
            dgvLugares.Columna("Descripcion").Visible = false;

            if (dgvLugares.Rows.Count > 0)
            {
                dgvLugares.ClearSelection();
                dgvLugares.CurrentCell = null;
            }
        }

        private LugarDTO? ObtenerSeleccionado()
        {
            return dgvLugares.Seleccionado<LugarDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            LugarDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un lugar de la grilla para editar.");
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
                LugarDTO dto = ArmarDto();
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
            LugarDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un lugar de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este lugar?"))
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

        private LugarDTO ArmarDto()
        {
            LugarDTO dto = new LugarDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.CostoBase = numCosto.Value;
            dto.Capacidad = Convert.ToInt32(numCapacidad.Value);
            dto.Ubicacion = txtBoxUbicacion.Text;
            return dto;
        }

        private void CargarFormulario(LugarDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            numCosto.Value = dto.CostoBase;
            numCapacidad.Value = dto.Capacidad;
            txtBoxUbicacion.Text = dto.Ubicacion;
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            numCosto.Value = 0;
            numCapacidad.Value = 0;
            txtBoxUbicacion.Text = string.Empty;
        }
    }
}
