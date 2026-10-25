using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Presupuesto
    {
        private readonly List<DetalleMenuPresupuestado> itemsMenu;
        private readonly List<ServicioPresupuestado> servicios;

        internal Presupuesto(int id, Evento eventoAsociado, DateTime fechaGeneracion, int diasValidez)
        {
            if (diasValidez < 0)
            {
                throw new NegocioException("Los días de validez del presupuesto no pueden ser negativos.");
            }

            Id = id;
            FechaGeneracion = fechaGeneracion;
            FechaVencimiento = diasValidez == 0 ? null : fechaGeneracion.Date.AddDays(diasValidez);
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
            Estado = EstadoPresupuesto.EnEspera;
        }

        public int Id { get; private set; }

        public DateTime FechaGeneracion { get; private set; }

        public DateTime? FechaVencimiento { get; private set; }

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

        public decimal Sena { get; private set; }

        public DateTime? FechaSena { get; private set; }

        public DateTime? FechaPago { get; private set; }

        public bool EstaPagado
        {
            get { return FechaPago != null; }
        }

        public decimal Saldo
        {
            get { return EstaPagado ? 0m : CalcularTotal() - Sena; }
        }

        public EstadoPago EstadoPago
        {
            get
            {
                if (EstaPagado)
                {
                    return EstadoPago.Pagado;
                }
                return Sena > 0 ? EstadoPago.Senado : EstadoPago.SinSena;
            }
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

        internal void RegistrarSena(decimal monto, DateTime fecha)
        {
            ValidarQuePuedeCobrarse();
            if (Sena > 0)
            {
                throw new NegocioException("El presupuesto ya tiene una seña registrada.");
            }
            if (monto <= 0)
            {
                throw new NegocioException("La seña debe ser mayor a cero.");
            }
            if (monto >= CalcularTotal())
            {
                throw new NegocioException("La seña debe ser menor al total del presupuesto (" + CalcularTotal().ToString(Formato.Moneda) +
                    "). Si el cliente paga todo, use Marcar como pagado.");
            }
            Sena = monto;
            FechaSena = fecha;
        }

        internal void MarcarPagado(DateTime fecha)
        {
            ValidarQuePuedeCobrarse();
            FechaPago = fecha;
        }

        internal void RecibirSenaDe(Presupuesto anterior)
        {
            if (anterior.Sena == 0)
            {
                return;
            }
            if (anterior.Sena >= CalcularTotal())
            {
                throw new NegocioException("La seña del presupuesto aprobado (" + anterior.Sena.ToString(Formato.Moneda) +
                    ") no es menor al total de este presupuesto.");
            }
            Sena = anterior.Sena;
            FechaSena = anterior.FechaSena;
            anterior.Sena = 0;
            anterior.FechaSena = null;
        }

        public bool EstaVencido(DateTime hoy)
        {
            return Estado != EstadoPresupuesto.Aprobado && FechaVencimiento != null && hoy.Date > FechaVencimiento.Value;
        }

        public int? DiasParaVencer(DateTime hoy)
        {
            if (FechaVencimiento == null)
            {
                return null;
            }
            return (FechaVencimiento.Value - hoy.Date).Days;
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
