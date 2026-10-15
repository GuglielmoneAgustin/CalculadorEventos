using ProyectoEventos.Common;

namespace ProyectoEventos.Forms.Utils
{
    // Cómo se muestra el estado de un presupuesto en las grillas: texto y colores.
    // verde = aprobado, terracota = cancelado, arena = en espera; sin presupuesto no se destaca.
    internal static class EstadoPresupuestoVista
    {
        private static Font? fuenteNegrita;

        public static string Texto(EstadoPresupuesto? estado)
        {
            switch (estado)
            {
                case EstadoPresupuesto.Aprobado:
                    return "Aprobado";
                case EstadoPresupuesto.Cancelado:
                    return "Cancelado";
                case EstadoPresupuesto.EnEspera:
                    return "En espera";
                default:
                    return "Sin presupuesto";
            }
        }

        public static void Aplicar(DataGridViewCellFormattingEventArgs e, EstadoPresupuesto? estado, Font fuenteBase)
        {
            string texto;
            Color fondo;
            Color color;
            switch (estado)
            {
                case EstadoPresupuesto.Aprobado:
                    texto = Texto(estado);
                    fondo = Color.FromArgb(205, 218, 193);
                    color = Color.FromArgb(52, 74, 40);
                    break;
                case EstadoPresupuesto.Cancelado:
                    texto = Texto(estado);
                    fondo = Color.FromArgb(235, 203, 195);
                    color = Color.FromArgb(120, 52, 38);
                    break;
                case EstadoPresupuesto.EnEspera:
                    texto = Texto(estado);
                    fondo = Tema.Vanilla;
                    color = Tema.Carob;
                    break;
                default:
                    e.Value = Texto(estado);
                    e.FormattingApplied = true;
                    return;
            }

            e.Value = texto;
            e.FormattingApplied = true;
            e.CellStyle.BackColor = fondo;
            e.CellStyle.ForeColor = color;
            e.CellStyle.SelectionBackColor = fondo;
            e.CellStyle.SelectionForeColor = color;
            e.CellStyle.Font = fuenteNegrita ??= new Font(fuenteBase, FontStyle.Bold);
        }
    }
}
