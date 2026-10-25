using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class LugarController
    {
        private readonly LugarService servicio;

        public LugarController()
        {
            servicio = new LugarService();
        }

        public void Guardar(LugarDTO dto)
        {
            servicio.Guardar(dto);
        }

        public List<LugarDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public LugarDTO ObtenerPorId(int id)
        {
            return servicio.ObtenerPorId(id);
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }
    }
}