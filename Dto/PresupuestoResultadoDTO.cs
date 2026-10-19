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

        public DateTime FechaEvento { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }

        public int Invitados
        {
            get { return CantidadAdultos + CantidadNinos; }
        }

        public decimal CostoTotal { get; set; }
        public EstadoPresupuesto Estado { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public bool Vigente { get; set; }
    }
}
