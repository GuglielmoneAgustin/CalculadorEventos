namespace ProyectoEventos.Forms.Utils
{
    // Ayudas para los formularios de alta y edición.
    internal static class Formulario
    {
        // Texto con los valores de todos los campos del contenedor. Comparando el de ahora con el del
        // momento en que se cargó (o limpió) el formulario se sabe si hay cambios sin guardar.
        public static string Valores(Control contenedor)
        {
            List<string> valores = new List<string>();
            AgregarValores(contenedor, valores);
            return string.Join("|", valores);
        }

        private static void AgregarValores(Control contenedor, List<string> valores)
        {
            foreach (Control control in contenedor.Controls)
            {
                switch (control)
                {
                    case TextBox texto:
                        valores.Add(texto.Text.Trim());
                        break;
                    case NumericUpDown numero:
                        valores.Add(numero.Value.ToString());
                        break;
                    case ComboBox combo:
                        valores.Add(combo.SelectedIndex.ToString());
                        break;
                    default:
                        AgregarValores(control, valores);
                        break;
                }
            }
        }

        // Pregunta antes de perder datos escritos y no guardados. Devuelve true si se puede continuar.
        public static bool ConfirmarDescartarCambios(bool hayCambios)
        {
            return !hayCambios || Mensajes.Confirmar("Hay datos sin guardar en el formulario. ¿Desea descartarlos?");
        }
    }
}
