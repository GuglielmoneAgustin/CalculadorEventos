using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class ProductoRepository : IRepository<Producto>
    {
        private const string Columnas = "Id, Nombre, PrecioAdulto, PrecioNino, Categoria, Restriccion";

        private readonly BaseDatosSqlite baseDatos;

        public ProductoRepository(BaseDatosSqlite baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Productos");
        }

        public void Agregar(Producto producto)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Productos (" + Columnas + ") VALUES ($id, $nombre, $precioAdulto, $precioNino, $categoria, $restriccion)";
            CargarParametros(comando, producto);
            comando.ExecuteNonQuery();
        }

        public void Actualizar(Producto producto)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Productos SET Nombre = $nombre, PrecioAdulto = $precioAdulto, PrecioNino = $precioNino, " +
                "Categoria = $categoria, Restriccion = $restriccion WHERE Id = $id";
            CargarParametros(comando, producto);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El producto seleccionado no existe.");
            }
        }

        public IReadOnlyList<Producto> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Productos ORDER BY Id";

            List<Producto> productos = new List<Producto>();
            using SqliteDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                productos.Add(Leer(lector));
            }
            return productos;
        }

        public Producto ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT " + Columnas + " FROM Productos WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);

            using SqliteDataReader lector = comando.ExecuteReader();
            if (!lector.Read())
            {
                throw new NoEncontradoException("El producto seleccionado no existe.");
            }
            return Leer(lector);
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Productos WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El producto seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, Producto producto)
        {
            comando.Parameters.AddWithValue("$id", producto.Id);
            comando.Parameters.AddWithValue("$nombre", producto.Nombre);
            comando.Parameters.AddWithValue("$precioAdulto", producto.PrecioAdulto);
            comando.Parameters.AddWithValue("$precioNino", producto.PrecioNino);
            comando.Parameters.AddWithValue("$categoria", (int)producto.Categoria);
            comando.Parameters.AddWithValue("$restriccion", (int)producto.Restriccion);
        }

        private static Producto Leer(SqliteDataReader lector)
        {
            return new Producto(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetDecimal(2),
                lector.GetDecimal(3),
                (CategoriasProductos)lector.GetInt32(4),
                (RestriccionesAlimenticias)lector.GetInt32(5));
        }
    }
}
