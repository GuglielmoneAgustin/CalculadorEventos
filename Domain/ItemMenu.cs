using System;
using System.Collections.Generic;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class ItemMenu
    {
        private List<Producto> componentes;
        private int cantidadAdultos;
        private int cantidadNinos;

        public ItemMenu(int id, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            componentes = new List<Producto>();
            CambiarCantidades(cantidadAdultos, cantidadNinos);
        }

        public int Id { get; private set; }

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

        public void AgregarComponente(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentException("El producto no puede ser nulo.");
            }
            componentes.Add(producto);
        }

        public IReadOnlyList<Producto> ObtenerComponentes()
        {
            return componentes.AsReadOnly();
        }

        public string ObtenerDescripcion()
        {
            List<string> nombres = new List<string>();
            foreach (Producto producto in componentes)
            {
                nombres.Add(producto.Nombre);
            }
            return string.Join(" + ", nombres);
        }

        public bool UsaProducto(int idProducto)
        {
            return componentes.Exists(p => p.Id == idProducto);
        }

        public decimal Subtotal()
        {
            decimal total = 0m;
            foreach (Producto producto in componentes)
            {
                total = total + (producto.PrecioAdulto * cantidadAdultos) + (producto.PrecioNino * cantidadNinos);
            }
            return total;
        }
    }
}
