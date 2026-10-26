using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Presupuesto
    {
        private readonly List<DetalleMenuPresupuestado> itemsMenu;
        private readonly List<ServicioPresupuestado> servicios;
        private readonly List<Pago> pagos;

        internal Presupuesto(int id, Evento eventoAsociado, DateTime fechaGeneracion)
        {
            Id = id;
            FechaGeneracion = fechaGeneracion;
            EventoAsociado = eventoAsociado;
            CostoBaseLugar = eventoAsociado.Lugar.CostoBase;
            CostoPorHoraLugar = eventoAsociado.Lugar.CostoPorHora;
            NombreCliente = eventoAsociado.Cliente.Nombre;
            NombreLugar = eventoAsociado.Lugar.Nombre;
            FechaEvento = eventoAsociado.Fecha;
            HorasDuracion = eventoAsociado.HorasDuracion;
            CantidadAdultos = eventoAsociado.CantidadAdultos;
            CantidadNinos = eventoAsociado.CantidadNinos;
            itemsMenu = CrearSnapshotMenu(eventoAsociado.Menu);
            servicios = CrearSnapshotServicios(eventoAsociado.ObtenerServicios());
            pagos = new List<Pago>();
            Estado = EstadoPresupuesto.EnEspera;
        }

        // Solo para reconstruir el presupuesto al leerlo de la base de datos: recibe la foto tal como se guardó.
        internal Presupuesto(int id, Evento eventoAsociado, DateTime fechaGeneracion, EstadoPresupuesto estado,
            decimal costoBaseLugar, decimal costoPorHoraLugar, string nombreCliente, string nombreLugar, DateTime fechaEvento,
            decimal horasDuracion, int cantidadAdultos, int cantidadNinos,
            List<DetalleMenuPresupuestado> itemsMenu, List<ServicioPresupuestado> servicios, List<Pago> pagos)
        {
            Id = id;
            EventoAsociado = eventoAsociado;
            FechaGeneracion = fechaGeneracion;
            Estado = estado;
            CostoBaseLugar = costoBaseLugar;
            CostoPorHoraLugar = costoPorHoraLugar;
            NombreCliente = nombreCliente;
            NombreLugar = nombreLugar;
            FechaEvento = fechaEvento;
            HorasDuracion = horasDuracion;
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
            this.itemsMenu = itemsMenu;
            this.servicios = servicios;
            this.pagos = pagos;
        }

        public int Id { get; private set; }

        public DateTime FechaGeneracion { get; private set; }

        public decimal CostoBaseLugar { get; private set; }

        public decimal CostoPorHoraLugar { get; private set; }

        public decimal CostoLugar
        {
            get { return CostoBaseLugar + CostoPorHoraLugar * HorasDuracion; }
        }

        public string NombreCliente { get; private set; }

        public string NombreLugar { get; private set; }

        public DateTime FechaEvento { get; private set; }

        public decimal HorasDuracion { get; private set; }

        public int CantidadAdultos { get; private set; }

        public int CantidadNinos { get; private set; }

        public Evento EventoAsociado { get; }

        public EstadoPresupuesto Estado { get; private set; }

        public decimal TotalPagado
        {
            get
            {
                decimal total = 0m;
                foreach (Pago pago in pagos)
                {
                    total += pago.Monto;
                }
                return total;
            }
        }

        public bool EstaPagado
        {
            get { return pagos.Count > 0 && TotalPagado >= CalcularTotal(); }
        }

        // Fecha del pago con el que se completó el total.
        public DateTime? FechaPago
        {
            get { return EstaPagado ? pagos[pagos.Count - 1].Fecha : null; }
        }

        public decimal Saldo
        {
            get { return CalcularTotal() - TotalPagado; }
        }

        public EstadoPago EstadoPago
        {
            get
            {
                if (EstaPagado)
                {
                    return EstadoPago.Pagado;
                }
                return TotalPagado > 0 ? EstadoPago.Senado : EstadoPago.SinSena;
            }
        }

        // Aprobado y con lo pagado llegando al porcentaje de seña: los cambios de precios ya no lo desactualizan.
        public bool TienePrecioCongelado
        {
            get
            {
                return Estado == EstadoPresupuesto.Aprobado && TotalPagado > 0 && TotalPagado >= CalcularSenaParaCongelarPrecio();
            }
        }

        public IReadOnlyList<Pago> ObtenerPagos()
        {
            return pagos.AsReadOnly();
        }

        public decimal CalcularSenaParaCongelarPrecio()
        {
            return Math.Round(CalcularTotal() * Configuracion.PorcentajeSenaParaCongelarPrecio / 100, 2);
        }

        public decimal CalcularFaltanteParaCongelarPrecio()
        {
            decimal faltante = CalcularSenaParaCongelarPrecio() - TotalPagado;
            return faltante > 0 ? faltante : 0;
        }

        public IReadOnlyList<DetalleMenuPresupuestado> ObtenerItemsMenu()
        {
            return itemsMenu.AsReadOnly();
        }

        public IReadOnlyList<ServicioPresupuestado> ObtenerServicios()
        {
            return servicios.AsReadOnly();
        }

        public decimal CalcularSubtotalServicios()
        {
            decimal total = 0m;
            foreach (ServicioPresupuestado servicio in servicios)
            {
                total = total + servicio.Monto;
            }
            return total;
        }

        public decimal CalcularTotal()
        {
            return CostoLugar + CalcularSubtotalMenu() + CalcularSubtotalServicios();
        }

        public decimal CalcularSubtotalMenu()
        {
            decimal total = 0m;
            foreach (DetalleMenuPresupuestado item in itemsMenu)
            {
                total += item.Subtotal;
            }
            return total;
        }

        internal void ValidarQuePuedeAprobarse()
        {
            if (Estado == EstadoPresupuesto.Aprobado)
            {
                throw new NegocioException("El presupuesto ya está aprobado.");
            }
        }

        internal bool CoincidenPreciosCon(Evento evento)
        {
            return CostoBaseLugar == evento.Lugar.CostoBase
                && CostoPorHoraLugar == evento.Lugar.CostoPorHora
                && CalcularSubtotalMenu() == evento.Menu.CalcularSubtotal()
                && CalcularSubtotalServicios() == evento.CalcularSubtotalServicios();
        }

        internal List<string> ObtenerDiferenciasCon(Evento evento)
        {
            List<string> diferencias = new List<string>();
            AgregarDiferenciasDelEvento(evento, diferencias);
            AgregarDiferenciasDelMenu(evento, diferencias);
            AgregarDiferenciasDeServicios(evento, diferencias);
            return diferencias;
        }

        private void AgregarDiferenciasDelEvento(Evento evento, List<string> diferencias)
        {
            if (NombreLugar != evento.Lugar.Nombre)
            {
                diferencias.Add("Cambió el lugar.");
            }
            else if (CostoBaseLugar != evento.Lugar.CostoBase || CostoPorHoraLugar != evento.Lugar.CostoPorHora)
            {
                diferencias.Add("Cambió el costo del lugar.");
            }
            if (FechaEvento != evento.Fecha || HorasDuracion != evento.HorasDuracion)
            {
                diferencias.Add("Cambió la fecha o la duración.");
            }
            if (CantidadAdultos != evento.CantidadAdultos || CantidadNinos != evento.CantidadNinos)
            {
                diferencias.Add("Cambió la cantidad de invitados.");
            }
        }

        private void AgregarDiferenciasDelMenu(Evento evento, List<string> diferencias)
        {
            IReadOnlyList<ItemMenu> actuales = evento.Menu.ObtenerItems();

            bool mismosPlatos = itemsMenu.Count == actuales.Count;
            for (int i = 0; mismosPlatos && i < itemsMenu.Count; i++)
            {
                mismosPlatos = itemsMenu[i].Descripcion == actuales[i].ObtenerDescripcion()
                    && itemsMenu[i].CantidadAdultos == actuales[i].CantidadAdultos
                    && itemsMenu[i].CantidadNinos == actuales[i].CantidadNinos;
            }
            if (!mismosPlatos)
            {
                diferencias.Add("Cambió el menú.");
                return;
            }

            for (int i = 0; i < itemsMenu.Count; i++)
            {
                if (itemsMenu[i].Subtotal != actuales[i].Subtotal())
                {
                    diferencias.Add("Cambió el precio de " + itemsMenu[i].Descripcion + ".");
                }
            }
        }

        private void AgregarDiferenciasDeServicios(Evento evento, List<string> diferencias)
        {
            IReadOnlyList<ServicioAdicional> actuales = evento.ObtenerServicios();

            bool mismosServicios = servicios.Count == actuales.Count
                && servicios.All(cotizado => actuales.Any(s => s.Nombre == cotizado.Nombre));
            if (!mismosServicios)
            {
                diferencias.Add("Cambiaron los servicios adicionales.");
                return;
            }

            foreach (ServicioPresupuestado cotizado in servicios)
            {
                ServicioAdicional? actual = actuales.FirstOrDefault(s => s.Nombre == cotizado.Nombre);
                if (actual != null && actual.Monto != cotizado.Monto)
                {
                    diferencias.Add("Cambió el monto de " + cotizado.Nombre + ".");
                }
            }
        }

        internal void Aprobar()
        {
            ValidarQuePuedeAprobarse();
            Estado = EstadoPresupuesto.Aprobado;
        }

        internal void Cancelar()
        {
            ValidarQuePuedeCancelarse();
            Estado = EstadoPresupuesto.Cancelado;
        }

        internal void ValidarQuePuedeCancelarse()
        {
            if (Estado == EstadoPresupuesto.Cancelado)
            {
                throw new NegocioException("El presupuesto ya está cancelado.");
            }
            if (EstaPagado)
            {
                throw new NegocioException("El presupuesto N° " + Id + " ya está pagado: no se puede cancelar ni reemplazar.");
            }
        }

        internal void RegistrarPago(decimal monto, DateTime fecha)
        {
            ValidarQuePuedeCobrarse();
            Pago pago = new Pago(fecha, monto);
            if (monto > Saldo)
            {
                throw new NegocioException("El pago no puede superar lo que falta cobrar (" + Saldo.ToString(Formato.Moneda) + ").");
            }
            pagos.Add(pago);
        }

        internal void MarcarPagado(DateTime fecha)
        {
            ValidarQuePuedeCobrarse();
            pagos.Add(new Pago(fecha, Saldo));
        }

        internal void RecibirPagosDe(Presupuesto anterior)
        {
            if (anterior.pagos.Count == 0)
            {
                return;
            }
            if (anterior.TotalPagado >= CalcularTotal())
            {
                throw new NegocioException("Lo pagado en el presupuesto aprobado (" + anterior.TotalPagado.ToString(Formato.Moneda) +
                    ") no es menor al total de este presupuesto.");
            }
            pagos.AddRange(anterior.pagos);
            anterior.pagos.Clear();
        }

        private void ValidarQuePuedeCobrarse()
        {
            if (Estado != EstadoPresupuesto.Aprobado)
            {
                throw new NegocioException("Solo se puede cobrar un presupuesto aprobado.");
            }
            if (EstaPagado)
            {
                throw new NegocioException("El presupuesto ya está pagado.");
            }
        }

        private static List<DetalleMenuPresupuestado> CrearSnapshotMenu(MenuEvento menu)
        {
            List<DetalleMenuPresupuestado> snapshot = new List<DetalleMenuPresupuestado>();
            foreach (ItemMenu item in menu.ObtenerItems())
            {
                snapshot.Add(new DetalleMenuPresupuestado(item.ObtenerDescripcion(), item.CantidadAdultos, item.CantidadNinos, item.Subtotal()));
            }
            return snapshot;
        }

        private static List<ServicioPresupuestado> CrearSnapshotServicios(IReadOnlyList<ServicioAdicional> serviciosEvento)
        {
            List<ServicioPresupuestado> snapshot = new List<ServicioPresupuestado>();
            foreach (ServicioAdicional servicio in serviciosEvento)
            {
                snapshot.Add(new ServicioPresupuestado(servicio.Nombre, servicio.Monto));
            }
            return snapshot;
        }
    }
}
