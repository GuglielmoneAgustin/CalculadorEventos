using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    // Paso 1 del alta/edición de un evento: datos generales.
    // "Siguiente" guarda (crea o edita) el evento y abre el paso 2 (menú y servicios).
    public partial class FrmEventoDatos : Form
    {
        private readonly EventoController controlador;
        private readonly ClienteController clienteControlador;
        private readonly LugarController lugarControlador;
        private readonly EventoResultadoDTO? eventoInicial;
        private int idEvento;
        // El evento se creó en este asistente y todavía no se finalizó el paso 2
        private bool creadoSinFinalizar;
        // Fecha que tiene el evento guardado (null si todavía no existe)
        private DateTime? fechaGuardada;

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

        // Si el usuario sale sin finalizar un evento nuevo, se ofrece descartarlo
        // para que no quede un evento a medio cargar (sin menú ni presupuesto).
        private void FrmEventoDatos_FormClosing(object? sender, FormClosingEventArgs e)
        {
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
            CargarComboCliente();
            CargarComboLugar();

            if (eventoInicial == null)
            {
                Text = "Nuevo evento — Paso 1 de 2";
                cmbCliente.SelectedIndex = -1;
                cmbLugar.SelectedIndex = -1;
            }
            else
            {
                Text = "Editar evento — Paso 1 de 2";
                CargarFormulario(eventoInicial);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccionCombos() || !ConfirmarFechaPasada())
            {
                return;
            }

            try
            {
                bool esNuevo = idEvento == 0;
                EventoResultadoDTO guardado = controlador.Guardar(ArmarDto());
                idEvento = guardado.Id;
                fechaGuardada = guardado.Fecha;
                if (esNuevo)
                {
                    creadoSinFinalizar = true;
                }
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
                return;
            }

            FrmEventoMenu pantallaMenu = new FrmEventoMenu(idEvento);
            pantallaMenu.Text += " — Paso 2 de 2";
            if (ParentForm is FrmPrincipal principal)
            {
                // Se abre en el mismo panel; si el usuario finaliza, tambien se cierra este paso
                principal.AbrirSubpantalla(pantallaMenu, () =>
                {
                    if (pantallaMenu.Finalizado)
                    {
                        MarcarFinalizadoYCerrar();
                    }
                });
            }
            else
            {
                using (pantallaMenu)
                {
                    if (pantallaMenu.ShowDialog(this) == DialogResult.OK)
                    {
                        MarcarFinalizadoYCerrar();
                    }
                }
            }
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

        // Registrar un evento ya realizado es válido, pero suele ser un error de carga:
        // si se elige (o se cambia a) un día anterior a hoy, se pide confirmación.
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
