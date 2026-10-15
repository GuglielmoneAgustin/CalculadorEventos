namespace ProyectoEventos.Dto
{
    public class PresupuestoItemDTO
    {
        public string Descripcion { get; set; } = string.Empty;
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal Subtotal { get; set; }
    }
}
