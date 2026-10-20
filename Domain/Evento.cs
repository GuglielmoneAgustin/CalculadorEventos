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
        private bool huboCambiosDesdeUltimoPresupuesto;

        public Evento(int id, Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            Id = id;
            menu = new MenuEvento();
            servicios = new List<ServicioAdicional>();
            presupuestos = new List<Presupuesto>();
            proximoIdItemMenu = 1;
            proximoIdPresupuesto = 1;
            ActualizarDatos(cliente, lugar, fecha, horasDuracion, cantidadAdultos, cantidadNinos);
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
            get
            {
                Presupuesto? ultimo = ObtenerUltimoPresupuesto();
                return ultimo == null || !EsVigente(ultimo);
            }
        }

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
            ValidarHayInvitados(cantidadAdultos, cantidadNinos);
            ValidarCapacidad(lugar, cantidadAdultos, cantidadNinos);

            bool huboCambios = HayCambios(cliente, lugar, fecha, horasDuracion, cantidadAdultos, cantidadNinos);

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

        private bool HayCambios(Cliente cliente, Lugar lugar, DateTime fecha, decimal horasDuracion, int cantidadAdultos, int cantidadNinos)
        {
            return this.cliente == null || this.lugar == null
                || this.cliente.Id != cliente.Id
                || this.lugar.Id != lugar.Id
                || this.fecha != fecha
                || this.horasDuracion != horasDuracion
                || this.cantidadAdultos != cantidadAdultos
                || this.cantidadNinos != cantidadNinos;
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

        public TipoPendiente? ObtenerTipoPendiente()
        {
            EstadoPresupuesto? estado = ObtenerEstadoPresupuesto();
            if (estado == null)
            {
                return TipoPendiente.SinPresupuesto;
            }
            if (estado != EstadoPresupuesto.EnEspera)
            {
                return null;
            }
            return RequierePresupuestoNuevo ? TipoPendiente.Desactualizado : TipoPendiente.EsperandoRespuesta;
        }

        public bool EsProximoAprobado(DateTime ahora)
        {
            return Fecha >= ahora && TienePresupuestoAprobado();
        }

        private void AgregarItemAlMenu(int cantidadAdultos, int cantidadNinos, List<Producto> componentes)
        {
            if (componentes == null || componentes.Count == 0)
            {
                throw new NegocioException("El item debe incluir al menos un producto.");
            }

            ItemMenu item = new ItemMenu(proximoIdItemMenu, cantidadAdultos, cantidadNinos);
            foreach (Producto componente in componentes)
            {
                item.AgregarComponente(componente);
            }

            proximoIdItemMenu++;
            Menu.AgregarItem(item);
            MarcarCambioPendienteDePresupuesto();
        }

        public void AgregarPlatosAlMenu(int cantidadAdultos, int cantidadNinos, List<Producto> productos)
        {
            if (productos == null || productos.Count == 0)
            {
                throw new NegocioException("Seleccione al menos un producto para el plato.");
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

        public void CambiarCantidadesItem(int idItem, int cantidadAdultos, int cantidadNinos)
        {
            ItemMenu? item = Menu.ObtenerItem(idItem);
            if (item == null)
            {
                throw new NoEncontradoException("El item de menú no existe.");
            }
            if (item.CantidadAdultos == cantidadAdultos && item.CantidadNinos == cantidadNinos)
            {
                return;
            }

            item.CambiarCantidades(cantidadAdultos, cantidadNinos);
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
                throw new NegocioException("El servicio ya está asociado al evento.");
            }
            servicios.Add(servicio);
            MarcarCambioPendienteDePresupuesto();
        }

        public void CopiarMenuYServiciosDe(Evento origen)
        {
            if (origen == null)
            {
                throw new ArgumentException("El evento a copiar no puede ser nulo.");
            }

            foreach (ItemMenu item in origen.Menu.ObtenerItems())
            {
                AgregarItemAlMenu(item.CantidadAdultos, item.CantidadNinos, new List<Producto>(item.ObtenerComponentes()));
            }
            foreach (ServicioAdicional servicio in origen.ObtenerServicios())
            {
                if (!TieneServicio(servicio.Id))
                {
                    AgregarServicio(servicio);
                }
            }
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
                throw new NegocioException("Debe agregar al menos un item al menú antes de generar un presupuesto.");
            }

            Presupuesto presupuesto = new Presupuesto(proximoIdPresupuesto, this, fechaGeneracion);
            proximoIdPresupuesto++;
            presupuestos.Add(presupuesto);
            huboCambiosDesdeUltimoPresupuesto = false;
            return presupuesto;
        }

        public Presupuesto AprobarPresupuesto(int idPresupuesto, bool reemplazarAprobado)
        {
            Presupuesto presupuesto = ValidarAprobacion(idPresupuesto);

            Presupuesto? otroAprobado = ObtenerPresupuestoAprobado();
            if (otroAprobado != null && !reemplazarAprobado)
            {
                throw new NegocioException("Ya existe un presupuesto aprobado para este evento.");
            }

            presupuesto.Aprobar();
            otroAprobado?.Cancelar();
            return presupuesto;
        }

        public Presupuesto ValidarAprobacion(int idPresupuesto)
        {
            Presupuesto presupuesto = ObtenerPresupuestoExistente(idPresupuesto);
            presupuesto.ValidarQuePuedeAprobarse();

            if (!EsVigente(presupuesto))
            {
                throw new NegocioException("El presupuesto está desactualizado. " +
                    string.Join(" ", ObtenerMotivosDesactualizacion(presupuesto)) +
                    " Actualice el presupuesto para obtener uno con los valores actuales.");
            }
            return presupuesto;
        }

        public List<string> ObtenerMotivosDesactualizacion(Presupuesto presupuesto)
        {
            List<string> motivos = new List<string>();
            if (EsVigente(presupuesto))
            {
                return motivos;
            }

            Presupuesto? ultimo = ObtenerUltimoPresupuesto();
            if (ultimo != null && ultimo != presupuesto)
            {
                motivos.Add("Hay un presupuesto más nuevo (N° " + ultimo.Id + ").");
                return motivos;
            }

            AgregarMotivosDelEvento(presupuesto, motivos);
            AgregarMotivosDelMenu(presupuesto, motivos);
            AgregarMotivosDeServicios(presupuesto, motivos);

            if (motivos.Count == 0)
            {
                motivos.Add("El evento se modificó después de generarlo.");
            }
            return motivos;
        }

        private void AgregarMotivosDelEvento(Presupuesto presupuesto, List<string> motivos)
        {
            if (presupuesto.NombreLugar != Lugar.Nombre)
            {
                motivos.Add("Cambió el lugar.");
            }
            else if (presupuesto.CostoBaseLugar != Lugar.CostoBase || presupuesto.CostoPorHoraLugar != Lugar.CostoPorHora)
            {
                motivos.Add("Cambió el costo del lugar.");
            }
            if (presupuesto.FechaEvento != Fecha || presupuesto.HorasDuracion != HorasDuracion)
            {
                motivos.Add("Cambió la fecha o la duración.");
            }
            if (presupuesto.CantidadAdultos != CantidadAdultos || presupuesto.CantidadNinos != CantidadNinos)
            {
                motivos.Add("Cambió la cantidad de invitados.");
            }
        }

        private void AgregarMotivosDelMenu(Presupuesto presupuesto, List<string> motivos)
        {
            IReadOnlyList<DetalleMenuPresupuestado> cotizados = presupuesto.ObtenerItemsMenu();
            IReadOnlyList<ItemMenu> actuales = Menu.ObtenerItems();

            bool mismosPlatos = cotizados.Count == actuales.Count;
            for (int i = 0; mismosPlatos && i < cotizados.Count; i++)
            {
                mismosPlatos = cotizados[i].Descripcion == actuales[i].ObtenerDescripcion()
                    && cotizados[i].CantidadAdultos == actuales[i].CantidadAdultos
                    && cotizados[i].CantidadNinos == actuales[i].CantidadNinos;
            }
            if (!mismosPlatos)
            {
                motivos.Add("Cambió el menú.");
                return;
            }

            for (int i = 0; i < cotizados.Count; i++)
            {
                if (cotizados[i].Subtotal != actuales[i].Subtotal())
                {
                    motivos.Add("Cambió el precio de " + cotizados[i].Descripcion + ".");
                }
            }
        }

        private void AgregarMotivosDeServicios(Presupuesto presupuesto, List<string> motivos)
        {
            IReadOnlyList<ServicioPresupuestado> cotizados = presupuesto.ObtenerServicios();

            bool mismosServicios = cotizados.Count == servicios.Count
                && cotizados.All(cotizado => servicios.Exists(s => s.Nombre == cotizado.Nombre));
            if (!mismosServicios)
            {
                motivos.Add("Cambiaron los servicios adicionales.");
                return;
            }

            foreach (ServicioPresupuestado cotizado in cotizados)
            {
                ServicioAdicional? actual = servicios.Find(s => s.Nombre == cotizado.Nombre);
                if (actual != null && actual.Monto != cotizado.Monto)
                {
                    motivos.Add("Cambió el monto de " + cotizado.Nombre + ".");
                }
            }
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

        public bool EsVigente(Presupuesto presupuesto)
        {
            return presupuesto == ObtenerUltimoPresupuesto()
                && !huboCambiosDesdeUltimoPresupuesto
                && presupuesto.CoincidenPreciosCon(this);
        }

        public decimal CalcularSubtotalServicios()
        {
            decimal total = 0m;
            foreach (ServicioAdicional servicio in servicios)
            {
                total += servicio.Monto;
            }
            return total;
        }

        public Presupuesto? ObtenerPresupuestoDeReferencia()
        {
            return ObtenerPresupuestoAprobado() ?? ObtenerUltimoPresupuesto();
        }

        public Presupuesto? ObtenerUltimoPresupuesto()
        {
            return presupuestos.Count == 0 ? null : presupuestos[presupuestos.Count - 1];
        }

        private void MarcarCambioPendienteDePresupuesto()
        {
            huboCambiosDesdeUltimoPresupuesto = true;
        }

        private static void ValidarHorasDuracion(decimal valor)
        {
            if (valor <= 0)
            {
                throw new NegocioException("La duración debe ser mayor a cero.");
            }
        }

        private static void ValidarCantidad(int valor, string tipoInvitado)
        {
            if (valor < 0)
            {
                throw new NegocioException("La cantidad de " + tipoInvitado + " no puede ser negativa.");
            }
        }

        private static void ValidarHayInvitados(int cantidadAdultos, int cantidadNinos)
        {
            if (cantidadAdultos + cantidadNinos == 0)
            {
                throw new NegocioException("El evento debe tener al menos un invitado.");
            }
        }

        private static void ValidarCapacidad(Lugar lugar, int cantidadAdultos, int cantidadNinos)
        {
            if (!lugar.AdmiteInvitados(cantidadAdultos + cantidadNinos))
            {
                throw new NegocioException("La cantidad de invitados supera la capacidad del lugar.");
            }
        }
    }
}
