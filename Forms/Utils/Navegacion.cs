namespace ProyectoEventos.Forms.Utils
{
    internal static class Navegacion
    {
        public static void AbrirSubpantalla(Form origen, Form pantalla, Action alVolver)
        {
            if (origen.ParentForm is FrmPrincipal principal)
            {
                principal.AbrirSubpantalla(pantalla, alVolver);
                return;
            }

            using (pantalla)
            {
                pantalla.ShowDialog(origen);
            }
            alVolver();
        }
    }
}
