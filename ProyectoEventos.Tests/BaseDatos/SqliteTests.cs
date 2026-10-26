using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Domain;
using ProyectoEventos.Repository;
using ProyectoEventos.Repository.Sqlite;

namespace ProyectoEventos.Tests.BaseDatos
{
    // Lo que se guarda tiene que volver igual al leerlo con repositorios nuevos (como al reabrir la app).
    public class SqliteTests : IDisposable
    {
        private readonly string ruta = Path.Combine(Path.GetTempPath(), "eventos-prueba-" + Guid.NewGuid() + ".db");
        private readonly BaseDatosSqlite baseDatos;

        public SqliteTests()
        {
            baseDatos = new BaseDatosSqlite(ruta);
            baseDatos.CrearTablasSiNoExisten();
        }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            File.Delete(ruta);
        }

        private IRepository<Evento> RepositorioEventos()
        {
            return new EventoRepository(baseDatos, new ClienteRepository(baseDatos), new LugarRepository(baseDatos),
                new ProductoRepository(baseDatos), new ServicioAdicionalRepository(baseDatos));
        }

        private Evento CrearEventoGuardado()
        {
            Cliente cliente = new Cliente(1, "Ana Pérez", "30111222", "ana@mail.com", "351 555");
            Lugar lugar = new Lugar(1, "Salón Jardín", 1000.50m, 99.99m, 100, "Centro");
            Producto producto = new Producto(1, "Milanesa", 1234.56m, 617.28m, CategoriasProductos.Principal, RestriccionesAlimenticias.Celiaco);
            Producto postre = new Producto(2, "Flan", 300, 150, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);
            ServicioAdicional dj = new ServicioAdicional(1, "DJ", "Sonido y luces", 50000);
            new ClienteRepository(baseDatos).Agregar(cliente);
            new LugarRepository(baseDatos).Agregar(lugar);
            new ProductoRepository(baseDatos).Agregar(producto);
            new ProductoRepository(baseDatos).Agregar(postre);
            new ServicioAdicionalRepository(baseDatos).Agregar(dj);

            Evento evento = new Evento(1, cliente, lugar, new DateTime(2030, 3, 15, 20, 30, 0), 4.25m, 40, 10);
            evento.AgregarPlatosAlMenu(40, 10, new List<Producto> { producto, postre });
            evento.AgregarServicio(dj);
            RepositorioEventos().Agregar(evento);
            return evento;
        }

        [Fact]
        public void LasEntidadesSimplesVuelvenIguales()
        {
            new ProductoRepository(baseDatos).Agregar(new Producto(1, "Ñoquis", 1234.56m, 0.01m, CategoriasProductos.Principal, RestriccionesAlimenticias.Vegetariano));
            new NegocioRepository(baseDatos).Agregar(new Negocio(1, "Salón", "Calle 1", "351", "a@b.com", "20-12345678-9", "@salon", "Gracias"));

            Producto producto = new ProductoRepository(baseDatos).ObtenerExistente(1);
            Negocio negocio = new NegocioRepository(baseDatos).ObtenerExistente(1);

            Assert.Equal("Ñoquis", producto.Nombre);
            Assert.Equal(1234.56m, producto.PrecioAdulto);
            Assert.Equal(0.01m, producto.PrecioNino);
            Assert.Equal(RestriccionesAlimenticias.Vegetariano, producto.Restriccion);
            Assert.Equal("20-12345678-9", negocio.Cuit);
            Assert.Equal("@salon", negocio.Instagram);
        }

        [Fact]
        public void LosIdsNoSeReutilizan()
        {
            ClienteRepository clientes = new ClienteRepository(baseDatos);
            int primero = clientes.ObtenerProximoId();
            clientes.Agregar(new Cliente(primero, "Ana", "1", "", ""));
            clientes.Eliminar(primero);

            int segundo = new ClienteRepository(baseDatos).ObtenerProximoId();

            Assert.Equal(1, primero);
            Assert.Equal(2, segundo);
        }

        [Fact]
        public void ElEventoVuelveConSuMenuYSusServicios()
        {
            CrearEventoGuardado();

            Evento leido = RepositorioEventos().ObtenerExistente(1);

            Assert.Equal(new DateTime(2030, 3, 15, 20, 30, 0), leido.Fecha);
            Assert.Equal(4.25m, leido.HorasDuracion);
            Assert.Equal("Ana Pérez", leido.Cliente.Nombre);
            Assert.Equal(1000.50m + 99.99m * 4.25m, leido.Lugar.CalcularCosto(leido.HorasDuracion));
            Assert.Equal(new[] { "Milanesa", "Flan" }, leido.Menu.ObtenerItems().Select(i => i.ObtenerDescripcion()));
            Assert.Equal(40 * 1234.56m + 10 * 617.28m + 40 * 300 + 10 * 150, leido.Menu.CalcularSubtotal());
            Assert.Equal("DJ", leido.ObtenerServicios().Single().Nombre);
        }

