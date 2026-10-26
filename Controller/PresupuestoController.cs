using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class PresupuestoController
    {
        private readonly PresupuestoService servicio;

        public PresupuestoController()
        {
            servicio = new PresupuestoService();
        }

        public PresupuestoResultadoDTO? Generar(int idEvento)
        {
            return servicio.Generar(idEvento);
        }

        public PresupuestoResultadoDTO Aprobar(int idEvento, int idPresupuesto, bool reemplazarAprobado)
        {
            return servicio.Aprobar(idEvento, idPresupuesto, reemplazarAprobado);
        }

        public PresupuestoResultadoDTO Cancelar(int idEvento, int idPresupuesto)
        {
            return servicio.Cancelar(idEvento, idPresupuesto);
        }

        public PresupuestoResultadoDTO RegistrarSena(int idEvento, decimal monto)
        {
            return servicio.RegistrarSena(idEvento, monto);
        }

        public PresupuestoResultadoDTO MarcarPagado(int idEvento)
        {
            return servicio.MarcarPagado(idEvento);
        }

        public PresupuestoResultadoDTO? ObtenerAprobado(int idEvento)
        {
            return servicio.ObtenerAprobado(idEvento);
        }

        public PresupuestoDetalleDTO ObtenerDetalleAprobado(int idEvento)
        {
            return servicio.ObtenerDetalleAprobado(idEvento);
        }

        public void ValidarAprobacion(int idEvento, int idPresupuesto)
        {
            servicio.ValidarAprobacion(idEvento, idPresupuesto);
        }

        public PresupuestoDetalleDTO ObtenerDetalle(int idEvento, int idPresupuesto)
        {
            return servicio.ObtenerDetalle(idEvento, idPresupuesto);
        }

        public List<PresupuestoResultadoDTO> ObtenerPorEvento(int idEvento)
        {
            return servicio.ObtenerPorEvento(idEvento);
        }
    }
}