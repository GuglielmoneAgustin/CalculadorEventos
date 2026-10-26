using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private const string Columnas = "Id, Nombre, Dni, Mail, Telefono";

        private readonly BaseDatosSqlite baseDatos;

        public ClienteRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Clientes");
        }

        public void Agregar(Cliente cliente)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Clientes (" + Columnas + ") VALUES ($id, $nombre, $dni, $mail, $telefono)";
            CargarParametros(comando, cliente);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(Cliente cliente)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Clientes SET Nombre = $nombre, Dni = $dni, Mail = $mail, Telefono = $telefono WHERE Id = $id";
            CargarParametros(comando, cliente);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El cliente seleccionado no existe.");
            }
        }

        public IReadOnlyList<Cliente> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Clientes ORDER BY Id";

            List<Cliente> clientes = new List<Cliente>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                clientes.Add(Leer(lector));
            }
            return clientes;
        }

        public Cliente ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Clientes WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("El cliente seleccionado no existe.");
            }
            return Leer(lector);
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Clientes WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El cliente seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, Cliente cliente)
        {
            comando.Parameters.AddWithValue("$id", cliente.Id);
            comando.Parameters.AddWithValue("$nombre", cliente.Nombre);
            comando.Parameters.AddWithValue("$dni", cliente.Dni);
            comando.Parameters.AddWithValue("$mail", cliente.Mail);
            comando.Parameters.AddWithValue("$telefono", cliente.Telefono);
        }

        private static Cliente Leer(SqliteDataReader lector)
        {
            return new Cliente(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetString(2),
                lector.GetString(3),
                lector.GetString(4));
        }
    }
}
