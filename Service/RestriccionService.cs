using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class RestriccionService
    {
        private readonly IRepository<Restriccion> repositorio;
        private readonly IRepository<Producto> productoRepositorio;

        public RestriccionService()
            : this(Repositorios.Restricciones, Repositorios.Productos)
        {
        }

        public RestriccionService(IRepository<Restriccion> repositorio, IRepository<Producto> productoRepositorio)
        {
            this.repositorio = repositorio;
            this.productoRepositorio = productoRepositorio;
        }

        public RestriccionDTO Agregar(string nombre)
        {
            Restriccion validada = new Restriccion(0, nombre);
            foreach (Restriccion existente in repositorio.ObtenerTodos())
            {
                if (existente.Nombre.Equals(validada.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    throw new NegocioException("Ya existe la restricción " + existente.Nombre + ".");
                }
            }

            Restriccion nueva = new Restriccion(repositorio.ObtenerProximoId(), validada.Nombre);
            repositorio.Agregar(nueva);
            return MapearADto(nueva);
        }

        public List<RestriccionDTO> ObtenerTodas()
        {
            return repositorio.ObtenerTodos().Select(MapearADto).ToList();
        }

        public void Eliminar(int id)
        {
            Restriccion restriccion = repositorio.ObtenerExistente(id);
            foreach (Producto producto in productoRepositorio.ObtenerTodos())
            {
                if (producto.Restriccion != null && producto.Restriccion.Id == id)
                {
                    throw new NegocioException("No se puede quitar la restricción " + restriccion.Nombre +
                        " porque la usa el producto " + producto.Nombre + ".");
                }
            }
            repositorio.Eliminar(id);
        }

        private static RestriccionDTO MapearADto(Restriccion restriccion)
        {
            RestriccionDTO dto = new RestriccionDTO();
            dto.Id = restriccion.Id;
            dto.Nombre = restriccion.Nombre;
            return dto;
        }
    }
}
