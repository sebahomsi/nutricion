using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.ObservacionAlergiaIntolerancia;

namespace Servicio.ObservacionAlergiaIntolerancia
{
    public class ObservacionAlergiaIntoleranciaServicio: ServicioBase, IObservacionAlergiaIntoleranciaServicio
    {
        public async Task Add(long observacionId, long alergiaId)
        {
            var observacion = await Context.Observaciones.Include("AlergiasIntolerancias").FirstOrDefaultAsync(x => x.Id == observacionId);

            var alergia = await Context.AlergiasIntolerancias.FirstOrDefaultAsync(x => x.Id == alergiaId);

            observacion.AlergiasIntolerancias.Add(alergia);

            await Context.SaveChangesAsync();

        }

        public async Task Delete(long observacionId, long alergiaId)
        {
            var observacion = await Context.Observaciones.Include("AlergiasIntolerancias").FirstOrDefaultAsync(x => x.Id == observacionId);

            var alergia = await Context.AlergiasIntolerancias.FirstOrDefaultAsync(x => x.Id == alergiaId);

            observacion.AlergiasIntolerancias.Remove(alergia);

            await Context.SaveChangesAsync();
        }
    }
}
