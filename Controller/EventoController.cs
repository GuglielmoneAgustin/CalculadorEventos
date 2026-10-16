using System.Collections.Generic;
using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class EventoController
    {
        private readonly EventoService servicio;

        public EventoController()
        {
            servicio = new EventoService();
        }

        public EventoResultadoDTO Guardar(EventoDTO dto)
        {
            return servicio.Guardar(dto);
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }

        public EventoResultadoDTO ObtenerPorId(int id)
        {
            return servicio.ObtenerPorIdComoDto(id);
        }

        public List<EventoResultadoDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodosComoDto();
        }

        public List<EventoResultadoDTO> ObtenerProximosAprobados()
        {
            return servicio.ObtenerProximosAprobadosComoDto();
        }

        public ResumenEventosDTO ObtenerResumen()
        {
            return servicio.ObtenerResumen();
        }

        public void AgregarItemAlMenu(int idEvento, ItemMenuDTO itemDto)
        {
            servicio.AgregarItemAlMenu(idEvento, itemDto);
        }

        public void AgregarPlatosAlMenu(int idEvento, int cantidadAdultos, int cantidadNinos, List<int> idsProductos)
        {
            servicio.AgregarPlatosAlMenu(idEvento, cantidadAdultos, cantidadNinos, idsProductos);
        }

        public void CambiarCantidadesItem(int idEvento, int idItem, int cantidadAdultos, int cantidadNinos)
        {
            servicio.CambiarCantidadesItem(idEvento, idItem, cantidadAdultos, cantidadNinos);
        }

        public void QuitarItemDelMenu(int idEvento, int idItem)
        {
            servicio.QuitarItemDelMenu(idEvento, idItem);
        }

        public List<ItemMenuResultadoDTO> ObtenerItemsMenu(int idEvento)
        {
            return servicio.ObtenerItemsMenuComoDto(idEvento);
        }

        public decimal ObtenerTotalMenu(int idEvento)
        {
            return servicio.ObtenerTotalMenu(idEvento);
        }

        public void CopiarMenuYServicios(int idEventoOrigen, int idEventoDestino)
        {
            servicio.CopiarMenuYServicios(idEventoOrigen, idEventoDestino);
        }

        public bool LugarDisponible(int idLugar, DateTime fecha, decimal horasDuracion, int idEventoAIgnorar)
        {
            return servicio.LugarDisponible(idLugar, fecha, horasDuracion, idEventoAIgnorar);
        }

        public void AgregarServicioAlEvento(int idEvento, int idServicio)
        {
            servicio.AgregarServicioAlEvento(idEvento, idServicio);
        }

        public void QuitarServicioDelEvento(int idEvento, int idServicio)
        {
            servicio.QuitarServicioDelEvento(idEvento, idServicio);
        }

        public List<ServicioAdicionalDTO> ObtenerServiciosEvento(int idEvento)
        {
            return servicio.ObtenerServiciosEventoComoDto(idEvento);
        }
    }
}
