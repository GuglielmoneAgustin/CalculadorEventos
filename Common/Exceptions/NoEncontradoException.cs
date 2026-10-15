namespace ProyectoEventos.Common.Exceptions
{
    // Un dato que se buscó por id (o por pertenencia a un evento) no existe.
    public class NoEncontradoException : NegocioException
    {
        public NoEncontradoException(string mensaje) : base(mensaje)
        {
        }
    }
}
