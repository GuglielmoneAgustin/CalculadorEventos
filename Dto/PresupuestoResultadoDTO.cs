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
        public decimal CostoTotal { get; set; }
        public EstadoPresupuesto Estado { get; set; }
    }
}