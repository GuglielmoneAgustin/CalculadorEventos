using System;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
    public class PendienteDTO
    {
        public int IdEvento { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
        public TipoPendiente Tipo { get; set; }

        public string Detalle { get; set; } = string.Empty;
    }
}
