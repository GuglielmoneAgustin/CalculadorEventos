using System.ComponentModel;

namespace ProyectoEventos.Common
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
