namespace ProyectoEventos.Common
{
    public static class Configuracion
    {
        public const int DiasProximos = 7;

        // Con una seña de al menos este porcentaje del total, el presupuesto aprobado mantiene sus precios.
        public const decimal PorcentajeSenaParaCongelarPrecio = 30;

        public static readonly string RutaBaseDatos = Path.Combine(AppContext.BaseDirectory, "eventos.db");
    }
}
