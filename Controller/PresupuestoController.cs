using System.Collections.Generic;
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

        public void ValidarAprobacion(int idEvento, int idPresupuesto)
        {
            servicio.ValidarAprobacion(idEvento, idPresupuesto);
        }

        public bool HayOtroAprobado(int idEvento, int idPresupuesto)
        {
            return servicio.HayOtroAprobado(idEvento, idPresupuesto);
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