namespace Servicio.Interface.GrupoReceta
{
    public class GrupoRecetaDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}
