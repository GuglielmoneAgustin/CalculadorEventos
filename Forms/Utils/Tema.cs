using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoEventos.Forms.Utils
{
    // Estilo visual compartido por todas las pantallas.
    // Se aplica con Tema.Aplicar(this) despues de InitializeComponent().
    internal static class Tema
    {
        // ---- Paleta de marca ----
        public static readonly Color Matcha = ColorTranslator.FromHtml("#809671");
        public static readonly Color Almond = ColorTranslator.FromHtml("#E5E0D8");
        public static readonly Color Pistache = ColorTranslator.FromHtml("#B3B792");
        public static readonly Color Chai = ColorTranslator.FromHtml("#D2AB80");
        public static readonly Color Carob = ColorTranslator.FromHtml("#725C3A");
        public static readonly Color Vanilla = ColorTranslator.FromHtml("#E5D2B8");

        // ---- Derivados de la paleta ----
        public static readonly Color Fondo = Almond;
        public static readonly Color Tarjeta = Color.FromArgb(243, 239, 232);       // Almond aclarado
        public static readonly Color Campo = Color.FromArgb(250, 247, 241);         // campos de entrada
        public static readonly Color FilaAlterna = Color.FromArgb(235, 229, 219);
        public static readonly Color Texto = Color.FromArgb(58, 46, 30);            // Carob oscuro
        public static readonly Color TextoSuave = Carob;
        public static readonly Color Borde = Vanilla;
        public static readonly Color CarobHover = Color.FromArgb(136, 112, 76);
        public static readonly Color Primario = Matcha;
        public static readonly Color PrimarioHover = Color.FromArgb(103, 124, 90);
        public static readonly Color Peligro = Color.FromArgb(170, 85, 66);         // terracota
        public static readonly Color PeligroHover = Color.FromArgb(140, 64, 48);
        public static readonly Color SecundarioHover = Color.FromArgb(222, 197, 160);

        private static readonly Font FuenteNormal = new Font("Segoe UI", 10F);
        private static readonly Font FuenteEtiqueta = new Font("Segoe UI", 9.5F);
        private static readonly Font FuenteNegrita = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font FuenteInfo = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
        private static readonly Font FuenteCampo = new Font("Segoe UI", 10.5F);
        private static readonly Font FuenteEncabezado = new Font("Segoe UI", 16F, FontStyle.Bold);
        private static readonly Font FuenteResumen = new Font("Segoe UI", 18F, FontStyle.Bold);

        public static void Aplicar(Form form)
        {
            form.BackColor = Fondo;
            EstilarHijos(form);
        }

        // Barra de titulo igual a la de la pantalla principal (para los dialogos)
        public static void AgregarEncabezado(Form form)
        {
            Label titulo = new Label();
            titulo.Dock = DockStyle.Fill;
            titulo.Font = FuenteEncabezado;
            titulo.ForeColor = Texto;
            titulo.Padding = new Padding(30, 0, 0, 0);
            titulo.Text = form.Text;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            titulo.Name = "lblTituloDialogo";

            Panel encabezado = new Panel();
            encabezado.BackColor = Vanilla;
            encabezado.Dock = DockStyle.Top;
            encabezado.Height = 70;
            encabezado.Name = "pnlEncabezadoDialogo";
            encabezado.Controls.Add(titulo);

            // Se agrega al final de la coleccion: se acopla primero (arriba del panel Fill)
            form.Controls.Add(encabezado);
            form.TextChanged += (s, e) => titulo.Text = form.Text;
        }

        private static void EstilarHijos(Control padre)
        {
            foreach (Control control in padre.Controls)
            {
                switch (control)
                {
                    case Button boton:
                        EstilarBoton(boton);
                        break;
                    case DataGridView grilla:
                        EstilarGrilla(grilla);
                        break;
                    case TextBox texto:
                        texto.BorderStyle = BorderStyle.FixedSingle;
                        texto.BackColor = Campo;
                        texto.ForeColor = Texto;
                        texto.Font = FuenteCampo;
                        break;
                    case NumericUpDown numero:
                        numero.BorderStyle = BorderStyle.FixedSingle;
                        numero.BackColor = Campo;
                        numero.ForeColor = Texto;
                        numero.Font = FuenteCampo;
                        break;
                    case ComboBox combo:
                        combo.FlatStyle = FlatStyle.Flat;
                        combo.BackColor = Campo;
                        combo.ForeColor = Texto;
                        combo.Font = FuenteCampo;
                        // En los de solo selección Windows ignora BackColor: se dibujan los ítems a mano
                        if (combo.DropDownStyle == ComboBoxStyle.DropDownList)
                        {
                            combo.DrawMode = DrawMode.OwnerDrawFixed;
                            combo.ItemHeight = FuenteCampo.Height + 6;
                            combo.DrawItem -= DibujarItemCombo;
                            combo.DrawItem += DibujarItemCombo;
                        }
                        break;
                    case DateTimePicker fecha:
                        fecha.Font = FuenteCampo;
                        fecha.CalendarMonthBackground = Campo;
                        break;
                    case CheckedListBox lista:
                        lista.BorderStyle = BorderStyle.None;
                        lista.BackColor = Campo;
                        lista.ForeColor = Texto;
                        lista.Font = FuenteCampo;
                        lista.IntegralHeight = false;
                        break;
                    case Label etiqueta:
                        EstilarEtiqueta(etiqueta);
                        break;
                    case TableLayoutPanel tabla:
                        EstilarTabla(tabla);
                        break;
                }
                EstilarHijos(control);
            }
        }

        private static void EstilarTabla(TableLayoutPanel tabla)
        {
            if (tabla.Name == "tlpRaiz")
            {
                tabla.Padding = new Padding(30, 24, 30, 30);
                return;
            }

            bool esTarjeta = tabla.Name == "tlpFormulario"
                || tabla.Name == "tlpGrilla"
                || tabla.Name.StartsWith("tlpTarjeta", StringComparison.Ordinal);
            if (!esTarjeta)
            {
                return;
            }

            tabla.BackColor = Tarjeta;
            tabla.Padding = new Padding(20);
            if (tabla.Name == "tlpFormulario")
            {
                tabla.Margin = new Padding(0, 0, 20, 0);
            }
            else if (tabla.Name == "tlpGrilla")
            {
                tabla.Margin = new Padding(0);
            }
            tabla.Paint += DibujarBorde;
            tabla.Resize += (s, e) => tabla.Invalidate();
        }

        private static void DibujarBorde(object? sender, PaintEventArgs e)
        {
            if (sender is Control control)
            {
                using (Pen lapiz = new Pen(Borde))
                {
                    e.Graphics.DrawRectangle(lapiz, 0, 0, control.Width - 1, control.Height - 1);
                }
            }
        }

        // Fondo de campo para el valor elegido y la lista; el ítem resaltado de la lista, en pistacho.
        // GetItemText respeta DisplayMember y el evento Format (por ejemplo, los textos con tildes).
        private static void DibujarItemCombo(object? sender, DrawItemEventArgs e)
        {
            if (sender is not ComboBox combo)
            {
                return;
            }

            bool resaltado = (e.State & DrawItemState.Selected) != 0 && (e.State & DrawItemState.ComboBoxEdit) == 0;
            using (SolidBrush fondo = new SolidBrush(resaltado ? Pistache : Campo))
            {
                e.Graphics.FillRectangle(fondo, e.Bounds);
            }

            if (e.Index >= 0)
            {
                Rectangle area = new Rectangle(e.Bounds.X + 3, e.Bounds.Y, e.Bounds.Width - 3, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, combo.GetItemText(combo.Items[e.Index]), combo.Font, area, Texto,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }

        private static void EstilarEtiqueta(Label etiqueta)
        {
            string nombre = etiqueta.Name.ToLowerInvariant();
            etiqueta.BackColor = Color.Transparent;

            if (nombre.StartsWith("lbldgv", StringComparison.Ordinal)
                || nombre == "lblproximoseventos"
                || nombre == "lblcomponentes"
                || nombre == "lblservicios")
            {
                etiqueta.Font = FuenteTitulo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre == "lblinfoevento")
            {
                // Franja con los datos del evento
                etiqueta.AutoSize = true;
                etiqueta.BackColor = Vanilla;
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
                etiqueta.Padding = new Padding(14, 10, 14, 10);
            }
            else if (nombre == "lbltotalmenu")
            {
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre.StartsWith("lblresumenvalor", StringComparison.Ordinal))
            {
                // Número grande de las tarjetas de resumen (pantalla de inicio)
                etiqueta.Font = FuenteResumen;
                etiqueta.ForeColor = Texto;
            }
            else
            {
                etiqueta.Font = FuenteEtiqueta;
                etiqueta.ForeColor = TextoSuave;
            }
        }

        private static void EstilarBoton(Button boton)
        {
            boton.UseVisualStyleBackColor = false;
            boton.FlatStyle = FlatStyle.Flat;
            boton.Cursor = Cursors.Hand;
            boton.Font = FuenteNegrita;
            boton.Height = 38;
            if (boton.Width < 110)
            {
                boton.Width = 110;
            }
            boton.FlatAppearance.BorderSize = 1;

            switch (boton.Name)
            {
                case "btnGuardar":
                case "btnNuevo":
                case "btnAprobar":
                case "btnFinalizar":
                case "btnSiguiente":
                case "btnAgregarItem":
                case "btnAgregarServicio":
                    boton.BackColor = Primario;
                    boton.ForeColor = Color.White;
                    boton.FlatAppearance.BorderColor = Primario;
                    boton.FlatAppearance.MouseOverBackColor = PrimarioHover;
                    boton.FlatAppearance.MouseDownBackColor = PrimarioHover;
                    break;
                case "btnEliminar":
                case "btnQuitarItem":
                case "btnQuitarServicio":
                case "btnCancelar":
                    boton.BackColor = Peligro;
                    boton.ForeColor = Color.White;
                    boton.FlatAppearance.BorderColor = Peligro;
                    boton.FlatAppearance.MouseOverBackColor = PeligroHover;
                    boton.FlatAppearance.MouseDownBackColor = PeligroHover;
                    break;
                default:
                    boton.BackColor = Vanilla;
                    boton.ForeColor = Texto;
                    boton.FlatAppearance.BorderColor = Chai;
                    boton.FlatAppearance.MouseOverBackColor = SecundarioHover;
                    boton.FlatAppearance.MouseDownBackColor = Chai;
                    break;
            }
        }

        private static void EstilarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = Tarjeta;
            grilla.GridColor = Vanilla;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.EnableHeadersVisualStyles = false;
            grilla.RowHeadersVisible = false;
            grilla.AllowUserToResizeRows = false;

            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.ColumnHeadersHeight = 42;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = Carob;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Almond;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Carob;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Almond;
            grilla.ColumnHeadersDefaultCellStyle.Font = FuenteNegrita;
            grilla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grilla.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            grilla.RowTemplate.Height = 36;
            grilla.DefaultCellStyle.BackColor = Tarjeta;
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.SelectionBackColor = Pistache;
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
            grilla.DefaultCellStyle.Font = FuenteNormal;
            grilla.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            grilla.AlternatingRowsDefaultCellStyle.SelectionBackColor = Pistache;
            grilla.AlternatingRowsDefaultCellStyle.SelectionForeColor = Texto;
        }
    }
}
