using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.ObservacionAlimento;

namespace Servicio.ObservacionAlimento
{
    public class ObservacionAlimentoServicio: ServicioBase, IObservacionAlimentoServicio
    {
        public async Task Add(long observacionId, long alimentoId)
        {
            var observacion = await Context.Observaciones.Include("Alimentos").FirstOrDefaultAsync(x => x.Id == observacionId);

            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x => x.Id == alimentoId);

            observacion.Alimentos.Add(alimento);

            await Context.SaveChangesAsync();

        }

        public async Task Delete(long observacionId, long alimentoId)
        {
            var observacion = await Context.Observaciones.Include("Alimentos").FirstOrDefaultAsync(x => x.Id == observacionId);

            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x => x.Id == alimentoId);

            observacion.Alimentos.Remove(alimento);

            await Context.SaveChangesAsync();
        }
    }
}
