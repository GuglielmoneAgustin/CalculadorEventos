namespace ProyectoEventos.Forms.Utils
{
    // Mensaje breve que confirma una acción (por ejemplo "Cliente guardado") y se borra solo,
    // sin un cuadro de diálogo que haya que cerrar.
    internal static class Aviso
    {
        private const int DuracionMilisegundos = 4000;

        public static void Mostrar(Label etiqueta, string texto)
        {
            // Si había un aviso anterior en la misma etiqueta, se reemplaza
            if (etiqueta.Tag is System.Windows.Forms.Timer anterior)
            {
                anterior.Stop();
                anterior.Dispose();
            }

            etiqueta.Text = texto;
            System.Windows.Forms.Timer temporizador = new System.Windows.Forms.Timer();
            temporizador.Interval = DuracionMilisegundos;
            temporizador.Tick += (sender, e) =>
            {
                temporizador.Stop();
                temporizador.Dispose();
                etiqueta.Tag = null;
                if (!etiqueta.IsDisposed)
                {
                    etiqueta.Text = string.Empty;
                }
            };
            etiqueta.Tag = temporizador;
            temporizador.Start();
        }
    }
}
