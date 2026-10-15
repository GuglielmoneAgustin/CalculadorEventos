using ProyectoEventos.Common;
using System;

namespace ProyectoEventos.Dto
{
    public class ProductoDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioAdulto { get; set; }

        public decimal PrecioNino { get; set; }

        public CategoriasProductos Categoria { get; set; }

        public RestriccionesAlimenticias Restriccion { get; set; }
    }
}