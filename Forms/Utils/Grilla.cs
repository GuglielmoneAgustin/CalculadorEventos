namespace ProyectoEventos.Forms.Utils
{
    // Accesos comunes a las grillas enlazadas a listas de DTOs.
    internal static class Grilla
    {
        // Columna generada por el DataSource. Si no existe es un error de programación (nombre mal escrito).
        public static DataGridViewColumn Columna(this DataGridView grilla, string nombre)
        {
            return grilla.Columns[nombre]
                ?? throw new InvalidOperationException("La grilla " + grilla.Name + " no tiene la columna " + nombre + ".");
        }

        // DTO de la fila actual, o null si no hay ninguna fila seleccionada.
        public static T? Seleccionado<T>(this DataGridView grilla) where T : class
        {
            return grilla.CurrentRow?.DataBoundItem as T;
        }
    }
}
