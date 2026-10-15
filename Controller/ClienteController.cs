using System.Collections.Generic;
using ProyectoEventos.Dto;
using ProyectoEventos.Service;

namespace ProyectoEventos.Controller
{
    public class ClienteController
    {
        private readonly ClienteService servicio;

        public ClienteController()
        {
            servicio = new ClienteService();
        }

        public void Guardar(ClienteDTO dto)
        {
            servicio.Guardar(dto);
        }

        public List<ClienteDTO> ObtenerTodos()
        {
            return servicio.ObtenerTodos();
        }

        public ClienteDTO ObtenerPorId(int id)
        {
            return servicio.ObtenerPorId(id);
        }

        public void Eliminar(int id)
        {
            servicio.Eliminar(id);
        }
    }
}