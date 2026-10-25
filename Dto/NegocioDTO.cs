namespace ProyectoEventos.Dto
{
    public class NegocioDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Mail { get; set; } = string.Empty;

        public string Cuit { get; set; } = string.Empty;

        public string Instagram { get; set; } = string.Empty;

        public string MensajePie { get; set; } = string.Empty;

        public int DiasValidez { get; set; }

        public decimal PorcentajeSena { get; set; }
    }
}
