using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Tests.Dominio
{
    public class PresupuestoTests
    {
        private static readonly DateTime Fecha = DateTime.Today.AddDays(30).AddHours(20);

        private readonly Lugar lugar = new Lugar(1, "Salón Jardín", 1000, 100, 50, "Centro");
        private readonly Producto milanesa = new Producto(1, "Milanesa", 100, 50, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
        private readonly Evento evento;

        // Total del presupuesto: lugar 1000 + 4 hs x 100 + 10 adultos x 100 = 2400.
        public PresupuestoTests()
        {
            evento = new Evento(1, new Cliente(1, "Ana Pérez", "30111222", "", ""), lugar, Fecha, 4, 10, 0);
            evento.AgregarPlatosAlMenu(10, 0, new List<Producto> { milanesa });
        }

        private Presupuesto GenerarYAprobar()
        {
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);
            return evento.AprobarPresupuesto(presupuesto.Id, false);
        }

        [Fact]
        public void CalculaElTotalCotizado()
        {
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);

            Assert.Equal(1400, presupuesto.CostoLugar);
            Assert.Equal(1000, presupuesto.CalcularSubtotalMenu());
            Assert.Equal(2400, presupuesto.CalcularTotal());
            Assert.Equal(EstadoPresupuesto.EnEspera, presupuesto.Estado);
        }

        [Fact]
        public void EsUnaFotoQueNoCambiaConLosPrecios()
        {
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);

            milanesa.PrecioAdulto = 200;

            Assert.Equal(2400, presupuesto.CalcularTotal());
            Assert.False(evento.EsVigente(presupuesto));
            Assert.Contains("Cambió el precio de Milanesa.", evento.ObtenerMotivosDesactualizacion(presupuesto));
            Assert.True(evento.RequierePresupuestoNuevo);
        }

        [Fact]
        public void ElAnteriorQuedaDesactualizadoPorUnoMasNuevo()
        {
            Presupuesto primero = evento.GenerarPresupuesto(DateTime.Now);
            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));
            Presupuesto segundo = evento.GenerarPresupuesto(DateTime.Now);

            Assert.Equal(2900, segundo.CalcularTotal());
            Assert.True(evento.EsVigente(segundo));
            Assert.Contains("Hay un presupuesto más nuevo (N° " + segundo.Id + ").", evento.ObtenerMotivosDesactualizacion(primero));
        }

        [Fact]
        public void SoloHayUnAprobadoYReemplazarloPideConfirmacion()
        {
            Presupuesto primero = GenerarYAprobar();
            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));
            Presupuesto segundo = evento.GenerarPresupuesto(DateTime.Now);

            Assert.Throws<NegocioException>(() => evento.AprobarPresupuesto(segundo.Id, false));

            evento.AprobarPresupuesto(segundo.Id, true);
            Assert.Equal(EstadoPresupuesto.Cancelado, primero.Estado);
            Assert.Equal(EstadoPresupuesto.Aprobado, segundo.Estado);
        }

        [Fact]
        public void SePuedePagarEnPartesSinPasarseDelSaldo()
        {
            GenerarYAprobar();

            Assert.Throws<NegocioException>(() => evento.RegistrarPago(0, DateTime.Now));
            Assert.Throws<NegocioException>(() => evento.RegistrarPago(2401, DateTime.Now));

            evento.RegistrarPago(700, new DateTime(2030, 1, 10));
            Presupuesto aprobado = evento.RegistrarPago(300, new DateTime(2030, 2, 10));
            Assert.Equal(EstadoPago.Senado, aprobado.EstadoPago);
            Assert.Equal(1000, aprobado.TotalPagado);
            Assert.Equal(1400, aprobado.Saldo);
            Assert.Equal(2, aprobado.ObtenerPagos().Count);
            Assert.Contains("1.400,00", Assert.Throws<NegocioException>(() => evento.RegistrarPago(1401, DateTime.Now)).Message);

            evento.RegistrarPago(1400, new DateTime(2030, 3, 10));
            Assert.Equal(EstadoPago.Pagado, aprobado.EstadoPago);
            Assert.Equal(new DateTime(2030, 3, 10), aprobado.FechaPago);
            Assert.Throws<NegocioException>(() => evento.RegistrarPago(1, DateTime.Now));
        }

        [Fact]
        public void MarcarPagadoRegistraLoQueFalta()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.RegistrarPago(700, DateTime.Now);

            evento.MarcarPagado(DateTime.Now);

            Assert.Equal(1700, aprobado.ObtenerPagos()[1].Monto);
            Assert.True(aprobado.EstaPagado);
            Assert.Equal(0, aprobado.Saldo);
        }

        [Fact]
        public void LosPagosEnPartesCongelanElPrecioAlLlegarAl30PorCiento()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.RegistrarPago(400, DateTime.Now);
            Assert.Equal(320, aprobado.CalcularFaltanteParaCongelarPrecio());
            Assert.False(aprobado.TienePrecioCongelado);

            evento.RegistrarPago(320, DateTime.Now);

            Assert.Equal(0, aprobado.CalcularFaltanteParaCongelarPrecio());
            Assert.True(aprobado.TienePrecioCongelado);
        }

        [Fact]
        public void SoloSeCobraUnPresupuestoAprobado()
        {
            evento.GenerarPresupuesto(DateTime.Now);

            Assert.Throws<NegocioException>(() => evento.RegistrarPago(100, DateTime.Now));
            Assert.Throws<NegocioException>(() => evento.MarcarPagado(DateTime.Now));
        }

        [Fact]
        public void UnPresupuestoPagadoNoSeCancelaNiSeReemplaza()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.MarcarPagado(DateTime.Now);

            Assert.Equal(EstadoPago.Pagado, aprobado.EstadoPago);
            Assert.Equal(0, aprobado.Saldo);
            Assert.Throws<NegocioException>(() => evento.MarcarPagado(DateTime.Now));
            Assert.Throws<NegocioException>(() => evento.CancelarPresupuesto(aprobado.Id));

            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));
            Presupuesto nuevo = evento.GenerarPresupuesto(DateTime.Now);
            Assert.Throws<NegocioException>(() => evento.AprobarPresupuesto(nuevo.Id, true));
        }

        [Fact]
        public void LaSenaPasaAlPresupuestoQueReemplazaAlAprobado()
        {
            Presupuesto primero = GenerarYAprobar();
            evento.RegistrarPago(700, DateTime.Now);
            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));
            Presupuesto segundo = evento.GenerarPresupuesto(DateTime.Now);

            evento.AprobarPresupuesto(segundo.Id, true);

            Assert.Equal(700, segundo.TotalPagado);
            Assert.Equal(0, primero.TotalPagado);
            Assert.Equal(2200, segundo.Saldo);
        }

        [Fact]
        public void AprobadoConSenaMenorAl30PorCientoSeDesactualizaConLosPrecios()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.RegistrarPago(719, DateTime.Now);

            milanesa.PrecioAdulto = 200;

            Assert.Equal(720, aprobado.CalcularSenaParaCongelarPrecio());
            Assert.False(aprobado.TienePrecioCongelado);
            Assert.False(evento.EsVigente(aprobado));
            Assert.True(evento.RequierePresupuestoNuevo);
        }

        [Fact]
        public void ConSenaDel30PorCientoElPrecioQuedaCongelado()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.RegistrarPago(720, DateTime.Now);

            milanesa.PrecioAdulto = 200;
            lugar.CostoBase = 5000;

            Assert.True(aprobado.TienePrecioCongelado);
            Assert.True(evento.EsVigente(aprobado));
            Assert.False(evento.RequierePresupuestoNuevo);
            Assert.Equal(2400, aprobado.CalcularTotal());
        }

        [Fact]
        public void PagadoTambienCongelaElPrecio()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.MarcarPagado(DateTime.Now);

            milanesa.PrecioAdulto = 200;

            Assert.True(aprobado.TienePrecioCongelado);
            Assert.True(evento.EsVigente(aprobado));
        }

        [Fact]
        public void ConPrecioCongeladoUnCambioDelEventoPideUnoNuevoConLosPreciosActuales()
        {
            Presupuesto aprobado = GenerarYAprobar();
            evento.RegistrarPago(720, DateTime.Now);
            milanesa.PrecioAdulto = 200;

            evento.AgregarServicio(new ServicioAdicional(1, "DJ", "", 500));

            Assert.False(evento.EsVigente(aprobado));
            Assert.True(evento.RequierePresupuestoNuevo);
            Assert.Equal(1400 + 2000 + 500, evento.GenerarPresupuesto(DateTime.Now).CalcularTotal());
        }

        [Fact]
        public void EnEsperaNoSeCongelaAunqueNoTengaSena()
        {
            Presupuesto presupuesto = evento.GenerarPresupuesto(DateTime.Now);

            Assert.False(presupuesto.TienePrecioCongelado);
        }
    }
}
