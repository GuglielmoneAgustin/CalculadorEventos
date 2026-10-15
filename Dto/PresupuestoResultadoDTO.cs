using System;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
    public class PresupuestoResultadoDTO
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreLugar { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }

        // Datos del evento tal como quedaron cotizados en este presupuesto
        public DateTime FechaEvento { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }

        // Para mostrar en las grillas: "adultos + niños"
        public string Invitados
        {
            get { return CantidadAdultos + " + " + CantidadNinos; }
        }

        public decimal CostoTotal { get; set; }
        public EstadoPresupuesto Estado { get; set; }
        // Es el último generado y no cambiaron el evento ni los precios (solo así puede aprobarse).
        public bool Vigente { get; set; }
    }
}
