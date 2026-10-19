using System;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Lugar : IEntidad
    {
        private string nombre = string.Empty;
        private decimal costoBase;
        private decimal costoPorHora;
        private int capacidad;
        private string ubicacion = string.Empty;

        public Lugar(int id, string nombre, decimal costoBase, decimal costoPorHora, int capacidad, string ubicacion)
        {
            Id = id;
            Nombre = nombre;
            CostoBase = costoBase;
            CostoPorHora = costoPorHora;
            Capacidad = capacidad;
            Ubicacion = ubicacion;
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El nombre del lugar no puede estar vacío.");
                }
                nombre = value.Trim();
            }
        }

        public int Id { get; private set; }

        public decimal CostoBase
        {
            get { return costoBase; }
            set
            {
                if (value < 0)
                {
                    throw new NegocioException("El costo base no puede ser negativo.");
                }
                costoBase = value;
            }
        }

        public decimal CostoPorHora
        {
            get { return costoPorHora; }
            set
            {
                if (value < 0)
                {
                    throw new NegocioException("El costo por hora no puede ser negativo.");
                }
                costoPorHora = value;
            }
        }

        public int Capacidad
        {
            get { return capacidad; }
            set
            {
                if (value <= 0)
                {
                    throw new NegocioException("La capacidad debe ser mayor a cero.");
                }
                capacidad = value;
            }
        }
        public string Ubicacion
        {
            get { return ubicacion; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("La ubicación del lugar no puede estar vacía.");
                }
                ubicacion = value.Trim();
            }
        }

        public decimal CalcularCosto(decimal horas)
        {
            return CostoBase + CostoPorHora * horas;
        }

        public bool AdmiteInvitados(int cantidadInvitados)
        {
            return cantidadInvitados <= Capacidad;
        }

        public string ObtenerDescripcion()
        {
            return Nombre + " (Capacidad " + Capacidad + ")";
        }
    }
}