namespace ProyectoEventos.Forms.Utils
{
    internal static class Formulario
    {
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
                    case DateTimePicker fecha:
                        valores.Add(fecha.Value.ToString("g"));
                        break;
                    default:
                        AgregarValores(control, valores);
                        break;
                }
            }
        }

        public static bool ConfirmarDescartarCambios(bool hayCambios)
        {
            return !hayCambios || Mensajes.Confirmar("Hay datos sin guardar en el formulario. ¿Desea descartarlos?");
        }
    }
}
