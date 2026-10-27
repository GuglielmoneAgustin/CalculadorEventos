namespace ProyectoEventos.Forms.Utils
{
    internal static class Navegacion
    {
        public static void AbrirSubpantalla(Form origen, Form pantalla)
        {
            if (origen.ParentForm is FrmPrincipal principal)
            {
                principal.AbrirSubpantalla(pantalla);
                return;
            }

            using (pantalla)
            {
                pantalla.ShowDialog(origen);
            }
            if (origen is IVuelveDeSubpantalla destino)
            {
                destino.AlVolverDeSubpantalla(pantalla);
            }
        }
    }
}
