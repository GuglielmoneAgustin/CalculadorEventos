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

        // Total de invitados (adultos + niños), para mostrar en las grillas
        public int Invitados
        {
            get { return CantidadAdultos + CantidadNinos; }
        }

        public EstadoPresupuesto? EstadoPresupuesto { get; set; }

        // Total del presupuesto aprobado; si no hay, el del último generado; null si no hay ninguno.
        public decimal? Total { get; set; }
    }
}
