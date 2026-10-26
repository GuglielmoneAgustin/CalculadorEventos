using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Tests.Dominio
{
    public class EventoTests
    {
        private static readonly DateTime Fecha = DateTime.Today.AddDays(30).AddHours(20);

        private readonly Cliente cliente = new Cliente(1, "Ana Pérez", "30111222", "", "");
        private readonly Lugar lugar = new Lugar(1, "Salón Jardín", 1000, 100, 50, "Centro");
        private readonly Producto milanesa = new Producto(1, "Milanesa", 100, 50, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);

        private Evento Crear(int adultos = 10, int ninos = 0, decimal horas = 4)
        {
            return new Evento(1, cliente, lugar, Fecha, horas, adultos, ninos);
        }

        [Theory]
        [InlineData(10, 0, 0, "La duración debe ser mayor a cero.")]
        [InlineData(-1, 0, 4, "La cantidad de adultos no puede ser negativa.")]
        [InlineData(1, -1, 4, "La cantidad de niños no puede ser negativa.")]
        [InlineData(0, 0, 4, "El evento debe tener al menos un invitado.")]
        [InlineData(40, 11, 4, "La cantidad de invitados supera la capacidad del lugar.")]
        public void ValidaSusDatos(int adultos, int ninos, decimal horas, string mensaje)
        {
            NegocioException error = Assert.Throws<NegocioException>(() => Crear(adultos, ninos, horas));
            Assert.Equal(mensaje, error.Message);
        }

        [Fact]
        public void SoloNinosEstaPermitido()
        {
            Assert.Equal(5, Crear(adultos: 0, ninos: 5).TotalInvitados());
        }

        [Fact]
        public void DetectaSuperposicionDeHorarios()
        {
            Evento evento = Crear(horas: 4);

            Assert.True(evento.SeSuperponeCon(Fecha.AddHours(3), 2));
            Assert.True(evento.SeSuperponeCon(Fecha.AddHours(-1), 2));
            Assert.False(evento.SeSuperponeCon(Fecha.AddHours(4), 2));
            Assert.False(evento.SeSuperponeCon(Fecha.AddHours(-2), 2));
        }

        [Fact]
        public void ElMenuSumaLosPlatosYPermiteCambiarlos()
        {
            Evento evento = Crear(adultos: 10, ninos: 4);
            evento.AgregarPlatosAlMenu(10, 4, new List<Producto> { milanesa });

            Assert.Equal(1200, evento.Menu.CalcularSubtotal());

            int idItem = evento.Menu.ObtenerItems()[0].Id;
            evento.CambiarCantidadesItem(idItem, 2, 0);
            Assert.Equal(200, evento.Menu.CalcularSubtotal());

            evento.QuitarItemDelMenu(idItem);
            Assert.True(evento.Menu.EstaVacio());
            Assert.Throws<NoEncontradoException>(() => evento.QuitarItemDelMenu(idItem));
        }

        [Fact]
        public void NoAgregaDosVecesElMismoServicio()
        {
            Evento evento = Crear();
            ServicioAdicional dj = new ServicioAdicional(1, "DJ", "", 500);

            evento.AgregarServicio(dj);

            Assert.Throws<NegocioException>(() => evento.AgregarServicio(dj));
            Assert.Equal(500, evento.CalcularSubtotalServicios());
        }

        [Fact]
        public void NoGeneraPresupuestoSinMenu()
        {
            Assert.Throws<NegocioException>(() => Crear().GenerarPresupuesto(DateTime.Now));
        }

        [Fact]
        public void IndicaLoPendienteSegunElEstadoDelPresupuesto()
        {
            Evento evento = Crear();
            Assert.Equal(TipoPendiente.SinPresupuesto, evento.ObtenerTipoPendiente(DateTime.Now));

            evento.AgregarPlatosAlMenu(10, 0, new List<Producto> { milanesa });
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);
            Assert.Equal(TipoPendiente.EsperandoRespuesta, evento.ObtenerTipoPendiente(DateTime.Now));

            evento.AprobarPresupuesto(presupuesto.Id, false);
            Assert.Null(evento.ObtenerTipoPendiente(DateTime.Now));
            Assert.Equal(TipoPendiente.FaltaCobrar, evento.ObtenerTipoPendiente(Fecha.AddDays(-3)));
        }

        [Fact]
        public void CualquierCambioDejaElPresupuestoDesactualizado()
        {
            Evento evento = Crear();
            evento.AgregarPlatosAlMenu(10, 0, new List<Producto> { milanesa });
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);

            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));

            Assert.False(evento.EsVigente(presupuesto));
            Assert.Equal(TipoPendiente.Desactualizado, evento.ObtenerTipoPendiente(DateTime.Now));
            Assert.Contains("Cambiaron los servicios adicionales.", evento.ObtenerMotivosDesactualizacion());
            Assert.Throws<NegocioException>(() => evento.AprobarPresupuesto(presupuesto.Id, false));
        }
    }
}
