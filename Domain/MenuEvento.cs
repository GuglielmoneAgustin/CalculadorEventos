using System.Collections.Generic;

namespace ProyectoEventos.Domain
{
    public class MenuEvento
    {
        private List<ItemMenu> items;

        public MenuEvento()
        {
            items = new List<ItemMenu>();
        }

        public void AgregarItem(ItemMenu itemMenu)
        {
            items.Add(itemMenu);
        }

        public bool QuitarItem(int idItem)
        {
            return items.RemoveAll(item => item.Id == idItem) > 0;
        }

        public ItemMenu? ObtenerItem(int idItem)
        {
            return items.Find(item => item.Id == idItem);
        }

        public IReadOnlyList<ItemMenu> ObtenerItems()
        {
            return items.AsReadOnly();
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
    }
}
