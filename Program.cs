using ProyectoEventos.Forms;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => Mensajes.ErrorInesperado(e.Exception);
#if DEBUG
            DatosIniciales.Cargar();
#endif
            Application.Run(new FrmPrincipal());
        }
    }
}
