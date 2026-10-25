using System.Text.RegularExpressions;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Cliente : IEntidad
    {
        private static readonly Regex FormatoMail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private string nombre = string.Empty;
        private string dni = string.Empty;
        private string mail = string.Empty;
        private string telefono = string.Empty;

        public Cliente(int id, string nombre, string dni, string mail, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Dni = dni;
            Mail = mail;
            Telefono = telefono;
        }

        public int Id { get; private set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El nombre del cliente no puede estar vacío.");
                }
                nombre = value.Trim();
            }
        }

        public string Dni
        {
            get { return dni; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El DNI del cliente no puede estar vacío.");
                }
                dni = value.Trim();
            }
        }

        public string Mail
        {
            get { return mail; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    mail = string.Empty;
                }
                else if (!FormatoMail.IsMatch(value.Trim()))
                {
                    throw new NegocioException("El mail ingresado no es válido (ejemplo: nombre@dominio.com).");
                }
                else
                {
                    mail = value.Trim();
                }
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = (value ?? string.Empty).Trim();
            }
        }

        public string ObtenerDescripcion()
        {
            return Nombre + " (DNI " + Dni + ")";
        }
    }
}