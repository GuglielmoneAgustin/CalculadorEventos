using ProyectoEventos.Common;
using ProyectoEventos.Domain;
using ProyectoEventos.Repository.Sqlite;

namespace ProyectoEventos.Repository
{
    // Repositorios que usa toda la app: los servicios los toman de acá.
    // Para volver a trabajar en memoria, cada uno se cambia por su versión de Repository.Memoria (por ejemplo,
    // Memoria.ClienteRepository.ObtenerInstancia()).
    public static class Repositorios
    {
        private static readonly BaseDatosSqlite baseDatos = AbrirBaseDatos();

        public static readonly IRepository<Cliente> Clientes = new ClienteRepository(baseDatos);
        public static readonly IRepository<Lugar> Lugares = new LugarRepository(baseDatos);
        public static readonly IRepository<Restriccion> Restricciones = new RestriccionRepository(baseDatos);
        public static readonly IRepository<Producto> Productos = new ProductoRepository(baseDatos);
        public static readonly IRepository<ServicioAdicional> Servicios = new ServicioAdicionalRepository(baseDatos);
        public static readonly IRepository<Negocio> Negocios = new NegocioRepository(baseDatos);
        public static readonly IRepository<Evento> Eventos = new EventoRepository(baseDatos, Clientes, Lugares, Productos, Servicios);

        // La primera vez que se usa un repositorio se crean las tablas que falten.
        private static BaseDatosSqlite AbrirBaseDatos()
        {
            BaseDatosSqlite baseDatos = new BaseDatosSqlite(Configuracion.RutaBaseDatos);
            baseDatos.CrearTablasSiNoExisten();
            return baseDatos;
        }
    }
}
