using Servicio.Interface.SubGrupoReceta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NutricionWeb.Helpers.SubGrupoReceta
{
    public class CmbSubGrupoReceta:ICmbSubGrupoReceta
    {
        private readonly ISubGrupoRecetaServicio _subGrupoServicio;

        public CmbSubGrupoReceta(ISubGrupoRecetaServicio subGrupoServicio)
        {
            _subGrupoServicio = subGrupoServicio;
        }

        public async Task<IEnumerable<SelectListItem>> Poblar()
        {
            var subGrupos = await _subGrupoServicio.Get(false, "");

            return new SelectList(subGrupos, "Id", "Descripcion");
        }
    }
}