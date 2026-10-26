using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Tests.Servicios
{
    public class EventoServiceTests
    {
        private static readonly DateTime Fecha = DateTime.Today.AddDays(20).AddHours(20);

        private readonly Escenario escenario = new Escenario();

        [Fact]
        public void NoPermiteDosEventosEnElMismoLugarYHorario()
        {
            int idCliente = escenario.CrearCliente();
            int idLugar = escenario.CrearLugar();
            escenario.CrearEvento(idCliente, idLugar, Fecha);

            NegocioException error = Assert.Throws<NegocioException>(() => escenario.CrearEvento(idCliente, idLugar, Fecha.AddHours(2)));
            Assert.Equal("El lugar ya tiene un evento en ese horario.", error.Message);

            escenario.CrearEvento(idCliente, idLugar, Fecha.AddHours(4));
            escenario.CrearEvento(idCliente, escenario.CrearLugar("Terraza"), Fecha);
            Assert.Equal(3, escenario.EventoServicio.ObtenerTodos().Count);
        }

        [Fact]
        public void EditarUnEventoNoChocaConsigoMismo()
        {
            int idEvento = escenario.CrearEvento(escenario.CrearCliente(), escenario.CrearLugar(), Fecha);
            EventoResultadoDTO evento = escenario.EventoServicio.ObtenerPorId(idEvento);

            EventoDTO dto = new EventoDTO();
            dto.Id = idEvento;
            dto.IdCliente = evento.IdCliente;
            dto.IdLugar = evento.IdLugar;
            dto.Fecha = Fecha.AddHours(1);
            dto.HorasDuracion = 5;
            dto.CantidadAdultos = 20;
            escenario.EventoServicio.Guardar(dto);

            Assert.Equal(20, escenario.EventoServicio.ObtenerPorId(idEvento).CantidadAdultos);
        }

        [Fact]
        public void NoSeEliminaUnEventoConPresupuestoAprobado()
        {
            int idEvento = escenario.CrearEventoConMenu();
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idEvento)!;
            escenario.PresupuestoServicio.Aprobar(idEvento, presupuesto.Id, false, false);

            Assert.Throws<NegocioException>(() => escenario.EventoServicio.Eliminar(idEvento));

            escenario.PresupuestoServicio.Cancelar(idEvento, presupuesto.Id);
            escenario.EventoServicio.Eliminar(idEvento);
            Assert.Empty(escenario.EventoServicio.ObtenerTodos());
        }

        [Fact]
        public void ElTotalDelMenuSumaLosPlatos()
        {
            int idEvento = escenario.CrearEventoConMenu();
            int idPostre = escenario.CrearProducto("Flan", 30, 20);
            escenario.EventoServicio.AgregarPlatosAlMenu(idEvento, 10, 0, new List<int> { idPostre });

            Assert.Equal(1300, escenario.EventoServicio.ObtenerTotalMenu(idEvento));
            Assert.Equal(2, escenario.EventoServicio.ObtenerItemsMenu(idEvento).Count);
        }

        [Fact]
        public void LosPendientesMuestranLoQueHayQueAtender()
        {
            int idSinPresupuesto = escenario.CrearEvento(escenario.CrearCliente("Sin", "1"), escenario.CrearLugar("A"), Fecha);
            int idEsperando = CrearEventoConMenuEn("B", "2", Fecha);
            escenario.PresupuestoServicio.Generar(idEsperando);
            int idCobrar = CrearEventoConMenuEn("C", "3", DateTime.Today.AddDays(3).AddHours(20));
            PresupuestoResultadoDTO presupuesto = escenario.PresupuestoServicio.Generar(idCobrar)!;
            escenario.PresupuestoServicio.Aprobar(idCobrar, presupuesto.Id, false, false);

            List<PendienteDTO> pendientes = escenario.EventoServicio.ObtenerPendientes();

            Assert.Equal(TipoPendiente.SinPresupuesto, pendientes.Single(p => p.IdEvento == idSinPresupuesto).Tipo);
            PendienteDTO esperando = pendientes.Single(p => p.IdEvento == idEsperando);
            Assert.Equal(TipoPendiente.EsperandoRespuesta, esperando.Tipo);
            Assert.Contains("enviado hoy", esperando.Detalle);
            Assert.Contains("Vence el", esperando.Detalle);
            Assert.Equal(TipoPendiente.FaltaCobrar, pendientes.Single(p => p.IdEvento == idCobrar).Tipo);

            escenario.PresupuestoServicio.MarcarPagado(idCobrar);
            Assert.DoesNotContain(escenario.EventoServicio.ObtenerPendientes(), p => p.IdEvento == idCobrar);
        }

        [Fact]
        public void UnPresupuestoViejoApareceComoVencido()
        {
            int idEvento = escenario.CrearEventoConMenu();
            escenario.Evento(idEvento).GenerarPresupuesto(DateTime.Today.AddDays(-20), 15);

            PendienteDTO pendiente = escenario.EventoServicio.ObtenerPendientes().Single();

            Assert.Equal(TipoPendiente.Vencido, pendiente.Tipo);
            Assert.Contains("hace 20 días", pendiente.Detalle);
            Assert.Contains("Vencido el " + DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy"), pendiente.Detalle);
        }

        private int CrearEventoConMenuEn(string nombreLugar, string dni, DateTime fecha)
        {
            int idEvento = escenario.CrearEvento(escenario.CrearCliente("Cliente " + dni, dni), escenario.CrearLugar(nombreLugar), fecha);
            escenario.EventoServicio.AgregarPlatosAlMenu(idEvento, 10, 0, new List<int> { escenario.CrearProducto("Plato " + dni) });
            return idEvento;
        }
    }
}
