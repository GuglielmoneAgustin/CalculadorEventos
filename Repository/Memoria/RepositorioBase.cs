using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Memoria
{
    public abstract class RepositorioBase<T> : IRepository<T> where T : class, IEntidad
    {
        protected readonly List<T> elementos = new List<T>();
        private readonly string mensajeNoEncontrado;
        private int proximoId = 1;

        protected RepositorioBase(string mensajeNoEncontrado)
        {
            this.mensajeNoEncontrado = mensajeNoEncontrado;
        }

        public int ObtenerProximoId()
        {
            int idAsignado = proximoId;
            proximoId++;
            return idAsignado;
        }

        public void Agregar(T elemento)
        {
            elementos.Add(elemento);
        }

        public void Actualizar(T elemento)
        {
            int indice = elementos.FindIndex(existente => existente.Id == elemento.Id);
            if (indice < 0)
            {
                throw new NoEncontradoException(mensajeNoEncontrado);
            }
            elementos[indice] = elemento;
        }

        public IReadOnlyList<T> ObtenerTodos()
        {
            return elementos.AsReadOnly();
        }

        public T ObtenerExistente(int id)
        {
            T? elemento = elementos.Find(e => e.Id == id);
            if (elemento == null)
            {
                throw new NoEncontradoException(mensajeNoEncontrado);
            }
            return elemento;
        }

        public void Eliminar(int id)
        {
            if (elementos.RemoveAll(elemento => elemento.Id == id) == 0)
            {
                throw new NoEncontradoException(mensajeNoEncontrado);
            }
        }
    }
}
