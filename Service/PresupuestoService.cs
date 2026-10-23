using System;
using System.Collections.Generic;
using ProyectoEventos.Common;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class PresupuestoService
    {
        private readonly IEventoRepository eventoRepositorio;

        public PresupuestoService()
            : this(EventoRepository.ObtenerInstancia())
        {
        }

        public PresupuestoService(IEventoRepository eventoRepositorio)
        {
            this.eventoRepositorio = eventoRepositorio;
        }

        public PresupuestoResultadoDTO? Generar(int idEvento)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            if (!evento.RequierePresupuestoNuevo)
            {
                return null;
            }

            return MapearAResultado(evento.GenerarPresupuesto(DateTime.Now));
        }

        public PresupuestoResultadoDTO Aprobar(int idEvento, int idPresupuesto, bool reemplazarAprobado)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            return MapearAResultado(evento.AprobarPresupuesto(idPresupuesto, reemplazarAprobado));
        }

        public PresupuestoResultadoDTO Cancelar(int idEvento, int idPresupuesto)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            return MapearAResultado(evento.CancelarPresupuesto(idPresupuesto));
        }

        public PresupuestoResultadoDTO RegistrarSena(int idEvento, decimal monto)
        {
            return MapearAResultado(eventoRepositorio.ObtenerExistente(idEvento).RegistrarSena(monto, DateTime.Now));
        }

        public PresupuestoResultadoDTO MarcarPagado(int idEvento)
        {
            return MapearAResultado(eventoRepositorio.ObtenerExistente(idEvento).MarcarPagado(DateTime.Now));
        }

        public PresupuestoResultadoDTO? ObtenerAprobado(int idEvento)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            return evento.TienePresupuestoAprobado() ? MapearAResultado(evento.ObtenerPresupuestoAprobadoExistente()) : null;
        }

        public PresupuestoDetalleDTO ObtenerDetalleAprobado(int idEvento)
        {
            return MapearADetalle(eventoRepositorio.ObtenerExistente(idEvento).ObtenerPresupuestoAprobadoExistente());
        }

        public void ValidarAprobacion(int idEvento, int idPresupuesto)
        {
            eventoRepositorio.ObtenerExistente(idEvento).ValidarAprobacion(idPresupuesto);
        }

        public PresupuestoDetalleDTO ObtenerDetalle(int idEvento, int idPresupuesto)
        {
            return MapearADetalle(ObtenerPresupuesto(idEvento, idPresupuesto));
        }

        private static PresupuestoDetalleDTO MapearADetalle(Presupuesto presupuesto)
        {
            PresupuestoDetalleDTO dto = new PresupuestoDetalleDTO();
            dto.Id = presupuesto.Id;
            dto.IdEvento = presupuesto.EventoAsociado.Id;
            dto.Estado = presupuesto.Estado;
            dto.FechaGeneracion = presupuesto.FechaGeneracion;
            dto.NombreCliente = presupuesto.NombreCliente;
            dto.NombreLugar = presupuesto.NombreLugar;
            dto.FechaEvento = presupuesto.FechaEvento;
            dto.HorasDuracion = presupuesto.HorasDuracion;
            dto.CantidadAdultos = presupuesto.CantidadAdultos;
            dto.CantidadNinos = presupuesto.CantidadNinos;
            dto.CostoBaseLugar = presupuesto.CostoBaseLugar;
            dto.CostoPorHoraLugar = presupuesto.CostoPorHoraLugar;
            dto.CostoLugar = presupuesto.CostoLugar;
            dto.Items = presupuesto.ObtenerItemsMenu().Select(item => new ItemMenuResultadoDTO
            {
                DescripcionComponentes = item.Descripcion,
                CantidadAdultos = item.CantidadAdultos,
                CantidadNinos = item.CantidadNinos,
                Subtotal = item.Subtotal
            }).ToList();
            dto.Servicios = presupuesto.ObtenerServicios().Select(servicio => new PresupuestoServicioDTO
            {
                Nombre = servicio.Nombre,
                Monto = servicio.Monto
            }).ToList();
            dto.SubtotalMenu = presupuesto.CalcularSubtotalMenu();
            dto.SubtotalServicios = presupuesto.CalcularSubtotalServicios();
            dto.CostoTotal = presupuesto.CalcularTotal();
            dto.EstadoPago = presupuesto.EstadoPago;
            dto.Sena = presupuesto.Sena;
            dto.FechaSena = presupuesto.FechaSena;
            dto.FechaPago = presupuesto.FechaPago;
            dto.Saldo = presupuesto.Saldo;
            return dto;
        }

        public List<PresupuestoResultadoDTO> ObtenerPorEvento(int idEvento)
        {
            return eventoRepositorio.ObtenerExistente(idEvento).ObtenerPresupuestos().Select(MapearAResultado).ToList();
        }

        private Presupuesto ObtenerPresupuesto(int idEvento, int idPresupuesto)
        {
            return eventoRepositorio.ObtenerExistente(idEvento).ObtenerPresupuestoExistente(idPresupuesto);
        }

        private PresupuestoResultadoDTO MapearAResultado(Presupuesto presupuesto)
        {
            PresupuestoResultadoDTO dto = new PresupuestoResultadoDTO();
            dto.Id = presupuesto.Id;
            dto.IdEvento = presupuesto.EventoAsociado.Id;
            dto.NombreCliente = presupuesto.NombreCliente;
            dto.NombreLugar = presupuesto.NombreLugar;
            dto.FechaGeneracion = presupuesto.FechaGeneracion;
            dto.FechaEvento = presupuesto.FechaEvento;
            dto.CantidadAdultos = presupuesto.CantidadAdultos;
            dto.CantidadNinos = presupuesto.CantidadNinos;
            dto.CostoTotal = presupuesto.CalcularTotal();
            dto.Estado = presupuesto.Estado;
            dto.EstadoPago = presupuesto.Estado == EstadoPresupuesto.Aprobado ? presupuesto.EstadoPago : null;
            dto.Sena = presupuesto.Sena;
            dto.FechaSena = presupuesto.FechaSena;
            dto.FechaPago = presupuesto.FechaPago;
            dto.Saldo = presupuesto.Saldo;
            dto.Vigente = presupuesto.EventoAsociado.EsVigente(presupuesto);
            dto.Detalle = presupuesto.Estado == EstadoPresupuesto.Cancelado && presupuesto.Sena > 0
                ? "Seña de $ " + presupuesto.Sena.ToString("#,##0.00") + " no reembolsable."
                : string.Join(" ", presupuesto.EventoAsociado.ObtenerMotivosDesactualizacion(presupuesto));
            return dto;
        }
    }
}
