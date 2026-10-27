using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmEventos : Form, IVuelveDeSubpantalla
    {
        private readonly EventoController controlador;
        private List<EventoResultadoDTO> eventos = new List<EventoResultadoDTO>();
        private int? idASeleccionarAlVolver;
        private int ultimoIdAntesDeAbrir;

        public FrmEventos()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new EventoController();
        }

        private void chkOcultarRealizados_CheckedChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        private void FrmEventos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CargarGrilla()
        {
            eventos = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        private void MostrarGrilla()
        {
            List<EventoResultadoDTO> visibles = new List<EventoResultadoDTO>();
            foreach (EventoResultadoDTO evento in eventos)
            {
                if ((!chkOcultarRealizados.Checked || !evento.Realizado)
                    && Busqueda.Coincide(txtBuscar.Text, evento.Id.ToString(), evento.NombreCliente, evento.NombreLugar,
                        evento.Fecha.ToString(Formato.FechaHora), evento.EstadoPresupuesto.Texto()))
                {
                    visibles.Add(evento);
                }
            }

            dgvEventos.MensajeSinFilas(eventos.Count == 0
                ? "Todavía no hay eventos. Toque Nuevo evento para empezar."
                : "Ningún evento coincide con la búsqueda.");
            dgvEventos.DataSource = null;
            dgvEventos.DataSource = visibles;
            ConfigurarColumnas();
            dgvEventos.QuitarSeleccion();
        }

        private void ConfigurarColumnas()
        {
            dgvEventos.Columna("Fecha").DefaultCellStyle.Format = Formato.FechaHora;
            dgvEventos.Columna("Id").HeaderText = "N°";
            dgvEventos.Columna("NombreCliente").HeaderText = "Cliente";
            dgvEventos.Columna("NombreLugar").HeaderText = "Lugar";
            dgvEventos.Columna("Fecha").HeaderText = "Fecha y hora";
            dgvEventos.Columna("HorasDuracion").HeaderText = "Duración (hs)";
            dgvEventos.Columna("CantidadAdultos").Visible = false;
            dgvEventos.Columna("CantidadNinos").Visible = false;
            dgvEventos.Columna("EstadoPresupuesto").HeaderText = "Presupuesto";
            dgvEventos.Columna("Total").HeaderText = "Total";
            dgvEventos.Columna("Total").DefaultCellStyle.Format = Formato.Moneda;
            dgvEventos.Columna("IdCliente").Visible = false;
            dgvEventos.Columna("IdLugar").Visible = false;
            dgvEventos.Columna("Realizado").Visible = false;
            dgvEventos.Columna("Id").FillWeight = 35;
            dgvEventos.Columna("NombreCliente").FillWeight = 130;
            dgvEventos.Columna("NombreLugar").FillWeight = 120;
            dgvEventos.Columna("Fecha").FillWeight = 120;
            dgvEventos.Columna("HorasDuracion").FillWeight = 70;
            dgvEventos.Columna("Invitados").FillWeight = 70;
            dgvEventos.Columna("EstadoPresupuesto").FillWeight = 95;
            dgvEventos.Columna("Total").FillWeight = 115;
            dgvEventos.Columna("EstadoPago").HeaderText = "Pago";
            dgvEventos.Columna("EstadoPago").FillWeight = 80;
        }

        private void dgvEventos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dgvEventos.Columns[e.ColumnIndex].Name == "EstadoPresupuesto")
            {
                Tema.PintarEstado(e, e.Value as EstadoPresupuesto?);
            }
            else if (dgvEventos.Columns[e.ColumnIndex].Name == "EstadoPago")
            {
                Tema.PintarEstadoPago(e, e.Value as EstadoPago?);
            }
            else if (dgvEventos.Rows[e.RowIndex].DataBoundItem is EventoResultadoDTO evento && evento.Realizado)
            {
                e.CellStyle.ForeColor = Tema.TextoApagado;
                e.CellStyle.SelectionForeColor = Tema.TextoApagado;
            }
        }

        private EventoResultadoDTO? ObtenerSeleccionado()
        {
            return dgvEventos.Seleccionado<EventoResultadoDTO>();
        }

        // Al volver se selecciona el evento con el que se trabajó, o el recién creado si era uno nuevo.
        private void AbrirSubpantalla(Form pantalla, int? idEvento = null)
        {
            idASeleccionarAlVolver = idEvento;
            ultimoIdAntesDeAbrir = 0;
            foreach (EventoResultadoDTO evento in eventos)
            {
                if (evento.Id > ultimoIdAntesDeAbrir)
                {
                    ultimoIdAntesDeAbrir = evento.Id;
                }
            }
            Navegacion.AbrirSubpantalla(this, pantalla);
        }

        public void AlVolverDeSubpantalla(Form subpantalla)
        {
            CargarGrilla();
            int? aSeleccionar = idASeleccionarAlVolver;
            if (aSeleccionar == null)
            {
                foreach (EventoResultadoDTO evento in eventos)
                {
                    if (evento.Id > ultimoIdAntesDeAbrir && (aSeleccionar == null || evento.Id > aSeleccionar))
                    {
                        aSeleccionar = evento.Id;
                    }
                }
            }
            if (aSeleccionar != null)
            {
                dgvEventos.SeleccionarFila("Id", aSeleccionar.Value.ToString());
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string? faltante = DescribirFaltanteParaEvento(new ClienteController().ObtenerTodos().Count > 0,
                new LugarController().ObtenerTodos().Count > 0);
            if (faltante != null)
            {
                Mensajes.Advertencia(faltante);
                return;
            }
            AbrirSubpantalla(new FrmEventoDatos());
        }

        internal static string? DescribirFaltanteParaEvento(bool hayClientes, bool hayLugares)
        {
            if (!hayClientes && !hayLugares)
            {
                return "Para crear un evento primero cargue al menos un cliente (en Clientes) y un lugar (en Lugares).";
            }
            if (!hayClientes)
            {
                return "Todavía no hay clientes cargados. Cargue al menos uno en Clientes antes de crear un evento.";
            }
            if (!hayLugares)
            {
                return "Todavía no hay lugares cargados. Cargue al menos uno en Lugares antes de crear un evento.";
            }
            return null;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para editar.");
                return;
            }

            AbrirSubpantalla(new FrmEventoDatos(seleccionado), seleccionado.Id);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para editar su menú.");
                return;
            }

            AbrirSubpantalla(new FrmEventoMenu(seleccionado.Id), seleccionado.Id);
        }

        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para ver sus presupuestos.");
                return;
            }

            AbrirSubpantalla(new FrmPresupuestos(seleccionado.Id), seleccionado.Id);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este evento?"))
            {
                try
                {
                    controlador.Eliminar(seleccionado.Id);
                    CargarGrilla();
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }
    }
}
