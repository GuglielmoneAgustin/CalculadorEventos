using System;
using System.Collections.Generic;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
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
        public decimal CostoBaseLugar { get; set; }
        public decimal CostoPorHoraLugar { get; set; }
        public decimal CostoLugar { get; set; }
        public List<ItemMenuResultadoDTO> Items { get; set; } = new List<ItemMenuResultadoDTO>();
        public List<PresupuestoServicioDTO> Servicios { get; set; } = new List<PresupuestoServicioDTO>();
        public decimal SubtotalMenu { get; set; }
        public decimal SubtotalServicios { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
