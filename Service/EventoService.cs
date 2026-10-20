using System;
using System.Collections.Generic;
using ProyectoEventos.Common;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Service
{
    public class EventoService
    {
        private readonly IEventoRepository repositorio;
        private readonly IRepository<Cliente> clienteRepositorio;
        private readonly IRepository<Lugar> lugarRepositorio;
        private readonly IRepository<Producto> productoRepositorio;
        private readonly IRepository<ServicioAdicional> servicioRepositorio;

        public EventoService()
            : this(EventoRepository.ObtenerInstancia(), ClienteRepository.ObtenerInstancia(), LugarRepository.ObtenerInstancia(),
                ProductoRepository.ObtenerInstancia(), ServicioAdicionalRepository.ObtenerInstancia())
        {
        }

        public EventoService(IEventoRepository repositorio, IRepository<Cliente> clienteRepositorio, IRepository<Lugar> lugarRepositorio,
            IRepository<Producto> productoRepositorio, IRepository<ServicioAdicional> servicioRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.lugarRepositorio = lugarRepositorio;
            this.productoRepositorio = productoRepositorio;
            this.servicioRepositorio = servicioRepositorio;
        }

        public EventoResultadoDTO Guardar(EventoDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del evento son obligatorios.");
            }

            Cliente cliente = clienteRepositorio.ObtenerExistente(dto.IdCliente);
            Lugar lugar = lugarRepositorio.ObtenerExistente(dto.IdLugar);
            if (repositorio.HayEventoEnLugarEnHorario(lugar.Id, dto.Fecha, dto.HorasDuracion, dto.Id))
            {
                throw new NegocioException("El lugar ya tiene un evento en ese horario.");
            }

            Evento evento = dto.Id == 0
                ? CrearEvento(cliente, lugar, dto)
                : EditarEvento(dto.Id, cliente, lugar, dto);
            return MapearAResultado(evento);
        }

        public void Eliminar(int id)
        {
            Evento evento = repositorio.ObtenerExistente(id);
            if (evento.TienePresupuestoAprobado())
            {
                throw new NegocioException("No se puede eliminar un evento con un presupuesto aprobado. Cancele primero ese presupuesto.");
            }

            repositorio.Eliminar(id);
        }

        public EventoResultadoDTO ObtenerPorIdComoDto(int id)
        {
            return MapearAResultado(repositorio.ObtenerExistente(id));
        }

        public List<EventoResultadoDTO> ObtenerTodosComoDto()
        {
            return repositorio.ObtenerTodos().Select(MapearAResultado).ToList();
        }

        public List<EventoResultadoDTO> ObtenerProximosAprobadosComoDto()
        {
            DateTime ahora = DateTime.Now;

            return repositorio.ObtenerTodos()
                .Where(evento => evento.EsProximoAprobado(ahora))
                .OrderBy(evento => evento.Fecha)
                .Select(MapearAResultado)
                .ToList();
        }

        public List<PendienteDTO> ObtenerPendientes()
        {
            DateTime ahora = DateTime.Now;
            List<PendienteDTO> pendientes = new List<PendienteDTO>();

            foreach (Evento evento in repositorio.ObtenerTodos().Where(e => e.Fecha >= ahora).OrderBy(e => e.Fecha))
            {
                TipoPendiente? tipo = evento.ObtenerTipoPendiente();
                if (tipo != null)
                {
                    pendientes.Add(new PendienteDTO
                    {
                        IdEvento = evento.Id,
                        NombreCliente = evento.Cliente.Nombre,
                        FechaEvento = evento.Fecha,
                        Tipo = tipo.Value,
                        Detalle = ArmarDetalle(evento, tipo.Value)
                    });
                }
            }

            return pendientes;
        }

        private static string ArmarDetalle(Evento evento, TipoPendiente tipo)
        {
            switch (tipo)
            {
                case TipoPendiente.SinPresupuesto:
                    return "Genere el presupuesto desde Eventos > Ver presupuestos.";
                case TipoPendiente.Desactualizado:
                    return string.Join(" ", evento.ObtenerMotivosDesactualizacion());
                default:
                    Presupuesto enEspera = evento.ObtenerPresupuestos().Last(p => p.Estado == EstadoPresupuesto.EnEspera);
                    return "Presupuesto N° " + enEspera.Id + " enviado " + DescribirAntiguedad(enEspera.FechaGeneracion) + ".";
            }
        }

        public List<EventoResultadoDTO> ObtenerSinAprobarProximosDias(int dias)
        {
            DateTime ahora = DateTime.Now;
            DateTime limite = DateTime.Today.AddDays(dias + 1);

            return repositorio.ObtenerTodos()
                .Where(evento => evento.Fecha >= ahora && evento.Fecha < limite && !evento.TienePresupuestoAprobado())
                .OrderBy(evento => evento.Fecha)
                .Select(MapearAResultado)
                .ToList();
        }

        private static string DescribirAntiguedad(DateTime fecha)
        {
            int dias = (DateTime.Today - fecha.Date).Days;
            if (dias <= 0)
            {
                return "hoy";
            }
            return dias == 1 ? "ayer" : "hace " + dias + " días";
        }

        public void AgregarPlatosAlMenu(int idEvento, int cantidadAdultos, int cantidadNinos, List<int> idsProductos)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            if (idsProductos == null)
            {
                throw new ArgumentException("Los productos del plato son obligatorios.");
            }

            List<Producto> productos = new List<Producto>();
            foreach (int idProducto in idsProductos)
            {
                productos.Add(productoRepositorio.ObtenerExistente(idProducto));
            }

            evento.AgregarPlatosAlMenu(cantidadAdultos, cantidadNinos, productos);
        }

        public void CambiarCantidadesItem(int idEvento, int idItem, int cantidadAdultos, int cantidadNinos)
        {
            repositorio.ObtenerExistente(idEvento).CambiarCantidadesItem(idItem, cantidadAdultos, cantidadNinos);
        }

        public void QuitarItemDelMenu(int idEvento, int idItem)
        {
            repositorio.ObtenerExistente(idEvento).QuitarItemDelMenu(idItem);
        }

        public List<ItemMenuResultadoDTO> ObtenerItemsMenuComoDto(int idEvento)
        {
            return repositorio.ObtenerExistente(idEvento).Menu.ObtenerItems().Select(item => new ItemMenuResultadoDTO
            {
                Id = item.Id,
                DescripcionComponentes = item.ObtenerDescripcion(),
                CantidadAdultos = item.CantidadAdultos,
                CantidadNinos = item.CantidadNinos,
                Subtotal = item.Subtotal()
            }).ToList();
        }

        public decimal ObtenerTotalMenu(int idEvento)
        {
            return repositorio.ObtenerExistente(idEvento).Menu.CalcularSubtotal();
        }

        public void CopiarMenuYServicios(int idEventoOrigen, int idEventoDestino)
        {
            Evento origen = repositorio.ObtenerExistente(idEventoOrigen);
            Evento destino = repositorio.ObtenerExistente(idEventoDestino);
            destino.CopiarMenuYServiciosDe(origen);
        }

        public decimal CalcularCostoLugar(int idLugar, decimal horasDuracion)
        {
            return lugarRepositorio.ObtenerExistente(idLugar).CalcularCosto(horasDuracion);
        }

        public bool LugarDisponible(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar)
        {
            return !repositorio.HayEventoEnLugarEnHorario(idLugar, fecha, horasDuracion, idEventoAIgnorar);
        }

        public void AgregarServicioAlEvento(int idEvento, int idServicio)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            ServicioAdicional servicio = servicioRepositorio.ObtenerExistente(idServicio);
            evento.AgregarServicio(servicio);
        }

        public void QuitarServicioDelEvento(int idEvento, int idServicio)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            ServicioAdicional servicio = servicioRepositorio.ObtenerExistente(idServicio);
            evento.QuitarServicio(servicio);
        }

        public List<ServicioAdicionalDTO> ObtenerServiciosEventoComoDto(int idEvento)
        {
            return repositorio.ObtenerExistente(idEvento).ObtenerServicios()
                .Select(ServicioAdicionalService.MapearADto).ToList();
        }

        private Evento CrearEvento(Cliente cliente, Lugar lugar, EventoDTO dto)
        {
            int nuevoId = repositorio.ObtenerProximoId();
            Evento nuevoEvento = new Evento(nuevoId, cliente, lugar, dto.Fecha, dto.HorasDuracion, dto.CantidadAdultos, dto.CantidadNinos);
            repositorio.Agregar(nuevoEvento);
            return nuevoEvento;
        }

        private Evento EditarEvento(int idEvento, Cliente cliente, Lugar lugar, EventoDTO dto)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            evento.ActualizarDatos(cliente, lugar, dto.Fecha, dto.HorasDuracion, dto.CantidadAdultos, dto.CantidadNinos);
            return evento;
        }

        private EventoResultadoDTO MapearAResultado(Evento evento)
        {
            EventoResultadoDTO dto = new EventoResultadoDTO();
            dto.Id = evento.Id;
            dto.IdCliente = evento.Cliente.Id;
            dto.IdLugar = evento.Lugar.Id;
            dto.NombreCliente = evento.Cliente.Nombre;
            dto.NombreLugar = evento.Lugar.Nombre;
            dto.Fecha = evento.Fecha;
            dto.HorasDuracion = evento.HorasDuracion;
            dto.CantidadAdultos = evento.CantidadAdultos;
            dto.CantidadNinos = evento.CantidadNinos;
            dto.EstadoPresupuesto = evento.ObtenerEstadoPresupuesto();
            dto.Total = evento.ObtenerPresupuestoDeReferencia()?.CalcularTotal();
            return dto;
        }
    }
}
