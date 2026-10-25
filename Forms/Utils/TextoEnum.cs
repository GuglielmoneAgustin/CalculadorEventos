using System.ComponentModel;
using System.Reflection;
using ProyectoEventos.Common.Enums;

namespace ProyectoEventos.Forms.Utils
{
    internal static class TextoEnum
    {
        public static string Texto(this Enum valor)
        {
            FieldInfo? campo = valor.GetType().GetField(valor.ToString());
            return campo?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? valor.ToString();
        }

        public static string Texto(this EstadoPresupuesto? estado)
        {
            return estado?.Texto() ?? "Sin presupuesto";
        }

        public static string? Texto(object? valor)
        {
            return valor is Enum enumerado ? enumerado.Texto() : valor?.ToString();
        }
    }
}
