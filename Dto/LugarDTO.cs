namespace ProyectoEventos.Dto
{
    public class LugarDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal CostoBase { get; set; }

        public decimal CostoPorHora { get; set; }

        public int Capacidad { get; set; }
        public string Ubicacion { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
    }
}