using System.ComponentModel;

namespace ProyectoEventos.Common
{
    public enum EstadoPresupuesto
    {
        [Description("En espera")] EnEspera,
        Aprobado,
        Cancelado
    }
}
