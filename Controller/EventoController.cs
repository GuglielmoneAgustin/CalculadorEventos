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
            return servicio.ObtenerPorId(id);
        }

        public List<EventoResultadoDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public List<EventoResultadoDTO> ObtenerProximosAprobados()
        {
            return servicio.ObtenerProximosAprobados();
        }

        public List<PendienteDTO> ObtenerPendientes()
        {
            return servicio.ObtenerPendientes();
        }

        public List<EventoResultadoDTO> ObtenerSinAprobarProximos()
        {
            return servicio.ObtenerSinAprobarProximos();
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
            return servicio.ObtenerItemsMenu(idEvento);
        }

        public decimal CalcularSubtotalPlatos(List<int> idsProductos, int cantidadAdultos, int cantidadNinos)
        {
            return servicio.CalcularSubtotalPlatos(idsProductos, cantidadAdultos, cantidadNinos);
        }

        public decimal ObtenerTotalMenu(int idEvento)
        {
            return servicio.ObtenerTotalMenu(idEvento);
        }

        public decimal CalcularCostoLugar(int idLugar, decimal horasDuracion)
        {
            return servicio.CalcularCostoLugar(idLugar, horasDuracion);
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
            return servicio.ObtenerServiciosEvento(idEvento);
        }
    }
}
