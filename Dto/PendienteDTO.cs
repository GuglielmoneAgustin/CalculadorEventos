using System;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
    // Un evento próximo que requiere atención (pantalla de inicio, "Para atender").
    public class PendienteDTO
    {
        public int IdEvento { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
        public TipoPendiente Tipo { get; set; }

        // Qué pasó o qué hay que hacer, por ejemplo "Cambió el precio de Bife de chorizo."
        public string Detalle { get; set; } = string.Empty;
    }
}
