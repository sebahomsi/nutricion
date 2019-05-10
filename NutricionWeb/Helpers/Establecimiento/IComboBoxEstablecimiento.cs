using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Helpers.Establecimiento
{
    public interface IComboBoxEstablecimiento
    {
        Task<IEnumerable<SelectListItem>> Poblar();
    }
}