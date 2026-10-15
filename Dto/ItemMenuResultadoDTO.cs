using System.Collections.Generic;

namespace ProyectoEventos.Dto
{
    public class ItemMenuResultadoDTO
    {
        public int Id { get; set; }
        public string DescripcionComponentes { get; set; } = string.Empty;
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal Subtotal { get; set; }
    }
}
