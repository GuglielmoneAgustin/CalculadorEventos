using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;

namespace ProyectoEventos.Service
{
    public class NegocioService
    {
        private readonly IRepository<Negocio> repositorio;

        public NegocioService()
            : this(Repositorios.Negocios)
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

        public void Guardar(NegocioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del negocio son obligatorios.");
            }

            Negocio negocio = ObtenerNegocio();
            Negocio validado = new Negocio(negocio.Id, dto.Nombre, dto.Direccion, dto.Telefono, dto.Mail, dto.Cuit,
                dto.Instagram, dto.MensajePie);

            negocio.Nombre = validado.Nombre;
            negocio.Direccion = validado.Direccion;
            negocio.Telefono = validado.Telefono;
            negocio.Mail = validado.Mail;
            negocio.Cuit = validado.Cuit;
            negocio.Instagram = validado.Instagram;
            negocio.MensajePie = validado.MensajePie;
            repositorio.Actualizar(negocio);
        }

        private Negocio ObtenerNegocio()
        {
            if (repositorio.ObtenerTodos().Count == 0)
            {
                Negocio nuevo = new Negocio(repositorio.ObtenerProximoId(), "Mi negocio", string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, "Gracias por elegirnos");
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
            return dto;
        }
    }
}
