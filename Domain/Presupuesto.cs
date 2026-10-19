using System;
using System.Collections.Generic;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Presupuesto
    {
        private readonly List<DetalleMenuPresupuestado> itemsMenu;
        private readonly List<ServicioPresupuestado> servicios;

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
            Estado = EstadoPresupuesto.EnEspera;
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

        internal void Aprobar()
        {
            ValidarQuePuedeAprobarse();
            Estado = EstadoPresupuesto.Aprobado;
        }

        internal void Cancelar()
        {
            if (Estado == EstadoPresupuesto.Cancelado)
            {
                throw new NegocioException("El presupuesto ya está cancelado.");
            }
            Estado = EstadoPresupuesto.Cancelado;
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
