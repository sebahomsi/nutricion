using System.Security.Claims;
using System.Security.Principal;

namespace NutricionWeb.Helpers.Identity
{
    public static class IdentityHelper
    {
        public static long GetConsultorioId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("EstablecimientoId");

            var idStr = claim != null ? claim.Value : string.Empty;

            return long.TryParse(idStr, out var id) ? id : -1;
        }
    }
}