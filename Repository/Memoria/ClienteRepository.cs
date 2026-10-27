using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class ClienteRepository : RepositorioBase<Cliente>
    {
        private static readonly ClienteRepository instancia = new ClienteRepository();

        private ClienteRepository() : base("El cliente seleccionado no existe.")
        {
        }

        public static ClienteRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
