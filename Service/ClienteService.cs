using System;
using System.Collections.Generic;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Service
{
    public class ClienteService
    {
        private readonly IRepository<Cliente> repositorio;
        private readonly IEventoRepository eventoRepositorio;

        public ClienteService()
            : this(ClienteRepository.ObtenerInstancia(), EventoRepository.ObtenerInstancia())
        {
        }

        // Los repositorios se reciben por constructor: el servicio depende de las interfaces, no de las clases concretas.
        public ClienteService(IRepository<Cliente> repositorio, IEventoRepository eventoRepositorio)
        {
            this.repositorio = repositorio;
            this.eventoRepositorio = eventoRepositorio;
        }

        public void Guardar(ClienteDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del cliente son obligatorios.");
            }

            if (ExisteDni(dto.Dni, dto.Id))
            {
                throw new ReglaNegocioException("Ya existe un cliente con ese DNI.");
            }

            if (dto.Id == 0)
            {
                Crear(dto);
            }
            else
            {
                Editar(dto);
            }
        }

        private void Crear(ClienteDTO dto)
        {
            int nuevoId = repositorio.ObtenerProximoId();
            Cliente nuevoCliente = new Cliente(nuevoId, dto.Nombre, dto.Dni, dto.Mail, dto.Telefono);
            repositorio.Agregar(nuevoCliente);
        }

        public List<ClienteDTO> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().Select(MapearADto).ToList();
        }

        public ClienteDTO ObtenerPorId(int id)
        {
            return MapearADto(repositorio.ObtenerExistente(id));
        }

        private void Editar(ClienteDTO dto)
        {
            Cliente cliente = repositorio.ObtenerExistente(dto.Id);
            // Se arma un Cliente con los datos nuevos para validarlos todos antes de tocar el original:
            // si alguno es inválido se lanza la excepción y el cliente queda sin cambios.
            Cliente validado = new Cliente(cliente.Id, dto.Nombre, dto.Dni, dto.Mail, dto.Telefono);

            cliente.Nombre = validado.Nombre;
            cliente.Dni = validado.Dni;
            cliente.Mail = validado.Mail;
            cliente.Telefono = validado.Telefono;
        }

        public void Eliminar(int id)
        {
            if (eventoRepositorio.TieneClienteAsociado(id))
            {
                throw new ReglaNegocioException("No se puede eliminar el cliente porque está asociado a un evento.");
            }
            repositorio.Eliminar(id);
        }

        private bool ExisteDni(string dni, int idAIgnorar)
        {
            string? normalizado = dni?.Trim();
            return repositorio.ObtenerTodos().Any(cliente =>
                cliente.Id != idAIgnorar && cliente.Dni.Equals(normalizado, StringComparison.OrdinalIgnoreCase));
        }

        private ClienteDTO MapearADto(Cliente cliente)
        {
            ClienteDTO dto = new ClienteDTO();
            dto.Id = cliente.Id;
            dto.Nombre = cliente.Nombre;
            dto.Dni = cliente.Dni;
            dto.Mail = cliente.Mail;
            dto.Telefono = cliente.Telefono;
            dto.Descripcion = cliente.ObtenerDescripcion();
            dto.CantidadEventos = eventoRepositorio.ContarEventosDeCliente(cliente.Id);
            return dto;
        }
    }
}
