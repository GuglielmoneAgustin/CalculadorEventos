namespace ProyectoEventos.Common.Exceptions
{
    // Se incumple una regla del negocio (dato inválido, duplicado, operación no permitida).
    public class ReglaNegocioException : NegocioException
    {
        public ReglaNegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
