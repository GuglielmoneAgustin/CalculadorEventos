using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class RestriccionController
    {
        private readonly RestriccionService servicio;

        public RestriccionController()
        {
            servicio = new RestriccionService();
        }

        public RestriccionDTO Agregar(string nombre)
        {
            return servicio.Agregar(nombre);
        }

        public List<RestriccionDTO> ObtenerTodas()
        {
            return servicio.ObtenerTodas();
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }
    }
}
