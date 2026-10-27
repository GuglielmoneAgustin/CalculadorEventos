using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoController controlador;
        private readonly RestriccionController restriccionControlador = new RestriccionController();
        private List<ProductoDTO> productos = new List<ProductoDTO>();
        private int idSeleccionado;
        private string valoresGuardados = string.Empty;

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

        // Combo del producto: la primera opción (id 0) es "Sin restricción" y después las cargadas por el usuario.
        private void CargarComboRestriccion()
        {
            List<RestriccionDTO> restricciones = restriccionControlador.ObtenerTodas();

            List<RestriccionDTO> opciones = new List<RestriccionDTO>();
            RestriccionDTO ninguna = new RestriccionDTO();
            ninguna.Nombre = ProductoDTO.SinRestriccion;
            opciones.Add(ninguna);
            opciones.AddRange(restricciones);

            cmbRestriccion.DataSource = null;
            cmbRestriccion.DisplayMember = "Nombre";
            cmbRestriccion.DataSource = opciones;

            cmbRestriccionesLista.DataSource = null;
            cmbRestriccionesLista.DisplayMember = "Nombre";
            cmbRestriccionesLista.DataSource = restricciones;
        }

        private void SeleccionarRestriccion(int idRestriccion)
        {
            for (int i = 0; i < cmbRestriccion.Items.Count; i++)
            {
                if (((RestriccionDTO)cmbRestriccion.Items[i]!).Id == idRestriccion)
                {
                    cmbRestriccion.SelectedIndex = i;
                    return;
                }
            }
            cmbRestriccion.SelectedIndex = 0;
        }

        private void btnAgregarRestriccion_Click(object? sender, EventArgs e)
        {
            try
            {
                RestriccionDTO nueva = restriccionControlador.Agregar(txtNuevaRestriccion.Text);
                int idElegida = ((RestriccionDTO)cmbRestriccion.SelectedItem!).Id;
                CargarComboRestriccion();
                SeleccionarRestriccion(idElegida);
                txtNuevaRestriccion.Text = string.Empty;
                Aviso.Mostrar(lblAviso, "Restricción " + nueva.Nombre + " agregada.");
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnQuitarRestriccion_Click(object? sender, EventArgs e)
        {
            if (cmbRestriccionesLista.SelectedItem is not RestriccionDTO elegida)
            {
                Mensajes.Advertencia("No hay restricciones cargadas para quitar.");
                return;
            }
            if (!Mensajes.Confirmar("¿Quitar la restricción " + elegida.Nombre + "?"))
            {
                return;
            }

            try
            {
                restriccionControlador.Eliminar(elegida.Id);
                int idElegida = ((RestriccionDTO)cmbRestriccion.SelectedItem!).Id;
                CargarComboRestriccion();
                SeleccionarRestriccion(idElegida);
                Aviso.Mostrar(lblAviso, "Restricción " + elegida.Nombre + " quitada.");
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
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
            if (columna == "Categoria")
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
            List<ProductoDTO> visibles = new List<ProductoDTO>();
            foreach (ProductoDTO producto in productos)
            {
                if (Busqueda.Coincide(txtBuscar.Text, producto.Nombre, producto.Categoria.Texto(), producto.Restriccion))
                {
                    visibles.Add(producto);
                }
            }

            dgvProductos.MensajeSinFilas(productos.Count == 0
                ? "Todavía no hay productos. Complete el formulario y toque Guardar."
                : "Ningún producto coincide con la búsqueda.");
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = visibles;
            ConfigurarColumnas();
            dgvProductos.QuitarSeleccion();
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.Columna("Id").Visible = false;
            dgvProductos.Columna("IdRestriccion").Visible = false;
            dgvProductos.Columna("PrecioAdulto").HeaderText = "Precio adulto";
            dgvProductos.Columna("PrecioNino").HeaderText = "Precio niño";
            dgvProductos.Columna("Categoria").HeaderText = "Categoría";
            dgvProductos.Columna("Restriccion").HeaderText = "Restricción";
            dgvProductos.Columna("PrecioAdulto").DefaultCellStyle.Format = Formato.Moneda;
            dgvProductos.Columna("PrecioNino").DefaultCellStyle.Format = Formato.Moneda;
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
                dgvProductos.SeleccionarFila("Nombre", dto.Nombre.Trim());
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
            int idRestriccion = ((RestriccionDTO)cmbRestriccion.SelectedItem!).Id;
            dto.IdRestriccion = idRestriccion == 0 ? null : idRestriccion;
            return dto;
        }

        private void CargarFormulario(ProductoDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            numPrecioAdulto.Value = dto.PrecioAdulto;
            numPrecioNino.Value = dto.PrecioNino;
            cmbEnumCategoria.SelectedItem = dto.Categoria;
            SeleccionarRestriccion(dto.IdRestriccion ?? 0);
        }

        private void MostrarModoEdicion(string nombre)
        {
            lblModo.Text = "Editando: " + nombre;
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 1);
            btnCancelarEdicion.Visible = true;
            tlpRestricciones.Visible = false;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            numPrecioAdulto.Value = 0;
            numPrecioNino.Value = 0;
            cmbEnumCategoria.SelectedIndex = 0;
            cmbRestriccion.SelectedIndex = 0;

            lblModo.Text = "Nuevo producto";
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 2);
            btnCancelarEdicion.Visible = false;
            tlpRestricciones.Visible = true;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private bool HayCambiosSinGuardar()
        {
            return Formulario.Valores(tlpFormulario) != valoresGuardados;
        }
    }
}
