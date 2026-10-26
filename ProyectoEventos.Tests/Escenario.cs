using ProyectoEventos.Common.Enums;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository.Memoria;
using ProyectoEventos.Service;

namespace ProyectoEventos.Tests
{
    // Repositorio en memoria propio de cada prueba (los de la app son únicos y compartidos).
    internal class RepositorioPrueba<T> : RepositorioBase<T> where T : class, IEntidad
    {
        public RepositorioPrueba() : base("elemento")
        {
        }
    }

    // Arma los servicios con repositorios vacíos y ofrece atajos para cargar datos.
    internal class Escenario
    {
        public readonly RepositorioPrueba<Cliente> Clientes = new RepositorioPrueba<Cliente>();
        public readonly RepositorioPrueba<Lugar> Lugares = new RepositorioPrueba<Lugar>();
        public readonly RepositorioPrueba<Producto> Productos = new RepositorioPrueba<Producto>();
        public readonly RepositorioPrueba<ServicioAdicional> Servicios = new RepositorioPrueba<ServicioAdicional>();
        public readonly RepositorioPrueba<Evento> Eventos = new RepositorioPrueba<Evento>();
        public readonly RepositorioPrueba<Negocio> Negocios = new RepositorioPrueba<Negocio>();

        public readonly ClienteService ClienteServicio;
        public readonly LugarService LugarServicio;
        public readonly ProductoService ProductoServicio;
        public readonly ServicioAdicionalService ServicioAdicionalServicio;
        public readonly EventoService EventoServicio;
        public readonly NegocioService NegocioServicio;
        public readonly PresupuestoService PresupuestoServicio;

        public Escenario()
        {
            ClienteServicio = new ClienteService(Clientes, Eventos);
            LugarServicio = new LugarService(Lugares, Eventos);
            ProductoServicio = new ProductoService(Productos, Eventos);
            ServicioAdicionalServicio = new ServicioAdicionalService(Servicios, Eventos);
            EventoServicio = new EventoService(Eventos, Clientes, Lugares, Productos, Servicios);
            NegocioServicio = new NegocioService(Negocios);
            PresupuestoServicio = new PresupuestoService(Eventos, NegocioServicio);
        }

        public int CrearCliente(string nombre = "Ana Pérez", string dni = "30111222")
        {
            ClienteDTO dto = new ClienteDTO();
            dto.Nombre = nombre;
            dto.Dni = dni;
            ClienteServicio.Guardar(dto);
            return ClienteServicio.ObtenerTodos().Single(c => c.Dni == dni).Id;
        }

        // Costo del lugar = 1000 + 100 por hora.
        public int CrearLugar(string nombre = "Salón Jardín", int capacidad = 100)
        {
            LugarDTO dto = new LugarDTO();
            dto.Nombre = nombre;
            dto.CostoBase = 1000;
            dto.CostoPorHora = 100;
            dto.Capacidad = capacidad;
            dto.Ubicacion = "Centro";
            LugarServicio.Guardar(dto);
            return LugarServicio.ObtenerTodos().Single(l => l.Nombre == nombre).Id;
        }

        public int CrearProducto(string nombre = "Milanesa", decimal precioAdulto = 100, decimal precioNino = 50)
        {
            ProductoDTO dto = new ProductoDTO();
            dto.Nombre = nombre;
            dto.PrecioAdulto = precioAdulto;
            dto.PrecioNino = precioNino;
            dto.Categoria = CategoriasProductos.Principal;
            dto.Restriccion = RestriccionesAlimenticias.Estandar;
            ProductoServicio.Guardar(dto);
            return ProductoServicio.ObtenerTodos().Single(p => p.Nombre == nombre).Id;
        }

        public int CrearServicio(string nombre = "DJ", decimal monto = 500)
        {
            ServicioAdicionalDTO dto = new ServicioAdicionalDTO();
            dto.Nombre = nombre;
            dto.Monto = monto;
            ServicioAdicionalServicio.Guardar(dto);
            return ServicioAdicionalServicio.ObtenerTodos().Single(s => s.Nombre == nombre).Id;
        }

        public int CrearEvento(int idCliente, int idLugar, DateTime fecha, int adultos = 10, int ninos = 0, decimal horas = 4)
        {
            EventoDTO dto = new EventoDTO();
            dto.IdCliente = idCliente;
            dto.IdLugar = idLugar;
            dto.Fecha = fecha;
            dto.HorasDuracion = horas;
            dto.CantidadAdultos = adultos;
            dto.CantidadNinos = ninos;
            return EventoServicio.Guardar(dto).Id;
        }

        // Evento de 10 adultos, 4 horas, con un plato de 100 por adulto: total 1000 + 400 + 1000 = 2400.
        public int CrearEventoConMenu(int diasHastaElEvento = 30)
        {
            int idCliente = CrearCliente();
            int idLugar = CrearLugar();
            int idProducto = CrearProducto();
            int idEvento = CrearEvento(idCliente, idLugar, DateTime.Today.AddDays(diasHastaElEvento).AddHours(20));
            EventoServicio.AgregarPlatosAlMenu(idEvento, 10, 0, new List<int> { idProducto });
            return idEvento;
        }

        public Evento Evento(int idEvento)
        {
            return Eventos.ObtenerExistente(idEvento);
        }
    }
}
