using System;
using System.Drawing;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmPresupuestos : Form
    {
        private readonly PresupuestoController controlador;
        private readonly EventoController eventoControlador;
        private readonly int idEvento;

        public FrmPresupuestos(int idEvento)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new PresupuestoController();
            eventoControlador = new EventoController();
            this.idEvento = idEvento;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmPresupuestos_Load(object sender, EventArgs e)
        {
            CargarInfoEvento();
            CargarGrillaPresupuestos();
        }

        private void CargarInfoEvento()
        {
            EventoResultadoDTO evento = eventoControlador.ObtenerPorId(idEvento);
            lblInfoEvento.Text = "Cliente: " + evento.NombreCliente +
                "   Lugar: " + evento.NombreLugar +
                "   Fecha: " + evento.Fecha.ToString(Formato.FechaHora);
        }

        private void CargarGrillaPresupuestos()
        {
            dgvPresupuestos.DataSource = null;
            dgvPresupuestos.DataSource = controlador.ObtenerPorEvento(idEvento);

            dgvPresupuestos.Columna("IdEvento").Visible = false;
            // La vigencia se muestra junto al estado (ver CellFormatting)
            dgvPresupuestos.Columna("Vigente").Visible = false;

            DarFormatoColumnas();

            if (dgvPresupuestos.Rows.Count > 0)
            {
                dgvPresupuestos.ClearSelection();
                dgvPresupuestos.CurrentCell = null;
            }
            ActualizarBotones();
        }

        // Cada acción se habilita solo si tiene sentido para el presupuesto seleccionado:
        // aprobar = en espera y vigente; cancelar = no cancelado; imprimir = cualquiera.
        private void ActualizarBotones()
        {
            PresupuestoResultadoDTO? seleccionado = ObtenerPresupuestoSeleccionado();
            btnAprobar.Enabled = seleccionado != null && seleccionado.Estado == EstadoPresupuesto.EnEspera && seleccionado.Vigente;
            btnCancelar.Enabled = seleccionado != null && seleccionado.Estado != EstadoPresupuesto.Cancelado;
            btnImprimir.Enabled = seleccionado != null;
        }

        private void dgvPresupuestos_CurrentCellChanged(object? sender, EventArgs e)
        {
            ActualizarBotones();
        }

        // Encabezados legibles, monto a la derecha con moneda y fecha con formato local
        private void DarFormatoColumnas()
        {
            DataGridViewColumn? costo = dgvPresupuestos.Columns["CostoTotal"];
            if (costo != null)
            {
                costo.HeaderText = "Costo total";
                costo.DefaultCellStyle.Format = Formato.Moneda;
                costo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                costo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                costo.HeaderCell.Style.Padding = new Padding(0, 0, 10, 0);
                costo.DefaultCellStyle.Padding = new Padding(0, 0, 10, 0);
            }

            DataGridViewColumn? fecha = dgvPresupuestos.Columns["FechaGeneracion"];
            if (fecha != null)
            {
                fecha.HeaderText = "Fecha de generación";
                fecha.DefaultCellStyle.Format = Formato.FechaHora;
            }

            DataGridViewColumn? cliente = dgvPresupuestos.Columns["NombreCliente"];
            if (cliente != null)
            {
                cliente.HeaderText = "Cliente";
            }

            DataGridViewColumn? lugar = dgvPresupuestos.Columns["NombreLugar"];
            if (lugar != null)
            {
                lugar.HeaderText = "Lugar";
            }
        }

        // Colorea la celda de Estado: verde = aprobado, terracota = cancelado, arena = en espera.
        // Si el evento cambió después de generar el presupuesto, se aclara que está desactualizado.
        private void dgvPresupuestos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvPresupuestos.Columns[e.ColumnIndex].Name != "Estado" || e.Value is not EstadoPresupuesto estado)
            {
                return;
            }

            EstadoPresupuestoVista.Aplicar(e, estado, dgvPresupuestos.DefaultCellStyle.Font ?? dgvPresupuestos.Font);

            if (estado != EstadoPresupuesto.Cancelado
                && dgvPresupuestos.Rows[e.RowIndex].DataBoundItem is PresupuestoResultadoDTO presupuesto
                && !presupuesto.Vigente)
            {
                e.Value = EstadoPresupuestoVista.Texto(estado) + " (desactualizado)";
            }
        }

        private PresupuestoResultadoDTO? ObtenerPresupuestoSeleccionado()
        {
            return dgvPresupuestos.Seleccionado<PresupuestoResultadoDTO>();
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? seleccionado = ObtenerPresupuestoSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un presupuesto de la grilla.");
                return;
            }

            try
            {
                // Primero se controla que se pueda aprobar, así no se pregunta por un reemplazo que después falla.
                controlador.ValidarAprobacion(idEvento, seleccionado.Id);

                bool reemplazar = false;
                if (controlador.HayOtroAprobado(idEvento, seleccionado.Id))
                {
                    if (!Mensajes.Confirmar("Ya hay un presupuesto aprobado para este evento. ¿Desea reemplazarlo por este?"))
                    {
                        return;
                    }
                    reemplazar = true;
                }

                controlador.Aprobar(idEvento, seleccionado.Id, reemplazar);
                CargarGrillaPresupuestos();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? seleccionado = ObtenerPresupuestoSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un presupuesto de la grilla.");
                return;
            }

            try
            {
                TicketPresupuesto.MostrarVistaPrevia(this, controlador.ObtenerDetalle(idEvento, seleccionado.Id));
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? seleccionado = ObtenerPresupuestoSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un presupuesto de la grilla.");
                return;
            }

            try
            {
                controlador.Cancelar(idEvento, seleccionado.Id);
                CargarGrillaPresupuestos();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }
    }
}
