using System;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        bool TieneClienteAsociado(int idCliente);
        int ContarEventosDeCliente(int idCliente);
        bool TieneLugarAsociado(int idLugar);
        bool TieneProductoAsociado(int idProducto);
        bool TieneServicioAsociado(int idServicio);
        bool HayEventoEnLugarEnHorario(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar);
        bool TieneEventoQueNoEntraEn(Lugar lugar);
    }
}
