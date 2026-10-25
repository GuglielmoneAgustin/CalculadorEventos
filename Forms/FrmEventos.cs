using System;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmEventos : Form
    {
        private readonly EventoController controlador;
        private List<EventoResultadoDTO> eventos = new List<EventoResultadoDTO>();

        public FrmEventos()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new EventoController();
            dgvEventos.MensajeSinFilas(() => eventos.Count == 0
                ? "Todavía no hay eventos. Toque Nuevo evento para empezar."
                : "Ningún evento coincide con la búsqueda.");
        }

        private void chkOcultarRealizados_CheckedChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        private static bool EsRealizado(EventoResultadoDTO evento)
        {
            return evento.Fecha.AddHours((double)evento.HorasDuracion) < DateTime.Now;
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
            List<EventoResultadoDTO> visibles = chkOcultarRealizados.Checked
                ? eventos.Where(ev => !EsRealizado(ev)).ToList()
                : eventos;
            dgvEventos.DataSource = null;
            dgvEventos.DataSource = Busqueda.Filtrar(visibles, txtBuscar.Text, ev => new[] { ev.Id.ToString(), ev.NombreCliente, ev.NombreLugar, ev.Fecha.ToString(Formato.FechaHora), ev.EstadoPresupuesto.Texto() });
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
            else if (dgvEventos.Rows[e.RowIndex].DataBoundItem is EventoResultadoDTO evento && EsRealizado(evento))
            {
                e.CellStyle.ForeColor = Tema.TextoApagado;
                e.CellStyle.SelectionForeColor = Tema.TextoApagado;
            }
        }

        private EventoResultadoDTO? ObtenerSeleccionado()
        {
            return dgvEventos.Seleccionado<EventoResultadoDTO>();
        }

        private void AbrirSubpantalla(Form pantalla, int? idEvento = null)
        {
            int ultimoId = eventos.Count == 0 ? 0 : eventos.Max(ev => ev.Id);
            Navegacion.AbrirSubpantalla(this, pantalla, () =>
            {
                CargarGrilla();
                int? aSeleccionar = idEvento ?? eventos.Where(ev => ev.Id > ultimoId).Select(ev => (int?)ev.Id).Max();
                if (aSeleccionar != null)
                {
                    dgvEventos.SeleccionarFila<EventoResultadoDTO>(ev => ev.Id == aSeleccionar);
                }
            });
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirSubpantalla(new FrmEventoDatos());
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
