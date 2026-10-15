using System;

namespace ProyectoEventos.Dto
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdLugar { get; set; }
        public DateTime Fecha { get; set; }
        public decimal HorasDuracion { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }
    }
}