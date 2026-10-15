using System.Collections.Generic;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository
{
    public interface IRepository<T> where T : class, IEntidad
    {
        void Agregar(T elemento);
        IReadOnlyList<T> ObtenerTodos();
        T? ObtenerPorId(int id);
        T ObtenerExistente(int id);
        void Eliminar(int id);
        int ObtenerProximoId();
    }
}
