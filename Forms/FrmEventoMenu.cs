using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        private readonly EventoController controlador;
        private readonly ProductoController productoControlador;
        private readonly ServicioAdicionalController servicioControlador;
        private readonly PresupuestoController presupuestoControlador;
        private readonly int idEvento;
        // Verdadero cuando el usuario termino con "Finalizar" (y no con "Volver")
        public bool Finalizado { get; private set; }
        private int adultosEvento;
        private int ninosEvento;
        private List<ProductoDTO> productosDisponibles = new List<ProductoDTO>();
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
            clbComponentes.Items.Clear();
            foreach (ProductoDTO producto in productosDisponibles)
            {
                clbComponentes.Items.Add(producto.Nombre);
            }
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

            // Solo las cantidades se pueden editar en la grilla (doble clic, F2 o escribiendo un número)
            foreach (DataGridViewColumn columna in dgvItemsMenu.Columns)
            {
                columna.ReadOnly = !EsColumnaCantidad(columna.Index);
            }
            dgvItemsMenu.Columna("CantidadAdultos").ToolTipText = "Doble clic para cambiar la cantidad";
            dgvItemsMenu.Columna("CantidadNinos").ToolTipText = "Doble clic para cambiar la cantidad";
            LimpiarSeleccionGrilla(dgvItemsMenu);

            lblTotalMenu.Text = "Total menú: " + controlador.ObtenerTotalMenu(idEvento).ToString(Formato.Moneda);
        }

        private void btnAgregarItem_Click(object sender, EventArgs e)
        {
            if (clbComponentes.CheckedIndices.Count == 0)
            {
                Mensajes.Advertencia("Seleccione al menos un producto para el plato.");
                return;
            }

            try
            {
                int cantidadAdultos = Convert.ToInt32(numCantAdultosItem.Value);
                int cantidadNinos = Convert.ToInt32(numCantNinosItem.Value);

                // Cada producto marcado se agrega como un plato propio del menú (todos o ninguno).
                List<int> idsProductos = new List<int>();
                foreach (int indice in clbComponentes.CheckedIndices)
                {
                    idsProductos.Add(productosDisponibles[indice].Id);
                }
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
                CargarGrillaItems();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        // ---------- Edición de cantidades de un plato ----------

        private bool EsColumnaCantidad(int indiceColumna)
        {
            string nombre = dgvItemsMenu.Columns[indiceColumna].Name;
            return nombre == "CantidadAdultos" || nombre == "CantidadNinos";
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            if (dgvItemsMenu.CurrentRow == null)
            {
                Mensajes.Advertencia("Seleccione un plato de la grilla para editar sus cantidades.");
                return;
            }

            dgvItemsMenu.Focus();
            dgvItemsMenu.CurrentCell = dgvItemsMenu.CurrentRow.Cells["CantidadAdultos"];
            dgvItemsMenu.BeginEdit(true);
        }

        private void dgvItemsMenu_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EsColumnaCantidad(e.ColumnIndex))
            {
                return;
            }

            dgvItemsMenu.CurrentCell = dgvItemsMenu.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dgvItemsMenu.BeginEdit(true);
        }

        private void dgvItemsMenu_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!dgvItemsMenu.IsCurrentCellInEditMode || !EsColumnaCantidad(e.ColumnIndex))
            {
                return;
            }

            if (!int.TryParse(Convert.ToString(e.FormattedValue), out int cantidad) || cantidad < 0)
            {
                Mensajes.Advertencia("Ingrese una cantidad entera, cero o mayor.");
                dgvItemsMenu.CancelEdit();
            }
        }

        // El valor nuevo ya quedó en la fila; se aplica al evento y se recarga la grilla
        // (con el subtotal nuevo, o con los valores anteriores si la regla de negocio lo rechazó).
        private void dgvItemsMenu_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (!EsColumnaCantidad(e.ColumnIndex) || dgvItemsMenu.Rows[e.RowIndex].DataBoundItem is not ItemMenuResultadoDTO item)
            {
                return;
            }

            try
            {
                controlador.CambiarCantidadesItem(idEvento, item.Id, item.CantidadAdultos, item.CantidadNinos);
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }

            // No se puede cambiar el DataSource dentro del evento de edición: se recarga apenas termina
            BeginInvoke(CargarGrillaItems);
        }

        private void dgvItemsMenu_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            // Un valor que no se pudo convertir se descarta (CellValidating ya avisó al usuario)
            e.ThrowException = false;
            dgvItemsMenu.CancelEdit();
        }

        private void LimpiarSeleccionItem()
        {
            for (int i = 0; i < clbComponentes.Items.Count; i++)
            {
                clbComponentes.SetItemChecked(i, false);
            }
            RestablecerCantidadesItem();
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
