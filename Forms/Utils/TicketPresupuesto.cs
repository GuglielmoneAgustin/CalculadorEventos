using System.Drawing.Printing;
using ProyectoEventos.Common;
using ProyectoEventos.Common.Enums;
using ProyectoEventos.Dto;

namespace ProyectoEventos.Forms.Utils
{
    internal static class TicketPresupuesto
    {
        public const int Ancho = 42;
        private const string ImpresoraPdf = "Microsoft Print to PDF";

        public static void Mostrar(IWin32Window padre, PresupuestoDetalleDTO detalle, NegocioDTO negocio)
        {
            MostrarVentana(padre, Armar(detalle, negocio), "Ticket del presupuesto " + detalle.Id, "Presupuesto " + detalle.Id);
        }

        public static void MostrarComprobante(IWin32Window padre, PresupuestoDetalleDTO detalle, NegocioDTO negocio)
        {
            MostrarVentana(padre, ArmarComprobante(detalle, negocio, DateTime.Now), "Comprobante de pago del presupuesto " + detalle.Id,
                "Comprobante presupuesto " + detalle.Id);
        }

        private static void MostrarVentana(IWin32Window padre, List<string> lineas, string titulo, string nombreArchivo)
        {
            TextBox texto = new TextBox();
            texto.Multiline = true;
            texto.ReadOnly = true;
            texto.ScrollBars = ScrollBars.Vertical;
            texto.Dock = DockStyle.Fill;
            texto.BorderStyle = BorderStyle.None;
            texto.BackColor = Color.White;
            texto.Font = new Font("Consolas", 10.5F);
            texto.Text = string.Join(Environment.NewLine, lineas);
            texto.SelectionStart = 0;

            using Form ventana = new Form();

            Button btnGuardarPdf = new Button();
            btnGuardarPdf.Name = "btnGuardar";
            btnGuardarPdf.Text = "Guardar PDF";
            btnGuardarPdf.Dock = DockStyle.Bottom;
            btnGuardarPdf.Click += (s, e) => PedirRutaYGuardarPdf(ventana, lineas, nombreArchivo);

            ventana.Text = titulo;
            ventana.StartPosition = FormStartPosition.CenterParent;
            ventana.ClientSize = new Size(440, 680);
            ventana.MinimizeBox = false;
            ventana.ShowInTaskbar = false;
            ventana.Padding = new Padding(16);
            ventana.BackColor = Color.White;
            ventana.Controls.Add(texto);
            ventana.Controls.Add(btnGuardarPdf);
            Tema.Aplicar(ventana);
            ventana.BackColor = Color.White;
            texto.BorderStyle = BorderStyle.None;
            texto.BackColor = Color.White;
            texto.Font = new Font("Consolas", 10.5F);
            ventana.ShowDialog(padre);
        }

