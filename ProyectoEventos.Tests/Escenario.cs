using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Repository.Sqlite;
using ProyectoEventos.Service;

namespace ProyectoEventos.Tests
{
    // Repositorio en memoria propio de cada prueba (los de la app son únicos y compartidos).
    internal class RepositorioPrueba<T> : Repository.Memoria.RepositorioBase<T> where T : class, IEntidad
    {
        public RepositorioPrueba() : base("elemento")
        {
        }
    }

    // Arma los servicios con repositorios vacíos (en memoria o en un archivo SQLite temporal)
    // y ofrece atajos para cargar datos.
    public class Escenario : IDisposable
    {
        public readonly IRepository<Cliente> Clientes;
        public readonly IRepository<Lugar> Lugares;
        public readonly IRepository<Producto> Productos;
        public readonly IRepository<ServicioAdicional> Servicios;
        public readonly IRepository<Evento> Eventos;
        public readonly IRepository<Negocio> Negocios;

        public readonly ClienteService ClienteServicio;
        public readonly LugarService LugarServicio;
        public readonly ProductoService ProductoServicio;
        public readonly ServicioAdicionalService ServicioAdicionalServicio;
        public readonly EventoService EventoServicio;
        public readonly NegocioService NegocioServicio;
        public readonly PresupuestoService PresupuestoServicio;

        private readonly string? rutaBaseDatos;

        public Escenario(bool usarSqlite = false)
        {
            if (usarSqlite)
            {
                rutaBaseDatos = Path.Combine(Path.GetTempPath(), "eventos-prueba-" + Guid.NewGuid() + ".db");
                BaseDatosSqlite baseDatos = new BaseDatosSqlite(rutaBaseDatos);
                baseDatos.CrearTablasSiNoExisten();
                Clientes = new ClienteRepository(baseDatos);
                Lugares = new LugarRepository(baseDatos);
                Productos = new ProductoRepository(baseDatos);
                Servicios = new ServicioAdicionalRepository(baseDatos);
                Negocios = new NegocioRepository(baseDatos);
                Eventos = new EventoRepository(baseDatos, Clientes, Lugares, Productos, Servicios);
            }
            else
            {
                Clientes = new RepositorioPrueba<Cliente>();
                Lugares = new RepositorioPrueba<Lugar>();
                Productos = new RepositorioPrueba<Producto>();
                Servicios = new RepositorioPrueba<ServicioAdicional>();
                Negocios = new RepositorioPrueba<Negocio>();
                Eventos = new RepositorioPrueba<Evento>();
            }

            ClienteServicio = new ClienteService(Clientes, Eventos);
            LugarServicio = new LugarService(Lugares, Eventos);
            ProductoServicio = new ProductoService(Productos, Eventos);
            ServicioAdicionalServicio = new ServicioAdicionalService(Servicios, Eventos);
            EventoServicio = new EventoService(Eventos, Clientes, Lugares, Productos, Servicios);
            NegocioServicio = new NegocioService(Negocios);
            PresupuestoServicio = new PresupuestoService(Eventos);
        }

        public void Dispose()
        {
            if (rutaBaseDatos != null)
            {
                SqliteConnection.ClearAllPools();
                File.Delete(rutaBaseDatos);
            }
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
    }
}
