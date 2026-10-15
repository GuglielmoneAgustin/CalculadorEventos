using System.Collections.Generic;

namespace ProyectoEventos.Dto
{
    public class ItemMenuDTO
    {
        public List<int> IdsComponentes { get; set; }
        public int CantidadAdultos { get; set; }
        public int CantidadNinos { get; set; }

        public ItemMenuDTO()
        {
            IdsComponentes = new List<int>();
        }
    }
}