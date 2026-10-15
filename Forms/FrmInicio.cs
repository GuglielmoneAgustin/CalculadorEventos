using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
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
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            CargarProximosEventos();
        }

        private void CargarProximosEventos()
        {
            dgvProximosEventos.DataSource = null;
            dgvProximosEventos.DataSource = eventoControlador.ObtenerProximosAprobados();
            dgvProximosEventos.Columna("Fecha").DefaultCellStyle.Format = Formato.FechaHora;
            if (dgvProximosEventos.Columns["Id"] != null)
            {
                dgvProximosEventos.Columna("NombreCliente").HeaderText = "Cliente";
                dgvProximosEventos.Columna("NombreLugar").HeaderText = "Lugar";
                dgvProximosEventos.Columna("Fecha").HeaderText = "Fecha y hora";
                dgvProximosEventos.Columna("IdCliente").Visible = false;
                dgvProximosEventos.Columna("IdLugar").Visible = false;
                dgvProximosEventos.Columna("Id").Visible = false;
                dgvProximosEventos.Columna("HorasDuracion").Visible = false;
                dgvProximosEventos.Columna("CantidadAdultos").Visible = false;
                dgvProximosEventos.Columna("CantidadNinos").Visible = false;
                dgvProximosEventos.Columna("EstadoPresupuesto").Visible = false;
            }
            dgvProximosEventos.ClearSelection();
            dgvProximosEventos.CurrentCell = null;
        }
    }
}
