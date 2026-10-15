using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository
{
    public class ClienteRepository : RepositorioBase<Cliente>
    {
        private static readonly ClienteRepository instancia = new ClienteRepository();

        private ClienteRepository() : base("cliente")
        {
        }

        public static ClienteRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
