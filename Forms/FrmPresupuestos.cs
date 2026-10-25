using ProyectoEventos.Common.Enums;
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
        private readonly NegocioDTO negocio;
        private readonly int idEvento;

        public FrmPresupuestos(int idEvento)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new PresupuestoController();
            eventoControlador = new EventoController();
            negocio = new NegocioController().Obtener();
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
            dgvPresupuestos.Columna("Sena").Visible = false;
            dgvPresupuestos.Columna("FechaSena").Visible = false;
            dgvPresupuestos.Columna("FechaPago").Visible = false;
            dgvPresupuestos.Columna("Saldo").Visible = false;
            dgvPresupuestos.Columna("FechaVencimiento").Visible = false;
            dgvPresupuestos.Columna("Vencido").Visible = false;
            dgvPresupuestos.Columna("SenaSugerida").Visible = false;

            DarFormatoColumnas();

            if (dgvPresupuestos.Rows.Count > 0)
            {
                dgvPresupuestos.QuitarSeleccion();
            }
            CargarEstadoPago();
        }

        private void CargarEstadoPago()
        {
            PresupuestoResultadoDTO? aprobado = controlador.ObtenerAprobado(idEvento);
            lblEstadoPago.Text = aprobado == null
                ? "Cobro"
                : "Cobro del presupuesto N° " + aprobado.Id + ": " + aprobado.EstadoPago?.Texto();
            lblDetallePago.Text = DetalleEstadoPago(aprobado);
            numSena.Value = aprobado == null ? 0 : aprobado.SenaSugerida;
        }

        private static string DetalleEstadoPago(PresupuestoResultadoDTO? aprobado)
        {
            if (aprobado == null)
            {
                return "El evento todavía no tiene un presupuesto aprobado.";
            }

            string total = "Total " + aprobado.CostoTotal.ToString(Formato.Moneda);
            switch (aprobado.EstadoPago)
            {
                case EstadoPago.Pagado:
                    return total + "  ·  Pagado el " + aprobado.FechaPago?.ToString(Formato.Fecha);
                case EstadoPago.Senado:
                    return total + "  ·  Seña " + aprobado.Sena.ToString(Formato.Moneda) + " (" + aprobado.FechaSena?.ToString(Formato.Fecha) + ")" +
                        "  ·  Falta cobrar " + aprobado.Saldo.ToString(Formato.Moneda);
                default:
                    return total + "  ·  Falta cobrar " + aprobado.Saldo.ToString(Formato.Moneda);
            }
        }

        private PresupuestoResultadoDTO? ObtenerAprobadoParaCobrar()
        {
            PresupuestoResultadoDTO? aprobado = controlador.ObtenerAprobado(idEvento);
            if (aprobado == null)
            {
                Mensajes.Advertencia("El evento no tiene un presupuesto aprobado. Apruebe uno antes de registrar la seña, el pago o ver el comprobante.");
            }
            return aprobado;
        }

        private void btnRegistrarSena_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? aprobado = ObtenerAprobadoParaCobrar();
            if (aprobado == null || !Mensajes.Confirmar("¿Registrar una seña de " + numSena.Value.ToString(Formato.Moneda) +
                " para el presupuesto N° " + aprobado.Id + "? La seña no es reembolsable."))
            {
                return;
            }

            try
            {
                controlador.RegistrarSena(idEvento, numSena.Value);
                CargarGrillaPresupuestos();
                MostrarComprobante();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnMarcarPagado_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? aprobado = ObtenerAprobadoParaCobrar();
            if (aprobado == null)
            {
                return;
            }
            if (aprobado.EstadoPago == EstadoPago.Pagado)
            {
                Mensajes.Informacion("El presupuesto N° " + aprobado.Id + " ya está pagado.");
                return;
            }
            if (!Mensajes.Confirmar("¿Confirmar que el cliente pagó " + aprobado.Saldo.ToString(Formato.Moneda) +
                " y el presupuesto N° " + aprobado.Id + " queda PAGADO?"))
            {
                return;
            }

            try
            {
                controlador.MarcarPagado(idEvento);
                CargarGrillaPresupuestos();
                MostrarComprobante();
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnComprobante_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? aprobado = ObtenerAprobadoParaCobrar();
            if (aprobado == null)
            {
                return;
            }
            if (aprobado.EstadoPago == EstadoPago.SinSena)
            {
                Mensajes.Advertencia("Todavía no se registró una seña ni un pago para el presupuesto N° " + aprobado.Id + ".");
                return;
            }
            MostrarComprobante();
        }

        private void MostrarComprobante()
        {
            TicketPresupuesto.MostrarComprobante(this, controlador.ObtenerDetalleAprobado(idEvento), negocio);
        }

        private void DarFormatoColumnas()
        {
            dgvPresupuestos.Columna("Id").HeaderText = "N°";
            dgvPresupuestos.Columna("Id").FillWeight = 35;
            dgvPresupuestos.Columna("Estado").FillWeight = 160;
            dgvPresupuestos.Columna("Detalle").FillWeight = 230;
            dgvPresupuestos.Columna("EstadoPago").HeaderText = "Pago";
            dgvPresupuestos.Columna("EstadoPago").FillWeight = 80;

            DataGridViewColumn costo = dgvPresupuestos.Columna("CostoTotal");
            costo.HeaderText = "Costo total";
            costo.DefaultCellStyle.Format = Formato.Moneda;

            DataGridViewColumn fecha = dgvPresupuestos.Columna("FechaGeneracion");
            fecha.HeaderText = "Fecha de generación";
            fecha.DefaultCellStyle.Format = Formato.FechaHora;

            DataGridViewColumn fechaEvento = dgvPresupuestos.Columna("FechaEvento");
            fechaEvento.HeaderText = "Fecha del evento";
            fechaEvento.DefaultCellStyle.Format = Formato.FechaHora;
        }

        private void dgvPresupuestos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (dgvPresupuestos.Columns[e.ColumnIndex].Name == "EstadoPago")
            {
                Tema.PintarEstadoPago(e, e.Value as EstadoPago?);
                return;
            }
            if (dgvPresupuestos.Columns[e.ColumnIndex].Name == "Detalle")
            {
                if (dgvPresupuestos.Rows[e.RowIndex].DataBoundItem is PresupuestoResultadoDTO conDetalle && conDetalle.Vencido
                    && conDetalle.Estado == EstadoPresupuesto.EnEspera)
                {
                    Tema.PintarCelda(e, Tema.PeligroSuave, Tema.PeligroOscuro);
                }
                return;
            }
            if (dgvPresupuestos.Columns[e.ColumnIndex].Name != "Estado" || e.Value is not EstadoPresupuesto estado)
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
                if (seleccionado.Vencido && !Mensajes.Confirmar("El presupuesto N° " + seleccionado.Id + " venció el " +
                    seleccionado.FechaVencimiento?.ToString(Formato.Fecha) + ". ¿Desea aprobarlo igual?"))
                {
                    return;
                }

                bool reemplazar = false;
                PresupuestoResultadoDTO? aprobado = controlador.ObtenerAprobado(idEvento);
                if (aprobado != null && aprobado.Id != seleccionado.Id)
                {
                    if (aprobado.EstadoPago == EstadoPago.Pagado)
                    {
                        Mensajes.Advertencia("El presupuesto aprobado N° " + aprobado.Id + " ya está pagado: no se puede reemplazar.");
                        return;
                    }
                    string pregunta = "Ya hay un presupuesto aprobado para este evento (N° " + aprobado.Id + "). ¿Desea reemplazarlo por este?";
                    if (aprobado.Sena > 0)
                    {
                        pregunta += " La seña de " + aprobado.Sena.ToString(Formato.Moneda) + " pasa al nuevo presupuesto.";
                    }
                    if (!Mensajes.Confirmar(pregunta))
                    {
                        return;
                    }
                    reemplazar = true;
                }

                controlador.Aprobar(idEvento, seleccionado.Id, reemplazar, seleccionado.Vencido);
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
                TicketPresupuesto.Mostrar(this, controlador.ObtenerDetalle(idEvento, seleccionado.Id), negocio);
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private static string PreguntaCancelar(PresupuestoResultadoDTO presupuesto)
        {
            if (presupuesto.Estado != EstadoPresupuesto.Aprobado)
            {
                return "¿Seguro que desea cancelar el presupuesto N° " + presupuesto.Id + "?";
            }

            string pregunta = "El presupuesto N° " + presupuesto.Id + " está APROBADO.";
            if (presupuesto.Sena > 0)
            {
                pregunta += " Tiene una seña de " + presupuesto.Sena.ToString(Formato.Moneda) + " que no se devuelve.";
            }
            return pregunta + " ¿Seguro que desea cancelarlo?";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PresupuestoResultadoDTO? seleccionado = ObtenerPresupuestoSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un presupuesto de la grilla.");
                return;
            }

            if (seleccionado.EstadoPago == EstadoPago.Pagado)
            {
                Mensajes.Advertencia("El presupuesto N° " + seleccionado.Id + " ya está pagado: no se puede cancelar.");
                return;
            }

            if (seleccionado.Estado != EstadoPresupuesto.Cancelado)
            {
                if (!Mensajes.Confirmar(PreguntaCancelar(seleccionado)))
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
