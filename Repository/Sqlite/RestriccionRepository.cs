using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class RestriccionRepository : IRepository<Restriccion>
    {
        private readonly BaseDatosSqlite baseDatos;

        public RestriccionRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Restricciones");
        }

        public void Agregar(Restriccion restriccion)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Restricciones (Id, Nombre) VALUES ($id, $nombre)";
            comando.Parameters.AddWithValue("$id", restriccion.Id);
            comando.Parameters.AddWithValue("$nombre", restriccion.Nombre);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(Restriccion restriccion)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Restricciones SET Nombre = $nombre WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", restriccion.Id);
            comando.Parameters.AddWithValue("$nombre", restriccion.Nombre);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("La restricción seleccionada no existe.");
            }
        }

        public IReadOnlyList<Restriccion> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT Id, Nombre FROM Restricciones ORDER BY Id";

            List<Restriccion> restricciones = new List<Restriccion>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                restricciones.Add(new Restriccion(lector.GetInt32(0), lector.GetString(1)));
            }
            return restricciones;
        }

        public Restriccion ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT Id, Nombre FROM Restricciones WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("La restricción seleccionada no existe.");
            }
            return new Restriccion(lector.GetInt32(0), lector.GetString(1));
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Restricciones WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("La restricción seleccionada no existe.");
            }
        }
    }
}
