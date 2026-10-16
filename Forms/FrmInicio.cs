using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmInicio : Form
    {
        private readonly EventoController eventoControlador;

        public FrmInicio()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            eventoControlador = new EventoController();
            dgvProximosEventos.MensajeSinFilas(() => "No hay eventos aprobados próximos. Los eventos aparecen acá cuando se aprueba su presupuesto.");
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            CargarResumen();
            CargarProximosEventos();
        }

        // Tarjetas de arriba: próximos eventos, eventos esperando aprobación y total aprobado del mes
        private void CargarResumen()
        {
            ResumenEventosDTO resumen = eventoControlador.ObtenerResumen();
            lblResumenValorProximos.Text = resumen.ProximosAprobados.ToString();
            lblResumenValorEnEspera.Text = resumen.EnEspera.ToString();
            lblResumenTituloMes.Text = "Aprobado en " + DateTime.Today.ToString("MMMM");
            lblResumenValorMes.Text = resumen.TotalAprobadoMes.ToString(Formato.Moneda);
        }

        private void CargarProximosEventos()
        {
            dgvProximosEventos.DataSource = null;
            dgvProximosEventos.DataSource = eventoControlador.ObtenerProximosAprobados();
            dgvProximosEventos.Columna("Fecha").DefaultCellStyle.Format = Formato.FechaHora;
            dgvProximosEventos.Columna("NombreCliente").HeaderText = "Cliente";
            dgvProximosEventos.Columna("NombreLugar").HeaderText = "Lugar";
            dgvProximosEventos.Columna("Fecha").HeaderText = "Fecha y hora";
            // Todos los eventos de esta lista están aprobados: el total es el del presupuesto aprobado
            dgvProximosEventos.Columna("Total").HeaderText = "Total aprobado";
            dgvProximosEventos.Columna("Total").DefaultCellStyle.Format = Formato.Moneda;
            dgvProximosEventos.Columna("IdCliente").Visible = false;
            dgvProximosEventos.Columna("IdLugar").Visible = false;
            dgvProximosEventos.Columna("Id").Visible = false;
            dgvProximosEventos.Columna("HorasDuracion").Visible = false;
            dgvProximosEventos.Columna("CantidadAdultos").Visible = false;
            dgvProximosEventos.Columna("CantidadNinos").Visible = false;
            dgvProximosEventos.Columna("EstadoPresupuesto").Visible = false;
            dgvProximosEventos.ClearSelection();
            dgvProximosEventos.CurrentCell = null;
        }
    }
}
