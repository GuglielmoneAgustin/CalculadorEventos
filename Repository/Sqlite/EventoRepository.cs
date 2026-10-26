using Microsoft.Data.Sqlite;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Domain;

namespace ProyectoEventos.Repository.Sqlite
{
    public class EventoRepository : IRepository<Evento>
    {
        private readonly BaseDatosSqlite baseDatos;
        private readonly IRepository<Cliente> clienteRepositorio;
        private readonly IRepository<Lugar> lugarRepositorio;
        private readonly IRepository<Producto> productoRepositorio;
        private readonly IRepository<ServicioAdicional> servicioRepositorio;

        public EventoRepository(BaseDatosSqlite baseDatos, IRepository<Cliente> clienteRepositorio, IRepository<Lugar> lugarRepositorio,
            IRepository<Producto> productoRepositorio, IRepository<ServicioAdicional> servicioRepositorio)
        {
            this.baseDatos = baseDatos;
            this.clienteRepositorio = clienteRepositorio;
            this.lugarRepositorio = lugarRepositorio;
            this.productoRepositorio = productoRepositorio;
            this.servicioRepositorio = servicioRepositorio;
        }

        public int ObtenerProximoId()
        {
            return baseDatos.ObtenerProximoId("Eventos");
        }

        public void Agregar(Evento evento)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteTransaction transaccion = conexion.BeginTransaction();

            using (SqliteCommand comando = conexion.CreateCommand())
            {
                comando.CommandText =
                    "INSERT INTO Eventos (Id, IdCliente, IdLugar, Fecha, HorasDuracion, CantidadAdultos, CantidadNinos, " +
                    "ProximoIdItem, ProximoIdPresupuesto, HuboCambios) VALUES ($id, $idCliente, $idLugar, $fecha, $horas, " +
                    "$adultos, $ninos, $proximoIdItem, $proximoIdPresupuesto, $huboCambios)";
                CargarParametros(comando, evento);
                comando.ExecuteNonQuery();
            }
            GuardarDetalle(conexion, evento);

            transaccion.Commit();
        }

        public void Actualizar(Evento evento)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteTransaction transaccion = conexion.BeginTransaction();

            using (SqliteCommand comando = conexion.CreateCommand())
            {
                comando.CommandText =
                    "UPDATE Eventos SET IdCliente = $idCliente, IdLugar = $idLugar, Fecha = $fecha, HorasDuracion = $horas, " +
                    "CantidadAdultos = $adultos, CantidadNinos = $ninos, ProximoIdItem = $proximoIdItem, " +
                    "ProximoIdPresupuesto = $proximoIdPresupuesto, HuboCambios = $huboCambios WHERE Id = $id";
                CargarParametros(comando, evento);
                if (comando.ExecuteNonQuery() == 0)
                {
                    throw new NoEncontradoException("El evento seleccionado no existe.");
                }
            }

            // El menú, los servicios y los presupuestos se reemplazan por los actuales.
            using (SqliteCommand comando = conexion.CreateCommand())
            {
                comando.CommandText =
                    "DELETE FROM ItemsMenu WHERE IdEvento = $id; " +
                    "DELETE FROM EventoServicios WHERE IdEvento = $id; " +
                    "DELETE FROM Presupuestos WHERE IdEvento = $id;";
                comando.Parameters.AddWithValue("$id", evento.Id);
                comando.ExecuteNonQuery();
            }
            GuardarDetalle(conexion, evento);

