using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Servicio.Interface.SubGrupo;

namespace NutricionWeb.Helpers.SubGrupo
{
    public class ComboBoxSubGrupo : IComboBoxSubGrupo
    {
        private readonly ISubGrupoServicio _subGrupoServicio;

        public ComboBoxSubGrupo(ISubGrupoServicio subGrupoServicio)
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