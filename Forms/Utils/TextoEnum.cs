using ProyectoEventos.Common;

namespace ProyectoEventos.Forms.Utils
{
    // Texto con tildes de los valores de las enumeraciones (los nombres en el código no las llevan).
    internal static class TextoEnum
    {
        public static string Categoria(CategoriasProductos categoria)
        {
            switch (categoria)
            {
                case CategoriasProductos.Guarnicion:
                    return "Guarnición";
                default:
                    return categoria.ToString();
            }
        }

        public static string Restriccion(RestriccionesAlimenticias restriccion)
        {
            switch (restriccion)
            {
                case RestriccionesAlimenticias.Estandar:
                    return "Estándar";
                case RestriccionesAlimenticias.Celiaco:
                    return "Celíaco";
                case RestriccionesAlimenticias.Diabetico:
                    return "Diabético";
                default:
                    return restriccion.ToString();
            }
        }

        // Para usar en el evento Format de un ComboBox o en el CellFormatting de una grilla
        public static string? Texto(object? valor)
        {
            switch (valor)
            {
                case CategoriasProductos categoria:
                    return Categoria(categoria);
                case RestriccionesAlimenticias restriccion:
                    return Restriccion(restriccion);
                default:
                    return valor?.ToString();
            }
        }
    }
}
