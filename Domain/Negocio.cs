using System.Text.RegularExpressions;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Negocio : IEntidad
    {
        private static readonly Regex FormatoMail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private string nombre = string.Empty;
        private string direccion = string.Empty;
        private string telefono = string.Empty;
        private string mail = string.Empty;
        private string cuit = string.Empty;
        private string instagram = string.Empty;
        private string mensajePie = string.Empty;
        private int diasValidez;
        private decimal porcentajeSena;

        public Negocio(int id, string nombre, string direccion, string telefono, string mail, string cuit,
            string instagram, string mensajePie, int diasValidez, decimal porcentajeSena)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Mail = mail;
            Cuit = cuit;
            Instagram = instagram;
            MensajePie = mensajePie;
            DiasValidez = diasValidez;
            PorcentajeSena = porcentajeSena;
        }

        public int Id { get; private set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NegocioException("El nombre del negocio no puede estar vacío.");
                }
                nombre = value.Trim();
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = (value ?? string.Empty).Trim();
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

        public string Cuit
        {
            get { return cuit; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    cuit = string.Empty;
                    return;
                }

                int digitos = 0;
                foreach (char caracter in value)
                {
                    if (char.IsDigit(caracter))
                    {
                        digitos++;
                    }
                    else if (caracter != '-' && caracter != ' ')
                    {
                        throw new NegocioException("El CUIT solo puede tener números y guiones (ejemplo: 20-12345678-9).");
                    }
                }
                if (digitos != 11)
                {
                    throw new NegocioException("El CUIT debe tener 11 números (ejemplo: 20-12345678-9).");
                }
                cuit = value.Trim();
            }
        }

        public string Instagram
        {
            get { return instagram; }
            set
            {
                instagram = (value ?? string.Empty).Trim();
            }
        }

        public string MensajePie
        {
            get { return mensajePie; }
            set
            {
                mensajePie = (value ?? string.Empty).Trim();
            }
        }

        public int DiasValidez
        {
            get { return diasValidez; }
            set
            {
                if (value < 0)
                {
                    throw new NegocioException("Los días de validez del presupuesto no pueden ser negativos.");
                }
                diasValidez = value;
            }
        }

        public decimal PorcentajeSena
        {
            get { return porcentajeSena; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new NegocioException("El porcentaje de seña sugerido debe estar entre 0 y 100.");
                }
                porcentajeSena = value;
            }
        }
    }
}
