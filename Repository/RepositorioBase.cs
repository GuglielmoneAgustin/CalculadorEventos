using System;
using System.Collections.Generic;
using ProyectoEventos.Domain;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Repository
{
    // Almacenamiento en memoria común a todos los repositorios.
    public abstract class RepositorioBase<T> : IRepository<T> where T : class, IEntidad
    {
        protected readonly List<T> elementos = new List<T>();
        private readonly string nombreEntidad;
        private int proximoId = 1;

        protected RepositorioBase(string nombreEntidad)
        {
            this.nombreEntidad = nombreEntidad;
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

        public IReadOnlyList<T> ObtenerTodos()
        {
            return elementos.AsReadOnly();
        }

        public T? ObtenerPorId(int id)
        {
            return elementos.Find(elemento => elemento.Id == id);
        }

        // Igual que ObtenerPorId pero lanza el error de "no existe" en lugar de devolver null.
        public T ObtenerExistente(int id)
        {
            T? elemento = ObtenerPorId(id);
            if (elemento == null)
            {
                throw new NoEncontradoException("El " + nombreEntidad + " seleccionado no existe.");
            }
            return elemento;
        }

        public void Eliminar(int id)
        {
            if (elementos.RemoveAll(elemento => elemento.Id == id) == 0)
            {
                throw new NoEncontradoException("El " + nombreEntidad + " seleccionado no existe.");
            }
        }
    }
}
