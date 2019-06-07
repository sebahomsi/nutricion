using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NutricionWeb.Helpers.SubGrupoReceta
{
    public interface ICmbSubGrupoReceta
    {
        Task<IEnumerable<SelectListItem>> Poblar();
    }
}