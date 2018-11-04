using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Servicio.Interface.Persona;

namespace NutricionWeb.Helpers.Persona
{
    public class ComboBoxSexo : IComboBoxSexo
    {
        private readonly ISexoServicio _sexoServicio;

        public ComboBoxSexo(ISexoServicio sexoServicio)
        {
            _sexoServicio = sexoServicio;
        }
        public async Task<IEnumerable<SelectListItem>> Poblar()
        {
            var sexo = await _sexoServicio.ObtenerSexo();
            return new SelectList(sexo, "Codigo", "Descripcion");
        }
    }
}