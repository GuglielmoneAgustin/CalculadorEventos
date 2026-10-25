using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class NegocioRepository : RepositorioBase<Negocio>
    {
        private static readonly NegocioRepository instancia = new NegocioRepository();

        private NegocioRepository() : base("negocio")
        {
        }

        public static NegocioRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
