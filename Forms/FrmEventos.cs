using System;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmEventos : Form
    {
        private readonly EventoController controlador;
        private List<EventoResultadoDTO> eventos = new List<EventoResultadoDTO>();

        public FrmEventos()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new EventoController();
        }

        private void FrmEventos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CargarGrilla()
        {
            eventos = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        // Muestra los eventos que coinciden con el texto de búsqueda (sin volver a pedirlos al controlador)
        private void MostrarGrilla()
        {
            dgvEventos.DataSource = null;
            dgvEventos.DataSource = Busqueda.Filtrar(eventos, txtBuscar.Text, ev => new[] { ev.Id.ToString(), ev.NombreCliente, ev.NombreLugar, ev.Fecha.ToString(Formato.FechaHora), EstadoPresupuestoVista.Texto(ev.EstadoPresupuesto) });
            dgvEventos.Columna("Fecha").DefaultCellStyle.Format = Formato.FechaHora;

            dgvEventos.Columna("Id").HeaderText = "N°";
            dgvEventos.Columna("NombreCliente").HeaderText = "Cliente";
            dgvEventos.Columna("NombreLugar").HeaderText = "Lugar";
            dgvEventos.Columna("Fecha").HeaderText = "Fecha y hora";
            dgvEventos.Columna("HorasDuracion").HeaderText = "Duración (hs)";
            dgvEventos.Columna("CantidadAdultos").Visible = false;
            dgvEventos.Columna("CantidadNinos").Visible = false;
            dgvEventos.Columna("EstadoPresupuesto").HeaderText = "Presupuesto";
            dgvEventos.Columna("Total").HeaderText = "Total";
            dgvEventos.Columna("Total").DefaultCellStyle.Format = Formato.Moneda;
            dgvEventos.Columna("IdCliente").Visible = false;
            dgvEventos.Columna("IdLugar").Visible = false;
            
            // Ancho relativo de cada columna: el número y los valores cortos ocupan menos
            dgvEventos.Columna("Id").FillWeight = 35;
            dgvEventos.Columna("NombreCliente").FillWeight = 130;
            dgvEventos.Columna("NombreLugar").FillWeight = 120;
            dgvEventos.Columna("Fecha").FillWeight = 120;
            dgvEventos.Columna("HorasDuracion").FillWeight = 70;
            dgvEventos.Columna("Invitados").FillWeight = 70;
            dgvEventos.Columna("EstadoPresupuesto").FillWeight = 95;
            dgvEventos.Columna("Total").FillWeight = 115;

            if (dgvEventos.Rows.Count > 0)
            {
                dgvEventos.ClearSelection();
                dgvEventos.CurrentCell = null;
            }
        }

        // Muestra en la grilla si el evento tiene presupuesto aprobado, en espera, cancelado o ninguno
        private void dgvEventos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvEventos.Columns[e.ColumnIndex].Name != "EstadoPresupuesto")
            {
                return;
            }

            EstadoPresupuestoVista.Aplicar(e, e.Value as EstadoPresupuesto?, dgvEventos.DefaultCellStyle.Font ?? dgvEventos.Font);
        }

        private EventoResultadoDTO? ObtenerSeleccionado()
        {
            return dgvEventos.Seleccionado<EventoResultadoDTO>();
        }

        // Abre la pantalla dentro del panel principal; al volver se refresca la grilla
        private void AbrirSubpantalla(Form pantalla)
        {
            if (ParentForm is FrmPrincipal principal)
            {
                principal.AbrirSubpantalla(pantalla, CargarGrilla);
            }
            else
            {
                pantalla.ShowDialog();
                CargarGrilla();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AbrirSubpantalla(new FrmEventoDatos());
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para editar.");
                return;
            }

            AbrirSubpantalla(new FrmEventoDatos(seleccionado));
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para editar su menú.");
                return;
            }

            AbrirSubpantalla(new FrmEventoMenu(seleccionado.Id));
        }

        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para ver sus presupuestos.");
                return;
            }

            AbrirSubpantalla(new FrmPresupuestos(seleccionado.Id));
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EventoResultadoDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un evento de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este evento?"))
            {
                try
                {
                    controlador.Eliminar(seleccionado.Id);
                    CargarGrilla();
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }
    }
}
