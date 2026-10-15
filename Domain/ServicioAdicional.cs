using System;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class ServicioAdicional : IEntidad
    {
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
                    throw new ReglaNegocioException("El nombre del servicio no puede estar vacío.");
                }
                nombre = value.Trim();
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = (value ?? string.Empty).Trim();
            }
        }

        public decimal Monto
        {
            get { return monto; }
            set
            {
                if (value <= 0)
                {
                    throw new ReglaNegocioException("El monto debe ser mayor a cero.");
                }
                monto = value;
            }
        }

    }
}