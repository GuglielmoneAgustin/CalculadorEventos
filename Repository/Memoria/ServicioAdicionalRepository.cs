using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class ServicioAdicionalRepository : RepositorioBase<ServicioAdicional>
    {
        private static readonly ServicioAdicionalRepository instancia = new ServicioAdicionalRepository();

        private ServicioAdicionalRepository() : base("servicio")
        {
        }

        public static ServicioAdicionalRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
