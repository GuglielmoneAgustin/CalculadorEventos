using System;
using System.Collections.Generic;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Service
{
    public class ProductoService
    {
        private readonly IRepository<Producto> repositorio;
        private readonly IEventoRepository eventoRepositorio;

        public ProductoService()
            : this(ProductoRepository.ObtenerInstancia(), EventoRepository.ObtenerInstancia())
        {
        }

        public ProductoService(IRepository<Producto> repositorio, IEventoRepository eventoRepositorio)
        {
            this.repositorio = repositorio;
            this.eventoRepositorio = eventoRepositorio;
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
            Producto nuevoProducto = new Producto(nuevoId, dto.Nombre, dto.PrecioAdulto, dto.PrecioNino, dto.Categoria, dto.Restriccion);
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
            Producto validado = new Producto(producto.Id, dto.Nombre, dto.PrecioAdulto, dto.PrecioNino, dto.Categoria, dto.Restriccion);

            producto.Nombre = validado.Nombre;
            producto.PrecioAdulto = validado.PrecioAdulto;
            producto.PrecioNino = validado.PrecioNino;
            producto.Categoria = validado.Categoria;
            producto.Restriccion = validado.Restriccion;
        }

        public void Eliminar(int id)
        {
            if (eventoRepositorio.TieneProductoAsociado(id))
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

        private ProductoDTO MapearADto(Producto producto)
        {
            ProductoDTO dto = new ProductoDTO();
            dto.Id = producto.Id;
            dto.Nombre = producto.Nombre;
            dto.PrecioAdulto = producto.PrecioAdulto;
            dto.PrecioNino = producto.PrecioNino;
            dto.Categoria = producto.Categoria;
            dto.Restriccion = producto.Restriccion;
            return dto;
        }
    }
}
