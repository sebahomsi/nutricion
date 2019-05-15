namespace Dominio.Entidades.MetaData
{
    public interface IEstablecimiento
    {
        string Nombre { get; set; }
        string Profesional { get; set; }
        string Direccion { get; set; }
        string Email { get; set; }
        string Facebook { get; set; }
        string Twitter { get; set; }
        string Instagram { get; set; }
        string Telefono { get; set; }
        string Horario { get; set; }
    }
}