            transaccion.Commit();
        }

        public IReadOnlyList<Evento> ObtenerTodos()
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            return Cargar(conexion, null);
        }

        public Evento ObtenerExistente(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            List<Evento> eventos = Cargar(conexion, id);
            if (eventos.Count == 0)
            {
                throw new NoEncontradoException("El evento seleccionado no existe.");
            }
            return eventos[0];
        }

        public void Eliminar(int id)
        {
            using SqliteConnection conexion = baseDatos.AbrirConexion();
            using SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Eventos WHERE Id = $id";
            comando.Parameters.AddWithValue("$id", id);
            if (comando.ExecuteNonQuery() == 0)
            {
                throw new NoEncontradoException("El evento seleccionado no existe.");
            }
        }

        private static void CargarParametros(SqliteCommand comando, Evento evento)
        {
            comando.Parameters.AddWithValue("$id", evento.Id);
            comando.Parameters.AddWithValue("$idCliente", evento.Cliente.Id);
            comando.Parameters.AddWithValue("$idLugar", evento.Lugar.Id);
            comando.Parameters.AddWithValue("$fecha", evento.Fecha);
            comando.Parameters.AddWithValue("$horas", evento.HorasDuracion);
            comando.Parameters.AddWithValue("$adultos", evento.CantidadAdultos);
            comando.Parameters.AddWithValue("$ninos", evento.CantidadNinos);
            comando.Parameters.AddWithValue("$proximoIdItem", evento.Menu.ProximoIdItem);
            comando.Parameters.AddWithValue("$proximoIdPresupuesto", evento.ProximoIdPresupuesto);
            comando.Parameters.AddWithValue("$huboCambios", evento.HuboCambiosDesdeUltimoPresupuesto);
        }

        private static void GuardarDetalle(SqliteConnection conexion, Evento evento)
        {
            foreach (ItemMenu item in evento.Menu.ObtenerItems())
            {
                using SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO ItemsMenu (IdEvento, Id, IdProducto, CantidadAdultos, CantidadNinos) " +
                    "VALUES ($idEvento, $id, $idProducto, $adultos, $ninos)";
                comando.Parameters.AddWithValue("$idEvento", evento.Id);
                comando.Parameters.AddWithValue("$id", item.Id);
                comando.Parameters.AddWithValue("$idProducto", item.Producto.Id);
                comando.Parameters.AddWithValue("$adultos", item.CantidadAdultos);
                comando.Parameters.AddWithValue("$ninos", item.CantidadNinos);
                comando.ExecuteNonQuery();
            }

            IReadOnlyList<ServicioAdicional> servicios = evento.ObtenerServicios();
            for (int orden = 0; orden < servicios.Count; orden++)
            {
                using SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO EventoServicios (IdEvento, IdServicio, Orden) VALUES ($idEvento, $idServicio, $orden)";
                comando.Parameters.AddWithValue("$idEvento", evento.Id);
                comando.Parameters.AddWithValue("$idServicio", servicios[orden].Id);
                comando.Parameters.AddWithValue("$orden", orden);
                comando.ExecuteNonQuery();
            }

            foreach (Presupuesto presupuesto in evento.ObtenerPresupuestos())
            {
                GuardarPresupuesto(conexion, evento.Id, presupuesto);
            }
        }

        private static void GuardarPresupuesto(SqliteConnection conexion, int idEvento, Presupuesto presupuesto)
        {
            using (SqliteCommand comando = conexion.CreateCommand())
            {
                comando.CommandText =
                    "INSERT INTO Presupuestos (IdEvento, Id, FechaGeneracion, Estado, CostoBaseLugar, " +
                    "CostoPorHoraLugar, NombreCliente, NombreLugar, FechaEvento, HorasDuracion, CantidadAdultos, CantidadNinos) " +
                    "VALUES ($idEvento, $id, $fechaGeneracion, $estado, " +
                    "$costoBase, $costoPorHora, $nombreCliente, $nombreLugar, $fechaEvento, $horas, $adultos, $ninos)";
                comando.Parameters.AddWithValue("$idEvento", idEvento);
                comando.Parameters.AddWithValue("$id", presupuesto.Id);
                comando.Parameters.AddWithValue("$fechaGeneracion", presupuesto.FechaGeneracion);
                comando.Parameters.AddWithValue("$estado", (int)presupuesto.Estado);
                comando.Parameters.AddWithValue("$costoBase", presupuesto.CostoBaseLugar);
                comando.Parameters.AddWithValue("$costoPorHora", presupuesto.CostoPorHoraLugar);
                comando.Parameters.AddWithValue("$nombreCliente", presupuesto.NombreCliente);
                comando.Parameters.AddWithValue("$nombreLugar", presupuesto.NombreLugar);
                comando.Parameters.AddWithValue("$fechaEvento", presupuesto.FechaEvento);
                comando.Parameters.AddWithValue("$horas", presupuesto.HorasDuracion);
                comando.Parameters.AddWithValue("$adultos", presupuesto.CantidadAdultos);
                comando.Parameters.AddWithValue("$ninos", presupuesto.CantidadNinos);
                comando.ExecuteNonQuery();
            }

            IReadOnlyList<DetalleMenuPresupuestado> items = presupuesto.ObtenerItemsMenu();
            for (int orden = 0; orden < items.Count; orden++)
            {
                using SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO PresupuestoItems (IdEvento, IdPresupuesto, Orden, Descripcion, CantidadAdultos, " +
                    "CantidadNinos, Subtotal) VALUES ($idEvento, $idPresupuesto, $orden, $descripcion, $adultos, $ninos, $subtotal)";
                comando.Parameters.AddWithValue("$idEvento", idEvento);
                comando.Parameters.AddWithValue("$idPresupuesto", presupuesto.Id);
                comando.Parameters.AddWithValue("$orden", orden);
                comando.Parameters.AddWithValue("$descripcion", items[orden].Descripcion);
                comando.Parameters.AddWithValue("$adultos", items[orden].CantidadAdultos);
                comando.Parameters.AddWithValue("$ninos", items[orden].CantidadNinos);
                comando.Parameters.AddWithValue("$subtotal", items[orden].Subtotal);
                comando.ExecuteNonQuery();
            }

            IReadOnlyList<ServicioPresupuestado> servicios = presupuesto.ObtenerServicios();
            for (int orden = 0; orden < servicios.Count; orden++)
            {
                using SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO PresupuestoServicios (IdEvento, IdPresupuesto, Orden, Nombre, Monto) " +
                    "VALUES ($idEvento, $idPresupuesto, $orden, $nombre, $monto)";
                comando.Parameters.AddWithValue("$idEvento", idEvento);
                comando.Parameters.AddWithValue("$idPresupuesto", presupuesto.Id);
                comando.Parameters.AddWithValue("$orden", orden);
                comando.Parameters.AddWithValue("$nombre", servicios[orden].Nombre);
                comando.Parameters.AddWithValue("$monto", servicios[orden].Monto);
                comando.ExecuteNonQuery();
            }

            IReadOnlyList<Pago> pagos = presupuesto.ObtenerPagos();
            for (int orden = 0; orden < pagos.Count; orden++)
            {
                using SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO PresupuestoPagos (IdEvento, IdPresupuesto, Orden, Fecha, Monto) " +
                    "VALUES ($idEvento, $idPresupuesto, $orden, $fecha, $monto)";
                comando.Parameters.AddWithValue("$idEvento", idEvento);
                comando.Parameters.AddWithValue("$idPresupuesto", presupuesto.Id);
                comando.Parameters.AddWithValue("$orden", orden);
                comando.Parameters.AddWithValue("$fecha", pagos[orden].Fecha);
                comando.Parameters.AddWithValue("$monto", pagos[orden].Monto);
                comando.ExecuteNonQuery();
            }
        }

        // Lee todos los eventos (idEvento nulo) o uno solo, con todo lo que contienen.
        private List<Evento> Cargar(SqliteConnection conexion, int? idEvento)
        {
            Dictionary<int, Cliente> clientes = clienteRepositorio.ObtenerTodos().ToDictionary(c => c.Id);
            Dictionary<int, Lugar> lugares = lugarRepositorio.ObtenerTodos().ToDictionary(l => l.Id);
            Dictionary<int, Producto> productos = productoRepositorio.ObtenerTodos().ToDictionary(p => p.Id);
            Dictionary<int, ServicioAdicional> servicios = servicioRepositorio.ObtenerTodos().ToDictionary(s => s.Id);

            Dictionary<int, Evento> eventos = new Dictionary<int, Evento>();
            Dictionary<int, int[]> estados = new Dictionary<int, int[]>();
            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT Id, IdCliente, IdLugar, Fecha, HorasDuracion, CantidadAdultos, CantidadNinos, ProximoIdItem, " +
                "ProximoIdPresupuesto, HuboCambios FROM Eventos", "Id", idEvento, "Id"))
            {
                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    eventos.Add(id, new Evento(id, clientes[lector.GetInt32(1)], lugares[lector.GetInt32(2)], lector.GetDateTime(3),
                        lector.GetDecimal(4), lector.GetInt32(5), lector.GetInt32(6)));
                    estados.Add(id, new int[] { lector.GetInt32(7), lector.GetInt32(8), lector.GetInt32(9) });
                }
            }

            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, Id, IdProducto, CantidadAdultos, CantidadNinos FROM ItemsMenu", "IdEvento", idEvento, "IdEvento, Id"))
            {
                while (lector.Read())
                {
                    ItemMenu item = new ItemMenu(lector.GetInt32(1), productos[lector.GetInt32(2)], lector.GetInt32(3), lector.GetInt32(4));
                    eventos[lector.GetInt32(0)].Menu.CargarItem(item);
                }
            }

            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, IdServicio FROM EventoServicios", "IdEvento", idEvento, "IdEvento, Orden"))
            {
                while (lector.Read())
                {
                    eventos[lector.GetInt32(0)].CargarServicio(servicios[lector.GetInt32(1)]);
                }
            }

            Dictionary<string, List<DetalleMenuPresupuestado>> itemsPresupuestados = new Dictionary<string, List<DetalleMenuPresupuestado>>();
            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, IdPresupuesto, Descripcion, CantidadAdultos, CantidadNinos, Subtotal FROM PresupuestoItems",
                "IdEvento", idEvento, "IdEvento, IdPresupuesto, Orden"))
            {
                while (lector.Read())
                {
                    string clave = lector.GetInt32(0) + "-" + lector.GetInt32(1);
                    if (!itemsPresupuestados.ContainsKey(clave))
                    {
                        itemsPresupuestados.Add(clave, new List<DetalleMenuPresupuestado>());
                    }
                    itemsPresupuestados[clave].Add(new DetalleMenuPresupuestado(lector.GetString(2), lector.GetInt32(3), lector.GetInt32(4), lector.GetDecimal(5)));
                }
            }

            Dictionary<string, List<ServicioPresupuestado>> serviciosPresupuestados = new Dictionary<string, List<ServicioPresupuestado>>();
            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, IdPresupuesto, Nombre, Monto FROM PresupuestoServicios", "IdEvento", idEvento, "IdEvento, IdPresupuesto, Orden"))
            {
                while (lector.Read())
                {
                    string clave = lector.GetInt32(0) + "-" + lector.GetInt32(1);
                    if (!serviciosPresupuestados.ContainsKey(clave))
                    {
                        serviciosPresupuestados.Add(clave, new List<ServicioPresupuestado>());
                    }
                    serviciosPresupuestados[clave].Add(new ServicioPresupuestado(lector.GetString(2), lector.GetDecimal(3)));
                }
            }

            Dictionary<string, List<Pago>> pagosPresupuestos = new Dictionary<string, List<Pago>>();
            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, IdPresupuesto, Fecha, Monto FROM PresupuestoPagos", "IdEvento", idEvento, "IdEvento, IdPresupuesto, Orden"))
            {
                while (lector.Read())
                {
                    string clave = lector.GetInt32(0) + "-" + lector.GetInt32(1);
                    if (!pagosPresupuestos.ContainsKey(clave))
                    {
                        pagosPresupuestos.Add(clave, new List<Pago>());
                    }
                    pagosPresupuestos[clave].Add(new Pago(lector.GetDateTime(2), lector.GetDecimal(3)));
                }
            }

            using (SqliteDataReader lector = Consultar(conexion,
                "SELECT IdEvento, Id, FechaGeneracion, Estado, CostoBaseLugar, CostoPorHoraLugar, NombreCliente, " +
                "NombreLugar, FechaEvento, HorasDuracion, CantidadAdultos, CantidadNinos FROM Presupuestos",
                "IdEvento", idEvento, "IdEvento, Id"))
            {
                while (lector.Read())
                {
                    Evento evento = eventos[lector.GetInt32(0)];
                    string clave = evento.Id + "-" + lector.GetInt32(1);
                    List<DetalleMenuPresupuestado> items = itemsPresupuestados.ContainsKey(clave)
                        ? itemsPresupuestados[clave] : new List<DetalleMenuPresupuestado>();
                    List<ServicioPresupuestado> serviciosCotizados = serviciosPresupuestados.ContainsKey(clave)
                        ? serviciosPresupuestados[clave] : new List<ServicioPresupuestado>();
                    List<Pago> pagos = pagosPresupuestos.ContainsKey(clave) ? pagosPresupuestos[clave] : new List<Pago>();

                    evento.CargarPresupuesto(new Presupuesto(
                        lector.GetInt32(1),
                        evento,
                        lector.GetDateTime(2),
                        (EstadoPresupuesto)lector.GetInt32(3),
                        lector.GetDecimal(4),
                        lector.GetDecimal(5),
                        lector.GetString(6),
                        lector.GetString(7),
                        lector.GetDateTime(8),
                        lector.GetDecimal(9),
                        lector.GetInt32(10),
                        lector.GetInt32(11),
                        items,
                        serviciosCotizados,
                        pagos));
                }
            }

            foreach (Evento evento in eventos.Values)
            {
                int[] estado = estados[evento.Id];
                evento.CargarEstado(estado[1], estado[0], estado[2] == 1);
            }
            return eventos.Values.OrderBy(e => e.Id).ToList();
        }

        private static SqliteDataReader Consultar(SqliteConnection conexion, string select, string columnaEvento, int? idEvento, string orden)
        {
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = select;
            if (idEvento != null)
            {
                comando.CommandText += " WHERE " + columnaEvento + " = $idEvento";
                comando.Parameters.AddWithValue("$idEvento", idEvento.Value);
            }
            comando.CommandText += " ORDER BY " + orden;
            return comando.ExecuteReader();
        }
    }
}
