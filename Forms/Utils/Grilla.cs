namespace ProyectoEventos.Forms.Utils
{
    internal static class Grilla
    {
        public static DataGridViewColumn Columna(this DataGridView grilla, string nombre)
        {
            return grilla.Columns[nombre]
                ?? throw new InvalidOperationException("La grilla " + grilla.Name + " no tiene la columna " + nombre + ".");
        }

        public static void QuitarSeleccion(this DataGridView grilla)
        {
            grilla.ClearSelection();
            grilla.CurrentCell = null;
        }

        public static T? Seleccionado<T>(this DataGridView grilla) where T : class
        {
            return grilla.CurrentRow?.DataBoundItem as T;
        }

        public static void SeleccionarFila(this DataGridView grilla, string columna, string valor)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                string? valorFila = fila.Cells[columna].Value?.ToString();
                if (valor.Equals(valorFila, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        if (celda.Visible)
                        {
                            grilla.CurrentCell = celda;
                            break;
                        }
                    }
                    fila.Selected = true;
                    return;
                }
            }
        }

        // El mensaje se guarda en el Tag de la grilla y se dibuja mientras no tenga filas.
        public static void MensajeSinFilas(this DataGridView grilla, string mensaje)
        {
            grilla.Tag = mensaje;
            grilla.Paint -= DibujarMensajeSinFilas;
            grilla.Paint += DibujarMensajeSinFilas;
            grilla.Resize -= Redibujar;
            grilla.Resize += Redibujar;
            grilla.Invalidate();
        }

        private static void DibujarMensajeSinFilas(object? sender, PaintEventArgs e)
        {
            if (sender is not DataGridView grilla || grilla.Rows.Count > 0 || grilla.Tag is not string mensaje)
            {
                return;
            }

            int arriba = grilla.ColumnHeadersVisible ? grilla.ColumnHeadersHeight : 0;
            Rectangle area = new Rectangle(20, arriba + 20, Math.Max(grilla.Width - 40, 0), 80);
            Font fuente = grilla.DefaultCellStyle.Font ?? grilla.Font;
            TextRenderer.DrawText(e.Graphics, mensaje, fuente, area, Tema.TextoSuave,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.WordBreak);
        }

        private static void Redibujar(object? sender, EventArgs e)
        {
            if (sender is Control control)
            {
                control.Invalidate();
            }
        }
    }
}
