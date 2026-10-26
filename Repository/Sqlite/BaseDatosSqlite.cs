using Microsoft.Data.Sqlite;

namespace ProyectoEventos.Repository.Sqlite
{
    // Archivo de la base de datos: abre conexiones, crea las tablas y entrega los ids nuevos.
    public class BaseDatosSqlite
    {
        private readonly string cadenaConexion;

        public BaseDatosSqlite(string rutaArchivo)
        {
            SqliteConnectionStringBuilder constructor = new SqliteConnectionStringBuilder();
            constructor.DataSource = rutaArchivo;
            constructor.ForeignKeys = true;
            cadenaConexion = constructor.ToString();
        }

        public SqliteConnection AbrirConexion()
        {
            SqliteConnection conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        public void CrearTablasSiNoExisten()
        {
            using SqliteConnection conexion = AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Secuencias (
                    Tabla  TEXT PRIMARY KEY,
                    Ultimo INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Clientes (
                    Id       INTEGER PRIMARY KEY,
                    Nombre   TEXT NOT NULL,
                    Dni      TEXT NOT NULL UNIQUE,
                    Mail     TEXT NOT NULL,
                    Telefono TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Lugares (
                    Id           INTEGER PRIMARY KEY,
                    Nombre       TEXT NOT NULL,
                    CostoBase    TEXT NOT NULL,
                    CostoPorHora TEXT NOT NULL,
                    Capacidad    INTEGER NOT NULL,
                    Ubicacion    TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Productos (
                    Id           INTEGER PRIMARY KEY,
                    Nombre       TEXT NOT NULL,
                    PrecioAdulto TEXT NOT NULL,
                    PrecioNino   TEXT NOT NULL,
                    Categoria    INTEGER NOT NULL,
                    Restriccion  INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS ServiciosAdicionales (
                    Id          INTEGER PRIMARY KEY,
                    Nombre      TEXT NOT NULL,
                    Descripcion TEXT NOT NULL,
                    Monto       TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Negocio (
                    Id             INTEGER PRIMARY KEY,
                    Nombre         TEXT NOT NULL,
                    Direccion      TEXT NOT NULL,
                    Telefono       TEXT NOT NULL,
                    Mail           TEXT NOT NULL,
                    Cuit           TEXT NOT NULL,
                    Instagram      TEXT NOT NULL,
                    MensajePie     TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Eventos (
                    Id                   INTEGER PRIMARY KEY,
                    IdCliente            INTEGER NOT NULL REFERENCES Clientes (Id),
                    IdLugar              INTEGER NOT NULL REFERENCES Lugares (Id),
                    Fecha                TEXT NOT NULL,
                    HorasDuracion        TEXT NOT NULL,
                    CantidadAdultos      INTEGER NOT NULL,
                    CantidadNinos        INTEGER NOT NULL,
                    ProximoIdItem        INTEGER NOT NULL,
                    ProximoIdPresupuesto INTEGER NOT NULL,
                    HuboCambios          INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS ItemsMenu (
                    IdEvento        INTEGER NOT NULL REFERENCES Eventos (Id) ON DELETE CASCADE,
                    Id              INTEGER NOT NULL,
                    IdProducto      INTEGER NOT NULL REFERENCES Productos (Id),
                    CantidadAdultos INTEGER NOT NULL,
                    CantidadNinos   INTEGER NOT NULL,
                    PRIMARY KEY (IdEvento, Id)
                );

                CREATE TABLE IF NOT EXISTS EventoServicios (
                    IdEvento   INTEGER NOT NULL REFERENCES Eventos (Id) ON DELETE CASCADE,
                    IdServicio INTEGER NOT NULL REFERENCES ServiciosAdicionales (Id),
                    Orden      INTEGER NOT NULL,
                    PRIMARY KEY (IdEvento, IdServicio)
                );

                CREATE TABLE IF NOT EXISTS Presupuestos (
                    IdEvento          INTEGER NOT NULL REFERENCES Eventos (Id) ON DELETE CASCADE,
                    Id                INTEGER NOT NULL,
                    FechaGeneracion   TEXT NOT NULL,
                    Estado            INTEGER NOT NULL,
                    CostoBaseLugar    TEXT NOT NULL,
                    CostoPorHoraLugar TEXT NOT NULL,
                    NombreCliente     TEXT NOT NULL,
                    NombreLugar       TEXT NOT NULL,
                    FechaEvento       TEXT NOT NULL,
                    HorasDuracion     TEXT NOT NULL,
                    CantidadAdultos   INTEGER NOT NULL,
                    CantidadNinos     INTEGER NOT NULL,
                    Sena              TEXT NOT NULL,
                    FechaSena         TEXT,
                    FechaPago         TEXT,
                    PRIMARY KEY (IdEvento, Id)
                );

                CREATE TABLE IF NOT EXISTS PresupuestoItems (
                    IdEvento        INTEGER NOT NULL,
                    IdPresupuesto   INTEGER NOT NULL,
                    Orden           INTEGER NOT NULL,
                    Descripcion     TEXT NOT NULL,
                    CantidadAdultos INTEGER NOT NULL,
                    CantidadNinos   INTEGER NOT NULL,
                    Subtotal        TEXT NOT NULL,
                    PRIMARY KEY (IdEvento, IdPresupuesto, Orden),
                    FOREIGN KEY (IdEvento, IdPresupuesto) REFERENCES Presupuestos (IdEvento, Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS PresupuestoServicios (
                    IdEvento      INTEGER NOT NULL,
                    IdPresupuesto INTEGER NOT NULL,
                    Orden         INTEGER NOT NULL,
                    Nombre        TEXT NOT NULL,
                    Monto         TEXT NOT NULL,
                    PRIMARY KEY (IdEvento, IdPresupuesto, Orden),
                    FOREIGN KEY (IdEvento, IdPresupuesto) REFERENCES Presupuestos (IdEvento, Id) ON DELETE CASCADE
                );";
            comando.ExecuteNonQuery();
        }

        // Ids que no se reutilizan aunque se borre el último registro, igual que en memoria.
        public int ObtenerProximoId(string tabla)
        {
            using SqliteConnection conexion = AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText =
                "INSERT OR IGNORE INTO Secuencias (Tabla, Ultimo) SELECT $tabla, IFNULL(MAX(Id), 0) FROM " + tabla + "; " +
                "UPDATE Secuencias SET Ultimo = Ultimo + 1 WHERE Tabla = $tabla RETURNING Ultimo;";
            comando.Parameters.AddWithValue("$tabla", tabla);
            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public static object ValorONulo(DateTime? valor)
        {
            return valor == null ? DBNull.Value : valor.Value;
        }

        public static DateTime? LeerFechaONula(SqliteDataReader lector, string columna)
        {
            int indice = lector.GetOrdinal(columna);
            return lector.IsDBNull(indice) ? null : lector.GetDateTime(indice);
        }
    }
}
