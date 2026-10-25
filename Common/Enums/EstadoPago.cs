using System.ComponentModel;

namespace ProyectoEventos.Common.Enums
{
    public enum EstadoPago
    {
        [Description("Sin seña")] SinSena,
        [Description("Señado")] Senado,
        Pagado
    }
}
