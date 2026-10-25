using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Repository.Memoria;

namespace ProyectoEventos.Service
{
    public class NegocioService
    {
        private readonly IRepository<Negocio> repositorio;

        public NegocioService()
            : this(NegocioRepository.ObtenerInstancia())
        {
        }

        public NegocioService(IRepository<Negocio> repositorio)
        {
            this.repositorio = repositorio;
        }

        public NegocioDTO Obtener()
        {
            return MapearADto(ObtenerNegocio());
        }

        public int ObtenerDiasValidez()
        {
            return ObtenerNegocio().DiasValidez;
        }

        public decimal CalcularSenaSugerida(decimal total)
        {
            return ObtenerNegocio().CalcularSenaSugerida(total);
        }

        public void Guardar(NegocioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del negocio son obligatorios.");
            }

            Negocio negocio = ObtenerNegocio();
            Negocio validado = new Negocio(negocio.Id, dto.Nombre, dto.Direccion, dto.Telefono, dto.Mail, dto.Cuit,
                dto.Instagram, dto.MensajePie, dto.DiasValidez, dto.PorcentajeSena);

            negocio.Nombre = validado.Nombre;
            negocio.Direccion = validado.Direccion;
            negocio.Telefono = validado.Telefono;
            negocio.Mail = validado.Mail;
            negocio.Cuit = validado.Cuit;
            negocio.Instagram = validado.Instagram;
            negocio.MensajePie = validado.MensajePie;
            negocio.DiasValidez = validado.DiasValidez;
            negocio.PorcentajeSena = validado.PorcentajeSena;
            repositorio.Actualizar(negocio);
        }

        private Negocio ObtenerNegocio()
        {
            if (repositorio.ObtenerTodos().Count == 0)
            {
                Negocio nuevo = new Negocio(repositorio.ObtenerProximoId(), "Mi negocio", string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, "Gracias por elegirnos", 15, 30);
                repositorio.Agregar(nuevo);
            }
            return repositorio.ObtenerTodos()[0];
        }

        private NegocioDTO MapearADto(Negocio negocio)
        {
            NegocioDTO dto = new NegocioDTO();
            dto.Nombre = negocio.Nombre;
            dto.Direccion = negocio.Direccion;
            dto.Telefono = negocio.Telefono;
            dto.Mail = negocio.Mail;
            dto.Cuit = negocio.Cuit;
            dto.Instagram = negocio.Instagram;
            dto.MensajePie = negocio.MensajePie;
            dto.DiasValidez = negocio.DiasValidez;
            dto.PorcentajeSena = negocio.PorcentajeSena;
            return dto;
        }
    }
}
