using ProyectoEventos.Common.Enums;

namespace ProyectoEventos.Dto
{
    public class ProductoDTO
    {
        public const string SinRestriccion = "Sin restricción";

        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioAdulto { get; set; }

        public decimal PrecioNino { get; set; }

        public CategoriasProductos Categoria { get; set; }

        // Nulo cuando el producto no tiene restricción.
        public int? IdRestriccion { get; set; }

        public string Restriccion { get; set; } = string.Empty;
    }
}