using System.Drawing;
using System.Windows.Forms;
using ProyectoEventos.Common.Enums;

namespace ProyectoEventos.Forms.Utils
{
    internal static class Tema1
    {
        public static readonly Color Matcha = ColorTranslator.FromHtml("#809671");
        public static readonly Color Almond = ColorTranslator.FromHtml("#E5E0D8");
        public static readonly Color Pistache = ColorTranslator.FromHtml("#B3B792");
        public static readonly Color Chai = ColorTranslator.FromHtml("#D2AB80");
        public static readonly Color Carob = ColorTranslator.FromHtml("#725C3A");
        public static readonly Color Vanilla = ColorTranslator.FromHtml("#E5D2B8");

        public static readonly Color Fondo = Almond;
        public static readonly Color Tarjeta = Color.FromArgb(243, 239, 232);
        public static readonly Color Campo = Color.FromArgb(250, 247, 241);
        public static readonly Color FilaAlterna = Color.FromArgb(235, 229, 219);
        public static readonly Color Texto = Color.FromArgb(58, 46, 30);
        public static readonly Color TextoSuave = Carob;
        public static readonly Color Borde = Vanilla;
        public static readonly Color CarobHover = Color.FromArgb(136, 112, 76);
        public static readonly Color Primario = Matcha;
        public static readonly Color PrimarioHover = Color.FromArgb(103, 124, 90);
        public static readonly Color Peligro = Color.FromArgb(170, 85, 66);
        public static readonly Color PeligroHover = Color.FromArgb(140, 64, 48);
        public static readonly Color SecundarioHover = Color.FromArgb(222, 197, 160);
        public static readonly Color TextoApagado = Color.FromArgb(150, 140, 125);
        public static readonly Color VerdeSuave = Color.FromArgb(205, 218, 193);
        public static readonly Color VerdeOscuro = Color.FromArgb(52, 74, 40);
        public static readonly Color TerracotaSuave = Color.FromArgb(235, 203, 195);
        public static readonly Color TerracotaOscuro = Color.FromArgb(120, 52, 38);

        public static readonly Color Menu = Carob;
        public static readonly Color MenuHover = CarobHover;
        public static readonly Color MenuTexto = Almond;
        public static readonly Color Encabezado = Vanilla;
        public static readonly Color PeligroSuave = TerracotaSuave;
        public static readonly Color PeligroOscuro = TerracotaOscuro;

        private static readonly Font FuenteNormal = new Font("Segoe UI", 10F);
        private static readonly Font FuenteEtiqueta = new Font("Segoe UI", 9.5F);
        private static readonly Font FuenteNegrita = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font FuenteInfo = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
        private static readonly Font FuenteCampo = new Font("Segoe UI", 10.5F);
        private static readonly Font FuenteResumen = new Font("Segoe UI", 18F, FontStyle.Bold);

        public static void Aplicar(Form form)
        {
            form.BackColor = Fondo;
            EstilarHijos(form);
        }

        public static void EstilarMenu(Panel menu, Label titulo, Panel encabezado, Label pantalla)
        {
            menu.BackColor = Menu;
            titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            titulo.ForeColor = MenuTexto;
            encabezado.BackColor = Encabezado;
            pantalla.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            pantalla.ForeColor = Texto;
            foreach (Control control in menu.Controls)
            {
                if (control is Button boton)
                {
                    boton.BackColor = Menu;
                    boton.ForeColor = MenuTexto;
                    boton.FlatAppearance.MouseOverBackColor = MenuHover;
                }
            }
        }

