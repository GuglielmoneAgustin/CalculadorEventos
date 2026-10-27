using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class EventoRepository : RepositorioBase<Evento>
    {
        private static readonly EventoRepository instancia = new EventoRepository();

        private EventoRepository() : base("El evento seleccionado no existe.")
        {
        }

        public static EventoRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
