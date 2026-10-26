using System.Globalization;
using System.Runtime.CompilerServices;

namespace ProyectoEventos.Tests
{
    // Igual que Program.cs: montos y fechas con formato argentino, sin depender de la configuración de la máquina.
    internal static class Cultura
    {
        [ModuleInitializer]
        internal static void UsarFormatoArgentino()
        {
            CultureInfo cultura = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = cultura;
            CultureInfo.DefaultThreadCurrentUICulture = cultura;
            CultureInfo.CurrentCulture = cultura;
            CultureInfo.CurrentUICulture = cultura;
        }
    }
}
