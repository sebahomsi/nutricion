using Servicio.Interface.Establecimiento;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Helpers.Establecimiento
{
    public class ComboBoxEstablecimiento : IComboBoxEstablecimiento
    {
        private readonly IEstablecimientoServicio _establecimientoServicio;

        public ComboBoxEstablecimiento(IEstablecimientoServicio establecimientoServicio)
        {
            _establecimientoServicio = establecimientoServicio;
        }

        public async Task<IEnumerable<SelectListItem>> Poblar()
        {
            var establecimientos = await _establecimientoServicio.Get();

            return new SelectList(establecimientos, "Id","Nombre");
        }
    }
}