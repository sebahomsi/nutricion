namespace Dominio.Entidades
{
    public class Anamnesis : EntidadBase
    {
        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
