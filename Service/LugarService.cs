using System;
using System.Collections.Generic;
using ProyectoEventos.Domain;
using ProyectoEventos.Dto;
using ProyectoEventos.Repository;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Service
{
    public class LugarService
    {
        private readonly IRepository<Lugar> repositorio;
        private readonly IEventoRepository eventoRepositorio;

        public LugarService()
            : this(LugarRepository.ObtenerInstancia(), EventoRepository.ObtenerInstancia())
        {
        }

        public LugarService(IRepository<Lugar> repositorio, IEventoRepository eventoRepositorio)
        {
            this.repositorio = repositorio;
            this.eventoRepositorio = eventoRepositorio;
        }

        public void Guardar(LugarDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos del lugar son obligatorios.");
            }

            if (ExisteNombre(dto.Nombre, dto.Id))
            {
                throw new NegocioException("Ya existe un lugar con ese nombre.");
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

        private void Crear(LugarDTO dto)
        {
            int nuevoId = repositorio.ObtenerProximoId();
            Lugar nuevoLugar = new Lugar(nuevoId, dto.Nombre, dto.CostoBase, dto.CostoPorHora, dto.Capacidad, dto.Ubicacion);
            repositorio.Agregar(nuevoLugar);
        }

        public List<LugarDTO> ObtenerTodos()
        {
            return repositorio.ObtenerTodos().Select(MapearADto).ToList();
        }

        public LugarDTO ObtenerPorId(int id)
        {
            return MapearADto(repositorio.ObtenerExistente(id));
        }

        private void Editar(LugarDTO dto)
        {
            Lugar lugar = repositorio.ObtenerExistente(dto.Id);
            Lugar validado = new Lugar(lugar.Id, dto.Nombre, dto.CostoBase, dto.CostoPorHora, dto.Capacidad, dto.Ubicacion);
            if (eventoRepositorio.TieneEventoQueNoEntraEn(validado))
            {
                throw new NegocioException("No se puede reducir la capacidad: hay eventos en este lugar con más invitados.");
            }

            lugar.Nombre = validado.Nombre;
            lugar.CostoBase = validado.CostoBase;
            lugar.CostoPorHora = validado.CostoPorHora;
            lugar.Capacidad = validado.Capacidad;
            lugar.Ubicacion = validado.Ubicacion;
        }

        public void Eliminar(int id)
        {
            if (eventoRepositorio.TieneLugarAsociado(id))
            {
                throw new NegocioException("No se puede eliminar el lugar porque está asociado a un evento.");
            }
            repositorio.Eliminar(id);
        }

        private bool ExisteNombre(string nombre, int idAIgnorar)
        {
            string? normalizado = nombre?.Trim();
            return repositorio.ObtenerTodos().Any(lugar =>
                lugar.Id != idAIgnorar && lugar.Nombre.Equals(normalizado, StringComparison.OrdinalIgnoreCase));
        }

        private LugarDTO MapearADto(Lugar lugar)
        {
            LugarDTO dto = new LugarDTO();
            dto.Id = lugar.Id;
            dto.Nombre = lugar.Nombre;
            dto.CostoBase = lugar.CostoBase;
            dto.CostoPorHora = lugar.CostoPorHora;
            dto.Capacidad = lugar.Capacidad;
            dto.Ubicacion = lugar.Ubicacion;
            dto.Descripcion = lugar.ObtenerDescripcion();
            return dto;
        }
    }
}
