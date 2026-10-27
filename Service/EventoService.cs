using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class EventoService
    {
        private readonly IRepository<Evento> repositorio;
        private readonly IRepository<Cliente> clienteRepositorio;
        private readonly IRepository<Lugar> lugarRepositorio;
        private readonly IRepository<Producto> productoRepositorio;
        private readonly IRepository<ServicioAdicional> servicioRepositorio;

        public EventoService()
            : this(Repositorios.Eventos, Repositorios.Clientes, Repositorios.Lugares,
                Repositorios.Productos, Repositorios.Servicios)
        {
        }

        public EventoService(IRepository<Evento> repositorio, IRepository<Cliente> clienteRepositorio, IRepository<Lugar> lugarRepositorio,
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
            if (HayEventoEnLugarEnHorario(lugar.Id, dto.Fecha, dto.HorasDuracion, dto.Id))
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

        public EventoResultadoDTO ObtenerPorId(int id)
        {
            return MapearAResultado(repositorio.ObtenerExistente(id));
        }

        public List<EventoResultadoDTO> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().Select(MapearAResultado).ToList();
        }

        public List<EventoResultadoDTO> ObtenerProximosAprobados()
        {
            DateTime ahora = DateTime.Now;

            return repositorio.ObtenerTodos()
                .Where(evento => evento.EsProximoAprobado(ahora))
                .OrderBy(evento => evento.Fecha)
                .Select(MapearAResultado)
                .ToList();
        }

        public List<EventoResultadoDTO> ObtenerSinAprobarProximos()
        {
            DateTime ahora = DateTime.Now;
            DateTime limite = DateTime.Today.AddDays(Configuracion.DiasProximos + 1);

            return repositorio.ObtenerTodos()
                .Where(evento => evento.Fecha >= ahora && evento.Fecha < limite && !evento.TienePresupuestoAprobado())
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
                TipoPendiente? tipo = evento.ObtenerTipoPendiente(ahora);
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

        public void AgregarPlatosAlMenu(int idEvento, int cantidadAdultos, int cantidadNinos, List<int> idsProductos)
        {
            if (idsProductos == null)
            {
                throw new ArgumentException("Los productos del plato son obligatorios.");
            }

            List<Producto> productos = new List<Producto>();
            foreach (int idProducto in idsProductos)
            {
                productos.Add(productoRepositorio.ObtenerExistente(idProducto));
            }

            Evento evento = repositorio.ObtenerExistente(idEvento);
            evento.AgregarPlatosAlMenu(cantidadAdultos, cantidadNinos, productos);
            repositorio.Actualizar(evento);
        }

        public void CambiarCantidadesItem(int idEvento, int idItem, int cantidadAdultos, int cantidadNinos)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            evento.CambiarCantidadesItem(idItem, cantidadAdultos, cantidadNinos);
            repositorio.Actualizar(evento);
        }

        public void QuitarItemDelMenu(int idEvento, int idItem)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            evento.QuitarItemDelMenu(idItem);
            repositorio.Actualizar(evento);
        }

        public List<ItemMenuResultadoDTO> ObtenerItemsMenu(int idEvento)
        {
            return repositorio.ObtenerExistente(idEvento).Menu.ObtenerItems().Select(item => new ItemMenuResultadoDTO
            {
                Id = item.Id,
                Producto = item.ObtenerDescripcion(),
                CantidadAdultos = item.CantidadAdultos,
                CantidadNinos = item.CantidadNinos,
                Subtotal = item.Subtotal()
            }).ToList();
        }

        public decimal CalcularSubtotalPlatos(List<int> idsProductos, int cantidadAdultos, int cantidadNinos)
        {
            decimal subtotal = 0m;
            foreach (int idProducto in idsProductos)
            {
                subtotal += productoRepositorio.ObtenerExistente(idProducto).CalcularPrecio(cantidadAdultos, cantidadNinos);
            }
            return subtotal;
        }

        public decimal ObtenerTotalMenu(int idEvento)
        {
            return repositorio.ObtenerExistente(idEvento).Menu.CalcularSubtotal();
        }

        public decimal CalcularCostoLugar(int idLugar, decimal horasDuracion)
        {
            return lugarRepositorio.ObtenerExistente(idLugar).CalcularCosto(horasDuracion);
        }

        public bool LugarDisponible(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar)
        {
            return !HayEventoEnLugarEnHorario(idLugar, fecha, horasDuracion, idEventoAIgnorar);
        }

        public void AgregarServicioAlEvento(int idEvento, int idServicio)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            ServicioAdicional servicio = servicioRepositorio.ObtenerExistente(idServicio);
            evento.AgregarServicio(servicio);
            repositorio.Actualizar(evento);
        }

        public void QuitarServicioDelEvento(int idEvento, int idServicio)
        {
            Evento evento = repositorio.ObtenerExistente(idEvento);
            ServicioAdicional servicio = servicioRepositorio.ObtenerExistente(idServicio);
            evento.QuitarServicio(servicio);
            repositorio.Actualizar(evento);
        }

        public List<ServicioAdicionalDTO> ObtenerServiciosEvento(int idEvento)
        {
            List<ServicioAdicionalDTO> servicios = new List<ServicioAdicionalDTO>();
            foreach (ServicioAdicional servicio in repositorio.ObtenerExistente(idEvento).ObtenerServicios())
            {
                ServicioAdicionalDTO dto = new ServicioAdicionalDTO();
                dto.Id = servicio.Id;
                dto.Nombre = servicio.Nombre;
                dto.Descripcion = servicio.Descripcion;
                dto.Monto = servicio.Monto;
                servicios.Add(dto);
            }
            return servicios;
        }

        private bool HayEventoEnLugarEnHorario(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar)
        {
            return repositorio.ObtenerTodos().Any(evento =>
                evento.Id != idEventoAIgnorar && evento.TieneLugar(idLugar) && evento.SeSuperponeCon(fecha, horasDuracion));
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
            repositorio.Actualizar(evento);
            return evento;
        }

        private static string ArmarDetalle(Evento evento, TipoPendiente tipo)
        {
            switch (tipo)
            {
                case TipoPendiente.SinPresupuesto:
                    return "Genere el presupuesto desde Eventos > Ver presupuestos.";
                case TipoPendiente.Desactualizado:
                    return string.Join(" ", evento.ObtenerMotivosDesactualizacion());
                case TipoPendiente.FaltaCobrar:
                    Presupuesto aprobado = evento.ObtenerPresupuestoAprobadoExistente();
                    return "Falta cobrar " + aprobado.Saldo.ToString(Formato.Moneda) +
                        (aprobado.TotalPagado > 0 ? " (pagado " + aprobado.TotalPagado.ToString(Formato.Moneda) + ")." : " (sin seña).");
                default:
                    Presupuesto enEspera = evento.ObtenerPresupuestoEnEspera()!;
                    return "Presupuesto N° " + enEspera.Id + " enviado " + DescribirAntiguedad(enEspera.FechaGeneracion) + ".";
            }
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

        private static EventoResultadoDTO MapearAResultado(Evento evento)
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
            dto.EstadoPago = evento.ObtenerEstadoPago();
            dto.Realizado = evento.EstaRealizado(DateTime.Now);
            return dto;
        }
    }
}
