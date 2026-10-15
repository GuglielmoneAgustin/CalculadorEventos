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

        // Solo de lectura: cuántos eventos tiene el cliente (se completa al consultar)
        public int CantidadEventos { get; set; }
    }
}