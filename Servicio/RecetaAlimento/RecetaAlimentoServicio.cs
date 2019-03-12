using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.RecetaAlimento;

namespace Servicio.RecetaAlimento
{
    public class RecetaAlimentoServicio : ServicioBase, IRecetaAlimentoServicio
    {
        public async Task Add(long recetaId, long alimentoId)
        {
            var receta = await Context.Recetas.Include("Alimentos").FirstOrDefaultAsync(x => x.Id == recetaId);

            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x => x.Id == alimentoId);

            receta.Alimentos.Add(alimento);

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long recetaId, long alimentoId)
        {
            var receta = await Context.Recetas.Include("Alimentos").FirstOrDefaultAsync(x => x.Id == recetaId);

            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x => x.Id == alimentoId);

            receta.Alimentos.Remove(alimento);

            await Context.SaveChangesAsync();
        }
    }
}
