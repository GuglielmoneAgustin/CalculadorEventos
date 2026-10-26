using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class LugarRepository : IRepository<Lugar>
    {
        private const string Columnas = "Id, Nombre, CostoBase, CostoPorHora, Capacidad, Ubicacion";

        private readonly BaseDatosSqlite baseDatos;

        public LugarRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Lugares");
        }

        public void Agregar(Lugar lugar)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Lugares (" + Columnas + ") VALUES ($id, $nombre, $costoBase, $costoPorHora, $capacidad, $ubicacion)";
            CargarParametros(comando, lugar);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(Lugar lugar)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Lugares SET Nombre = $nombre, CostoBase = $costoBase, CostoPorHora = $costoPorHora, " +
                "Capacidad = $capacidad, Ubicacion = $ubicacion WHERE Id = $id";
            CargarParametros(comando, lugar);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El lugar seleccionado no existe.");
            }
        }

        public IReadOnlyList<Lugar> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Lugares ORDER BY Id";

            List<Lugar> lugares = new List<Lugar>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lugares.Add(Leer(lector));
            }
            return lugares;
        }

        public Lugar ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Lugares WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("El lugar seleccionado no existe.");
            }
            return Leer(lector);
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Lugares WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El lugar seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, Lugar lugar)
        {
            comando.Parameters.AddWithValue("$id", lugar.Id);
            comando.Parameters.AddWithValue("$nombre", lugar.Nombre);
            comando.Parameters.AddWithValue("$costoBase", lugar.CostoBase);
            comando.Parameters.AddWithValue("$costoPorHora", lugar.CostoPorHora);
            comando.Parameters.AddWithValue("$capacidad", lugar.Capacidad);
            comando.Parameters.AddWithValue("$ubicacion", lugar.Ubicacion);
        }

        private static Lugar Leer(SqliteDataReader lector)
        {
            return new Lugar(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetDecimal(2),
                lector.GetDecimal(3),
                lector.GetInt32(4),
                lector.GetString(5));
        }
    }
}
