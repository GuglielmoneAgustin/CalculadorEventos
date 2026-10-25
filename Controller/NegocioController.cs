using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class NegocioController
    {
        private readonly NegocioService servicio;

        public NegocioController()
        {
            servicio = new NegocioService();
        }

        public NegocioDTO Obtener()
        {
            return servicio.Obtener();
        }

        public void Guardar(NegocioDTO dto)
        {
            servicio.Guardar(dto);
        }
    }
}
