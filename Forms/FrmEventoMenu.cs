using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    // Paso 2 del alta/edición de un evento: menú (platos con cantidades por adulto y niño) y servicios adicionales.
    // Cada acción se aplica directamente sobre el evento ya creado (idEvento).
    public partial class FrmEventoMenu : Form
    {
        private const string TodosLosProductos = "Todos los productos";
        private const string FormatoPrecioCorto = "$ #,##0";

        private readonly EventoController controlador;
        private readonly ProductoController productoControlador;
        private readonly ServicioAdicionalController servicioControlador;
        private readonly PresupuestoController presupuestoControlador;
        private readonly int idEvento;
        // Verdadero cuando el usuario termino con "Finalizar" (y no con "Volver")
        public bool Finalizado { get; private set; }
        private int adultosEvento;
        private int ninosEvento;
        // Plato que se está editando (0 = el formulario agrega platos nuevos)
        private int idItemEditando;
        private List<ProductoDTO> productosDisponibles = new List<ProductoDTO>();
        // Productos marcados para agregar; se conservan aunque el filtro los oculte
        private readonly HashSet<int> idsMarcados = new HashSet<int>();
        // Mientras se rellena la lista no se procesan los cambios de marca
        private bool llenandoLista;
        private List<ServicioAdicionalDTO> serviciosDisponibles = new List<ServicioAdicionalDTO>();

        public FrmEventoMenu(int idEvento)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new EventoController();
            productoControlador = new ProductoController();
            servicioControlador = new ServicioAdicionalController();
            presupuestoControlador = new PresupuestoController();
            this.idEvento = idEvento;
            dgvItemsMenu.MensajeSinFilas(() => "Todavía no hay platos. Marque productos a la izquierda y toque Agregar plato.");
            dgvServiciosEvento.MensajeSinFilas(() => "Sin servicios adicionales.");
        }

        private void FrmEventoMenu_Load(object sender, EventArgs e)
        {
            CargarInfoEvento();
            CargarProductosDisponibles();
            CargarServiciosDisponibles();
            CargarGrillaItems();
            CargarGrillaServicios();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CargarInfoEvento()
        {
            EventoResultadoDTO evento = controlador.ObtenerPorId(idEvento);
            adultosEvento = evento.CantidadAdultos;
            ninosEvento = evento.CantidadNinos;

            lblInfoEvento.Text = "Evento de " + evento.NombreCliente + " en " + evento.NombreLugar +
                " — Adultos: " + adultosEvento + " | Niños: " + ninosEvento;

            RestablecerCantidadesItem();
        }

        // ---------- Menú ----------

        private void CargarProductosDisponibles()
        {
            productosDisponibles = productoControlador.ObtenerTodos();

            lvProductos.Columns.Clear();
            lvProductos.Columns.Add("Producto");
            lvProductos.Columns.Add("Adulto / niño", 125, HorizontalAlignment.Right);

            // Filtro por restricción alimentaria: todos o una restricción (se muestra con tildes)
            cmbFiltroRestriccion.Format += (sender, e) => e.Value = e.ListItem as string ?? TextoEnum.Texto(e.ListItem);
            cmbFiltroRestriccion.Items.Clear();
            cmbFiltroRestriccion.Items.Add(TodosLosProductos);
            foreach (RestriccionesAlimenticias restriccion in Enum.GetValues<RestriccionesAlimenticias>())
            {
                cmbFiltroRestriccion.Items.Add(restriccion);
            }
            cmbFiltroRestriccion.SelectedIndex = 0;
        }

        private void cmbFiltroRestriccion_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LlenarListaProductos();
        }

        // Productos agrupados por categoría (Principal, Guarnición, ...) con el precio por adulto y por niño
        private void LlenarListaProductos()
        {
            RestriccionesAlimenticias? filtro = cmbFiltroRestriccion.SelectedItem as RestriccionesAlimenticias?;

            llenandoLista = true;
            lvProductos.BeginUpdate();
            lvProductos.Items.Clear();
            lvProductos.Groups.Clear();
            foreach (CategoriasProductos categoria in Enum.GetValues<CategoriasProductos>())
            {
                List<ProductoDTO> productos = productosDisponibles
                    .Where(p => p.Categoria == categoria && (filtro == null || p.Restriccion == filtro))
                    .OrderBy(p => p.Nombre)
                    .ToList();
                if (productos.Count == 0)
                {
                    continue;
                }

                ListViewGroup grupo = new ListViewGroup(TextoEnum.Categoria(categoria));
                lvProductos.Groups.Add(grupo);
                foreach (ProductoDTO producto in productos)
                {
                    string nombre = producto.Restriccion == RestriccionesAlimenticias.Estandar
                        ? producto.Nombre
                        : producto.Nombre + " · " + TextoEnum.Restriccion(producto.Restriccion);
                    // Precios sin centavos para que entren en la columna: "$ 9.500 / 6.500"
                    string precios = producto.PrecioAdulto.ToString(FormatoPrecioCorto) + " / " + producto.PrecioNino.ToString("#,##0");
                    ListViewItem item = new ListViewItem(new[] { nombre, precios }, grupo);
                    item.Tag = producto;
                    item.Checked = idsMarcados.Contains(producto.Id);
                    lvProductos.Items.Add(item);
                }
            }
            lvProductos.EndUpdate();
            llenandoLista = false;

            AjustarColumnasProductos();
            ActualizarSubtotalPlato();
        }

        private void lvProductos_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (llenandoLista || e.Item.Tag is not ProductoDTO producto)
            {
                return;
            }

            if (e.Item.Checked)
            {
                idsMarcados.Add(producto.Id);
            }
            else
            {
                idsMarcados.Remove(producto.Id);
            }
            ActualizarSubtotalPlato();
        }

        private void lvProductos_Resize(object? sender, EventArgs e)
        {
            AjustarColumnasProductos();
        }

        // El nombre ocupa todo el ancho que deja la columna de precios
        private void AjustarColumnasProductos()
        {
            if (lvProductos.Columns.Count < 2)
            {
                return;
            }
            int anchoPrecios = lvProductos.Columns[1].Width;
            lvProductos.Columns[0].Width = Math.Max(lvProductos.ClientSize.Width - anchoPrecios - 4, 100);
        }

        private void CantidadesItem_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarSubtotalPlato();
        }

        // Cuánto suman los productos marcados con las cantidades elegidas, antes de agregarlos
        private void ActualizarSubtotalPlato()
        {
            if (idItemEditando != 0)
            {
                lblSubtotalPlato.Text = "Cambie las cantidades y toque Guardar cambios.";
                return;
            }

            List<ProductoDTO> marcados = productosDisponibles.Where(p => idsMarcados.Contains(p.Id)).ToList();
            if (marcados.Count == 0)
            {
                lblSubtotalPlato.Text = "Marque uno o más productos para agregarlos al menú.";
                return;
            }

            decimal adultos = numCantAdultosItem.Value;
            decimal ninos = numCantNinosItem.Value;
            decimal subtotal = marcados.Sum(p => p.PrecioAdulto * adultos + p.PrecioNino * ninos);
            string cantidad = marcados.Count == 1 ? "1 producto marcado" : marcados.Count + " productos marcados";
            lblSubtotalPlato.Text = cantidad + " · Subtotal: " + subtotal.ToString(Formato.Moneda);
        }

        private void CargarGrillaItems()
        {
            dgvItemsMenu.DataSource = null;
            dgvItemsMenu.DataSource = controlador.ObtenerItemsMenu(idEvento);
            dgvItemsMenu.Columna("Subtotal").DefaultCellStyle.Format = Formato.Moneda;

            dgvItemsMenu.Columna("Id").Visible = false;
            dgvItemsMenu.Columna("DescripcionComponentes").HeaderText = "Productos";
            dgvItemsMenu.Columna("DescripcionComponentes").FillWeight = 250;
            dgvItemsMenu.Columna("CantidadAdultos").HeaderText = "Adultos";
            dgvItemsMenu.Columna("CantidadNinos").HeaderText = "Niños";
            dgvItemsMenu.Columna("Subtotal").HeaderText = "Subtotal";
            LimpiarSeleccionGrilla(dgvItemsMenu);

            lblTotalMenu.Text = "Total menú: " + controlador.ObtenerTotalMenu(idEvento).ToString(Formato.Moneda);
        }

        // Igual que "Guardar" en los otros ABM: si se está editando un plato guarda sus cantidades,
        // si no agrega un plato por cada producto marcado.
        private void btnAgregarItem_Click(object sender, EventArgs e)
        {
            if (idItemEditando != 0)
            {
                GuardarCambiosItem();
                return;
            }

            if (idsMarcados.Count == 0)
            {
                Mensajes.Advertencia("Seleccione al menos un producto para el plato.");
                return;
            }

            try
            {
                int cantidadAdultos = Convert.ToInt32(numCantAdultosItem.Value);
                int cantidadNinos = Convert.ToInt32(numCantNinosItem.Value);

                // Cada producto marcado se agrega como un plato propio del menú (todos o ninguno).
                List<int> idsProductos = productosDisponibles.Where(p => idsMarcados.Contains(p.Id)).Select(p => p.Id).ToList();
                controlador.AgregarPlatosAlMenu(idEvento, cantidadAdultos, cantidadNinos, idsProductos);

                LimpiarSeleccionItem();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }

            CargarGrillaItems();
        }

        private void btnQuitarItem_Click(object sender, EventArgs e)
        {
            if (dgvItemsMenu.CurrentRow?.DataBoundItem is not ItemMenuResultadoDTO item)
            {
                Mensajes.Advertencia("Seleccione un plato de la grilla para quitar.");
                return;
            }

            try
            {
                controlador.QuitarItemDelMenu(idEvento, item.Id);
                if (item.Id == idItemEditando)
                {
                    TerminarEdicionItem();
                }
                CargarGrillaItems();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        // ---------- Edición de un plato (como en los otros ABM: Editar carga, Guardar aplica) ----------

        // Carga las cantidades del plato seleccionado en el formulario de la izquierda.
        // El producto no se cambia: para otro producto se quita el plato y se agrega uno nuevo.
        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            if (dgvItemsMenu.CurrentRow?.DataBoundItem is not ItemMenuResultadoDTO item)
            {
                Mensajes.Advertencia("Seleccione un plato de la grilla para editar.");
                return;
            }

            LimpiarSeleccionItem();
            idItemEditando = item.Id;
            numCantAdultosItem.Value = item.CantidadAdultos;
            numCantNinosItem.Value = item.CantidadNinos;

            lblComponentes.Text = "Editando: " + item.DescripcionComponentes;
            btnAgregarItem.Text = "Guardar cambios";
            btnCancelarEdicion.Visible = true;
            ActualizarSubtotalPlato();
            numCantAdultosItem.Focus();
        }

        private void GuardarCambiosItem()
        {
            if (idsMarcados.Count > 0)
            {
                Mensajes.Advertencia("Al editar un plato solo se cambian las cantidades. Para usar otro producto, quite el plato y agréguelo de nuevo.");
                return;
            }

            try
            {
                int cantidadAdultos = Convert.ToInt32(numCantAdultosItem.Value);
                int cantidadNinos = Convert.ToInt32(numCantNinosItem.Value);
                controlador.CambiarCantidadesItem(idEvento, idItemEditando, cantidadAdultos, cantidadNinos);

                TerminarEdicionItem();
                CargarGrillaItems();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            TerminarEdicionItem();
        }

        // Vuelve el formulario al modo "agregar plato"
        private void TerminarEdicionItem()
        {
            idItemEditando = 0;
            lblComponentes.Text = "Productos disponibles";
            btnAgregarItem.Text = "Agregar plato";
            btnCancelarEdicion.Visible = false;
            LimpiarSeleccionItem();
        }

        private void LimpiarSeleccionItem()
        {
            idsMarcados.Clear();
            llenandoLista = true;
            foreach (ListViewItem item in lvProductos.Items)
            {
                item.Checked = false;
            }
            llenandoLista = false;
            RestablecerCantidadesItem();
            ActualizarSubtotalPlato();
        }

        // Por defecto un plato se sirve a todos los invitados del evento.
        private void RestablecerCantidadesItem()
        {
            numCantAdultosItem.Value = adultosEvento;
            numCantNinosItem.Value = ninosEvento;
        }

        // ---------- Servicios adicionales ----------

        private void CargarServiciosDisponibles()
        {
            serviciosDisponibles = servicioControlador.ObtenerTodos();
            clbServicios.Items.Clear();
            foreach (ServicioAdicionalDTO servicio in serviciosDisponibles)
            {
                clbServicios.Items.Add(servicio.Nombre);
            }
        }

        private void CargarGrillaServicios()
        {
            dgvServiciosEvento.DataSource = null;
            dgvServiciosEvento.DataSource = controlador.ObtenerServiciosEvento(idEvento);
            dgvServiciosEvento.Columna("Monto").DefaultCellStyle.Format = Formato.Moneda;

            dgvServiciosEvento.Columna("Id").Visible = false;
            dgvServiciosEvento.Columna("Descripcion").HeaderText = "Descripción";
            LimpiarSeleccionGrilla(dgvServiciosEvento);
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            if (clbServicios.CheckedIndices.Count == 0)
            {
                Mensajes.Advertencia("Seleccione al menos un servicio para agregar.");
                return;
            }

            try
            {
                foreach (int indice in clbServicios.CheckedIndices)
                {
                    controlador.AgregarServicioAlEvento(idEvento, serviciosDisponibles[indice].Id);
                }
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }

            CargarGrillaServicios();
            for (int i = 0; i < clbServicios.Items.Count; i++)
            {
                clbServicios.SetItemChecked(i, false);
            }
        }

        private void btnQuitarServicio_Click(object sender, EventArgs e)
        {
            if (dgvServiciosEvento.CurrentRow?.DataBoundItem is not ServicioAdicionalDTO servicio)
            {
                Mensajes.Advertencia("Seleccione un servicio de la grilla para quitar.");
                return;
            }

            try
            {
                controlador.QuitarServicioDelEvento(idEvento, servicio.Id);
                CargarGrillaServicios();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        // ---------- Cierre ----------

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                PresupuestoResultadoDTO? presupuesto = presupuestoControlador.Generar(idEvento);
                string mensaje = presupuesto == null
                    ? "Cambios guardados. No hubo cambios que requieran un nuevo presupuesto."
                    : "Evento guardado y presupuesto generado.";
                Mensajes.Informacion(mensaje);
                Finalizado = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private static void LimpiarSeleccionGrilla(DataGridView grilla)
        {
            if (grilla.Rows.Count > 0)
            {
                grilla.ClearSelection();
                grilla.CurrentCell = null;
            }
        }
    }
}