        [Fact]
        public void LosPresupuestosVuelvenConSuFotoYSuCobro()
        {
            Evento evento = CrearEventoGuardado();
            Presupuesto presupuesto = evento.GenerarPresupuesto(new DateTime(2030, 1, 10, 11, 0, 0));
            evento.AprobarPresupuesto(presupuesto.Id, false);
            evento.RegistrarPago(10000.10m, new DateTime(2030, 1, 12, 9, 0, 0));
            evento.RegistrarPago(5000, new DateTime(2030, 2, 1, 18, 30, 0));
            RepositorioEventos().Actualizar(evento);

            // Después de guardar cambian los precios: la foto del presupuesto no tiene que cambiar.
            Producto producto = new ProductoRepository(baseDatos).ObtenerExistente(1);
            producto.PrecioAdulto = 2000;
            new ProductoRepository(baseDatos).Actualizar(producto);

            Evento leido = RepositorioEventos().ObtenerExistente(1);
            Presupuesto guardado = leido.ObtenerPresupuestos().Single();

            Assert.Equal(presupuesto.CalcularTotal(), guardado.CalcularTotal());
            Assert.Equal(EstadoPresupuesto.Aprobado, guardado.Estado);
            Assert.Equal(new DateTime(2030, 1, 10, 11, 0, 0), guardado.FechaGeneracion);
            Assert.Equal(15000.10m, guardado.TotalPagado);
            Assert.Equal(new[] { new DateTime(2030, 1, 12, 9, 0, 0), new DateTime(2030, 2, 1, 18, 30, 0) },
                guardado.ObtenerPagos().Select(p => p.Fecha));
            Assert.Equal(new[] { 10000.10m, 5000m }, guardado.ObtenerPagos().Select(p => p.Monto));
            Assert.Null(guardado.FechaPago);
            Assert.Equal("DJ", guardado.ObtenerServicios().Single().Nombre);
            Assert.False(leido.EsVigente(guardado));
            Assert.Contains("Cambió el precio de Milanesa.", leido.ObtenerMotivosDesactualizacion(guardado));
        }

        [Fact]
        public void ConservaElEstadoInternoDelEvento()
        {
            Evento evento = CrearEventoGuardado();
            evento.GenerarPresupuesto(DateTime.Now);
            int idPostre = evento.Menu.ObtenerItems()[1].Id;
            evento.QuitarItemDelMenu(idPostre);
            RepositorioEventos().Actualizar(evento);

            Evento leido = RepositorioEventos().ObtenerExistente(1);

            // Sigue desactualizado (se quitó un plato después de generar el presupuesto).
            Assert.True(leido.RequierePresupuestoNuevo);
            // Los números de plato y de presupuesto siguen desde donde estaban.
            leido.AgregarPlatosAlMenu(1, 0, new List<Producto> { new ProductoRepository(baseDatos).ObtenerExistente(2) });
            Assert.Equal(3, leido.Menu.ObtenerItems().Last().Id);
            Assert.Equal(2, leido.GenerarPresupuesto(DateTime.Now).Id);
        }

        [Fact]
        public void QuitarUnServicioFuncionaConElEventoLeido()
        {
            CrearEventoGuardado();
            IRepository<Evento> eventos = RepositorioEventos();

            Evento leido = eventos.ObtenerExistente(1);
            leido.QuitarServicio(new ServicioAdicionalRepository(baseDatos).ObtenerExistente(1));
            eventos.Actualizar(leido);

            Assert.Empty(eventos.ObtenerExistente(1).ObtenerServicios());
        }

        [Fact]
        public void BorrarUnEventoBorraTodoLoQueContiene()
        {
            Evento evento = CrearEventoGuardado();
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);
            evento.AprobarPresupuesto(presupuesto.Id, false);
            evento.RegistrarPago(1000, DateTime.Now);
            RepositorioEventos().Actualizar(evento);

            RepositorioEventos().Eliminar(1);

            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT (SELECT COUNT(*) FROM ItemsMenu) + (SELECT COUNT(*) FROM EventoServicios) + " +
                "(SELECT COUNT(*) FROM Presupuestos) + (SELECT COUNT(*) FROM PresupuestoItems) + (SELECT COUNT(*) FROM PresupuestoServicios) + (SELECT COUNT(*) FROM PresupuestoPagos)";
            Assert.Equal(0L, comando.ExecuteScalar());
            Assert.Empty(RepositorioEventos().ObtenerTodos());
        }
    }
}