        private static void PedirRutaYGuardarPdf(IWin32Window padre, List<string> lineas, string nombre)
        {
            if (!PrinterSettings.InstalledPrinters.Cast<string>().Contains(ImpresoraPdf))
            {
                Mensajes.Advertencia("No se encontró la impresora \"" + ImpresoraPdf + "\" de Windows, que se usa para generar el PDF.");
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Title = "Guardar ticket en PDF";
            dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
            dialogo.FileName = nombre + ".pdf";
            if (dialogo.ShowDialog(padre) != DialogResult.OK)
            {
                return;
            }

            GuardarPdf(lineas, dialogo.FileName, nombre);
            Mensajes.Informacion("Ticket guardado en " + dialogo.FileName);
        }

        public static void GuardarPdf(List<string> lineas, string ruta, string nombreDocumento)
        {
            int proximaLinea = 0;
            Font? fuente = null;
            using PrintDocument documento = new PrintDocument();
            documento.DocumentName = nombreDocumento;
            documento.PrinterSettings.PrinterName = ImpresoraPdf;
            documento.PrinterSettings.PrintToFile = true;
            documento.PrinterSettings.PrintFileName = ruta;
            documento.PrintController = new StandardPrintController();
            documento.PrintPage += (s, e) =>
            {
                Graphics grafico = e.Graphics!;
                fuente ??= CrearFuenteParaHoja(grafico, e.MarginBounds, lineas.Count);
                float alto = fuente.GetHeight(grafico);
                float anchoTicket = grafico.MeasureString(new string('0', Ancho), fuente, int.MaxValue, StringFormat.GenericTypographic).Width;
                float x = e.MarginBounds.Left + (e.MarginBounds.Width - anchoTicket) / 2;
                float y = e.MarginBounds.Top;
                while (proximaLinea < lineas.Count && y + alto <= e.MarginBounds.Bottom)
                {
                    grafico.DrawString(lineas[proximaLinea], fuente, Brushes.Black, x, y, StringFormat.GenericTypographic);
                    y += alto;
                    proximaLinea++;
                }
                e.HasMorePages = proximaLinea < lineas.Count;
            };
            documento.Print();
            fuente?.Dispose();
        }

        private static Font CrearFuenteParaHoja(Graphics grafico, Rectangle area, int cantidadLineas)
        {
            const float tamanoBase = 10F;
            const float tamanoMaximo = 16F;
            using Font baseFuente = new Font("Consolas", tamanoBase);
            float anchoBase = grafico.MeasureString(new string('0', Ancho), baseFuente, int.MaxValue, StringFormat.GenericTypographic).Width;
            float altoBase = baseFuente.GetHeight(grafico) * cantidadLineas;

            float escala = Math.Min(area.Width / anchoBase, area.Height / altoBase);
            float tamano = Math.Clamp(tamanoBase * escala, tamanoBase, tamanoMaximo);
            return new Font("Consolas", tamano);
        }

        private static readonly string LineaDoble = new string('=', Ancho);
        private static readonly string LineaSimple = new string('-', Ancho);

        private static List<string> Armar(PresupuestoDetalleDTO d, NegocioDTO negocio)
        {
            List<string> t = new List<string>();
            ArmarDatosNegocio(t, negocio);
            ArmarEncabezado(t, d);
            ArmarLugar(t, d);
            ArmarMenu(t, d);
            ArmarServicios(t, d);
            ArmarTotal(t, d);
            ArmarPie(t, negocio);
            return t;
        }

        public static List<string> ArmarVistaPrevia(NegocioDTO negocio)
        {
            List<string> t = new List<string>();
            ArmarDatosNegocio(t, negocio);
            t.Add(Centrar("PRESUPUESTO"));
            t.Add(LineaDoble);
            t.Add("Emitido: " + DateTime.Now.ToString(Formato.FechaHora));
            t.Add(LineaSimple);
            t.Add(Centrar("(detalle del presupuesto)"));
            t.Add(LineaDoble);
            t.AddRange(Fila("TOTAL", 100000));
            t.Add(LineaDoble);
            ArmarPie(t, negocio);
            return t;
        }

        private static void ArmarDatosNegocio(List<string> t, NegocioDTO negocio)
        {
            t.AddRange(CentrarEnLineas(negocio.Nombre.ToUpper()));
            t.AddRange(CentrarEnLineas(negocio.Direccion));
            if (negocio.Telefono.Trim().Length > 0)
            {
                t.AddRange(CentrarEnLineas("Tel: " + negocio.Telefono));
            }
            t.AddRange(CentrarEnLineas(negocio.Mail));
            if (negocio.Cuit.Trim().Length > 0)
            {
                t.AddRange(CentrarEnLineas("CUIT: " + negocio.Cuit));
            }
            t.AddRange(CentrarEnLineas(negocio.Instagram));
            t.Add(LineaDoble);
        }

        private static void ArmarPie(List<string> t, NegocioDTO negocio)
        {
            t.AddRange(CentrarEnLineas(negocio.MensajePie));
        }

        private static void ArmarEncabezado(List<string> t, PresupuestoDetalleDTO d)
        {
            t.Add(Centrar("PRESUPUESTO"));
            t.Add(Centrar(d.Estado.Texto().ToUpper()));
            t.Add(LineaDoble);
            t.Add("Emitido: " + d.FechaGeneracion.ToString(Formato.FechaHora));
            t.Add("Cliente: " + d.NombreCliente);
            t.Add("Lugar: " + d.NombreLugar);
            t.Add("Fecha: " + d.FechaEvento.ToString(Formato.FechaHora));
            t.Add("Duración: " + d.HorasDuracion.ToString("0.##") + " hs");
            t.Add("Invitados: " + Personas(d.CantidadAdultos, d.CantidadNinos));
            t.Add(LineaSimple);
        }

        private static void ArmarLugar(List<string> t, PresupuestoDetalleDTO d)
        {
            if (d.CostoPorHoraLugar == 0)
            {
                t.AddRange(Fila("Costo del lugar", d.CostoLugar));
            }
            else
            {
                t.AddRange(Fila("Lugar (base)", d.CostoBaseLugar));
                t.AddRange(Fila("Lugar (" + d.HorasDuracion.ToString("0.##") + " hs x " + d.CostoPorHoraLugar.ToString(Formato.Moneda) + ")",
                    d.CostoPorHoraLugar * d.HorasDuracion));
            }
            t.Add(LineaSimple);
        }

        private static void ArmarMenu(List<string> t, PresupuestoDetalleDTO d)
        {
            t.Add("MENÚ");
            if (d.Items.Count == 0)
            {
                t.Add("  (sin platos)");
            }
            foreach (ItemMenuResultadoDTO item in d.Items)
            {
                t.AddRange(Fila("  " + item.Producto, item.Subtotal));
                t.Add("    " + Personas(item.CantidadAdultos, item.CantidadNinos));
            }
            t.AddRange(Fila("Subtotal menú", d.SubtotalMenu));
            t.Add(LineaSimple);
        }

        private static void ArmarServicios(List<string> t, PresupuestoDetalleDTO d)
        {
            t.Add("SERVICIOS ADICIONALES");
            if (d.Servicios.Count == 0)
            {
                t.Add("  (sin servicios)");
            }
            foreach (PresupuestoServicioDTO servicio in d.Servicios)
            {
                t.AddRange(Fila("  " + servicio.Nombre, servicio.Monto));
            }
            t.AddRange(Fila("Subtotal servicios", d.SubtotalServicios));
            t.Add(LineaDoble);
        }

        private static void ArmarTotal(List<string> t, PresupuestoDetalleDTO d)
        {
            t.AddRange(Fila("TOTAL", d.CostoTotal));
            if (d.Estado == EstadoPresupuesto.Aprobado && d.EstadoPago != EstadoPago.SinSena)
            {
                ArmarCobrado(t, d);
            }
            t.Add(LineaDoble);
        }

        private static List<string> ArmarComprobante(PresupuestoDetalleDTO d, NegocioDTO negocio, DateTime emision)
        {
            List<string> t = new List<string>();
            ArmarDatosNegocio(t, negocio);
            t.Add(Centrar("COMPROBANTE DE PAGO"));
            t.Add(LineaDoble);
            t.Add("Emitido: " + emision.ToString(Formato.FechaHora));
            t.Add("Cliente: " + d.NombreCliente);
            t.Add("Lugar: " + d.NombreLugar);
            t.Add("Fecha del evento: " + d.FechaEvento.ToString(Formato.FechaHora));
            t.Add(LineaSimple);
            if (d.Sena > 0)
            {
                t.AddRange(Fila("Seña (" + d.FechaSena?.ToString(Formato.Fecha) + ")", d.Sena));
            }
            if (d.FechaPago != null)
            {
                string concepto = d.Sena > 0 ? "Pago del saldo" : "Pago total";
                t.AddRange(Fila(concepto + " (" + d.FechaPago.Value.ToString(Formato.Fecha) + ")", d.CostoTotal - d.Sena));
            }
            t.Add(LineaSimple);
            t.AddRange(Fila("Total presupuesto", d.CostoTotal));
            ArmarCobrado(t, d);
            t.Add(LineaDoble);
            if (d.Sena > 0)
            {
                t.Add(Centrar("La seña no es reembolsable."));
            }
            t.Add(Centrar("No válido como factura."));
            ArmarPie(t, negocio);
            return t;
        }

        private static void ArmarCobrado(List<string> t, PresupuestoDetalleDTO d)
        {
            t.AddRange(Fila("Pagado", d.CostoTotal - d.Saldo));
            t.AddRange(d.Saldo == 0 ? Fila("Resta - PAGADO", 0) : Fila("RESTA", d.Saldo));
        }

        private static string Personas(int adultos, int ninos)
        {
            return adultos + (adultos == 1 ? " adulto, " : " adultos, ") + ninos + (ninos == 1 ? " niño" : " niños");
        }

        private static string Centrar(string texto)
        {
            return texto.Length >= Ancho ? texto : new string(' ', (Ancho - texto.Length) / 2) + texto;
        }

        private static List<string> CentrarEnLineas(string texto)
        {
            List<string> lineas = new List<string>();
            string actual = string.Empty;
            foreach (string palabra in texto.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (actual.Length > 0 && actual.Length + 1 + palabra.Length > Ancho)
                {
                    lineas.Add(Centrar(actual));
                    actual = palabra;
                }
                else
                {
                    actual = actual.Length == 0 ? palabra : actual + " " + palabra;
                }
            }
            if (actual.Length > 0)
            {
                lineas.Add(Centrar(actual));
            }
            return lineas;
        }

        private static List<string> Fila(string texto, decimal monto)
        {
            string importe = monto.ToString(Formato.Moneda);
            if (texto.Length + importe.Length + 1 > Ancho)
            {
                return new List<string> { texto, importe.PadLeft(Ancho) };
            }
            return new List<string> { texto.PadRight(Ancho - importe.Length) + importe };
        }
    }
}
