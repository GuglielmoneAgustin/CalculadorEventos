using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    // Un pago del cliente a cuenta del presupuesto aprobado (no reembolsable).
    public class Pago
    {
        public Pago(DateTime fecha, decimal monto)
        {
            if (monto <= 0)
            {
                throw new NegocioException("El pago debe ser mayor a cero.");
            }
            Fecha = fecha;
            Monto = monto;
        }

        public DateTime Fecha { get; }
        public decimal Monto { get; }
    }
}
