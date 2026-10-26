using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class ProductoService
    {
        private readonly IRepository<Producto> repositorio;
        private readonly IRepository<Evento> eventoRepositorio;
        private readonly IRepository<Restriccion> restriccionRepositorio;

        public ProductoService()
            : this(Repositorios.Productos, Repositorios.Eventos, Repositorios.Restricciones)
        {
        }

        public ProductoService(IRepository<Producto> repositorio, IRepository<Evento> eventoRepositorio,
            IRepository<Restriccion> restriccionRepositorio)
        {
            this.repositorio = repositorio;
            this.eventoRepositorio = eventoRepositorio;
            this.restriccionRepositorio = restriccionRepositorio;
        }

        public void Guardar(ProductoDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del producto son obligatorios.");
            }

            if (ExisteNombre(dto.Nombre, dto.Id))
            {
                throw new NegocioException("Ya existe un producto con ese nombre.");
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

        private void Crear(ProductoDTO dto)
        {
            int nuevoId = repositorio.ObtenerProximoId();
            Producto nuevoProducto = new Producto(nuevoId, dto.Nombre, dto.PrecioAdulto, dto.PrecioNino, dto.Categoria, ObtenerRestriccion(dto.IdRestriccion));
            repositorio.Agregar(nuevoProducto);
        }

        public List<ProductoDTO> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().Select(MapearADto).ToList();
        }

        public ProductoDTO ObtenerPorId(int id)
        {
            return MapearADto(repositorio.ObtenerExistente(id));
        }

        private void Editar(ProductoDTO dto)
        {
            Producto producto = repositorio.ObtenerExistente(dto.Id);
            Producto validado = new Producto(producto.Id, dto.Nombre, dto.PrecioAdulto, dto.PrecioNino, dto.Categoria, ObtenerRestriccion(dto.IdRestriccion));

            producto.Nombre = validado.Nombre;
            producto.PrecioAdulto = validado.PrecioAdulto;
            producto.PrecioNino = validado.PrecioNino;
            producto.Categoria = validado.Categoria;
            producto.Restriccion = validado.Restriccion;
            repositorio.Actualizar(producto);
        }

        public void Eliminar(int id)
        {
            if (eventoRepositorio.ObtenerTodos().Any(evento => evento.UsaProducto(id)))
            {
                throw new NegocioException("No se puede eliminar el producto porque forma parte del menú de un evento.");
            }
            repositorio.Eliminar(id);
        }

        private bool ExisteNombre(string nombre, int idAIgnorar)
        {
            string? normalizado = nombre?.Trim();
            return repositorio.ObtenerTodos().Any(producto =>
                producto.Id != idAIgnorar && producto.Nombre.Equals(normalizado, StringComparison.OrdinalIgnoreCase));
        }

        private Restriccion? ObtenerRestriccion(int? idRestriccion)
        {
            if (idRestriccion == null)
            {
                return null;
            }
            return restriccionRepositorio.ObtenerExistente(idRestriccion.Value);
        }

        private ProductoDTO MapearADto(Producto producto)
        {
            ProductoDTO dto = new ProductoDTO();
            dto.Id = producto.Id;
            dto.Nombre = producto.Nombre;
            dto.PrecioAdulto = producto.PrecioAdulto;
            dto.PrecioNino = producto.PrecioNino;
            dto.Categoria = producto.Categoria;
            dto.IdRestriccion = producto.Restriccion?.Id;
            dto.Restriccion = producto.Restriccion == null ? ProductoDTO.SinRestriccion : producto.Restriccion.Nombre;
            return dto;
        }
    }
}
