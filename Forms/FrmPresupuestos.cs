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
            dgvPresupuestos.MensajeSinFilas(() => "Este evento todavía no tiene presupuestos. Arme el menú y toque Finalizar, o toque Actualizar presupuesto.");
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
            dgvPresupuestos.Columna("NombreCliente").Visible = false;
            dgvPresupuestos.Columna("NombreLugar").Visible = false;
            dgvPresupuestos.Columna("CantidadAdultos").Visible = false;
            dgvPresupuestos.Columna("CantidadNinos").Visible = false;
            dgvPresupuestos.Columna("Vigente").Visible = false;

            DarFormatoColumnas();

            if (dgvPresupuestos.Rows.Count > 0)
            {
                dgvPresupuestos.QuitarSeleccion();
            }
        }

        private void DarFormatoColumnas()
        {
            dgvPresupuestos.Columna("Id").HeaderText = "N°";
            dgvPresupuestos.Columna("Id").FillWeight = 35;
            dgvPresupuestos.Columna("Estado").FillWeight = 160;
            dgvPresupuestos.Columna("Detalle").FillWeight = 230;

            DataGridViewColumn costo = dgvPresupuestos.Columna("CostoTotal");
            costo.HeaderText = "Costo total";
            costo.DefaultCellStyle.Format = Formato.Moneda;
            costo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            costo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            costo.HeaderCell.Style.Padding = new Padding(0, 0, 10, 0);
            costo.DefaultCellStyle.Padding = new Padding(0, 0, 10, 0);

            DataGridViewColumn fecha = dgvPresupuestos.Columna("FechaGeneracion");
            fecha.HeaderText = "Fecha de generación";
            fecha.DefaultCellStyle.Format = Formato.FechaHora;

            DataGridViewColumn fechaEvento = dgvPresupuestos.Columna("FechaEvento");
            fechaEvento.HeaderText = "Fecha del evento";
            fechaEvento.DefaultCellStyle.Format = Formato.FechaHora;
        }

        private void dgvPresupuestos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvPresupuestos.Columns[e.ColumnIndex].Name != "Estado" || e.Value is not EstadoPresupuesto estado)
            {
                return;
            }

            Tema.PintarEstado(e, estado);

            if (dgvPresupuestos.Rows[e.RowIndex].DataBoundItem is PresupuestoResultadoDTO presupuesto
                && !presupuesto.Vigente)
            {
                e.Value = estado.Texto() + " (desactualizado)";
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                PresupuestoResultadoDTO? generado = controlador.Generar(idEvento);
                if (generado == null)
                {
                    Mensajes.Informacion("El presupuesto ya está actualizado: tiene los datos y precios actuales del evento.");
                    return;
                }

                Mensajes.Informacion("Presupuesto actualizado: se generó el N° " + generado.Id + " con los valores actuales.");
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
                TicketPresupuesto.Mostrar(this, controlador.ObtenerDetalle(idEvento, seleccionado.Id));
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

            if (seleccionado.Estado != EstadoPresupuesto.Cancelado)
            {
                string pregunta = seleccionado.Estado == EstadoPresupuesto.Aprobado
                    ? "El presupuesto N° " + seleccionado.Id + " está APROBADO. ¿Seguro que desea cancelarlo?"
                    : "¿Seguro que desea cancelar el presupuesto N° " + seleccionado.Id + "?";
                if (!Mensajes.Confirmar(pregunta))
                {
                    return;
                }
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
