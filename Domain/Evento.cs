using System.Diagnostics.CodeAnalysis;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Evento : IEntidad
    {
        private DateTime fecha;
        private decimal horasDuracion;
        private int cantidadAdultos;
        private int cantidadNinos;
        private Cliente cliente;
        private Lugar lugar;
        private readonly MenuEvento menu;
        private readonly List<ServicioAdicional> servicios;
        private readonly List<Presupuesto> presupuestos;
        private int proximoIdPresupuesto;
        private bool huboCambiosDesdeUltimoPresupuesto;

        public Evento(int id, Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            menu = new MenuEvento();
            servicios = new List<ServicioAdicional>();
            presupuestos = new List<Presupuesto>();
            proximoIdPresupuesto = 1;
            ActualizarDatos(cliente, lugar, fecha, horasDuracion, cantidadAdultos, cantidadNinos);
        }

        public int Id { get; private set; }

        public Cliente Cliente
        {
            get { return cliente; }
        }

        public Lugar Lugar
        {
            get { return lugar; }
        }

        public MenuEvento Menu
        {
            get { return menu; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
        }

        public decimal HorasDuracion
        {
            get { return horasDuracion; }
        }

        public int CantidadAdultos
        {
            get { return cantidadAdultos; }
        }

        public int CantidadNinos
        {
            get { return cantidadNinos; }
        }

        public DateTime FechaFin
        {
            get { return Fecha.AddHours((double)HorasDuracion); }
        }

        public bool RequierePresupuestoNuevo
        {
            get
            {
                Presupuesto? ultimo = ObtenerUltimoPresupuesto();
                return ultimo == null || !EsVigente(ultimo);
            }
        }

        [MemberNotNull(nameof(cliente), nameof(lugar))]
        public void ActualizarDatos(Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            if (cliente == null)
            {
                throw new ArgumentException("El evento debe tener un cliente asignado.");
            }
            if (lugar == null)
            {
                throw new ArgumentException("El evento debe tener un lugar asignado.");
            }
            ValidarHorasDuracion(horasDuracion);
            ValidarCantidad(cantidadAdultos, "adultos");
            ValidarCantidad(cantidadNinos, "niños");
            ValidarHayInvitados(cantidadAdultos, cantidadNinos);
            ValidarCapacidad(lugar, cantidadAdultos, cantidadNinos);

            bool huboCambios = HayCambios(cliente, lugar, fecha, horasDuracion, cantidadAdultos, cantidadNinos);

            this.cliente = cliente;
            this.lugar = lugar;
            this.fecha = fecha;
            this.horasDuracion = horasDuracion;
            this.cantidadAdultos = cantidadAdultos;
            this.cantidadNinos = cantidadNinos;

            if (huboCambios)
            {
                MarcarCambioPendienteDePresupuesto();
            }
        }

        private bool HayCambios(Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            return this.cliente == null || this.lugar == null
                || this.cliente.Id != cliente.Id
                || this.lugar.Id != lugar.Id
                || this.fecha != fecha
                || this.horasDuracion != horasDuracion
                || this.cantidadAdultos != cantidadAdultos
                || this.cantidadNinos != cantidadNinos;
        }

        public int TotalInvitados()
        {
            return CantidadAdultos + CantidadNinos;
        }

        public bool SeSuperponeCon(DateTime inicio, decimal horasDuracion)
        {
            DateTime fin = inicio.AddHours((double)horasDuracion);
            return inicio < FechaFin && Fecha < fin;
        }

        public bool TieneCliente(int idCliente)
        {
            return Cliente.Id == idCliente;
        }

        public bool TieneLugar(int idLugar)
        {
            return Lugar.Id == idLugar;
        }

        public bool UsaProducto(int idProducto)
        {
            return Menu.UsaProducto(idProducto);
        }

        public bool TieneServicio(int idServicio)
        {
            return servicios.Exists(s => s.Id == idServicio);
        }

        public bool TienePresupuestoAprobado()
        {
            return ObtenerPresupuestoAprobado() != null;
        }

        public EstadoPresupuesto? ObtenerEstadoPresupuesto()
        {
            if (TienePresupuestoAprobado())
            {
                return EstadoPresupuesto.Aprobado;
            }
            if (ObtenerPresupuestoEnEspera() != null)
            {
                return EstadoPresupuesto.EnEspera;
            }
            if (presupuestos.Count > 0)
            {
                return EstadoPresupuesto.Cancelado;
            }
            return null;
        }

        public TipoPendiente? ObtenerTipoPendiente(DateTime ahora)
        {
            EstadoPresupuesto? estado = ObtenerEstadoPresupuesto();
            if (estado == null)
            {
                return TipoPendiente.SinPresupuesto;
            }
            if (estado == EstadoPresupuesto.Aprobado)
            {
                return ObtenerEstadoPago() != EstadoPago.Pagado && Fecha.Date <= ahora.Date.AddDays(Configuracion.DiasProximos)
                    ? TipoPendiente.FaltaCobrar
                    : null;
            }
            if (estado != EstadoPresupuesto.EnEspera)
            {
                return null;
            }
            return RequierePresupuestoNuevo ? TipoPendiente.Desactualizado : TipoPendiente.EsperandoRespuesta;
        }

        public bool EsProximoAprobado(DateTime ahora)
        {
            return Fecha >= ahora && TienePresupuestoAprobado();
        }

        public void AgregarPlatosAlMenu(int cantidadAdultos, int cantidadNinos, List<Producto> productos)
        {
            Menu.AgregarPlatos(cantidadAdultos, cantidadNinos, productos);
            MarcarCambioPendienteDePresupuesto();
        }

        public void CambiarCantidadesItem(int idItem, int cantidadAdultos, int cantidadNinos)
        {
            if (Menu.CambiarCantidades(idItem, cantidadAdultos, cantidadNinos))
            {
                MarcarCambioPendienteDePresupuesto();
            }
        }

        public void QuitarItemDelMenu(int idItem)
        {
            Menu.QuitarItem(idItem);
            MarcarCambioPendienteDePresupuesto();
        }

        public void AgregarServicio(ServicioAdicional servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentException("El servicio no puede ser nulo.");
            }
            if (servicios.Exists(s => s.Id == servicio.Id))
            {
                throw new NegocioException("El servicio ya está asociado al evento.");
            }
            servicios.Add(servicio);
            MarcarCambioPendienteDePresupuesto();
        }

        public void QuitarServicio(ServicioAdicional servicio)
        {
            if (!servicios.Remove(servicio))
            {
                throw new NoEncontradoException("El servicio no está asociado al evento.");
            }
            MarcarCambioPendienteDePresupuesto();
        }

        public IReadOnlyList<ServicioAdicional> ObtenerServicios()
        {
            return servicios.AsReadOnly();
        }

        public Presupuesto GenerarPresupuesto(DateTime fechaGeneracion)
        {
            if (Menu.EstaVacio())
            {
                throw new NegocioException("Debe agregar al menos un item al menú antes de generar un presupuesto.");
            }

            Presupuesto presupuesto = new Presupuesto(proximoIdPresupuesto, this, fechaGeneracion);
            proximoIdPresupuesto++;
            presupuestos.Add(presupuesto);
            huboCambiosDesdeUltimoPresupuesto = false;
            return presupuesto;
        }

        public Presupuesto AprobarPresupuesto(int idPresupuesto, bool reemplazarAprobado)
        {
            Presupuesto presupuesto = ValidarAprobacion(idPresupuesto);

            Presupuesto? otroAprobado = ObtenerPresupuestoAprobado();
            if (otroAprobado != null && !reemplazarAprobado)
            {
                throw new NegocioException("Ya existe un presupuesto aprobado para este evento.");
            }

            if (otroAprobado != null)
            {
                otroAprobado.ValidarQuePuedeCancelarse();
                presupuesto.RecibirSenaDe(otroAprobado);
            }
            presupuesto.Aprobar();
            otroAprobado?.Cancelar();
            return presupuesto;
        }

        public Presupuesto RegistrarSena(decimal monto, DateTime fecha)
        {
            Presupuesto aprobado = ObtenerPresupuestoAprobadoExistente();
            aprobado.RegistrarSena(monto, fecha);
            return aprobado;
        }

        public Presupuesto MarcarPagado(DateTime fecha)
        {
            Presupuesto aprobado = ObtenerPresupuestoAprobadoExistente();
            aprobado.MarcarPagado(fecha);
            return aprobado;
        }

        public EstadoPago? ObtenerEstadoPago()
        {
            return ObtenerPresupuestoAprobado()?.EstadoPago;
        }

        public Presupuesto ObtenerPresupuestoAprobadoExistente()
        {
            Presupuesto? aprobado = ObtenerPresupuestoAprobado();
            if (aprobado == null)
            {
                throw new NegocioException("El evento no tiene un presupuesto aprobado. Apruebe uno antes de registrar la seña, el pago o ver el comprobante.");
            }
            return aprobado;
        }

        public Presupuesto ValidarAprobacion(int idPresupuesto)
        {
            Presupuesto presupuesto = ObtenerPresupuestoExistente(idPresupuesto);
            presupuesto.ValidarQuePuedeAprobarse();

            if (!EsVigente(presupuesto))
            {
                throw new NegocioException("El presupuesto está desactualizado. " +
                    string.Join(" ", ObtenerMotivosDesactualizacion(presupuesto)) +
                    " Actualice el presupuesto para obtener uno con los valores actuales.");
            }
            return presupuesto;
        }

        public List<string> ObtenerMotivosDesactualizacion()
        {
            Presupuesto? ultimo = ObtenerUltimoPresupuesto();
            return ultimo == null ? new List<string>() : ObtenerMotivosDesactualizacion(ultimo);
        }

        public List<string> ObtenerMotivosDesactualizacion(Presupuesto presupuesto)
        {
            List<string> motivos = new List<string>();
            if (EsVigente(presupuesto))
            {
                return motivos;
            }

            Presupuesto? ultimo = ObtenerUltimoPresupuesto();
            if (ultimo != null && ultimo != presupuesto)
            {
                motivos.Add("Hay un presupuesto más nuevo (N° " + ultimo.Id + ").");
                return motivos;
            }

            motivos.AddRange(presupuesto.ObtenerDiferenciasCon(this));
            if (motivos.Count == 0)
            {
                motivos.Add("El evento se modificó después de generarlo.");
            }
            return motivos;
        }

        public Presupuesto CancelarPresupuesto(int idPresupuesto)
        {
            Presupuesto presupuesto = ObtenerPresupuestoExistente(idPresupuesto);
            presupuesto.Cancelar();
            return presupuesto;
        }

        public Presupuesto ObtenerPresupuestoExistente(int idPresupuesto)
        {
            Presupuesto? presupuesto = presupuestos.Find(p => p.Id == idPresupuesto);
            if (presupuesto == null)
            {
                throw new NoEncontradoException("El presupuesto seleccionado no existe.");
            }
            return presupuesto;
        }

        public IReadOnlyList<Presupuesto> ObtenerPresupuestos()
        {
            return presupuestos.AsReadOnly();
        }

        private Presupuesto? ObtenerPresupuestoAprobado()
        {
            return presupuestos.Find(presupuesto => presupuesto.Estado == EstadoPresupuesto.Aprobado);
        }

        public bool EsVigente(Presupuesto presupuesto)
        {
            return presupuesto == ObtenerUltimoPresupuesto()
                && !huboCambiosDesdeUltimoPresupuesto
                && presupuesto.CoincidenPreciosCon(this);
        }

        public decimal CalcularSubtotalServicios()
        {
            decimal total = 0m;
            foreach (ServicioAdicional servicio in servicios)
            {
                total += servicio.Monto;
            }
            return total;
        }

        public Presupuesto? ObtenerPresupuestoEnEspera()
        {
            return presupuestos.FindLast(presupuesto => presupuesto.Estado == EstadoPresupuesto.EnEspera);
        }

        public Presupuesto? ObtenerPresupuestoDeReferencia()
        {
            return ObtenerPresupuestoAprobado() ?? ObtenerUltimoPresupuesto();
        }

        private Presupuesto? ObtenerUltimoPresupuesto()
        {
            return presupuestos.Count == 0 ? null : presupuestos[presupuestos.Count - 1];
        }

        private void MarcarCambioPendienteDePresupuesto()
        {
            huboCambiosDesdeUltimoPresupuesto = true;
        }

        private static void ValidarHorasDuracion(decimal valor)
        {
            if (valor <= 0)
            {
                throw new NegocioException("La duración debe ser mayor a cero.");
            }
        }

        private static void ValidarCantidad(int valor, string tipoInvitado)
        {
            if (valor < 0)
            {
                throw new NegocioException("La cantidad de " + tipoInvitado + " no puede ser negativa.");
            }
        }

        private static void ValidarHayInvitados(int cantidadAdultos, int cantidadNinos)
        {
            if (cantidadAdultos + cantidadNinos == 0)
            {
                throw new NegocioException("El evento debe tener al menos un invitado.");
            }
        }

        private static void ValidarCapacidad(Lugar lugar, int cantidadAdultos, int cantidadNinos)
        {
            if (!lugar.AdmiteInvitados(cantidadAdultos + cantidadNinos))
            {
                throw new NegocioException("La cantidad de invitados supera la capacidad del lugar.");
            }
        }
    }
}
