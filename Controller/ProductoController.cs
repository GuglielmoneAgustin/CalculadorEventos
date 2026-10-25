using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class ProductoController
    {
        private readonly ProductoService servicio;

        public ProductoController()
        {
            servicio = new ProductoService();
        }

        public void Guardar(ProductoDTO dto)
        {
            servicio.Guardar(dto);
        }

        public List<ProductoDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public ProductoDTO ObtenerPorId(int id)
        {
            return servicio.ObtenerPorId(id);
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }
    }
}