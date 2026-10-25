using System.ComponentModel;

namespace ProyectoEventos.Common.Enums
{
    public enum RestriccionesAlimenticias
    {
        [Description("Estándar")] Estandar,
        [Description("Celíaco")] Celiaco,
        Vegetariano,
        [Description("Diabético")] Diabetico,
        Hipertenso
    }
}
