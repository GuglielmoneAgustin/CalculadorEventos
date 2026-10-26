using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Tests.Dominio
{
    public class EntidadesTests
    {
        [Fact]
        public void ClienteQuitaEspaciosALosCostados()
        {
            Cliente cliente = new Cliente(1, "  Ana Pérez  ", " 30111222 ", " ana@mail.com ", " 351 555 ");

            Assert.Equal("Ana Pérez", cliente.Nombre);
            Assert.Equal("30111222", cliente.Dni);
            Assert.Equal("ana@mail.com", cliente.Mail);
            Assert.Equal("351 555", cliente.Telefono);
        }

        [Theory]
        [InlineData("", "123", "", "El nombre del cliente no puede estar vacío.")]
        [InlineData("Ana", "", "", "El DNI del cliente no puede estar vacío.")]
        [InlineData("Ana", "123", "ana@", "El mail ingresado no es válido (ejemplo: nombre@dominio.com).")]
        public void ClienteConDatosInvalidosAvisa(string nombre, string dni, string mail, string mensaje)
        {
            NegocioException error = Assert.Throws<NegocioException>(() => new Cliente(1, nombre, dni, mail, ""));
            Assert.Equal(mensaje, error.Message);
        }

        [Fact]
        public void ClienteRespetaLosLargosMaximos()
        {
            Assert.Contains("80 caracteres", Assert.Throws<NegocioException>(() => new Cliente(1, new string('a', 81), "1", "", "")).Message);
            Assert.Contains("15 caracteres", Assert.Throws<NegocioException>(() => new Cliente(1, "Ana", new string('1', 16), "", "")).Message);
            Assert.Contains("100 caracteres", Assert.Throws<NegocioException>(() => new Cliente(1, "Ana", "1", new string('a', 92) + "@mail.com", "")).Message);
            Assert.Contains("30 caracteres", Assert.Throws<NegocioException>(() => new Cliente(1, "Ana", "1", "", new string('1', 31))).Message);

            Cliente justo = new Cliente(1, new string('a', 80), new string('1', 15), "", new string('1', 30));
            Assert.Equal(80, justo.Nombre.Length);
        }

        [Fact]
        public void LugarCalculaSuCostoYCapacidad()
        {
            Lugar lugar = new Lugar(1, "Salón", 1000, 100, 50, "Centro");

            Assert.Equal(1450, lugar.CalcularCosto(4.5m));
            Assert.True(lugar.AdmiteInvitados(50));
            Assert.False(lugar.AdmiteInvitados(51));
        }

        [Theory]
        [InlineData(-1, 0, 10, "El costo base no puede ser negativo.")]
        [InlineData(0, -1, 10, "El costo por hora no puede ser negativo.")]
        [InlineData(0, 0, 0, "La capacidad debe ser mayor a cero.")]
        public void LugarConValoresInvalidosAvisa(decimal costoBase, decimal costoPorHora, int capacidad, string mensaje)
        {
            NegocioException error = Assert.Throws<NegocioException>(() => new Lugar(1, "Salón", costoBase, costoPorHora, capacidad, "Centro"));
            Assert.Equal(mensaje, error.Message);
        }

        [Fact]
        public void ProductoYServicioValidanPreciosYLargos()
        {
            Assert.Throws<NegocioException>(() => new Producto(1, "Flan", 0, 10, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar));
            Assert.Throws<NegocioException>(() => new Producto(1, "Flan", 10, 0, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar));
            Assert.Contains("60 caracteres", Assert.Throws<NegocioException>(() =>
                new Producto(1, new string('p', 61), 10, 10, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar)).Message);

            Assert.Throws<NegocioException>(() => new ServicioAdicional(1, "DJ", "", 0));
            Assert.Contains("200 caracteres", Assert.Throws<NegocioException>(() => new ServicioAdicional(1, "DJ", new string('d', 201), 10)).Message);
        }

        [Fact]
        public void ItemMenuNecesitaProductoEInvitados()
        {
            Producto producto = new Producto(1, "Flan", 10, 5, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);

            Assert.Throws<ArgumentException>(() => new ItemMenu(1, null!, 1, 0));
            Assert.Throws<NegocioException>(() => new ItemMenu(1, producto, 0, 0));
            Assert.Throws<NegocioException>(() => new ItemMenu(1, producto, -1, 2));
            Assert.Equal(40, new ItemMenu(1, producto, 3, 2).Subtotal());
        }
    }
}
