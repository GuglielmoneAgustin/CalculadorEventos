using System.ComponentModel;

namespace ProyectoEventos.Common.Enums
{
    public enum TipoPendiente
    {
        [Description("Sin presupuesto")] SinPresupuesto,
        Desactualizado,
        [Description("Esperando respuesta")] EsperandoRespuesta,
        [Description("Falta cobrar")] FaltaCobrar
    }
}
