namespace Servicio.Interface.OpcionDetalle
{
    public class OpcionDetalleDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public long OpcionId { get; set; }
        public string OpcionStr { get; set; }
        public long AlimentoId { get; set; }
        public string AlimentoStr { get; set; }
        public decimal Cantidad { get; set; }
        public long UnidadMedidaId { get; set; }
        public string UnidadMedidaStr { get; set; }
        public bool Eliminado { get; set; }
    }
}
