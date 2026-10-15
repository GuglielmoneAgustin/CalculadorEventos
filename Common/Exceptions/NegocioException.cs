namespace ProyectoEventos.Common.Exceptions
{
    // Base de los errores que se le muestran al usuario (reglas incumplidas o datos que no existen).
    // Cualquier otra excepción es un error de programación y no debe atraparse en los formularios.
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
