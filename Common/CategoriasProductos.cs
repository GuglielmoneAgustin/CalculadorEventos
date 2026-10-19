using System.ComponentModel;

namespace ProyectoEventos.Common
{
    public enum CategoriasProductos
    {
        Principal,
        [Description("Guarnición")] Guarnicion,
        Bebida,
        Postre
    }
}
