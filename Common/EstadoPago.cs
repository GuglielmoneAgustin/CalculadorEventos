using System.ComponentModel;

namespace ProyectoEventos.Common
{
    public enum EstadoPago
    {
        [Description("Sin seña")] SinSena,
        [Description("Señado")] Senado,
        Pagado
    }
}
