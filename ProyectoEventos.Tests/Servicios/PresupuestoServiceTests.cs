using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public abstract class PresupuestoServiceTests : IDisposable
    {
        protected readonly Escenario escenario;
        private readonly int idEvento;

        protected PresupuestoServiceTests(bool usarSqlite)
        {
            escenario = new Escenario(usarSqlite);
            idEvento = escenario.CrearEventoConMenu();
        }

        public void Dispose()
        {
            escenario.Dispose();
        }

        private PresupuestoResultadoDTO GenerarYAprobar()
        {
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idEvento)!;
            return escenario.PresupuestoServicio.Aprobar(idEvento, presupuesto.Id, false);
        }

        [Fact]
        public void SoloGeneraUnoNuevoSiElEventoCambio()
        {
            PresupuestoResultadoDTO primero = escenario.PresupuestoServicio.Generar(idEvento)!;
            Assert.Equal(2400, primero.CostoTotal);
            Assert.Null(escenario.PresupuestoServicio.Generar(idEvento));

            escenario.EventoServicio.AgregarServicioAlEvento(idEvento, escenario.CrearServicio("DJ", 500));

            PresupuestoResultadoDTO segundo = escenario.PresupuestoServicio.Generar(idEvento)!;
            Assert.Equal(2900, segundo.CostoTotal);
            Assert.Equal(2, escenario.PresupuestoServicio.ObtenerPorEvento(idEvento).Count);
        }

        [Fact]
        public void ElDetalleExplicaPorQueEstaDesactualizado()
        {
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idEvento)!;
            Assert.Equal(string.Empty, presupuesto.Detalle);

            ProductoDTO milanesa = escenario.ProductoServicio.ObtenerTodos().Single();
            milanesa.PrecioAdulto = 150;
            escenario.ProductoServicio.Guardar(milanesa);

            PresupuestoResultadoDTO actualizado = escenario.PresupuestoServicio.ObtenerPorEvento(idEvento).Single();
            Assert.False(actualizado.Vigente);
            Assert.Equal("Cambió el precio de Milanesa.", actualizado.Detalle);
            Assert.Throws<NegocioException>(() => escenario.PresupuestoServicio.ValidarAprobacion(idEvento, actualizado.Id));
        }

        [Fact]
        public void ConLaSenaDel30PorCientoUnCambioDePrecioNoPideOtroPresupuesto()
        {
            PresupuestoResultadoDTO aprobado = GenerarYAprobar();
            Assert.False(aprobado.PrecioCongelado);
            Assert.Equal(720, aprobado.FaltaParaCongelarPrecio);
            Assert.Equal("Precio sin congelar: faltan $ 720,00 de seña.", aprobado.Detalle);

            escenario.PresupuestoServicio.RegistrarPago(idEvento, 720);
            ProductoDTO milanesa = escenario.ProductoServicio.ObtenerTodos().Single();
            milanesa.PrecioAdulto = 150;
            escenario.ProductoServicio.Guardar(milanesa);

            PresupuestoResultadoDTO congelado = escenario.PresupuestoServicio.ObtenerAprobado(idEvento)!;
            Assert.True(congelado.PrecioCongelado);
            Assert.True(congelado.Vigente);
            Assert.Equal("Precio congelado.", congelado.Detalle);
            Assert.Null(escenario.PresupuestoServicio.Generar(idEvento));
        }

        [Fact]
        public void RegistrarLaSenaDejaElSaldo()
        {
            GenerarYAprobar();

            escenario.PresupuestoServicio.RegistrarPago(idEvento, 500);

            PresupuestoResultadoDTO senado = escenario.PresupuestoServicio.ObtenerAprobado(idEvento)!;
            Assert.Equal(EstadoPago.Senado, senado.EstadoPago);
            Assert.Equal(1900, senado.Saldo);
        }

        [Fact]
        public void CancelarUnAprobadoConSenaAvisaQueNoSeDevuelve()
        {
            PresupuestoResultadoDTO aprobado = GenerarYAprobar();
            escenario.PresupuestoServicio.RegistrarPago(idEvento, 500);

            PresupuestoResultadoDTO cancelado = escenario.PresupuestoServicio.Cancelar(idEvento, aprobado.Id);

            Assert.Equal(EstadoPresupuesto.Cancelado, cancelado.Estado);
            Assert.Equal("Pagos de $ 500,00 no reembolsables.", cancelado.Detalle);
        }

        [Fact]
        public void ElComprobanteTieneLoCobrado()
        {
            GenerarYAprobar();
            escenario.PresupuestoServicio.RegistrarPago(idEvento, 500);
            escenario.PresupuestoServicio.MarcarPagado(idEvento);

            PresupuestoDetalleDTO detalle = escenario.PresupuestoServicio.ObtenerDetalleAprobado(idEvento);

            Assert.Equal(EstadoPago.Pagado, detalle.EstadoPago);
            Assert.Equal(new[] { 500m, 1900m }, detalle.Pagos.Select(p => p.Monto));
            Assert.Equal(2400, detalle.TotalPagado);
            Assert.NotNull(detalle.FechaPago);
            Assert.Equal(0, detalle.Saldo);
        }
    }

    public class PresupuestoServiceTestsEnMemoria : PresupuestoServiceTests
    {
        public PresupuestoServiceTestsEnMemoria() : base(false)
        {
        }
    }

    public class PresupuestoServiceTestsEnSqlite : PresupuestoServiceTests
    {
        public PresupuestoServiceTestsEnSqlite() : base(true)
        {
        }
    }
}
