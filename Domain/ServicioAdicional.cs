using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class ServicioAdicional : IEntidad
    {
        private const int LargoMaximoNombre = 60;
        private const int LargoMaximoDescripcion = 200;

        private string nombre = string.Empty;
        private string descripcion = string.Empty;
        private decimal monto;

        public ServicioAdicional(int id, string nombre, string descripcion, decimal monto)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Monto = monto;
        }

        public int Id { get; private set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El nombre del servicio no puede estar vacío.");
                }
                if (value.Trim().Length > LargoMaximoNombre)
                {
                    throw new NegocioException("El nombre del servicio no puede tener más de " + LargoMaximoNombre + " caracteres.");
                }
                nombre = value.Trim();
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                string limpio = (value ?? string.Empty).Trim();
                if (limpio.Length > LargoMaximoDescripcion)
                {
                    throw new NegocioException("La descripción del servicio no puede tener más de " + LargoMaximoDescripcion + " caracteres.");
                }
                descripcion = limpio;
            }
        }

        public decimal Monto
        {
            get { return monto; }
            set
            {
                if (value <= 0)
                {
                    throw new NegocioException("El monto debe ser mayor a cero.");
                }
                monto = value;
            }
        }
    }
}