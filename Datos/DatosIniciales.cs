using ProyectoEventos.Common.Enums;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Datos
{
    internal static class DatosIniciales
    {
        public static void Cargar()
        {
            ClienteController clientes = new ClienteController();
            if (clientes.ObtenerTodos().Count > 0)
            {
                return;
            }

            LugarController lugares = new LugarController();
            ProductoController productos = new ProductoController();
            ServicioAdicionalController servicios = new ServicioAdicionalController();
            EventoController eventos = new EventoController();
            PresupuestoController presupuestos = new PresupuestoController();

            CargarNegocio();
            CargarClientes(clientes);
            CargarLugares(lugares);
            CargarProductos(productos);
            CargarServicios(servicios);

            Dictionary<string, ClienteDTO> cli = clientes.ObtenerTodos().ToDictionary(c => c.Nombre);
            Dictionary<string, LugarDTO> lug = lugares.ObtenerTodos().ToDictionary(l => l.Nombre);
            Dictionary<string, ProductoDTO> prod = productos.ObtenerTodos().ToDictionary(p => p.Nombre);
            Dictionary<string, ServicioAdicionalDTO> serv = servicios.ObtenerTodos().ToDictionary(s => s.Nombre);

            DateTime hoy = DateTime.Today;

            int idCena = CrearEvento(eventos, cli["María González"].Id, lug["Salón Jardín"].Id, hoy.AddDays(7).AddHours(20), 4, 45, 15);
            AgregarPlatos(eventos, idCena, 45, 15, prod, "Mini hamburguesa", "Papas rústicas", "Limonada", "Brownie sin TACC");
            AgregarServicios(eventos, idCena, serv, "DJ y sonido", "Decoración temática");
            PresupuestoResultadoDTO presupuestoCena = GenerarPresupuesto(presupuestos, idCena);
            presupuestos.Aprobar(idCena, presupuestoCena.Id, false);
            presupuestos.RegistrarSena(idCena, Porcentaje(presupuestoCena.CostoTotal, 30));

            int idReunion = CrearEvento(eventos, cli["Carlos Rodríguez"].Id, lug["Terraza Norte"].Id, hoy.AddDays(14).AddHours(19), 4, 30, 10);
            AgregarPlatos(eventos, idReunion, 30, 10, prod, "Milanesa napolitana", "Ensalada mixta", "Gaseosas", "Flan con dulce de leche");
            GenerarPresupuesto(presupuestos, idReunion);

            int idCasamiento = CrearEvento(eventos, cli["Lucía Fernández"].Id, lug["Quinta Los Sauces"].Id, hoy.AddDays(21).AddHours(21), 5, 120, 30);
            AgregarPlatos(eventos, idCasamiento, 120, 30, prod, "Bife de chorizo", "Salmón al horno", "Puré de calabaza", "Papas rústicas", "Jugo natural", "Agua mineral", "Helado artesanal", "Torta de cumpleaños");
            AgregarServicios(eventos, idCasamiento, serv, "DJ y sonido", "Fotografía y video", "Mozos adicionales", "Barra de bebidas", "Decoración temática");
            PresupuestoResultadoDTO presupuestoCasamiento = GenerarPresupuesto(presupuestos, idCasamiento);
            presupuestos.Aprobar(idCasamiento, presupuestoCasamiento.Id, false);
            presupuestos.RegistrarSena(idCasamiento, Porcentaje(presupuestoCasamiento.CostoTotal, 40));

            int idBautismo = CrearEvento(eventos, cli["Martín López"].Id, lug["Loft Palermo"].Id, hoy.AddDays(10).AddHours(18), 3, 25, 5);
            AgregarPlatos(eventos, idBautismo, 25, 5, prod, "Risotto de hongos", "Ensalada de quinoa", "Té helado", "Ensalada de frutas frescas");
            AgregarServicios(eventos, idBautismo, serv, "Decoración temática");
            PresupuestoResultadoDTO presupuestoBautismo = GenerarPresupuesto(presupuestos, idBautismo);
            presupuestos.Cancelar(idBautismo, presupuestoBautismo.Id);

            int idEmpresa = CrearEvento(eventos, cli["Sofía Martínez"].Id, lug["Club Náutico"].Id, hoy.AddDays(28).AddHours(20).AddMinutes(30), 4, 80, 20);
            AgregarPlatos(eventos, idEmpresa, 80, 20, prod, "Pollo grillé", "Puré de calabaza", "Limonada", "Mousse de chocolate");
            PresupuestoResultadoDTO primero = GenerarPresupuesto(presupuestos, idEmpresa);
            AgregarPlatos(eventos, idEmpresa, 80, 20, prod, "Helado artesanal");
            AgregarServicios(eventos, idEmpresa, serv, "Barra de bebidas", "Mozos adicionales");
            presupuestos.Cancelar(idEmpresa, primero.Id);
            PresupuestoResultadoDTO segundo = GenerarPresupuesto(presupuestos, idEmpresa);
            presupuestos.Aprobar(idEmpresa, segundo.Id, false);

            int idSinPresupuesto = CrearEvento(eventos, cli["Diego Pereyra"].Id, lug["Salón Jardín"].Id, hoy.AddDays(35).AddHours(20), 4, 100, 20);
            AgregarPlatos(eventos, idSinPresupuesto, 100, 20, prod, "Ravioles de ricota", "Pechuga al vapor sin sal", "Agua mineral", "Brownie sin TACC");

            int idCumple = CrearEvento(eventos, cli["Valentina Suárez"].Id, lug["Terraza Norte"].Id, hoy.AddDays(3).AddHours(16), 3, 25, 40);
            AgregarPlatos(eventos, idCumple, 25, 40, prod, "Mini hamburguesa", "Papas rústicas", "Gaseosas", "Torta de cumpleaños");
            AgregarServicios(eventos, idCumple, serv, "Animación infantil", "Decoración temática");
            PresupuestoResultadoDTO presupuestoCumple = GenerarPresupuesto(presupuestos, idCumple);
            presupuestos.Aprobar(idCumple, presupuestoCumple.Id, false);

            int idPasado = CrearEvento(eventos, cli["Federico Romero"].Id, lug["Salón Jardín"].Id, hoy.AddDays(-20).AddHours(21), 4, 60, 10);
            AgregarPlatos(eventos, idPasado, 60, 10, prod, "Milanesa napolitana", "Ensalada mixta", "Jugo natural", "Flan con dulce de leche");
            AgregarServicios(eventos, idPasado, serv, "DJ y sonido");
            PresupuestoResultadoDTO presupuestoPasado = GenerarPresupuesto(presupuestos, idPasado);
            presupuestos.Aprobar(idPasado, presupuestoPasado.Id, false);
            presupuestos.RegistrarSena(idPasado, Porcentaje(presupuestoPasado.CostoTotal, 30));
            presupuestos.MarcarPagado(idPasado);
        }

        private static decimal Porcentaje(decimal total, int porcentaje)
        {
            return Math.Round(total * porcentaje / 100 / 1000) * 1000;
        }

        private static void CargarNegocio()
        {
            new NegocioController().Guardar(new NegocioDTO
            {
                Nombre = "Salón Las Rosas",
                Direccion = "Av. Libertador 1450, CABA",
                Telefono = "11 4555-2020",
                Mail = "hola@salonlasrosas.com",
                Cuit = "30-71234567-8",
                Instagram = "@salonlasrosas",
                MensajePie = "Gracias por elegirnos. ¡Te esperamos!",
                DiasValidez = 15,
                PorcentajeSena = 30
            });
        }

        private static void CargarClientes(ClienteController clientes)
        {
            clientes.Guardar(new ClienteDTO { Nombre = "María González", Dni = "30123456", Mail = "maria@ejemplo.com", Telefono = "11 5555-0101" });
            clientes.Guardar(new ClienteDTO { Nombre = "Carlos Rodríguez", Dni = "28456789", Mail = "carlos@ejemplo.com", Telefono = "11 5555-0102" });
            clientes.Guardar(new ClienteDTO { Nombre = "Lucía Fernández", Dni = "33987654", Mail = "lucia@ejemplo.com", Telefono = "11 5555-0103" });
            clientes.Guardar(new ClienteDTO { Nombre = "Martín López", Dni = "35111222", Mail = "martin@ejemplo.com", Telefono = "11 5555-0104" });
            clientes.Guardar(new ClienteDTO { Nombre = "Sofía Martínez", Dni = "31456123", Mail = "sofia@ejemplo.com", Telefono = "11 5555-0105" });
            clientes.Guardar(new ClienteDTO { Nombre = "Diego Pereyra", Dni = "29876543", Mail = "diego@ejemplo.com", Telefono = "11 5555-0106" });
            clientes.Guardar(new ClienteDTO { Nombre = "Valentina Suárez", Dni = "37222333", Mail = "valentina@ejemplo.com", Telefono = "11 5555-0107" });
            clientes.Guardar(new ClienteDTO { Nombre = "Federico Romero", Dni = "27333444", Mail = "federico@ejemplo.com", Telefono = "11 5555-0108" });
        }

        private static void CargarLugares(LugarController lugares)
        {
            lugares.Guardar(new LugarDTO { Nombre = "Salón Jardín", Ubicacion = "Palermo", Capacidad = 120, CostoBase = 180000m, CostoPorHora = 25000m });
            lugares.Guardar(new LugarDTO { Nombre = "Terraza Norte", Ubicacion = "Belgrano", Capacidad = 70, CostoBase = 130000m, CostoPorHora = 18000m });
            lugares.Guardar(new LugarDTO { Nombre = "Quinta Los Sauces", Ubicacion = "Pilar", Capacidad = 200, CostoBase = 320000m, CostoPorHora = 40000m });
            lugares.Guardar(new LugarDTO { Nombre = "Loft Palermo", Ubicacion = "Palermo Soho", Capacidad = 50, CostoBase = 95000m, CostoPorHora = 15000m });
            lugares.Guardar(new LugarDTO { Nombre = "Club Náutico", Ubicacion = "Tigre", Capacidad = 150, CostoBase = 250000m, CostoPorHora = 30000m });
        }

        private static void CargarProductos(ProductoController productos)
        {
            Producto(productos, "Mini hamburguesa", 4500m, 3200m, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Milanesa napolitana", 6200m, 4300m, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Pollo grillé", 5800m, 4000m, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Bife de chorizo", 9500m, 6500m, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Salmón al horno", 8900m, 6000m, CategoriasProductos.Principal, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Risotto de hongos", 6500m, 4800m, CategoriasProductos.Principal, RestriccionesAlimenticias.Vegetariano);
            Producto(productos, "Ravioles de ricota", 5400m, 3900m, CategoriasProductos.Principal, RestriccionesAlimenticias.Vegetariano);
            Producto(productos, "Pechuga al vapor sin sal", 5200m, 3700m, CategoriasProductos.Principal, RestriccionesAlimenticias.Hipertenso);

            Producto(productos, "Papas rústicas", 2200m, 1700m, CategoriasProductos.Guarnicion, RestriccionesAlimenticias.Vegetariano);
            Producto(productos, "Ensalada mixta", 1900m, 1400m, CategoriasProductos.Guarnicion, RestriccionesAlimenticias.Vegetariano);
            Producto(productos, "Puré de calabaza", 2000m, 1500m, CategoriasProductos.Guarnicion, RestriccionesAlimenticias.Vegetariano);
            Producto(productos, "Ensalada de quinoa", 2600m, 1900m, CategoriasProductos.Guarnicion, RestriccionesAlimenticias.Diabetico);

            Producto(productos, "Limonada", 1500m, 1200m, CategoriasProductos.Bebida, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Gaseosas", 1300m, 1100m, CategoriasProductos.Bebida, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Agua mineral", 900m, 800m, CategoriasProductos.Bebida, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Jugo natural", 1600m, 1300m, CategoriasProductos.Bebida, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Té helado", 1400m, 1100m, CategoriasProductos.Bebida, RestriccionesAlimenticias.Estandar);

            Producto(productos, "Brownie sin TACC", 2800m, 2200m, CategoriasProductos.Postre, RestriccionesAlimenticias.Celiaco);
            Producto(productos, "Mousse de chocolate", 2600m, 2000m, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Flan con dulce de leche", 2400m, 1800m, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Ensalada de frutas frescas", 2100m, 1700m, CategoriasProductos.Postre, RestriccionesAlimenticias.Diabetico);
            Producto(productos, "Helado artesanal", 2900m, 2300m, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);
            Producto(productos, "Torta de cumpleaños", 3200m, 2600m, CategoriasProductos.Postre, RestriccionesAlimenticias.Estandar);
        }

        private static void CargarServicios(ServicioAdicionalController servicios)
        {
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "DJ y sonido", Descripcion = "Musicalización durante todo el evento", Monto = 95000m });
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "Decoración temática", Descripcion = "Ambientación y centro de mesa", Monto = 65000m });
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "Fotografía y video", Descripcion = "Cobertura completa con álbum digital", Monto = 80000m });
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "Barra de bebidas", Descripcion = "Barra con bartender y bebidas sin alcohol", Monto = 70000m });
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "Animación infantil", Descripcion = "Juegos y show para chicos", Monto = 55000m });
            servicios.Guardar(new ServicioAdicionalDTO { Nombre = "Mozos adicionales", Descripcion = "Personal de servicio extra para mesas", Monto = 48000m });
        }

        private static void Producto(ProductoController productos, string nombre, decimal precioAdulto, decimal precioNino,
            CategoriasProductos categoria, RestriccionesAlimenticias restriccion)
        {
            productos.Guardar(new ProductoDTO
            {
                Nombre = nombre,
                PrecioAdulto = precioAdulto,
                PrecioNino = precioNino,
                Categoria = categoria,
                Restriccion = restriccion
            });
        }

        private static int CrearEvento(EventoController eventos, int idCliente, int idLugar, DateTime fecha, decimal horas, int adultos, int ninos)
        {
            EventoDTO datos = new EventoDTO
            {
                IdCliente = idCliente,
                IdLugar = idLugar,
                Fecha = fecha,
                HorasDuracion = horas,
                CantidadAdultos = adultos,
                CantidadNinos = ninos
            };
            return eventos.Guardar(datos).Id;
        }

        private static PresupuestoResultadoDTO GenerarPresupuesto(PresupuestoController presupuestos, int idEvento)
        {
            return presupuestos.Generar(idEvento)
                ?? throw new InvalidOperationException("No se generó el presupuesto del evento " + idEvento + ".");
        }

        private static void AgregarPlatos(EventoController eventos, int idEvento, int adultos, int ninos,
            Dictionary<string, ProductoDTO> productos, params string[] nombres)
        {
            eventos.AgregarPlatosAlMenu(idEvento, adultos, ninos, nombres.Select(nombre => productos[nombre].Id).ToList());
        }

        private static void AgregarServicios(EventoController eventos, int idEvento,
            Dictionary<string, ServicioAdicionalDTO> servicios, params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                eventos.AgregarServicioAlEvento(idEvento, servicios[nombre].Id);
            }
        }
    }
}
