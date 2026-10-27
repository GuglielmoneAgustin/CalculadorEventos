using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class ServicioAdicionalRepository : RepositorioBase<ServicioAdicional>
    {
        private static readonly ServicioAdicionalRepository instancia = new ServicioAdicionalRepository();

        private ServicioAdicionalRepository() : base("El servicio seleccionado no existe.")
        {
        }

        public static ServicioAdicionalRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
