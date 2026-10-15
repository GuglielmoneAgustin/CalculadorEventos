using System;
using System.Collections.Generic;
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

        // Los repositorios se reciben por constructor: el servicio depende de las interfaces, no de las clases concretas.
        public EventoService(IEventoRepository repositorio, IRepository<Cliente> clienteRepositorio, IRepository<Lugar> lugarRepositorio,
            IRepository<Producto> productoRepositorio, IRepository<ServicioAdicional> servicioRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.lugarRepositorio = lugarRepositorio;
            this.productoRepositorio = productoRepositorio;
            this.servicioRepositorio = servicioRepositorio;
        }

        // ---------- Datos del evento ----------

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
                throw new ReglaNegocioException("El lugar ya tiene un evento en ese horario.");
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
                throw new ReglaNegocioException("No se puede eliminar un evento con un presupuesto aprobado. Cancele primero ese presupuesto.");
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

        public void AgregarItemAlMenu(int idEvento, ItemMenuDTO itemDto)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            if (itemDto == null || itemDto.IdsComponentes == null)
            {
                throw new ArgumentException("Los datos del plato son obligatorios.");
            }

            List<Producto> componentes = new List<Producto>();
            foreach (int idComponente in itemDto.IdsComponentes)
            {
                componentes.Add(productoRepositorio.ObtenerExistente(idComponente));
            }

            evento.AgregarItemAlMenu(itemDto.CantidadAdultos, itemDto.CantidadNinos, componentes);
        }

        // Un plato por cada producto; si alguno falla no se agrega ninguno.
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

        // ---------- Servicios adicionales del evento ----------

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
            // El evento valida todos los datos juntos (rangos y capacidad del lugar) antes de modificarse.
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
