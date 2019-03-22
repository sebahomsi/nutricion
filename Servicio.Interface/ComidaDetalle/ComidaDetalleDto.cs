using Servicio.Interface.Comida;
using Servicio.Interface.Opcion;

namespace Servicio.Interface.ComidaDetalle
{
    public class ComidaDetalleDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }

        public string OpcionStr { get; set; }
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }

        public OpcionDto Opcion { get; set; }
        public ComidaDto Comida { get; set; }
    }
}
