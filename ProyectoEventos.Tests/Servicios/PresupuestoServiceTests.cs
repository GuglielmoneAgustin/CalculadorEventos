using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public class PresupuestoServiceTests
    {
        private readonly Escenario escenario = new Escenario();
        private readonly int idEvento;

        public PresupuestoServiceTests()
        {
            idEvento = escenario.CrearEventoConMenu();
        }

        private PresupuestoResultadoDTO GenerarYAprobar()
        {
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idEvento)!;
            return escenario.PresupuestoServicio.Aprobar(idEvento, presupuesto.Id, false, false);
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
            Assert.Equal("Vence el " + DateTime.Today.AddDays(15).ToString("dd/MM/yyyy") + " (en 15 días).", presupuesto.Detalle);

            ProductoDTO milanesa = escenario.ProductoServicio.ObtenerTodos().Single();
            milanesa.PrecioAdulto = 150;
            escenario.ProductoServicio.Guardar(milanesa);

            PresupuestoResultadoDTO actualizado = escenario.PresupuestoServicio.ObtenerPorEvento(idEvento).Single();
            Assert.False(actualizado.Vigente);
            Assert.Equal("Cambió el precio de Milanesa.", actualizado.Detalle);
            Assert.Throws<NegocioException>(() => escenario.PresupuestoServicio.ValidarAprobacion(idEvento, actualizado.Id));
        }

        [Fact]
        public void UsaLaValidezDelNegocioYNoLaCambiaDespues()
        {
            NegocioDTO negocio = escenario.NegocioServicio.Obtener();
            negocio.DiasValidez = 30;
            escenario.NegocioServicio.Guardar(negocio);
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idEvento)!;

            negocio.DiasValidez = 5;
            escenario.NegocioServicio.Guardar(negocio);

            Assert.Equal(DateTime.Today.AddDays(30), presupuesto.FechaVencimiento);
            Assert.Equal(DateTime.Today.AddDays(30), escenario.PresupuestoServicio.ObtenerDetalle(idEvento, presupuesto.Id).FechaVencimiento);
        }

        [Fact]
        public void SugiereLaSenaConElPorcentajeDelNegocio()
        {
            PresupuestoResultadoDTO aprobado = GenerarYAprobar();
            Assert.Equal(720, aprobado.SenaSugerida);

            escenario.PresupuestoServicio.RegistrarSena(idEvento, 500);

            PresupuestoResultadoDTO senado = escenario.PresupuestoServicio.ObtenerAprobado(idEvento)!;
            Assert.Equal(EstadoPago.Senado, senado.EstadoPago);
            Assert.Equal(0, senado.SenaSugerida);
            Assert.Equal(1900, senado.Saldo);
        }

        [Fact]
        public void CancelarUnAprobadoConSenaAvisaQueNoSeDevuelve()
        {
            PresupuestoResultadoDTO aprobado = GenerarYAprobar();
            escenario.PresupuestoServicio.RegistrarSena(idEvento, 500);

            PresupuestoResultadoDTO cancelado = escenario.PresupuestoServicio.Cancelar(idEvento, aprobado.Id);

            Assert.Equal(EstadoPresupuesto.Cancelado, cancelado.Estado);
            Assert.Equal("Seña de $ 500,00 no reembolsable.", cancelado.Detalle);
        }

        [Fact]
        public void ElComprobanteTieneLoCobrado()
        {
            GenerarYAprobar();
            escenario.PresupuestoServicio.RegistrarSena(idEvento, 500);
            escenario.PresupuestoServicio.MarcarPagado(idEvento);

            PresupuestoDetalleDTO detalle = escenario.PresupuestoServicio.ObtenerDetalleAprobado(idEvento);

            Assert.Equal(EstadoPago.Pagado, detalle.EstadoPago);
            Assert.Equal(500, detalle.Sena);
            Assert.NotNull(detalle.FechaPago);
            Assert.Equal(0, detalle.Saldo);
        }

        [Fact]
        public void UnVencidoSeMarcaYSoloSeApruebaConfirmando()
        {
            escenario.Evento(idEvento).GenerarPresupuesto(DateTime.Today.AddDays(-20), 15);
            PresupuestoResultadoDTO vencido = escenario.PresupuestoServicio.ObtenerPorEvento(idEvento).Single();

            Assert.True(vencido.Vencido);
            Assert.Equal("Vencido el " + DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy") + ".", vencido.Detalle);
            Assert.Throws<NegocioException>(() => escenario.PresupuestoServicio.Aprobar(idEvento, vencido.Id, false, false));

            PresupuestoResultadoDTO aprobado = escenario.PresupuestoServicio.Aprobar(idEvento, vencido.Id, false, true);
            Assert.Equal(EstadoPresupuesto.Aprobado, aprobado.Estado);
            Assert.False(aprobado.Vencido);
        }
    }
}
