using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public abstract class NegocioServiceTests : IDisposable
    {
        protected readonly Escenario escenario;

        protected NegocioServiceTests(bool usarSqlite)
        {
            escenario = new Escenario(usarSqlite);
        }

        public void Dispose()
        {
            escenario.Dispose();
        }

        [Fact]
        public void SinDatosCargadosArrancaConValoresPorDefecto()
        {
            NegocioDTO negocio = escenario.NegocioServicio.Obtener();

            Assert.Equal("Mi negocio", negocio.Nombre);
            Assert.Single(escenario.Negocios.ObtenerTodos());
        }

        [Fact]
        public void GuardaLosDatosEnElMismoRegistro()
        {
            NegocioDTO negocio = escenario.NegocioServicio.Obtener();
            negocio.Nombre = "Salón Las Rosas";
            negocio.Cuit = "30-71234567-8";

            escenario.NegocioServicio.Guardar(negocio);

            Assert.Equal("Salón Las Rosas", escenario.NegocioServicio.Obtener().Nombre);
            Assert.Single(escenario.Negocios.ObtenerTodos());
        }

        [Fact]
        public void UnDatoInvalidoNoCambiaNada()
        {
            NegocioDTO negocio = escenario.NegocioServicio.Obtener();
            negocio.Nombre = "Otro nombre";
            negocio.Mail = "sin-arroba";

            Assert.Throws<NegocioException>(() => escenario.NegocioServicio.Guardar(negocio));
            Assert.Equal("Mi negocio", escenario.NegocioServicio.Obtener().Nombre);
        }
    }

    public class NegocioServiceTestsEnMemoria : NegocioServiceTests
    {
        public NegocioServiceTestsEnMemoria() : base(false)
        {
        }
    }

    public class NegocioServiceTestsEnSqlite : NegocioServiceTests
    {
        public NegocioServiceTestsEnSqlite() : base(true)
        {
        }
    }
}
