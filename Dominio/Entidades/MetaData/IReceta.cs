namespace Dominio.Entidades.MetaData
{
    public interface IReceta
    {
        int Codigo { get; set; }
        string Descripcion { get; set; }
        bool Eliminado { get; set; }
    }
}
