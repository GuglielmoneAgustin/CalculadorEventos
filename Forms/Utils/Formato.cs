namespace ProyectoEventos.Forms.Utils
{
    internal static class Formato
    {
        public const string FechaHora = "dd/MM/yyyy HH:mm";
        public const string Fecha = "dd/MM/yyyy";
        public const string Moneda = "$ #,##0.00";

        public static string FechaLarga(DateTime fecha)
        {
            return Capitalizar(fecha.ToString("dddd d 'de' MMMM 'de' yyyy"));
        }

        public static string FechaCorta(DateTime fecha)
        {
            return Capitalizar(fecha.ToString("ddd dd/MM, HH:mm").Replace(".", ""));
        }

        public static string Horario(DateTime inicio, decimal horasDuracion)
        {
            return inicio.ToString("HH:mm") + " a " + inicio.AddHours((double)horasDuracion).ToString("HH:mm");
        }

        public static string Falta(DateTime fecha)
        {
            int dias = DiasHasta(fecha);
            if (dias <= 0)
            {
                return "Hoy";
            }
            return dias == 1 ? "Mañana" : "En " + dias + " días";
        }

        public static int DiasHasta(DateTime fecha)
        {
            return (fecha.Date - DateTime.Today).Days;
        }

        private static string Capitalizar(string texto)
        {
            return texto.Length == 0 ? texto : char.ToUpper(texto[0]) + texto.Substring(1);
        }
    }
}
