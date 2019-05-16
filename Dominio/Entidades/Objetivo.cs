namespace Dominio.Entidades
{
    public class Objetivo : EntidadBase
    {
        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
