using System;
using ProyectoEventos.Common;

namespace ProyectoEventos.Dto
{
    public class EventoResultadoDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdLugar { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreLugar { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal HorasDuracion { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
        public EstadoPresupuesto? EstadoPresupuesto { get; set; }
    }
}