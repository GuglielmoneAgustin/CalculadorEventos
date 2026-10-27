using ProyectoEventos.Common.Enums;

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
        public decimal CostoHorasLugar { get; set; }
        public decimal CostoLugar { get; set; }
        public List<ItemMenuResultadoDTO> Items { get; set; } = new List<ItemMenuResultadoDTO>();
        public List<PresupuestoServicioDTO> Servicios { get; set; } = new List<PresupuestoServicioDTO>();
        public decimal SubtotalMenu { get; set; }
        public decimal SubtotalServicios { get; set; }
        public decimal CostoTotal { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public List<PagoDTO> Pagos { get; set; } = new List<PagoDTO>();
        public decimal TotalPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal Saldo { get; set; }
    }
}
