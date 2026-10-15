using System.Drawing.Printing;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Forms.Utils
{
    // Arma e imprime el ticket de un presupuesto: texto de ancho fijo con fuente monoespaciada,
    // pensado para papel angosto (ticketera) pero válido en cualquier impresora.
    internal sealed class TicketPresupuesto
    {
        private const int Ancho = 42;

        private readonly List<string> lineas;
        private int proximaLinea;

        private TicketPresupuesto(PresupuestoDetalleDTO detalle)
        {
            lineas = Armar(detalle);
        }

        // Muestra la vista previa; desde ahí se imprime con el botón de la impresora.
        public static void MostrarVistaPrevia(IWin32Window padre, PresupuestoDetalleDTO detalle)
        {
            TicketPresupuesto ticket = new TicketPresupuesto(detalle);

            using PrintDocument documento = new PrintDocument();
            documento.DocumentName = "Presupuesto " + detalle.Id;
            documento.BeginPrint += (sender, e) => ticket.proximaLinea = 0;
            documento.PrintPage += ticket.ImprimirPagina;

            using PrintPreviewDialog vista = new PrintPreviewDialog();
            vista.Document = documento;
            vista.Text = "Ticket del presupuesto " + detalle.Id;
            vista.Width = 700;
            vista.Height = 800;
            vista.ShowDialog(padre);
        }

        // ---------- Impresión ----------

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics grafico = e.Graphics!;
            using Font fuente = CrearFuente(grafico, e.MarginBounds.Width);
            float alto = fuente.GetHeight(grafico);
            float y = e.MarginBounds.Top;

            while (proximaLinea < lineas.Count)
            {
                if (y > e.MarginBounds.Top && y + alto > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                grafico.DrawString(lineas[proximaLinea], fuente, Brushes.Black, e.MarginBounds.Left, y, StringFormat.GenericTypographic);
                y += alto;
                proximaLinea++;
            }

            e.HasMorePages = false;
        }

        // Si el papel es más angosto que el ticket, se achica la letra para que entren las 42 columnas.
        private static Font CrearFuente(Graphics grafico, float anchoDisponible)
        {
            string familia = FontFamily.Families.Any(f => f.Name == "Consolas") ? "Consolas" : "Courier New";
            const float tamanoBase = 9f;

            Font fuente = new Font(familia, tamanoBase);
            float anchoCaracter = grafico.MeasureString("0", fuente, int.MaxValue, StringFormat.GenericTypographic).Width;
            float anchoTicket = anchoCaracter * Ancho;

            if (anchoTicket > anchoDisponible)
            {
                fuente.Dispose();
                fuente = new Font(familia, tamanoBase * anchoDisponible / anchoTicket);
            }

            return fuente;
        }

        // ---------- Contenido ----------

        private static List<string> Armar(PresupuestoDetalleDTO d)
        {
            string doble = new string('=', Ancho);
            string simple = new string('-', Ancho);
            List<string> t = new List<string>();

            t.Add(Centrar("PRESUPUESTO N° " + d.Id));
            t.Add(Centrar("Evento N° " + d.IdEvento));
            t.Add(Centrar(EstadoPresupuestoVista.Texto(d.Estado).ToUpper()));
            t.Add(doble);
            t.AddRange(Campo("Emitido", d.FechaGeneracion.ToString(Formato.FechaHora)));
            t.AddRange(Campo("Cliente", d.NombreCliente));
            t.AddRange(Campo("Lugar", d.NombreLugar));
            t.AddRange(Campo("Fecha", d.FechaEvento.ToString(Formato.FechaHora)));
            t.AddRange(Campo("Duración", d.HorasDuracion.ToString("0.##") + " hs"));
            t.AddRange(Campo("Invitados", Personas(d.CantidadAdultos, d.CantidadNinos)));
            t.Add(simple);
            t.AddRange(Fila("Costo del lugar", Moneda(d.CostoLugar)));
            t.Add(simple);

            t.Add("MENÚ");
            if (d.Items.Count == 0)
            {
                t.Add("  (sin platos)");
            }
            foreach (PresupuestoItemDTO item in d.Items)
            {
                t.AddRange(Fila(item.Descripcion, Moneda(item.Subtotal), 2));
                t.Add("    " + Personas(item.CantidadAdultos, item.CantidadNinos));
            }
            t.AddRange(Fila("Subtotal menú", Moneda(d.SubtotalMenu)));
            t.Add(simple);

            t.Add("SERVICIOS ADICIONALES");
            if (d.Servicios.Count == 0)
            {
                t.Add("  (sin servicios)");
            }
            foreach (PresupuestoServicioDTO servicio in d.Servicios)
            {
                t.AddRange(Fila(servicio.Nombre, Moneda(servicio.Monto), 2));
            }
            t.AddRange(Fila("Subtotal servicios", Moneda(d.SubtotalServicios)));
            t.Add(doble);

            t.AddRange(Fila("TOTAL", Moneda(d.CostoTotal)));
            t.Add(doble);
            t.Add(Centrar("Gracias por elegirnos"));
            return t;
        }

        private static string Moneda(decimal valor)
        {
            return valor.ToString(Formato.Moneda);
        }

        private static string Personas(int adultos, int ninos)
        {
            return adultos + (adultos == 1 ? " adulto, " : " adultos, ") + ninos + (ninos == 1 ? " niño" : " niños");
        }

        private static string Centrar(string texto)
        {
            return texto.Length >= Ancho ? texto : new string(' ', (Ancho - texto.Length) / 2) + texto;
        }

        // "Etiqueta: valor"; si el valor no entra, sigue en la línea de abajo alineado con el primero.
        private static List<string> Campo(string etiqueta, string valor)
        {
            string prefijo = etiqueta + ": ";
            List<string> partes = Ajustar(valor, Ancho - prefijo.Length);
            List<string> resultado = new List<string>();
            for (int i = 0; i < partes.Count; i++)
            {
                resultado.Add((i == 0 ? prefijo : new string(' ', prefijo.Length)) + partes[i]);
            }
            return resultado;
        }

        // Texto a la izquierda y monto a la derecha; si el texto es largo se parte en varias líneas
        // y el monto va en la última.
        private static List<string> Fila(string izquierda, string derecha, int sangria = 0)
        {
            List<string> partes = Ajustar(izquierda, Ancho - derecha.Length - 1 - sangria);
            List<string> resultado = new List<string>();
            for (int i = 0; i < partes.Count; i++)
            {
                string linea = new string(' ', sangria) + partes[i];
                if (i == partes.Count - 1)
                {
                    linea = linea.PadRight(Ancho - derecha.Length) + derecha;
                }
                resultado.Add(linea);
            }
            return resultado;
        }

        // Parte un texto en líneas de hasta "ancho" caracteres, cortando entre palabras.
        private static List<string> Ajustar(string texto, int ancho)
        {
            ancho = Math.Max(ancho, 10);
            List<string> resultado = new List<string>();
            string actual = string.Empty;

            foreach (string original in (texto ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                string palabra = original;
                while (palabra.Length > ancho)
                {
                    if (actual.Length > 0)
                    {
                        resultado.Add(actual);
                        actual = string.Empty;
                    }
                    resultado.Add(palabra.Substring(0, ancho));
                    palabra = palabra.Substring(ancho);
                }

                if (actual.Length == 0)
                {
                    actual = palabra;
                }
                else if (actual.Length + 1 + palabra.Length <= ancho)
                {
                    actual += " " + palabra;
                }
                else
                {
                    resultado.Add(actual);
                    actual = palabra;
                }
            }

            if (actual.Length > 0 || resultado.Count == 0)
            {
                resultado.Add(actual);
            }
            return resultado;
        }
    }
}
