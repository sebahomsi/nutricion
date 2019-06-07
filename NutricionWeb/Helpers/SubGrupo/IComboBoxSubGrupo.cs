using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Helpers.SubGrupo
{
    public interface IComboBoxSubGrupo
    {
        Task<IEnumerable<SelectListItem>> Poblar();
    }
}