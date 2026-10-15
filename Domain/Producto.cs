using ProyectoEventos.Common;
using System;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Producto : IEntidad
    {
        private string nombre = string.Empty;
        private decimal precioAdulto;
        private decimal precioNino;
        private CategoriasProductos categoria;

        public Producto(int id, string nombre, decimal precioAdulto, decimal precioNino, CategoriasProductos categoria, RestriccionesAlimenticias restriccion)
        {
            Id = id;
            Nombre = nombre;
            PrecioAdulto = precioAdulto;
            PrecioNino = precioNino;
            Categoria = categoria;
            Restriccion = restriccion;
        }

        public int Id { get; private set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ReglaNegocioException("El nombre del producto no puede estar vacío.");
                }
                nombre = value.Trim();
            }
        }

        public decimal PrecioAdulto
        {
            get { return precioAdulto; }
            set
            {
                if (value <= 0)
                {
                    throw new ReglaNegocioException("El precio para adulto no puede ser menor o igual a cero.");
                }
                precioAdulto = value;
            }
        }

        public decimal PrecioNino
        {
            get { return precioNino; }
            set
            {
                if (value <= 0)
                {
                    throw new ReglaNegocioException("El precio para niño no puede ser menor o igual a cero.");
                }
                precioNino = value;
            }
        }

        public CategoriasProductos Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public RestriccionesAlimenticias Restriccion { get; set; }
    }
}