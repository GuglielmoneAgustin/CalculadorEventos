using System;
using System.Windows.Forms;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Exceptions;
using ProyectoEventos.Controller;
using ProyectoEventos.Dto;
using ProyectoEventos.Forms.Utils;

namespace ProyectoEventos.Forms
{
    public partial class FrmLugares : Form
    {
        private readonly LugarController controlador;
        private List<LugarDTO> lugares = new List<LugarDTO>();
        private int idSeleccionado;
        private string valoresGuardados = string.Empty;

        public FrmLugares()
        {
            InitializeComponent();
            Tema.Aplicar(this);
            controlador = new LugarController();
            idSeleccionado = 0;
            dgvLugares.MensajeSinFilas(() => lugares.Count == 0
                ? "Todavía no hay lugares. Complete el formulario y toque Guardar."
                : "Ningún lugar coincide con la búsqueda.");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmLugares_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarFormulario();
        }

        private void FrmLugares_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                e.Cancel = true;
            }
        }

        private void CargarGrilla()
        {
            lugares = controlador.ObtenerTodos();
            MostrarGrilla();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            MostrarGrilla();
        }

        private void MostrarGrilla()
        {
            dgvLugares.DataSource = null;
            dgvLugares.DataSource = Busqueda.Filtrar(lugares, txtBuscar.Text, l => new[] { l.Nombre, l.Ubicacion });
            ConfigurarColumnas();
            dgvLugares.QuitarSeleccion();
        }

        private void ConfigurarColumnas()
        {
            dgvLugares.Columna("Id").Visible = false;
            dgvLugares.Columna("CostoBase").HeaderText = "Costo base";
            dgvLugares.Columna("Ubicacion").HeaderText = "Ubicación";
            dgvLugares.Columna("CostoBase").DefaultCellStyle.Format = Formato.Moneda;
            dgvLugares.Columna("CostoPorHora").HeaderText = "Costo por hora";
            dgvLugares.Columna("CostoPorHora").DefaultCellStyle.Format = Formato.Moneda;
            dgvLugares.Columna("Descripcion").Visible = false;
        }

        private LugarDTO? ObtenerSeleccionado()
        {
            return dgvLugares.Seleccionado<LugarDTO>();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            LugarDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un lugar de la grilla para editar.");
                return;
            }
            if (!Formulario.ConfirmarDescartarCambios(HayCambiosSinGuardar()))
            {
                return;
            }

            try
            {
                LugarDTO lugar = controlador.ObtenerPorId(seleccionado.Id);
                CargarFormulario(lugar);
                idSeleccionado = lugar.Id;
                MostrarModoEdicion(lugar.Nombre);
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
                LugarDTO dto = ArmarDto();
                bool eraEdicion = idSeleccionado != 0;
                controlador.Guardar(dto);

                CargarGrilla();
                LimpiarFormulario();
                string nombre = dto.Nombre.Trim();
                dgvLugares.SeleccionarFila<LugarDTO>(l => l.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                Aviso.Mostrar(lblAviso, eraEdicion ? "Lugar actualizado." : "Lugar guardado.");
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
            LugarDTO? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                Mensajes.Advertencia("Seleccione un lugar de la grilla para eliminar.");
                return;
            }

            if (Mensajes.Confirmar("¿Seguro que desea eliminar este lugar?"))
            {
                try
                {
                    controlador.Eliminar(seleccionado.Id);
                    CargarGrilla();
                    if (seleccionado.Id == idSeleccionado)
                    {
                        LimpiarFormulario();
                    }
                    Aviso.Mostrar(lblAviso, "Lugar eliminado.");
                }
                catch (NegocioException ex)
                {
                    Mensajes.Advertencia(ex.Message);
                }
            }
        }

        private LugarDTO ArmarDto()
        {
            LugarDTO dto = new LugarDTO();
            dto.Id = idSeleccionado;
            dto.Nombre = txtBoxNombre.Text;
            dto.CostoBase = numCosto.Value;
            dto.CostoPorHora = numCostoHora.Value;
            dto.Capacidad = Convert.ToInt32(numCapacidad.Value);
            dto.Ubicacion = txtBoxUbicacion.Text;
            return dto;
        }

        private void CargarFormulario(LugarDTO dto)
        {
            txtBoxNombre.Text = dto.Nombre;
            numCosto.Value = dto.CostoBase;
            numCostoHora.Value = dto.CostoPorHora;
            numCapacidad.Value = dto.Capacidad;
            txtBoxUbicacion.Text = dto.Ubicacion;
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
            numCosto.Value = 0;
            numCostoHora.Value = 0;
            numCapacidad.Value = 0;
            txtBoxUbicacion.Text = string.Empty;

            lblModo.Text = "Nuevo lugar";
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
