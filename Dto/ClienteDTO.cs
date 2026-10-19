namespace ProyectoEventos.Dto
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Dni { get; set; } = string.Empty;

        public string Mail { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int CantidadEventos { get; set; }
    }
}