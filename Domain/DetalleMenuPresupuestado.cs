namespace ProyectoEventos.Domain
{
    public class DetalleMenuPresupuestado
    {
        public DetalleMenuPresupuestado(string descripcion, int cantidadAdultos, int cantidadNinos, decimal subtotal)
        {
            Descripcion = descripcion;
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
            Subtotal = subtotal;
        }

        public string Descripcion { get; }
        public int CantidadAdultos { get; }
        public int CantidadNinos { get; }
        public decimal Subtotal { get; }
    }
}