        public static void MarcarBotonMenu(Button boton, bool activo)
        {
            boton.BackColor = activo ? Matcha : Menu;
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
                    case ListView vista:
                        vista.BorderStyle = BorderStyle.None;
                        vista.BackColor = Campo;
                        vista.ForeColor = Texto;
                        vista.Font = FuenteCampo;
                        break;
                    case CheckBox casilla:
                        casilla.Font = FuenteEtiqueta;
                        casilla.ForeColor = TextoSuave;
                        casilla.BackColor = Color.Transparent;
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
                // El NumericUpDown tiene adentro su propio cuadro de texto: si se estiliza, le queda un borde extra.
                if (control is not NumericUpDown)
                {
                    EstilarHijos(control);
                }
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
                || tabla.Name.StartsWith("tlpGrilla", StringComparison.Ordinal)
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
                tabla.AutoScroll = true;
            }
            else if (tabla.Name.StartsWith("tlpGrilla", StringComparison.Ordinal))
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
                || nombre == "lblpendientes"
                || nombre == "lblfechahoy"
                || nombre == "lblcomponentes"
                || nombre == "lblservicios"
                || nombre == "lblvistaprevia")
            {
                etiqueta.Font = FuenteTitulo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre == "lblinfoevento")
            {
                etiqueta.AutoSize = true;
                etiqueta.BackColor = Vanilla;
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
                etiqueta.Padding = new Padding(14, 10, 14, 10);
            }
            else if (nombre == "lblalerta")
            {
                etiqueta.BackColor = TerracotaSuave;
                etiqueta.Font = FuenteNegrita;
                etiqueta.ForeColor = TerracotaOscuro;
                etiqueta.Padding = new Padding(14, 10, 14, 10);
            }
            else if (nombre == "lbltotalmenu")
            {
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre.StartsWith("lblresumenvalor", StringComparison.Ordinal) || nombre == "lblsaludo")
            {
                etiqueta.Font = FuenteResumen;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre.StartsWith("lblresumentitulo", StringComparison.Ordinal))
            {
                etiqueta.Font = FuenteNegrita;
                etiqueta.ForeColor = TextoSuave;
            }
            else if (nombre.StartsWith("lblresumendetalle", StringComparison.Ordinal) || nombre == "lbldetallepago")
            {
                etiqueta.Font = FuenteNormal;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre == "lblmodo")
            {
                etiqueta.Font = FuenteTitulo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre == "lblestadopago")
            {
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
            }
            else if (nombre == "lblaviso")
            {
                etiqueta.Font = FuenteNegrita;
                etiqueta.ForeColor = PrimarioHover;
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
            Size texto = TextRenderer.MeasureText(boton.Text, FuenteNegrita);
            boton.Height = Math.Max(38, texto.Height + 14);
            boton.Width = Math.Max(Math.Max(boton.Width, 110), texto.Width + 32);
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
                case "btnRegistrarPago":
                case "btnMarcarPagado":
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
            grilla.AllowUserToResizeColumns = false;

            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.ColumnHeadersHeight = 42;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = Carob;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Almond;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Carob;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Almond;
            grilla.ColumnHeadersDefaultCellStyle.Font = FuenteNegrita;
            grilla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            grilla.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grilla.CellPainting -= AjustarAnchoAlTitulo;
            grilla.CellPainting += AjustarAnchoAlTitulo;
            grilla.ColumnAdded -= SinOrdenar;
            grilla.ColumnAdded += SinOrdenar;

            grilla.RowTemplate.Height = 36;
            grilla.DefaultCellStyle.BackColor = Tarjeta;
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.SelectionBackColor = Pistache;
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
            grilla.DefaultCellStyle.Font = FuenteNormal;
            grilla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            grilla.AlternatingRowsDefaultCellStyle.SelectionBackColor = Pistache;
            grilla.AlternatingRowsDefaultCellStyle.SelectionForeColor = Texto;
        }

        private static void SinOrdenar(object? sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static void AjustarAnchoAlTitulo(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1 || e.ColumnIndex < 0 || sender is not DataGridView grilla)
            {
                return;
            }

            DataGridViewColumn columna = grilla.Columns[e.ColumnIndex];
            int ancho = TextRenderer.MeasureText(columna.HeaderText, FuenteNegrita).Width
                + columna.HeaderCell.InheritedStyle.Padding.Horizontal + 6;
            if (columna.MinimumWidth < ancho)
            {
                grilla.BeginInvoke(() => columna.MinimumWidth = ancho);
            }
        }

        public static void PintarCelda(DataGridViewCellFormattingEventArgs e, Color fondo, Color color)
        {
            e.CellStyle!.BackColor = fondo;
            e.CellStyle.ForeColor = color;
            e.CellStyle.SelectionBackColor = fondo;
            e.CellStyle.SelectionForeColor = color;
            e.CellStyle.Font = FuenteNegrita;
        }

        public static void PintarEstado(DataGridViewCellFormattingEventArgs e, EstadoPresupuesto? estado)
        {
            e.Value = estado.Texto();
            e.FormattingApplied = true;
            switch (estado)
            {
                case EstadoPresupuesto.Aprobado:
                    PintarCelda(e, VerdeSuave, VerdeOscuro);
                    break;
                case EstadoPresupuesto.Cancelado:
                    PintarCelda(e, TerracotaSuave, TerracotaOscuro);
                    break;
                case EstadoPresupuesto.EnEspera:
                    PintarCelda(e, Vanilla, Carob);
                    break;
            }
        }

        public static void PintarEstadoPago(DataGridViewCellFormattingEventArgs e, EstadoPago? estado)
        {
            e.Value = estado?.Texto() ?? "-";
            e.FormattingApplied = true;
            switch (estado)
            {
                case EstadoPago.Pagado:
                    PintarCelda(e, VerdeSuave, VerdeOscuro);
                    break;
                case EstadoPago.Senado:
                    PintarCelda(e, Vanilla, Carob);
                    break;
                case EstadoPago.SinSena:
                    PintarCelda(e, TerracotaSuave, TerracotaOscuro);
                    break;
            }
        }

        public static void PintarPendiente(DataGridViewCellFormattingEventArgs e, TipoPendiente tipo)
        {
            e.Value = tipo.Texto();
            e.FormattingApplied = true;
            switch (tipo)
            {
                case TipoPendiente.SinPresupuesto:
                    PintarCelda(e, TerracotaSuave, TerracotaOscuro);
                    break;
                case TipoPendiente.Desactualizado:
                    PintarCelda(e, Chai, Texto);
                    break;
                case TipoPendiente.FaltaCobrar:
                    PintarCelda(e, Pistache, Texto);
                    break;
                default:
                    PintarCelda(e, Vanilla, Carob);
                    break;
            }
        }

        public static void ResaltarFila(DataGridViewCellFormattingEventArgs e, bool negrita)
        {
            e.CellStyle!.BackColor = Vanilla;
            e.CellStyle.SelectionBackColor = Chai;
            if (negrita)
            {
                e.CellStyle.Font = FuenteNegrita;
            }
        }
    }
}
