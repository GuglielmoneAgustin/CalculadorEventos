using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class LugarRepository : RepositorioBase<Lugar>
    {
        private static readonly LugarRepository instancia = new LugarRepository();

        private LugarRepository() : base("lugar")
        {
        }

        public static LugarRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
