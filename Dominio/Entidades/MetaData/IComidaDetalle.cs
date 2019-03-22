namespace Dominio.Entidades.MetaData
{
    public interface IComidaDetalle
    {
        int Codigo { get; set; }
        string Comentario { get; set; }
        long OpcionId { get; set; }
        long ComidaId { get; set; }
        bool Eliminado { get; set; }
    }
}
