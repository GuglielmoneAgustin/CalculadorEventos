using System.Drawing;
using System.Windows.Forms;
using ProyectoEventos.Common.Enums;

namespace ProyectoEventos.Forms.Utils
{
    internal static class Tema2
    {
        public static readonly Color VerdeIngles = ColorTranslator.FromHtml("#1F3A2E");
        public static readonly Color VerdeInglesHover = ColorTranslator.FromHtml("#2B4A3C");
        public static readonly Color Laton = ColorTranslator.FromHtml("#B39457");
        public static readonly Color LatonClaro = ColorTranslator.FromHtml("#E3C98E");
        public static readonly Color Marfil = ColorTranslator.FromHtml("#F4F0E6");
        public static readonly Color Pergamino = ColorTranslator.FromHtml("#E2DACA");

        public static readonly Color Fondo = Marfil;
        public static readonly Color Tarjeta = ColorTranslator.FromHtml("#FBF9F3");
        public static readonly Color Campo = Color.White;
        public static readonly Color FilaAlterna = ColorTranslator.FromHtml("#F6F2EA");
        public static readonly Color Texto = ColorTranslator.FromHtml("#2A2A24");
        public static readonly Color TextoSuave = ColorTranslator.FromHtml("#5E6657");
        public static readonly Color TextoApagado = ColorTranslator.FromHtml("#A39E91");
        public static readonly Color Borde = Pergamino;
        public static readonly Color Seleccion = ColorTranslator.FromHtml("#E3EADF");
        public static readonly Color Primario = VerdeIngles;
        public static readonly Color PrimarioHover = VerdeInglesHover;
        public static readonly Color SecundarioHover = ColorTranslator.FromHtml("#F1EBDD");
        public static readonly Color Peligro = ColorTranslator.FromHtml("#8E3B2E");
        public static readonly Color PeligroHover = ColorTranslator.FromHtml("#74302A");
        public static readonly Color VerdeSuave = ColorTranslator.FromHtml("#DDE7DA");
        public static readonly Color VerdeOscuro = ColorTranslator.FromHtml("#2E4F33");
        public static readonly Color AmbarSuave = ColorTranslator.FromHtml("#EFE4C9");
        public static readonly Color AmbarOscuro = ColorTranslator.FromHtml("#6E5520");
        public static readonly Color OlivaSuave = ColorTranslator.FromHtml("#E1E6D3");
        public static readonly Color OlivaOscuro = ColorTranslator.FromHtml("#46522B");
        public static readonly Color PeligroSuave = ColorTranslator.FromHtml("#F1DDD5");
        public static readonly Color PeligroOscuro = ColorTranslator.FromHtml("#7A2E22");

        public static readonly Color Menu = VerdeIngles;
        public static readonly Color MenuHover = VerdeInglesHover;
        public static readonly Color MenuTexto = ColorTranslator.FromHtml("#CFC9B4");
        public static readonly Color Encabezado = Marfil;

        private const string Serif = "Georgia";

        private static readonly Font FuenteNormal = new Font("Segoe UI", 10F);
        private static readonly Font FuenteEtiqueta = new Font("Segoe UI", 9.5F);
        private static readonly Font FuenteNegrita = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font FuenteInfo = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        private static readonly Font FuenteTitulo = new Font(Serif, 13F);
        private static readonly Font FuenteCampo = new Font("Segoe UI", 10.5F);
        private static readonly Font FuenteResumen = new Font(Serif, 18F);

        public static void Aplicar(Form form)
        {
            form.BackColor = Fondo;
            EstilarHijos(form);
        }

        public static void EstilarMenu(Panel menu, Label titulo, Panel encabezado, Label pantalla)
        {
            menu.BackColor = Menu;
            titulo.Font = new Font(Serif, 15F);
            titulo.ForeColor = Marfil;
            titulo.Paint += DibujarLineaInferior;
            encabezado.BackColor = Encabezado;
            pantalla.Font = new Font(Serif, 18F);
            pantalla.ForeColor = VerdeIngles;
            pantalla.Paint += DibujarLineaInferior;
            foreach (Control control in menu.Controls)
            {
                if (control is Button boton)
                {
                    boton.BackColor = Menu;
                    boton.ForeColor = MenuTexto;
                    boton.FlatAppearance.MouseOverBackColor = MenuHover;
                    boton.FlatAppearance.MouseDownBackColor = MenuHover;
                    boton.Paint += DibujarMarcaActivo;
                }
            }
        }

        public static void MarcarBotonMenu(Button boton, bool activo)
        {
            boton.Tag = activo ? "activo" : null;
            boton.ForeColor = activo ? LatonClaro : MenuTexto;
            boton.Invalidate();
        }

        private static void DibujarMarcaActivo(object? sender, PaintEventArgs e)
        {
            if (sender is Button boton && "activo".Equals(boton.Tag))
            {
                using (SolidBrush pincel = new SolidBrush(Laton))
                {
                    e.Graphics.FillRectangle(pincel, 0, 8, 3, boton.Height - 16);
                }
            }
        }

