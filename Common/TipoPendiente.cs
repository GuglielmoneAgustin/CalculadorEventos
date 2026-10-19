using System.ComponentModel;

namespace ProyectoEventos.Common
{
    public enum TipoPendiente
    {
        [Description("Sin presupuesto")] SinPresupuesto,
        Desactualizado,
        [Description("Esperando respuesta")] EsperandoRespuesta
    }
}
