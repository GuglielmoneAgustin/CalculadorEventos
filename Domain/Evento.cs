using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Exceptions;

namespace ProyectoEventos.Domain
{
    public class Evento : IEntidad
    {
        private DateTime fecha;
        private decimal horasDuracion;
        private int cantidadAdultos;
        private int cantidadNinos;
        private Cliente cliente;
        private Lugar lugar;
        private readonly MenuEvento menu;
        private readonly List<ServicioAdicional> servicios;
        private readonly List<Presupuesto> presupuestos;
        private int proximoIdItemMenu;
        private int proximoIdPresupuesto;
        private bool requierePresupuestoNuevo;

        public Evento(int id, Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            menu = new MenuEvento();
            servicios = new List<ServicioAdicional>();
            presupuestos = new List<Presupuesto>();
            proximoIdItemMenu = 1;
            proximoIdPresupuesto = 1;
            ActualizarDatos(cliente, lugar, fecha, horasDuracion, cantidadAdultos, cantidadNinos);
            requierePresupuestoNuevo = true;
        }

        public int Id { get; private set; }

        public Cliente Cliente
        {
            get { return cliente; }
        }

        public Lugar Lugar
        {
            get { return lugar; }
        }

        public MenuEvento Menu
        {
            get { return menu; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
        }

        public decimal HorasDuracion
        {
            get { return horasDuracion; }
        }

        public int CantidadAdultos
        {
            get { return cantidadAdultos; }
        }

        public int CantidadNinos
        {
            get { return cantidadNinos; }
        }

        public DateTime FechaFin
        {
            get { return Fecha.AddHours((double)HorasDuracion); }
        }

        public bool RequierePresupuestoNuevo
        {
            get { return requierePresupuestoNuevo; }
        }

        // Única forma de cambiar los datos generales: se validan todos juntos (rangos y capacidad del lugar)
        // antes de asignar, así el evento nunca queda en un estado inválido ni a medio actualizar.
        [MemberNotNull(nameof(cliente), nameof(lugar))]
        public void ActualizarDatos(Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            if (cliente == null)
            {
                throw new ArgumentException("El evento debe tener un cliente asignado.");
            }
            if (lugar == null)
            {
                throw new ArgumentException("El evento debe tener un lugar asignado.");
            }
            ValidarHorasDuracion(horasDuracion);
            ValidarCantidad(cantidadAdultos, "adultos");
            ValidarCantidad(cantidadNinos, "niños");
            ValidarCapacidad(lugar, cantidadAdultos, cantidadNinos);

            bool huboCambios = this.cliente == null || this.lugar == null
                || this.cliente.Id != cliente.Id
                || this.lugar.Id != lugar.Id
                || this.fecha != fecha
                || this.horasDuracion != horasDuracion
                || this.cantidadAdultos != cantidadAdultos
                || this.cantidadNinos != cantidadNinos;

            this.cliente = cliente;
            this.lugar = lugar;
            this.fecha = fecha;
            this.horasDuracion = horasDuracion;
            this.cantidadAdultos = cantidadAdultos;
            this.cantidadNinos = cantidadNinos;

            if (huboCambios)
            {
                MarcarCambioPendienteDePresupuesto();
            }
        }

        public int TotalInvitados()
        {
            return CantidadAdultos + CantidadNinos;
        }

        public bool SeSuperponeCon(DateTime inicio, decimal horasDuracion)
        {
            DateTime fin = inicio.AddHours((double)horasDuracion);
            return inicio < FechaFin && Fecha < fin;
        }

        public bool TieneCliente(int idCliente)
        {
            return Cliente.Id == idCliente;
        }

        public bool TieneLugar(int idLugar)
        {
            return Lugar.Id == idLugar;
        }

        public bool UsaProducto(int idProducto)
        {
            return Menu.UsaProducto(idProducto);
        }

        public bool TieneServicio(int idServicio)
        {
            return servicios.Exists(s => s.Id == idServicio);
        }

        public bool TienePresupuestoAprobado()
        {
            return ObtenerPresupuestoAprobado() != null;
        }

        // Resume la situación del evento: aprobado si hay uno aprobado; si no, en espera si queda alguno
        // pendiente; si no, cancelado; y null si todavía no se generó ningún presupuesto.
        public EstadoPresupuesto? ObtenerEstadoPresupuesto()
        {
            if (TienePresupuestoAprobado())
            {
                return EstadoPresupuesto.Aprobado;
            }
            if (presupuestos.Exists(presupuesto => presupuesto.Estado == EstadoPresupuesto.EnEspera))
            {
                return EstadoPresupuesto.EnEspera;
            }
            if (presupuestos.Count > 0)
            {
                return EstadoPresupuesto.Cancelado;
            }
            return null;
        }

        public bool EsProximoAprobado(DateTime ahora)
        {
            return Fecha >= ahora && TienePresupuestoAprobado();
        }

        public void AgregarItemAlMenu(int cantidadAdultos, int cantidadNinos, List<Producto> componentes)
        {
            if (componentes == null || componentes.Count == 0)
            {
                throw new ReglaNegocioException("El item debe incluir al menos un producto.");
            }

            ItemMenu item = new ItemMenu(proximoIdItemMenu, cantidadAdultos, cantidadNinos);
            foreach (Producto componente in componentes)
            {
                item.AgregarComponente(componente);
            }

            // El id solo se consume si el item se pudo armar sin errores.
            proximoIdItemMenu++;
            Menu.AgregarItem(item);
            MarcarCambioPendienteDePresupuesto();
        }

        // Agrega un plato por cada producto, todos con las mismas cantidades.
        // Primero se arman todos: si alguno es inválido no se agrega ninguno.
        public void AgregarPlatosAlMenu(int cantidadAdultos, int cantidadNinos, List<Producto> productos)
        {
            if (productos == null || productos.Count == 0)
            {
                throw new ReglaNegocioException("Seleccione al menos un producto para el plato.");
            }

            List<ItemMenu> nuevos = new List<ItemMenu>();
            int idItem = proximoIdItemMenu;
            foreach (Producto producto in productos)
            {
                ItemMenu item = new ItemMenu(idItem, cantidadAdultos, cantidadNinos);
                item.AgregarComponente(producto);
                nuevos.Add(item);
                idItem++;
            }

            proximoIdItemMenu = idItem;
            foreach (ItemMenu item in nuevos)
            {
                Menu.AgregarItem(item);
            }
            MarcarCambioPendienteDePresupuesto();
        }

        public void QuitarItemDelMenu(int idItem)
        {
            if (!Menu.QuitarItem(idItem))
            {
                throw new NoEncontradoException("El item de menú no existe.");
            }
            MarcarCambioPendienteDePresupuesto();
        }

        public void AgregarServicio(ServicioAdicional servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentException("El servicio no puede ser nulo.");
            }
            if (servicios.Exists(s => s.Id == servicio.Id))
            {
                throw new ReglaNegocioException("El servicio ya está asociado al evento.");
            }
            servicios.Add(servicio);
            MarcarCambioPendienteDePresupuesto();
        }

