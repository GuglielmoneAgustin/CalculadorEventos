using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class MenuEvento
    {
        private List<ItemMenu> items;
        private int proximoIdItem;

        public MenuEvento()
        {
            items = new List<ItemMenu>();
            proximoIdItem = 1;
        }

        public void AgregarPlatos(int cantidadAdultos, int cantidadNinos, List<Producto> productos)
        {
            if (productos == null || productos.Count == 0)
            {
                throw new NegocioException("Seleccione al menos un producto para el plato.");
            }

            List<ItemMenu> nuevos = new List<ItemMenu>();
            int idItem = proximoIdItem;
            foreach (Producto producto in productos)
            {
                nuevos.Add(new ItemMenu(idItem, producto, cantidadAdultos, cantidadNinos));
                idItem++;
            }

            proximoIdItem = idItem;
            items.AddRange(nuevos);
        }

        public bool CambiarCantidades(int idItem, int cantidadAdultos, int cantidadNinos)
        {
            ItemMenu item = ObtenerItemExistente(idItem);
            if (item.CantidadAdultos == cantidadAdultos && item.CantidadNinos == cantidadNinos)
            {
                return false;
            }
            item.CambiarCantidades(cantidadAdultos, cantidadNinos);
            return true;
        }

        public void QuitarItem(int idItem)
        {
            items.Remove(ObtenerItemExistente(idItem));
        }

        public IReadOnlyList<ItemMenu> ObtenerItems()
        {
            return items.AsReadOnly();
        }

        public bool EstaVacio()
        {
            return items.Count == 0;
        }

        public bool UsaProducto(int idProducto)
        {
            return items.Exists(item => item.UsaProducto(idProducto));
        }

        public decimal CalcularSubtotal()
        {
            decimal total = 0m;
            foreach (ItemMenu item in items)
            {
                total += item.Subtotal();
            }
            return total;
        }

        private ItemMenu ObtenerItemExistente(int idItem)
        {
            ItemMenu? item = items.Find(item => item.Id == idItem);
            if (item == null)
            {
                throw new NoEncontradoException("El item de menú no existe.");
            }
            return item;
        }
    }
}
