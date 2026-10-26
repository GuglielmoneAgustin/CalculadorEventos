using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class ServicioAdicionalService
    {
        private readonly IRepository<ServicioAdicional> repositorio;
        private readonly IRepository<Evento> eventoRepositorio;

        public ServicioAdicionalService()
            : this(Repositorios.Servicios, Repositorios.Eventos)
        {
        }

        public ServicioAdicionalService(IRepository<ServicioAdicional> repositorio, IRepository<Evento> eventoRepositorio)
        {
            this.repositorio = repositorio;
            this.eventoRepositorio = eventoRepositorio;
        }

        public void Guardar(ServicioAdicionalDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del servicio son obligatorios.");
            }

            if (ExisteNombre(dto.Nombre, dto.Id))
            {
                throw new NegocioException("Ya existe un servicio con ese nombre.");
            }

            if (dto.Id == 0)
            {
                Crear(dto);
            }
            else
            {
                Editar(dto);
            }
        }

        private void Crear(ServicioAdicionalDTO dto)
        {
            int nuevoId = repositorio.ObtenerProximoId();
            ServicioAdicional nuevoServicio = new ServicioAdicional(nuevoId, dto.Nombre, dto.Descripcion, dto.Monto);
            repositorio.Agregar(nuevoServicio);
        }

        public List<ServicioAdicionalDTO> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().Select(MapearADto).ToList();
        }

        public ServicioAdicionalDTO ObtenerPorId(int id)
        {
            return MapearADto(repositorio.ObtenerExistente(id));
        }

        private void Editar(ServicioAdicionalDTO dto)
        {
            ServicioAdicional servicio = repositorio.ObtenerExistente(dto.Id);
            ServicioAdicional validado = new ServicioAdicional(servicio.Id, dto.Nombre, dto.Descripcion, dto.Monto);

            servicio.Nombre = validado.Nombre;
            servicio.Descripcion = validado.Descripcion;
            servicio.Monto = validado.Monto;
            repositorio.Actualizar(servicio);
        }

        public void Eliminar(int id)
        {
            if (eventoRepositorio.ObtenerTodos().Any(evento => evento.TieneServicio(id)))
            {
                throw new NegocioException("No se puede eliminar el servicio porque está asociado a un evento.");
            }
            repositorio.Eliminar(id);
        }

        private bool ExisteNombre(string nombre, int idAIgnorar)
        {
            string? normalizado = nombre?.Trim();
            return repositorio.ObtenerTodos().Any(servicio =>
                servicio.Id != idAIgnorar && servicio.Nombre.Equals(normalizado, StringComparison.OrdinalIgnoreCase));
        }

        internal static ServicioAdicionalDTO MapearADto(ServicioAdicional servicio)
        {
            ServicioAdicionalDTO dto = new ServicioAdicionalDTO();
            dto.Id = servicio.Id;
            dto.Nombre = servicio.Nombre;
            dto.Descripcion = servicio.Descripcion;
            dto.Monto = servicio.Monto;
            return dto;
        }
    }
}
