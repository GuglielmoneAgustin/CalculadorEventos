using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public class NegocioServiceTests
    {
        private readonly Escenario escenario = new Escenario();

        [Fact]
        public void SinDatosCargadosArrancaConValoresPorDefecto()
        {
            NegocioDTO negocio = escenario.NegocioServicio.Obtener();

            Assert.Equal("Mi negocio", negocio.Nombre);
            Assert.Equal(15, negocio.DiasValidez);
            Assert.Equal(30, negocio.PorcentajeSena);
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
}
