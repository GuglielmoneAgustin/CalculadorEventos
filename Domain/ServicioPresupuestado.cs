namespace ProyectoEventos.Domain
{
    public class ServicioPresupuestado
    {
        public ServicioPresupuestado(string nombre, decimal monto)
        {
            Nombre = nombre;
            Monto = monto;
        }

        public string Nombre { get; }
        public decimal Monto { get; }
    }
}
