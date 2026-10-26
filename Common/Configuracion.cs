namespace ProyectoEventos.Common
{
    public static class Configuracion
    {
        public const int DiasProximos = 7;

        // Archivo de la base de datos SQLite, junto al ejecutable de la app.
        public static readonly string RutaBaseDatos = Path.Combine(AppContext.BaseDirectory, "eventos.db");
    }
}
