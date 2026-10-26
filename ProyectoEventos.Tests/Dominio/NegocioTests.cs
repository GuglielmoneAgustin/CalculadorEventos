using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Tests.Dominio
{
    public class NegocioTests
    {
        private static Negocio Crear(string nombre = "Salón Las Rosas", string telefono = "", string mail = "", string cuit = "",
            string instagram = "")
        {
            return new Negocio(1, nombre, "", telefono, mail, cuit, instagram, "");
        }

        [Theory]
        [InlineData("20-12345678-9")]
        [InlineData("20123456789")]
        [InlineData("20 12345678 9")]
        public void AceptaCuitConOSinGuiones(string cuit)
        {
            Assert.Equal(cuit, Crear(cuit: cuit).Cuit);
        }

        [Theory]
        [InlineData("20-123", "11 números")]
        [InlineData("20-1234567a-9", "números y guiones")]
        public void RechazaCuitInvalido(string cuit, string mensaje)
        {
            Assert.Contains(mensaje, Assert.Throws<NegocioException>(() => Crear(cuit: cuit)).Message);
        }

        [Fact]
        public void ValidaLosDemasCampos()
        {
            Assert.Throws<NegocioException>(() => Crear(nombre: " "));
            Assert.Contains("60 caracteres", Assert.Throws<NegocioException>(() => Crear(nombre: new string('a', 61))).Message);
            Assert.Contains("teléfono", Assert.Throws<NegocioException>(() => Crear(telefono: "abc")).Message);
            Assert.Equal("+54 (351) 555-1234", Crear(telefono: "+54 (351) 555-1234").Telefono);
            Assert.Contains("mail", Assert.Throws<NegocioException>(() => Crear(mail: "hola")).Message);
            Assert.Contains("espacios", Assert.Throws<NegocioException>(() => Crear(instagram: "@mi negocio")).Message);
        }
    }
}
