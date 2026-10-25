using System.Text.RegularExpressions;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Negocio : IEntidad
    {
        private const int LargoMaximoNombre = 60;
        private const int LargoMaximoDireccion = 100;
        private const int LargoMaximoTelefono = 30;
        private const int LargoMaximoMail = 100;
        private const int LargoMaximoInstagram = 40;
        private const int LargoMaximoMensajePie = 120;
        private const int DiasValidezMaximo = 365;

        private static readonly Regex FormatoMail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private static readonly Regex FormatoTelefono = new Regex(@"^[0-9 +()\-]+$");

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
                nombre = ValidarLargo(value, LargoMaximoNombre, "El nombre del negocio");
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = ValidarLargo(value, LargoMaximoDireccion, "La dirección");
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                string limpio = ValidarLargo(value, LargoMaximoTelefono, "El teléfono");
                if (limpio.Length > 0 && !FormatoTelefono.IsMatch(limpio))
                {
                    throw new NegocioException("El teléfono solo puede tener números, espacios, +, - y paréntesis.");
                }
                telefono = limpio;
            }
        }

        public string Mail
        {
            get { return mail; }
            set
            {
                string limpio = ValidarLargo(value, LargoMaximoMail, "El mail");
                if (limpio.Length > 0 && !FormatoMail.IsMatch(limpio))
                {
                    throw new NegocioException("El mail ingresado no es válido (ejemplo: nombre@dominio.com).");
                }
                mail = limpio;
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
                string limpio = ValidarLargo(value, LargoMaximoInstagram, "El Instagram");
                if (limpio.Contains(' '))
                {
                    throw new NegocioException("El Instagram no puede tener espacios (ejemplo: @minegocio).");
                }
                instagram = limpio;
            }
        }

        public string MensajePie
        {
            get { return mensajePie; }
            set
            {
                mensajePie = ValidarLargo(value, LargoMaximoMensajePie, "El mensaje al pie");
            }
        }

        public int DiasValidez
        {
            get { return diasValidez; }
            set
            {
                if (value < 0 || value > DiasValidezMaximo)
                {
                    throw new NegocioException("Los días de validez del presupuesto deben estar entre 0 y " + DiasValidezMaximo + ".");
                }
                diasValidez = value;
            }
        }

        public decimal PorcentajeSena
        {
            get { return porcentajeSena; }
            set
            {
                if (value < 0 || value >= 100)
                {
                    throw new NegocioException("El porcentaje de seña sugerido debe ser menor a 100 (el 100 % es el pago completo).");
                }
                porcentajeSena = value;
            }
        }

        public decimal CalcularSenaSugerida(decimal total)
        {
            if (total <= 0)
            {
                return 0;
            }
            return Math.Round(total * porcentajeSena / 100, 2);
        }

        private static string ValidarLargo(string? valor, int largoMaximo, string campo)
        {
            string limpio = (valor ?? string.Empty).Trim();
            if (limpio.Length > largoMaximo)
            {
                throw new NegocioException(campo + " no puede tener más de " + largoMaximo + " caracteres.");
            }
            return limpio;
        }
    }
}
