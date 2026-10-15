using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Common;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoController controlador;
        private int idSeleccionado;

        public FrmProductos()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ProductoController();
            idSeleccionado = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarComboCategoria();
            CargarComboRestriccion();
            CargarGrilla();
        }

        private void CargarComboCategoria()
        {
            cmbEnumCategoria.DataSource = Enum.GetValues(typeof(CategoriasProductos));
        }

        private void CargarComboRestriccion()
        {
            cmbEnumRestriccion.DataSource = Enum.GetValues(typeof(RestriccionesAlimenticias));
        }

        private void CargarGrilla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = controlador.ObtenerTodos();
            dgvProductos.Columna("PrecioAdulto").DefaultCellStyle.Format = Formato.Moneda;
            dgvProductos.Columna("PrecioNino").DefaultCellStyle.Format = Formato.Moneda;

            if (dgvProductos.Rows.Count > 0)
            {
                dgvProductos.ClearSelection();
                dgvProductos.CurrentCell = null;
            }
        }

        private ProductoDTO? ObtenerSeleccionado()
        {
            return dgvProductos.Seleccionado<ProductoDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ProductoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un producto de la grilla para editar.");
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
                ProductoDTO dto = ArmarDto();
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
            ProductoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un producto de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este producto?"))
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

        private ProductoDTO ArmarDto()
        {
            ProductoDTO dto = new ProductoDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.PrecioAdulto = numPrecioAdulto.Value;
            dto.PrecioNino = numPrecioNino.Value;
            dto.Categoria = (CategoriasProductos)cmbEnumCategoria.SelectedItem!;
            dto.Restriccion = (RestriccionesAlimenticias)cmbEnumRestriccion.SelectedItem!;
            return dto;
        }

        private void CargarFormulario(ProductoDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            numPrecioAdulto.Value = dto.PrecioAdulto;
            numPrecioNino.Value = dto.PrecioNino;
            cmbEnumCategoria.SelectedItem = dto.Categoria;
            cmbEnumRestriccion.SelectedItem = dto.Restriccion;
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            numPrecioAdulto.Value = 0;
            numPrecioNino.Value = 0;
            cmbEnumCategoria.SelectedIndex = 0;
            cmbEnumRestriccion.SelectedIndex = 0;
        }
    }
}
