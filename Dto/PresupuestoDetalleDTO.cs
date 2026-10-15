using System;
using System.Collections.Generic;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
    // Todo lo que quedó pactado en un presupuesto; es lo que se imprime en el ticket.
    public class PresupuestoDetalleDTO
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public EstadoPresupuesto Estado { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreLugar { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
        public decimal HorasDuracion { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public decimal CostoLugar { get; set; }
        public List<PresupuestoItemDTO> Items { get; set; } = new List<PresupuestoItemDTO>();
        public List<PresupuestoServicioDTO> Servicios { get; set; } = new List<PresupuestoServicioDTO>();
        public decimal SubtotalMenu { get; set; }
        public decimal SubtotalServicios { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
