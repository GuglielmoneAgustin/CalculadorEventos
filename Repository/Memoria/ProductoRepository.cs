using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public class ProductoRepository : RepositorioBase<Producto>
    {
        private static readonly ProductoRepository instancia = new ProductoRepository();

        private ProductoRepository() : base("El producto seleccionado no existe.")
        {
        }

        public static ProductoRepository ObtenerInstancia()
        {
            return instancia;
        }
    }
}
