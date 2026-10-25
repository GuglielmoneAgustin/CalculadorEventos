using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Repository.Memoria;

namespace ProyectoEventos.Service
{
    public class PresupuestoService
    {
        private readonly IRepository<Evento> eventoRepositorio;
        private readonly NegocioService negocioServicio;

        public PresupuestoService()
            : this(EventoRepository.ObtenerInstancia(), new NegocioService())
        {
        }

        public PresupuestoService(IRepository<Evento> eventoRepositorio, NegocioService negocioServicio)
        {
            this.eventoRepositorio = eventoRepositorio;
            this.negocioServicio = negocioServicio;
        }

        public PresupuestoResultadoDTO? Generar(int idEvento)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            if (!evento.RequierePresupuestoNuevo)
            {
                return null;
            }

            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now, negocioServicio.ObtenerDiasValidez());
            eventoRepositorio.Actualizar(evento);
            return MapearAResultado(presupuesto);
        }

        public PresupuestoResultadoDTO Aprobar(int idEvento, int idPresupuesto, bool reemplazarAprobado, bool aprobarVencido)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            Presupuesto presupuesto = evento.AprobarPresupuesto(idPresupuesto, reemplazarAprobado, aprobarVencido, DateTime.Today);
            eventoRepositorio.Actualizar(evento);
            return MapearAResultado(presupuesto);
        }

        public PresupuestoResultadoDTO Cancelar(int idEvento, int idPresupuesto)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            Presupuesto presupuesto = evento.CancelarPresupuesto(idPresupuesto);
            eventoRepositorio.Actualizar(evento);
            return MapearAResultado(presupuesto);
        }

        public PresupuestoResultadoDTO RegistrarSena(int idEvento, decimal monto)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            Presupuesto presupuesto = evento.RegistrarSena(monto, DateTime.Now);
            eventoRepositorio.Actualizar(evento);
            return MapearAResultado(presupuesto);
        }

        public PresupuestoResultadoDTO MarcarPagado(int idEvento)
        {
            Evento evento = eventoRepositorio.ObtenerExistente(idEvento);
            Presupuesto presupuesto = evento.MarcarPagado(DateTime.Now);
            eventoRepositorio.Actualizar(evento);
            return MapearAResultado(presupuesto);
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
            dto.FechaVencimiento = presupuesto.FechaVencimiento;
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
                Producto = item.Descripcion,
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
            dto.FechaVencimiento = presupuesto.FechaVencimiento;
            dto.Vencido = presupuesto.EstaVencido(DateTime.Today);
            dto.SenaSugerida = presupuesto.EstadoPago == EstadoPago.SinSena && presupuesto.Estado == EstadoPresupuesto.Aprobado
                ? negocioServicio.CalcularSenaSugerida(presupuesto.CalcularTotal())
                : 0;
            dto.Detalle = ArmarDetalle(presupuesto, dto.Vigente);
            return dto;
        }

        private static string ArmarDetalle(Presupuesto presupuesto, bool vigente)
        {
            if (presupuesto.Estado == EstadoPresupuesto.Cancelado && presupuesto.Sena > 0)
            {
                return "Seña de " + presupuesto.Sena.ToString(Formato.Moneda) + " no reembolsable.";
            }
            if (!vigente)
            {
                return string.Join(" ", presupuesto.EventoAsociado.ObtenerMotivosDesactualizacion(presupuesto));
            }
            if (presupuesto.Estado == EstadoPresupuesto.EnEspera && presupuesto.FechaVencimiento != null)
            {
                return Formato.Vencimiento(presupuesto.FechaVencimiento.Value, presupuesto.DiasParaVencer(DateTime.Today)!.Value);
            }
            return string.Empty;
        }
    }
}
