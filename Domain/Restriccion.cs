using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Restriccion : IEntidad
    {
        private const int LargoMaximoNombre = 40;

        private string nombre = string.Empty;

        public Restriccion(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int Id { get; private set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El nombre de la restricción no puede estar vacío.");
                }
                if (value.Trim().Length > LargoMaximoNombre)
                {
                    throw new NegocioException("El nombre de la restricción no puede tener más de " + LargoMaximoNombre + " caracteres.");
                }
                nombre = value.Trim();
            }
        }
    }
}
