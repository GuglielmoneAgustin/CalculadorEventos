namespace ProyectoEventos.Forms.Utils
{
    internal static class Mensajes
    {
        public static void Advertencia(string texto)
        {
            MessageBox.Show(texto, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Informacion(string texto)
        {
            MessageBox.Show(texto, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool Confirmar(string texto)
        {
            return MessageBox.Show(texto, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void ErrorInesperado(Exception excepcion)
        {
            MessageBox.Show("Ocurrió un error inesperado: " + excepcion.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
