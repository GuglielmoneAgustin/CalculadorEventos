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
        private string valoresGuardados = string.Empty;

        public FrmProductos()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ProductoController();
            idSeleccionado = 0;
            dgvProductos.MensajeSinFilas(() => productos.Count == 0
                ? "Todavía no hay productos. Complete el formulario y toque Guardar."
                : "Ningún producto coincide con la búsqueda.");
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
            LimpiarFormulario();
        }

        private void FrmProductos_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
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

        private void MostrarGrilla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = Busqueda.Filtrar(productos, txtBuscar.Text, p => new[] { p.Nombre, p.Categoria.Texto(), p.Restriccion.Texto() });
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
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                return;
            }

            try
            {
                ProductoDTO producto = controlador.ObtenerPorId(seleccionado.Id);
                CargarFormulario(producto);
                idSeleccionado = producto.Id;
                MostrarModoEdicion(producto.Nombre);
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
                bool eraEdicion = idSeleccionado != 0;
                controlador.Guardar(dto);

                CargarGrilla();
                LimpiarFormulario();
                string nombre = dto.Nombre.Trim();
                dgvProductos.SeleccionarFila<ProductoDTO>(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                Aviso.Mostrar(lblAviso, eraEdicion ? "Producto actualizado." : "Producto guardado.");
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
                    if (seleccionado.Id == idSeleccionado)
                    {
                        LimpiarFormulario();
                    }
                    Aviso.Mostrar(lblAviso, "Producto eliminado.");
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
            numPrecioAdulto.Value = 0;
            numPrecioNino.Value = 0;
            cmbEnumCategoria.SelectedIndex = 0;
            cmbEnumRestriccion.SelectedIndex = 0;

            lblModo.Text = "Nuevo producto";
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
