using System;

namespace ProyectoEventos.Common.Exceptions
{
    public class ConflictoAprobacionException : ReglaNegocioException
    {
        public ConflictoAprobacionException(string mensaje) : base(mensaje)
        {
        }
    }
}