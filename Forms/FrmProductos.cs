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
        private List<ProductoDTO> productos = new List<ProductoDTO>();
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
            cmbEnumCategoria.FormattingEnabled = true;
            cmbEnumCategoria.Format += FormatearEnum;
            cmbEnumCategoria.DataSource = Enum.GetValues(typeof(CategoriasProductos));
        }

        private void CargarComboRestriccion()
        {
            cmbEnumRestriccion.FormattingEnabled = true;
            cmbEnumRestriccion.Format += FormatearEnum;
            cmbEnumRestriccion.DataSource = Enum.GetValues(typeof(RestriccionesAlimenticias));
        }

        // Los combos guardan el valor de la enumeración pero muestran el texto con tildes
        private void FormatearEnum(object? sender, ListControlConvertEventArgs e)
        {
            e.Value = TextoEnum.Texto(e.ListItem);
        }

        private void dgvProductos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }

            string columna = dgvProductos.Columns[e.ColumnIndex].Name;
            if (columna == "Categoria" || columna == "Restriccion")
            {
                e.Value = TextoEnum.Texto(e.Value);
                e.FormattingApplied = true;
            }
        }

        private void CargarGrilla()
        {
            productos = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        // Muestra los productos que coinciden con el texto de búsqueda (sin volver a pedirlos al controlador)
        private void MostrarGrilla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = Busqueda.Filtrar(productos, txtBuscar.Text, p => new[] { p.Nombre, TextoEnum.Categoria(p.Categoria), TextoEnum.Restriccion(p.Restriccion) });
            dgvProductos.Columna("Id").Visible = false;
            dgvProductos.Columna("PrecioAdulto").HeaderText = "Precio adulto";
            dgvProductos.Columna("PrecioNino").HeaderText = "Precio niño";
            dgvProductos.Columna("Categoria").HeaderText = "Categoría";
            dgvProductos.Columna("Restriccion").HeaderText = "Restricción";
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
