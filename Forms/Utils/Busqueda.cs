using System.Globalization;
using System.Text;

namespace ProyectoEventos.Forms.Utils
{
    // Filtro de las grillas: cada palabra buscada tiene que aparecer en alguno de los campos.
    // No distingue mayúsculas ni tildes ("maria salon" encuentra "María González - Salón Jardín").
    internal static class Busqueda
    {
        public static List<T> Filtrar<T>(List<T> elementos, string texto, Func<T, IEnumerable<string>> campos)
        {
            string[] palabras = Normalizar(texto).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length == 0)
            {
                return elementos;
            }

            return elementos.Where(elemento =>
            {
                List<string> valores = campos(elemento).Select(Normalizar).ToList();
                return palabras.All(palabra => valores.Any(valor => valor.Contains(palabra)));
            }).ToList();
        }

        private static string Normalizar(string? texto)
        {
            string descompuesto = (texto ?? string.Empty).Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder(descompuesto.Length);
            foreach (char caracter in descompuesto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                {
                    resultado.Append(caracter);
                }
            }
            return resultado.ToString();
        }
    }
}
