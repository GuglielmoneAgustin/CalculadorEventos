using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class RestriccionRepository : RepositorioBase<Restriccion>
    {
        private static readonly RestriccionRepository instancia = new RestriccionRepository();

        private RestriccionRepository() : base("La restricción seleccionada no existe.")
        {
        }

        public static RestriccionRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
