using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class ServicioAdicionalRepository : IRepository<ServicioAdicional>
    {
        private const string Columnas = "Id, Nombre, Descripcion, Monto";

        private readonly BaseDatosSqlite baseDatos;

        public ServicioAdicionalRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("ServiciosAdicionales");
        }

        public void Agregar(ServicioAdicional servicio)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO ServiciosAdicionales (" + Columnas + ") VALUES ($id, $nombre, $descripcion, $monto)";
            CargarParametros(comando, servicio);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(ServicioAdicional servicio)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE ServiciosAdicionales SET Nombre = $nombre, Descripcion = $descripcion, Monto = $monto WHERE Id = $id";
            CargarParametros(comando, servicio);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El servicio seleccionado no existe.");
            }
        }

        public IReadOnlyList<ServicioAdicional> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM ServiciosAdicionales ORDER BY Id";

            List<ServicioAdicional> servicios = new List<ServicioAdicional>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                servicios.Add(Leer(lector));
            }
            return servicios;
        }

        public ServicioAdicional ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM ServiciosAdicionales WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("El servicio seleccionado no existe.");
            }
            return Leer(lector);
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM ServiciosAdicionales WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El servicio seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, ServicioAdicional servicio)
        {
            comando.Parameters.AddWithValue("$id", servicio.Id);
            comando.Parameters.AddWithValue("$nombre", servicio.Nombre);
            comando.Parameters.AddWithValue("$descripcion", servicio.Descripcion);
            comando.Parameters.AddWithValue("$monto", servicio.Monto);
        }

        private static ServicioAdicional Leer(SqliteDataReader lector)
        {
            return new ServicioAdicional(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetString(2),
                lector.GetDecimal(3));
        }
    }
}
