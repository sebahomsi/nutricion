using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.ObservacionPatologia;

namespace Servicio.ObservacionPatologia
{
    public class ObservacionPatologiaServicio: ServicioBase, IObservacionPatologiaServicio
    {
        public async Task Add(long observacionId, long patologiaId)
        {
            var observacion = await Context.Observaciones.Include("Patologias").FirstOrDefaultAsync(x => x.Id == observacionId);

            var patologia = await Context.Patologias.FirstOrDefaultAsync(x => x.Id == patologiaId);

            observacion.Patologias.Add(patologia);

            await Context.SaveChangesAsync();

        }

        public async Task Delete(long observacionId, long patologiaId)
        {
            var observacion = await Context.Observaciones.Include("Patologias").FirstOrDefaultAsync(x => x.Id == observacionId);

            var patologia = await Context.Patologias.FirstOrDefaultAsync(x => x.Id == patologiaId);

            observacion.Patologias.Remove(patologia);

            await Context.SaveChangesAsync();
        }
    }
}
