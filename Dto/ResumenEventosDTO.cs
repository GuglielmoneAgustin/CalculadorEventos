namespace ProyectoEventos.Dto
{
    // Números que se muestran en las tarjetas de la pantalla de inicio.
    public class ResumenEventosDTO
    {
        // Eventos aprobados que todavía no ocurrieron
        public int ProximosAprobados { get; set; }

        // Eventos cuyo presupuesto está esperando respuesta del cliente
        public int EnEspera { get; set; }

        // Suma de los presupuestos aprobados de los eventos de este mes
        public decimal TotalAprobadoMes { get; set; }
    }
}
