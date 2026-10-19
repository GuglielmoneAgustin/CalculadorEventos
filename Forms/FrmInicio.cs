using System;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmInicio : Form
    {
        private const int DiasUrgentes = 7;

        private readonly EventoController eventoControlador;

        public FrmInicio()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            lblResumenDetalleHoy.Font = new Font(lblResumenDetalleHoy.Font, FontStyle.Bold);
            tlpGrillaPendientes.Margin = new Padding(0, 20, 0, 0);
            eventoControlador = new EventoController();
            dgvProximosEventos.MensajeSinFilas(() => "No hay eventos aprobados próximos. Los eventos aparecen acá cuando se aprueba su presupuesto.");
            dgvPendientes.MensajeSinFilas(() => "No hay nada pendiente: todos los eventos próximos tienen su presupuesto al día.");
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            lblResumenValorHoy.Text = Formato.DiaSemana(DateTime.Today);
            lblResumenDetalleHoy.Text = DateTime.Today.ToString("d 'de' MMMM");
            List<EventoResultadoDTO> proximos = eventoControlador.ObtenerProximosAprobados();
            List<EventoResultadoDTO> sinAprobar = eventoControlador.ObtenerSinAprobarProximosDias(DiasUrgentes);
            List<PendienteDTO> pendientes = eventoControlador.ObtenerPendientes();
            CargarAlerta(sinAprobar);
            CargarSemana(proximos, sinAprobar);
            CargarResumenPendientes(pendientes);
            CargarProximoEvento(proximos);
            CargarProximosEventos(proximos);
            CargarPendientes(pendientes);
        }

        private void CargarAlerta(List<EventoResultadoDTO> urgentes)
        {
            if (urgentes.Count == 0)
            {
                lblAlerta.Visible = false;
                return;
            }

            string cuales = string.Join(", ", urgentes.Take(3).Select(e => e.NombreCliente + " (" + Formato.Falta(e.Fecha).ToLower() + ")"));
            if (urgentes.Count > 3)
            {
                cuales += " y " + (urgentes.Count - 3) + " más";
            }
            lblAlerta.Text = (urgentes.Count == 1
                ? "⚠  1 evento de los próximos " + DiasUrgentes + " días sigue sin aprobar: "
                : "⚠  " + urgentes.Count + " eventos de los próximos " + DiasUrgentes + " días siguen sin aprobar: ") + cuales + ".";
            lblAlerta.Visible = true;
        }

        private void CargarSemana(List<EventoResultadoDTO> proximos, List<EventoResultadoDTO> sinAprobar)
        {
            int aprobados = proximos.Count(e => Formato.DiasHasta(e.Fecha) <= DiasUrgentes);
            int total = aprobados + sinAprobar.Count;
            lblResumenValorSemana.Text = total == 1 ? "1 evento" : total + " eventos";
            lblResumenDetalleSemana.Text = total == 0
                ? "Nada en los próximos " + DiasUrgentes + " días."
                : aprobados + (aprobados == 1 ? " aprobado" : " aprobados") + Environment.NewLine + sinAprobar.Count + " sin aprobar";
        }

        private void CargarResumenPendientes(List<PendienteDTO> pendientes)
        {
            lblResumenValorPendientes.Text = pendientes.Count.ToString();
            if (pendientes.Count == 0)
            {
                lblResumenDetallePendientes.Text = "Todo al día.";
                return;
            }

            lblResumenDetallePendientes.Text = string.Join(Environment.NewLine, pendientes
                .GroupBy(p => p.Tipo)
                .OrderBy(g => g.Key)
                .Select(g => g.Count() + " " + g.Key.Texto().ToLower()));
        }

        private void CargarProximoEvento(List<EventoResultadoDTO> proximos)
        {
            if (proximos.Count == 0)
            {
                lblResumenTituloProximoEvento.Text = "Próximo evento";
                lblResumenValorProximoEvento.Text = "Sin eventos aprobados";
                lblResumenDetalleProximoEvento.Text = "Aparece acá cuando se aprueba un presupuesto.";
                return;
            }

            EventoResultadoDTO proximo = proximos[0];
            lblResumenTituloProximoEvento.Text = "Próximo evento · " + Formato.Falta(proximo.Fecha);
            lblResumenValorProximoEvento.Text = Formato.FechaCorta(proximo.Fecha);
            lblResumenDetalleProximoEvento.Text = proximo.NombreCliente + " · " + proximo.NombreLugar + Environment.NewLine + proximo.Invitados + " invitados · " + Formato.Horario(proximo.Fecha, proximo.HorasDuracion);
        }

        private void CargarProximosEventos(List<EventoResultadoDTO> proximos)
        {
            dgvProximosEventos.DataSource = null;
            dgvProximosEventos.DataSource = proximos.Select(e => new FilaProximoEvento(e)).ToList();
            dgvProximosEventos.Columna("Fecha").DefaultCellStyle.Format = Formato.Fecha;
            dgvProximosEventos.Columna("Total").HeaderText = "Total aprobado";
            dgvProximosEventos.Columna("Total").DefaultCellStyle.Format = Formato.Moneda;
            dgvProximosEventos.Columna("Falta").FillWeight = 70;
            dgvProximosEventos.Columna("Fecha").FillWeight = 80;
            dgvProximosEventos.Columna("Invitados").FillWeight = 60;
            dgvProximosEventos.ClearSelection();
            dgvProximosEventos.CurrentCell = null;
        }

        private void CargarPendientes(List<PendienteDTO> pendientes)
        {
            dgvPendientes.DataSource = null;
            dgvPendientes.DataSource = pendientes;
            dgvPendientes.Columna("IdEvento").Visible = false;
            dgvPendientes.Columna("NombreCliente").HeaderText = "Cliente";
            dgvPendientes.Columna("FechaEvento").HeaderText = "Fecha del evento";
            dgvPendientes.Columna("FechaEvento").DefaultCellStyle.Format = Formato.FechaHora;
            dgvPendientes.Columna("Tipo").HeaderText = "Situación";
            dgvPendientes.Columna("Detalle").FillWeight = 220;
            dgvPendientes.ClearSelection();
            dgvPendientes.CurrentCell = null;
        }

        private void dgvProximosEventos_Enter(object? sender, EventArgs e)
        {
            QuitarSeleccion(dgvPendientes);
        }

        private void dgvPendientes_Enter(object? sender, EventArgs e)
        {
            QuitarSeleccion(dgvProximosEventos);
        }

        private static void QuitarSeleccion(DataGridView grilla)
        {
            grilla.ClearSelection();
            grilla.CurrentCell = null;
        }

        private void btnVerPresupuestos_Click(object sender, EventArgs e)
        {
            int? idEvento = dgvProximosEventos.Seleccionado<FilaProximoEvento>()?.IdEvento
                ?? dgvPendientes.Seleccionado<PendienteDTO>()?.IdEvento;
            if (idEvento == null)
            {
                Mensajes.Advertencia("Seleccione un evento en cualquiera de las dos grillas para ver sus presupuestos.");
                return;
            }

            FrmPresupuestos pantalla = new FrmPresupuestos(idEvento.Value);
            if (ParentForm is FrmPrincipal principal)
            {
                principal.AbrirSubpantalla(pantalla, Cargar);
            }
            else
            {
                pantalla.ShowDialog();
                Cargar();
            }
        }

        private void dgvProximosEventos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProximosEventos.Rows[e.RowIndex].DataBoundItem is FilaProximoEvento fila
                && Formato.DiasHasta(fila.Fecha) <= DiasUrgentes)
            {
                Tema.ResaltarFila(e, dgvProximosEventos.Columns[e.ColumnIndex].Name == "Falta");
            }
        }

        private void dgvPendientes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPendientes.Columns[e.ColumnIndex].Name == "Tipo" && e.Value is TipoPendiente tipo)
            {
                Tema.PintarPendiente(e, tipo);
            }
        }

        private sealed class FilaProximoEvento
        {
            public FilaProximoEvento(EventoResultadoDTO evento)
            {
                IdEvento = evento.Id;
                Falta = Formato.Falta(evento.Fecha);
                Cliente = evento.NombreCliente;
                Lugar = evento.NombreLugar;
                Fecha = evento.Fecha;
                Horario = Formato.Horario(evento.Fecha, evento.HorasDuracion);
                Invitados = evento.Invitados;
                Total = evento.Total;
            }

            [System.ComponentModel.Browsable(false)]
            public int IdEvento { get; }

            public string Falta { get; }
            public string Cliente { get; }
            public string Lugar { get; }
            public DateTime Fecha { get; }
            public string Horario { get; }
            public int Invitados { get; }
            public decimal? Total { get; }
        }
    }
}
