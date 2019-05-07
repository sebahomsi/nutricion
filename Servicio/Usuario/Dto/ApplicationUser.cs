using Microsoft.AspNet.Identity.EntityFramework;

namespace Servicio.Usuario.Dto
{
    public class ApplicationUser : IdentityUser
    {
        public long? EstablecimientoId { get; set; }
    }
}
