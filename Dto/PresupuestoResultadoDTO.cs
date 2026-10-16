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

        // Total de invitados (adultos + niños), para mostrar en las grillas
        public int Invitados
        {
            get { return CantidadAdultos + CantidadNinos; }
        }

        public decimal CostoTotal { get; set; }
        public EstadoPresupuesto Estado { get; set; }
        // Por qué quedó desactualizado (vacío si está vigente)
        public string Detalle { get; set; } = string.Empty;
        // Es el último generado y no cambiaron el evento ni los precios (solo así puede aprobarse).
        public bool Vigente { get; set; }
    }
}
