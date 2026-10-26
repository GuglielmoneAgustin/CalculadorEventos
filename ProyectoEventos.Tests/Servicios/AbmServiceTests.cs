using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public class AbmServiceTests
    {
        private readonly Escenario escenario = new Escenario();

        [Fact]
        public void ClienteNoRepiteDni()
        {
            escenario.CrearCliente("Ana", "123");

            NegocioException error = Assert.Throws<NegocioException>(() => escenario.CrearCliente("Otra Ana", "123"));
            Assert.Equal("Ya existe un cliente con ese DNI.", error.Message);
        }

        [Fact]
        public void EditarClienteActualizaSusDatos()
        {
            int id = escenario.CrearCliente("Ana", "123");
            ClienteDTO dto = escenario.ClienteServicio.ObtenerPorId(id);
            dto.Nombre = "Ana María";
            dto.Mail = "ana@mail.com";

            escenario.ClienteServicio.Guardar(dto);

            ClienteDTO guardado = escenario.ClienteServicio.ObtenerPorId(id);
            Assert.Equal("Ana María", guardado.Nombre);
            Assert.Equal("ana@mail.com", guardado.Mail);
            Assert.Single(escenario.ClienteServicio.ObtenerTodos());
        }

        [Fact]
        public void UnErrorAlEditarNoCambiaNada()
        {
            int id = escenario.CrearCliente("Ana", "123");
            ClienteDTO dto = escenario.ClienteServicio.ObtenerPorId(id);
            dto.Nombre = "Ana María";
            dto.Mail = "mail-invalido";

            Assert.Throws<NegocioException>(() => escenario.ClienteServicio.Guardar(dto));
            Assert.Equal("Ana", escenario.ClienteServicio.ObtenerPorId(id).Nombre);
        }

        [Fact]
        public void NoSeEliminaNadaQueUseUnEvento()
        {
            int idCliente = escenario.CrearCliente();
            int idLugar = escenario.CrearLugar();
            int idProducto = escenario.CrearProducto();
            int idServicio = escenario.CrearServicio();
            int idEvento = escenario.CrearEvento(idCliente, idLugar, DateTime.Today.AddDays(10));
            escenario.EventoServicio.AgregarPlatosAlMenu(idEvento, 10, 0, new List<int> { idProducto });
            escenario.EventoServicio.AgregarServicioAlEvento(idEvento, idServicio);

            Assert.Throws<NegocioException>(() => escenario.ClienteServicio.Eliminar(idCliente));
            Assert.Throws<NegocioException>(() => escenario.LugarServicio.Eliminar(idLugar));
            Assert.Throws<NegocioException>(() => escenario.ProductoServicio.Eliminar(idProducto));
            Assert.Throws<NegocioException>(() => escenario.ServicioAdicionalServicio.Eliminar(idServicio));
            Assert.Equal(1, escenario.ClienteServicio.ObtenerPorId(idCliente).CantidadEventos);
        }

        [Fact]
        public void SeEliminaLoQueNoSeUsa()
        {
            int idCliente = escenario.CrearCliente();

            escenario.ClienteServicio.Eliminar(idCliente);

            Assert.Empty(escenario.ClienteServicio.ObtenerTodos());
            Assert.Throws<NoEncontradoException>(() => escenario.ClienteServicio.Eliminar(idCliente));
        }

        [Fact]
        public void LugarNoRepiteNombreSinImportarMayusculas()
        {
            escenario.CrearLugar("Salón Jardín");

            Assert.Throws<NegocioException>(() => escenario.CrearLugar("salón jardín"));
        }

        [Fact]
        public void NoSeBajaLaCapacidadPorDebajoDeLosInvitadosDeUnEvento()
        {
            int idLugar = escenario.CrearLugar(capacidad: 100);
            escenario.CrearEvento(escenario.CrearCliente(), idLugar, DateTime.Today.AddDays(10), adultos: 80);
            LugarDTO dto = escenario.LugarServicio.ObtenerPorId(idLugar);
            dto.Capacidad = 50;

            NegocioException error = Assert.Throws<NegocioException>(() => escenario.LugarServicio.Guardar(dto));
            Assert.Contains("capacidad", error.Message);
            Assert.Equal(100, escenario.LugarServicio.ObtenerPorId(idLugar).Capacidad);
        }

        [Fact]
        public void ProductoYServicioNoRepitenNombre()
        {
            escenario.CrearProducto("Flan");
            escenario.CrearServicio("DJ");

            Assert.Throws<NegocioException>(() => escenario.CrearProducto("Flan"));
            Assert.Throws<NegocioException>(() => escenario.CrearServicio("DJ"));
        }
    }
}
