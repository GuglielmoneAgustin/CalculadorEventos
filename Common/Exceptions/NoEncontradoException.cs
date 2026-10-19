namespace ProyectoEventos.Common.Exceptions
{
    public class NoEncontradoException : NegocioException
    {
        public NoEncontradoException(string mensaje) : base(mensaje)
        {
        }
    }
}
