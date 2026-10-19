using ProyectoEventos.Dto;

namespace ProyectoEventos.Forms.Utils
{
    internal static class TicketPresupuesto
    {
        private const int Ancho = 42;

        public static void Mostrar(IWin32Window padre, PresupuestoDetalleDTO detalle)
        {
            TextBox texto = new TextBox();
            texto.Multiline = true;
            texto.ReadOnly = true;
            texto.ScrollBars = ScrollBars.Vertical;
            texto.Dock = DockStyle.Fill;
            texto.BorderStyle = BorderStyle.None;
            texto.BackColor = Color.White;
            texto.Font = new Font("Consolas", 10.5F);
            texto.Text = string.Join(Environment.NewLine, Armar(detalle));
            texto.SelectionStart = 0;

            using Form ventana = new Form();
            ventana.Text = "Ticket del presupuesto " + detalle.Id;
            ventana.StartPosition = FormStartPosition.CenterParent;
            ventana.ClientSize = new Size(440, 640);
            ventana.MinimizeBox = false;
            ventana.ShowInTaskbar = false;
            ventana.Padding = new Padding(16);
            ventana.BackColor = Color.White;
            ventana.Controls.Add(texto);
            ventana.ShowDialog(padre);
        }

        private static List<string> Armar(PresupuestoDetalleDTO d)
        {
            string doble = new string('=', Ancho);
            string simple = new string('-', Ancho);
            List<string> t = new List<string>();

            t.Add(Centrar("PRESUPUESTO N° " + d.Id));
            t.Add(Centrar("Evento N° " + d.IdEvento));
            t.Add(Centrar(d.Estado.Texto().ToUpper()));
            t.Add(doble);
            t.Add("Emitido: " + d.FechaGeneracion.ToString(Formato.FechaHora));
            t.Add("Cliente: " + d.NombreCliente);
            t.Add("Lugar: " + d.NombreLugar);
            t.Add("Fecha: " + d.FechaEvento.ToString(Formato.FechaHora));
            t.Add("Duración: " + d.HorasDuracion.ToString("0.##") + " hs");
            t.Add("Invitados: " + Personas(d.CantidadAdultos, d.CantidadNinos));
            t.Add(simple);
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
            t.Add(simple);

            t.Add("MENÚ");
            if (d.Items.Count == 0)
            {
                t.Add("  (sin platos)");
            }
            foreach (ItemMenuResultadoDTO item in d.Items)
            {
                t.AddRange(Fila("  " + item.DescripcionComponentes, item.Subtotal));
                t.Add("    " + Personas(item.CantidadAdultos, item.CantidadNinos));
            }
            t.AddRange(Fila("Subtotal menú", d.SubtotalMenu));
            t.Add(simple);

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
            t.Add(doble);

            t.AddRange(Fila("TOTAL", d.CostoTotal));
            t.Add(doble);
            t.Add(Centrar("Gracias por elegirnos"));
            return t;
        }

        private static string Personas(int adultos, int ninos)
        {
            return adultos + (adultos == 1 ? " adulto, " : " adultos, ") + ninos + (ninos == 1 ? " niño" : " niños");
        }

        private static string Centrar(string texto)
        {
            return texto.Length >= Ancho ? texto : new string(' ', (Ancho - texto.Length) / 2) + texto;
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