        private static void DibujarLineaInferior(object? sender, PaintEventArgs e)
        {
            if (sender is Control control)
            {
                using (Pen lapiz = new Pen(Laton))
                {
                    int y = control.Height - 1;
                    int margen = control.Padding.Left;
                    e.Graphics.DrawLine(lapiz, margen, y, control.Width - margen, y);
                }
            }
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
            using (SolidBrush fondo = new SolidBrush(resaltado ? Seleccion : Campo))
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
                etiqueta.ForeColor = VerdeIngles;
            }
            else if (nombre == "lblinfoevento")
            {
                etiqueta.AutoSize = true;
                etiqueta.BackColor = AmbarSuave;
                etiqueta.Font = FuenteInfo;
                etiqueta.ForeColor = Texto;
                etiqueta.Padding = new Padding(14, 10, 14, 10);
            }
            else if (nombre == "lblalerta")
            {
                etiqueta.BackColor = PeligroSuave;
                etiqueta.Font = FuenteNegrita;
                etiqueta.ForeColor = PeligroOscuro;
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
                etiqueta.ForeColor = VerdeIngles;
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
                etiqueta.ForeColor = VerdeIngles;
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
                case "btnRegistrarSena":
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
                    boton.BackColor = Tarjeta;
                    boton.ForeColor = VerdeIngles;
                    boton.FlatAppearance.BorderColor = Laton;
                    boton.FlatAppearance.MouseOverBackColor = SecundarioHover;
                    boton.FlatAppearance.MouseDownBackColor = Pergamino;
                    break;
            }
        }

        private static void EstilarGrilla(DataGridView grilla)
        {
            grilla.BorderStyle = BorderStyle.None;
            grilla.BackgroundColor = Tarjeta;
            grilla.GridColor = Pergamino;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.EnableHeadersVisualStyles = false;
            grilla.RowHeadersVisible = false;
            grilla.AllowUserToResizeRows = false;
            grilla.AllowUserToResizeColumns = false;

            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.ColumnHeadersHeight = 42;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = Tarjeta;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = VerdeIngles;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Tarjeta;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = VerdeIngles;
            grilla.ColumnHeadersDefaultCellStyle.Font = FuenteNegrita;
            grilla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            grilla.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grilla.CellPainting -= AjustarAnchoAlTitulo;
            grilla.CellPainting += AjustarAnchoAlTitulo;
            grilla.CellPainting -= DibujarLineaEncabezado;
            grilla.CellPainting += DibujarLineaEncabezado;
            grilla.ColumnAdded -= SinOrdenar;
            grilla.ColumnAdded += SinOrdenar;

            grilla.RowTemplate.Height = 36;
            grilla.DefaultCellStyle.BackColor = Tarjeta;
            grilla.DefaultCellStyle.ForeColor = Texto;
            grilla.DefaultCellStyle.SelectionBackColor = Seleccion;
            grilla.DefaultCellStyle.SelectionForeColor = Texto;
            grilla.DefaultCellStyle.Font = FuenteNormal;
            grilla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
            grilla.AlternatingRowsDefaultCellStyle.SelectionBackColor = Seleccion;
            grilla.AlternatingRowsDefaultCellStyle.SelectionForeColor = Texto;
        }

        private static void SinOrdenar(object? sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static void DibujarLineaEncabezado(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1 || e.Graphics == null)
            {
                return;
            }
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            using (Pen lapiz = new Pen(Laton, 2))
            {
                e.Graphics.DrawLine(lapiz, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
            }
            e.Handled = true;
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
                    PintarCelda(e, PeligroSuave, PeligroOscuro);
                    break;
                case EstadoPresupuesto.EnEspera:
                    PintarCelda(e, AmbarSuave, AmbarOscuro);
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
                    PintarCelda(e, AmbarSuave, AmbarOscuro);
                    break;
                case EstadoPago.SinSena:
                    PintarCelda(e, PeligroSuave, PeligroOscuro);
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
                    PintarCelda(e, PeligroSuave, PeligroOscuro);
                    break;
                case TipoPendiente.Desactualizado:
                    PintarCelda(e, AmbarSuave, AmbarOscuro);
                    break;
                case TipoPendiente.FaltaCobrar:
                    PintarCelda(e, OlivaSuave, OlivaOscuro);
                    break;
                default:
                    PintarCelda(e, Pergamino, TextoSuave);
                    break;
            }
        }

        public static void ResaltarFila(DataGridViewCellFormattingEventArgs e, bool negrita)
        {
            e.CellStyle!.BackColor = AmbarSuave;
            e.CellStyle.SelectionBackColor = Pergamino;
            if (negrita)
            {
                e.CellStyle.Font = FuenteNegrita;
            }
        }
    }
}
