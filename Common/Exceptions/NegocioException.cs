namespace ProyectoEventos.Common.Exceptions
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
