using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class ItemMenu
    {
        private Producto producto = null!;
        private int cantidadAdultos;
        private int cantidadNinos;

        public ItemMenu(int id, Producto producto, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            Producto = producto;
            CambiarCantidades(cantidadAdultos, cantidadNinos);
        }

        public int Id { get; private set; }

        public Producto Producto
        {
            get { return producto; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("El producto no puede ser nulo.");
                }
                producto = value;
            }
        }

        public int CantidadAdultos
        {
            get { return cantidadAdultos; }
        }

        public int CantidadNinos
        {
            get { return cantidadNinos; }
        }

        public void CambiarCantidades(int cantidadAdultos, int cantidadNinos)
        {
            if (cantidadAdultos < 0)
            {
                throw new NegocioException("La cantidad de adultos no puede ser negativa.");
            }
            if (cantidadNinos < 0)
            {
                throw new NegocioException("La cantidad de niños no puede ser negativa.");
            }
            if (cantidadAdultos + cantidadNinos == 0)
            {
                throw new NegocioException("El item del menú debe tener al menos un adulto o un niño.");
            }

            this.cantidadAdultos = cantidadAdultos;
            this.cantidadNinos = cantidadNinos;
        }

        public string ObtenerDescripcion()
        {
            return producto.Nombre;
        }

        public bool UsaProducto(int idProducto)
        {
            return producto.Id == idProducto;
        }

        public decimal Subtotal()
        {
            return producto.CalcularPrecio(cantidadAdultos, cantidadNinos);
        }
    }
}
