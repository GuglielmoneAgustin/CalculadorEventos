using ProyectoEventos.Common;
using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmEventoDatos : Form
    {
        private readonly EventoController controlador;
        private readonly ClienteController clienteControlador;
        private readonly LugarController lugarControlador;
        private readonly EventoResultadoDTO? eventoInicial;
        private int idEvento;
        private bool creadoSinFinalizar;
        private DateTime? fechaGuardada;
        private bool cargando;
        private string valoresGuardados = string.Empty;

        public FrmEventoDatos(EventoResultadoDTO? evento = null)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new EventoController();
            clienteControlador = new ClienteController();
            lugarControlador = new LugarController();
            eventoInicial = evento;
            idEvento = evento == null ? 0 : evento.Id;
            fechaGuardada = evento?.Fecha;
            FormClosing += FrmEventoDatos_FormClosing;
        }

        private void FrmEventoDatos_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
                return;
            }

            if (!creadoSinFinalizar)
            {
                return;
            }

            creadoSinFinalizar = false;
            if (Mensajes.Confirmar("El evento nuevo quedó sin finalizar. ¿Desea descartarlo?"))
            {
                try
                {
                    controlador.Eliminar(idEvento);
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }

        private void MarcarFinalizadoYCerrar()
        {
            creadoSinFinalizar = false;
            Close();
        }

        private void FrmEventoDatos_Load(object sender, EventArgs e)
        {
            cargando = true;
            CargarComboCliente();
            CargarComboLugar();

            if (eventoInicial != null)
            {
                Text = "Editar evento — Paso 1 de 2";
                CargarFormulario(eventoInicial);
            }
            else
            {
                Text = "Nuevo evento — Paso 1 de 2";
                cmbCliente.SelectedIndex = -1;
                cmbLugar.SelectedIndex = -1;
            }
            cargando = false;
            ActualizarInfoLugar();
            valoresGuardados = Formulario.Valores(tlpTarjetaDatos);
        }

        private void DatosDelLugar_Changed(object? sender, EventArgs e)
        {
            if (!cargando)
            {
                ActualizarInfoLugar();
            }
        }

        private void ActualizarInfoLugar()
        {
            if (cmbLugar.SelectedItem is not LugarDTO lugar)
            {
                lblInfoLugar.Text = "Elija un lugar para ver su capacidad y si está libre en ese horario.";
                lblInfoLugar.ForeColor = Tema.TextoSuave;
                return;
            }

            int libres = lugar.Capacidad - InvitadosCargados();
            bool disponible = controlador.LugarDisponible(lugar.Id, dtpFechaHora.Value, numHorasDuracion.Value, idEvento);

            lblInfoLugar.Text = TextoCapacidad(lugar) + Environment.NewLine + TextoDisponibilidad(disponible)
                + Environment.NewLine + TextoCosto(lugar);
            lblInfoLugar.ForeColor = libres < 0 || !disponible ? Tema.Peligro : Tema.TextoSuave;
        }

        private int InvitadosCargados()
        {
            return Convert.ToInt32(numCantidadAdultos.Value + numCantidadNinos.Value);
        }

        private string TextoCapacidad(LugarDTO lugar)
        {
            int invitados = InvitadosCargados();
            int libres = lugar.Capacidad - invitados;
            return "Capacidad: " + lugar.Capacidad + " · Invitados: " + invitados + " · " +
                (libres >= 0 ? "Quedan " + libres + " lugares." : "Supera la capacidad por " + (-libres) + ".");
        }

        private static string TextoDisponibilidad(bool disponible)
        {
            return disponible
                ? "El lugar está libre en ese horario."
                : "Atención: el lugar ya tiene otro evento en ese horario.";
        }

        private string TextoCosto(LugarDTO lugar)
        {
            decimal horas = numHorasDuracion.Value;
            string costo = "Costo del lugar: " + controlador.CalcularCostoLugar(lugar.Id, horas).ToString(Formato.Moneda);
            if (lugar.CostoPorHora > 0)
            {
                costo += " (" + lugar.CostoBase.ToString(Formato.Moneda) + " + " + horas.ToString("0.##") + " hs x "
                    + lugar.CostoPorHora.ToString(Formato.Moneda) + ")";
            }
            return costo;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (ValidarSeleccionCombos() && ConfirmarFechaPasada() && GuardarDatos())
            {
                AbrirPasoMenu();
            }
        }

        private bool GuardarDatos()
        {
            try
            {
                bool esNuevo = idEvento == 0;
                EventoResultadoDTO guardado = controlador.Guardar(ArmarDto());
                idEvento = guardado.Id;
                fechaGuardada = guardado.Fecha;
                valoresGuardados = Formulario.Valores(tlpTarjetaDatos);
                if (esNuevo)
                {
                    creadoSinFinalizar = true;
                }
                return true;
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
                return false;
            }
        }

        private void AbrirPasoMenu()
        {
            FrmEventoMenu pantallaMenu = new FrmEventoMenu(idEvento);
            pantallaMenu.Text += " — Paso 2 de 2";
            Navegacion.AbrirSubpantalla(this, pantallaMenu, () =>
            {
                if (pantallaMenu.Finalizado)
                {
                    MarcarFinalizadoYCerrar();
                }
            });
        }

        private void CargarComboCliente()
        {
            cmbCliente.DataSource = clienteControlador.ObtenerTodos();
            cmbCliente.DisplayMember = "Descripcion";
            cmbCliente.ValueMember = "Id";
        }

        private void CargarComboLugar()
        {
            cmbLugar.DataSource = lugarControlador.ObtenerTodos();
            cmbLugar.DisplayMember = "Descripcion";
            cmbLugar.ValueMember = "Id";
        }

        private bool ConfirmarFechaPasada()
        {
            DateTime fecha = dtpFechaHora.Value;
            if (fecha.Date >= DateTime.Today || fecha == fechaGuardada)
            {
                return true;
            }
            return Mensajes.Confirmar("La fecha elegida (" + fecha.ToString(Formato.FechaHora) +
                ") ya pasó. ¿Desea guardar el evento de todas formas?");
        }

        private bool ValidarSeleccionCombos()
        {
            string? faltante = FrmEventos.DescribirFaltanteParaEvento(cmbCliente.Items.Count > 0, cmbLugar.Items.Count > 0);
            if (faltante != null)
            {
                Mensajes.Advertencia(faltante);
                return false;
            }

            if (cmbCliente.SelectedValue == null)
            {
                Mensajes.Advertencia("Debe seleccionar un cliente.");
                return false;
            }

            if (cmbLugar.SelectedValue == null)
            {
                Mensajes.Advertencia("Debe seleccionar un lugar.");
                return false;
            }

            return true;
        }

        private EventoDTO ArmarDto()
        {
            EventoDTO dto = new EventoDTO();
            dto.Id = idEvento;
            dto.IdCliente = (int)cmbCliente.SelectedValue!;
            dto.IdLugar = (int)cmbLugar.SelectedValue!;
            dto.Fecha = dtpFechaHora.Value;
            dto.HorasDuracion = numHorasDuracion.Value;
            dto.CantidadAdultos = Convert.ToInt32(numCantidadAdultos.Value);
            dto.CantidadNinos = Convert.ToInt32(numCantidadNinos.Value);
            return dto;
        }

        private bool HayCambiosSinGuardar()
        {
            return Formulario.Valores(tlpTarjetaDatos) != valoresGuardados;
        }

        private void CargarFormulario(EventoResultadoDTO dto)
        {
            cmbCliente.SelectedValue = dto.IdCliente;
            cmbLugar.SelectedValue = dto.IdLugar;
            dtpFechaHora.Value = dto.Fecha;
            numHorasDuracion.Value = dto.HorasDuracion;
            numCantidadAdultos.Value = dto.CantidadAdultos;
            numCantidadNinos.Value = dto.CantidadNinos;
        }
    }
}
