namespace Dominio.Entidades
{
    public class Estrategia : EntidadBase
    {
        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
