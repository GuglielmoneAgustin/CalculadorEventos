using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class ServicioAdicionalController
    {
        private readonly ServicioAdicionalService servicio;

        public ServicioAdicionalController()
        {
            servicio = new ServicioAdicionalService();
        }

        public void Guardar(ServicioAdicionalDTO dto)
        {
            servicio.Guardar(dto);
        }

        public List<ServicioAdicionalDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public ServicioAdicionalDTO ObtenerPorId(int id)
        {
            return servicio.ObtenerPorId(id);
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }
    }
}