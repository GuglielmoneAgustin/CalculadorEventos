using ProyectoEventos.Common.Enums;

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

        public int Invitados
        {
            get { return CantidadAdultos + CantidadNinos; }
        }

        public EstadoPresupuesto? EstadoPresupuesto { get; set; }

        public decimal? Total { get; set; }

        public EstadoPago? EstadoPago { get; set; }

        public bool Realizado { get; set; }
    }
}
