namespace ProyectoEventos.Forms.Utils
{
    internal static class Grilla
    {
        public static DataGridViewColumn Columna(this DataGridView grilla, string nombre)
        {
            return grilla.Columns[nombre]
                ?? throw new InvalidOperationException("La grilla " + grilla.Name + " no tiene la columna " + nombre + ".");
        }

        public static T? Seleccionado<T>(this DataGridView grilla) where T : class
        {
            return grilla.CurrentRow?.DataBoundItem as T;
        }

        public static void SeleccionarFila<T>(this DataGridView grilla, Func<T, bool> condicion) where T : class
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is T elemento && condicion(elemento))
                {
                    DataGridViewCell? primeraVisible = fila.Cells.Cast<DataGridViewCell>().FirstOrDefault(celda => celda.Visible);
                    if (primeraVisible != null)
                    {
                        grilla.CurrentCell = primeraVisible;
                    }
                    fila.Selected = true;
                    return;
                }
            }
        }

        public static void MensajeSinFilas(this DataGridView grilla, Func<string> mensaje)
        {
            grilla.Paint += (sender, e) =>
            {
                if (grilla.Rows.Count > 0)
                {
                    return;
                }

                int arriba = grilla.ColumnHeadersVisible ? grilla.ColumnHeadersHeight : 0;
                Rectangle area = new Rectangle(20, arriba + 20, Math.Max(grilla.Width - 40, 0), 80);
                Font fuente = grilla.DefaultCellStyle.Font ?? grilla.Font;
                TextRenderer.DrawText(e.Graphics, mensaje(), fuente, area, Tema.TextoSuave,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.WordBreak);
            };
            grilla.Resize += (sender, e) => grilla.Invalidate();
        }
    }
}
