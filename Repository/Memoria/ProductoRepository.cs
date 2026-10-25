using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class ProductoRepository : RepositorioBase<Producto>
    {
        private static readonly ProductoRepository instancia = new ProductoRepository();

        private ProductoRepository() : base("producto")
        {
        }

        public static ProductoRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