        public void QuitarServicio(ServicioAdicional servicio)
        {
            if (!servicios.Remove(servicio))
            {
                throw new NoEncontradoException("El servicio no está asociado al evento.");
            }
            MarcarCambioPendienteDePresupuesto();
        }

        public IReadOnlyList<ServicioAdicional> ObtenerServicios()
        {
            return servicios.AsReadOnly();
        }

        public Presupuesto GenerarPresupuesto(DateTime fechaGeneracion)
        {
            if (Menu.ObtenerItems().Count == 0)
            {
                throw new ReglaNegocioException("Debe agregar al menos un item al menú antes de generar un presupuesto.");
            }

            Presupuesto presupuesto = new Presupuesto(proximoIdPresupuesto, this, fechaGeneracion);
            proximoIdPresupuesto++;
            presupuestos.Add(presupuesto);
            requierePresupuestoNuevo = false;
            return presupuesto;
        }

        // El evento coordina la aprobación porque la regla abarca a todos sus presupuestos:
        // solo puede haber uno aprobado y solo se aprueba el que refleja los datos actuales.
        public Presupuesto AprobarPresupuesto(int idPresupuesto, bool reemplazarAprobado)
        {
            Presupuesto presupuesto = ObtenerPresupuestoExistente(idPresupuesto);
            presupuesto.ValidarQuePuedeAprobarse();

            if (!EsVigente(presupuesto))
            {
                throw new ReglaNegocioException("El presupuesto está desactualizado: el evento cambió después de generarlo. Genere un presupuesto nuevo.");
            }

            Presupuesto? otroAprobado = ObtenerPresupuestoAprobado();
            if (otroAprobado != null && !reemplazarAprobado)
            {
                throw new ConflictoAprobacionException("Ya existe un presupuesto aprobado para este evento.");
            }

            presupuesto.Aprobar();
            otroAprobado?.Cancelar();
            return presupuesto;
        }

        public Presupuesto CancelarPresupuesto(int idPresupuesto)
        {
            Presupuesto presupuesto = ObtenerPresupuestoExistente(idPresupuesto);
            presupuesto.Cancelar();
            return presupuesto;
        }

        public bool HayOtroPresupuestoAprobado(int idPresupuesto)
        {
            Presupuesto? aprobado = ObtenerPresupuestoAprobado();
            return aprobado != null && aprobado.Id != idPresupuesto;
        }

        public Presupuesto? ObtenerPresupuesto(int idPresupuesto)
        {
            return presupuestos.Find(presupuesto => presupuesto.Id == idPresupuesto);
        }

        public Presupuesto ObtenerPresupuestoExistente(int idPresupuesto)
        {
            Presupuesto? presupuesto = ObtenerPresupuesto(idPresupuesto);
            if (presupuesto == null)
            {
                throw new NoEncontradoException("El presupuesto seleccionado no existe.");
            }
            return presupuesto;
        }

        public IReadOnlyList<Presupuesto> ObtenerPresupuestos()
        {
            return presupuestos.AsReadOnly();
        }

        public Presupuesto? ObtenerPresupuestoAprobado()
        {
            return presupuestos.Find(presupuesto => presupuesto.Estado == EstadoPresupuesto.Aprobado);
        }

        // Vigente = es el último generado y el evento no cambió desde entonces.
        private bool EsVigente(Presupuesto presupuesto)
        {
            return !requierePresupuestoNuevo && presupuestos.Count > 0 && presupuestos[presupuestos.Count - 1] == presupuesto;
        }

        // Los presupuestos ya generados no se tocan (son una foto de lo que se cotizó);
        // solo se marca que hace falta generar uno nuevo con los datos actuales.
        private void MarcarCambioPendienteDePresupuesto()
        {
            requierePresupuestoNuevo = true;
        }

        private static void ValidarHorasDuracion(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ReglaNegocioException("La duración debe ser mayor a cero.");
            }
        }

        private static void ValidarCantidad(int valor, string tipoInvitado)
        {
            if (valor < 0)
            {
                throw new ReglaNegocioException("La cantidad de " + tipoInvitado + " no puede ser negativa.");
            }
        }

        private static void ValidarCapacidad(Lugar lugar, int cantidadAdultos, int cantidadNinos)
        {
            if (!lugar.AdmiteInvitados(cantidadAdultos + cantidadNinos))
            {
                throw new ReglaNegocioException("La cantidad de invitados supera la capacidad del lugar.");
            }
        }
    }
}
