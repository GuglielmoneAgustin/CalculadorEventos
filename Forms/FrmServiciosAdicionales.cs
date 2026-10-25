using ProyectoEventos.Common;
using System;
using System.Windows.Forms;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmServiciosAdicionales : Form
    {
        private readonly ServicioAdicionalController controlador;
        private List<ServicioAdicionalDTO> servicios = new List<ServicioAdicionalDTO>();
        private int idSeleccionado;
        private string valoresGuardados = string.Empty;

        public FrmServiciosAdicionales()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new ServicioAdicionalController();
            idSeleccionado = 0;
            dgvServicios.MensajeSinFilas(() => servicios.Count == 0
                ? "Todavía no hay servicios. Complete el formulario y toque Guardar."
                : "Ningún servicio coincide con la búsqueda.");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmServiciosAdicionales_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarFormulario();
        }

        private void FrmServiciosAdicionales_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
        }

        private void CargarGrilla()
        {
            servicios = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        private void MostrarGrilla()
        {
            dgvServicios.DataSource = null;
            dgvServicios.DataSource = Busqueda.Filtrar(servicios, txtBuscar.Text, s => new[] { s.Nombre, s.Descripcion });
            ConfigurarColumnas();
            dgvServicios.QuitarSeleccion();
        }

        private void ConfigurarColumnas()
        {
            dgvServicios.Columna("Id").Visible = false;
            dgvServicios.Columna("Descripcion").HeaderText = "Descripción";
            dgvServicios.Columna("Monto").DefaultCellStyle.Format = Formato.Moneda;
        }

        private ServicioAdicionalDTO? ObtenerSeleccionado()
        {
            return dgvServicios.Seleccionado<ServicioAdicionalDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un servicio de la grilla para editar.");
                return;
            }
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                return;
            }

            try
            {
                ServicioAdicionalDTO servicio = controlador.ObtenerPorId(seleccionado.Id);
                CargarFormulario(servicio);
                idSeleccionado = servicio.Id;
                MostrarModoEdicion(servicio.Nombre);
            }
            catch (NoEncontradoException ex)
            {
                Mensajes.Advertencia(ex.Message);
                CargarGrilla();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ServicioAdicionalDTO dto = ArmarDto();
                bool eraEdicion = idSeleccionado != 0;
                controlador.Guardar(dto);

                CargarGrilla();
                LimpiarFormulario();
                string nombre = dto.Nombre.Trim();
                dgvServicios.SeleccionarFila<ServicioAdicionalDTO>(s => s.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                Aviso.Mostrar(lblAviso, eraEdicion ? "Servicio actualizado." : "Servicio guardado.");
            }
            catch (NegocioException ex)
            {
                Mensajes.Advertencia(ex.Message);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un servicio de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este servicio?"))
            {
                try
                {
                    controlador.Eliminar(seleccionado.Id);
                    CargarGrilla();
                    if (seleccionado.Id == idSeleccionado)
                    {
                        LimpiarFormulario();
                    }
                    Aviso.Mostrar(lblAviso, "Servicio eliminado.");
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }

        private ServicioAdicionalDTO ArmarDto()
        {
            ServicioAdicionalDTO dto = new ServicioAdicionalDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.Descripcion = txtBoxDescripcion.Text;
            dto.Monto = numMonto.Value;
            return dto;
        }

        private void CargarFormulario(ServicioAdicionalDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            txtBoxDescripcion.Text = dto.Descripcion;
            numMonto.Value = dto.Monto;
        }

        private void MostrarModoEdicion(string nombre)
        {
            lblModo.Text = "Editando: " + nombre;
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 1);
            btnCancelarEdicion.Visible = true;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtBoxNombre.Text = string.Empty;
            txtBoxDescripcion.Text = string.Empty;
            numMonto.Value = 0;

            lblModo.Text = "Nuevo servicio";
            btnGuardar.Text = "Guardar";
            tlpBotones.SetColumnSpan(btnGuardar, 2);
            btnCancelarEdicion.Visible = false;
            valoresGuardados = Formulario.Valores(tlpFormulario);
        }

        private bool HayCambiosSinGuardar()
        {
            return Formulario.Valores(tlpFormulario) != valoresGuardados;
        }
    }
}
