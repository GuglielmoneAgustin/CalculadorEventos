using System;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository
{
    public class EventoRepository : RepositorioBase<Evento>, IEventoRepository
    {
        private static readonly EventoRepository instancia = new EventoRepository();

        private EventoRepository() : base("evento")
        {
        }

        public static EventoRepository ObtenerInstancia()
        {
            return instancia;
        }

        public bool TieneClienteAsociado(int idCliente)
        {
            return elementos.Exists(evento => evento.TieneCliente(idCliente));
        }

        public bool TieneLugarAsociado(int idLugar)
        {
            return elementos.Exists(evento => evento.TieneLugar(idLugar));
        }

        public bool TieneProductoAsociado(int idProducto)
        {
            return elementos.Exists(evento => evento.UsaProducto(idProducto));
        }

        public bool TieneServicioAsociado(int idServicio)
        {
            return elementos.Exists(evento => evento.TieneServicio(idServicio));
        }

        public bool HayEventoEnLugarEnHorario(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar)
        {
            return elementos.Exists(evento =>
                evento.Id != idEventoAIgnorar && evento.TieneLugar(idLugar) && evento.SeSuperponeCon(fecha, horasDuracion));
        }

        public bool TieneEventoQueNoEntraEn(Lugar lugar)
        {
            return elementos.Exists(evento => evento.TieneLugar(lugar.Id) && !lugar.AdmiteInvitados(evento.TotalInvitados()));
        }
    }
}
