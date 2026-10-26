using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class NegocioRepository : IRepository<Negocio>
    {
        private const string Columnas = "Id, Nombre, Direccion, Telefono, Mail, Cuit, Instagram, MensajePie";

        private readonly BaseDatosSqlite baseDatos;

        public NegocioRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Negocio");
        }

        public void Agregar(Negocio negocio)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Negocio (" + Columnas + ") VALUES " +
                "($id, $nombre, $direccion, $telefono, $mail, $cuit, $instagram, $mensajePie)";
            CargarParametros(comando, negocio);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(Negocio negocio)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Negocio SET Nombre = $nombre, Direccion = $direccion, Telefono = $telefono, Mail = $mail, " +
                "Cuit = $cuit, Instagram = $instagram, MensajePie = $mensajePie WHERE Id = $id";
            CargarParametros(comando, negocio);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El negocio seleccionado no existe.");
            }
        }

        public IReadOnlyList<Negocio> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Negocio ORDER BY Id";

            List<Negocio> negocios = new List<Negocio>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                negocios.Add(Leer(lector));
            }
            return negocios;
        }

        public Negocio ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Negocio WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("El negocio seleccionado no existe.");
            }
            return Leer(lector);
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Negocio WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El negocio seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, Negocio negocio)
        {
            comando.Parameters.AddWithValue("$id", negocio.Id);
            comando.Parameters.AddWithValue("$nombre", negocio.Nombre);
            comando.Parameters.AddWithValue("$direccion", negocio.Direccion);
            comando.Parameters.AddWithValue("$telefono", negocio.Telefono);
            comando.Parameters.AddWithValue("$mail", negocio.Mail);
            comando.Parameters.AddWithValue("$cuit", negocio.Cuit);
            comando.Parameters.AddWithValue("$instagram", negocio.Instagram);
            comando.Parameters.AddWithValue("$mensajePie", negocio.MensajePie);
        }

        private static Negocio Leer(SqliteDataReader lector)
        {
            return new Negocio(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetString(2),
                lector.GetString(3),
                lector.GetString(4),
                lector.GetString(5),
                lector.GetString(6),
                lector.GetString(7));
        }
    }
}
