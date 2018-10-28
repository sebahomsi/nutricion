using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Dia;

namespace Servicio.Dia
{
    public class DiaServicio: ServicioBase, IDiaServicio
    {
        public async Task GenerarDias(long planId)
        {
            var lunes = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Lunes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(lunes);

            var martes = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Martes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(martes);

            var miercoles = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Miercoles",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(miercoles);

            var jueves = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Jueves",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId,
            };
            Context.Dias.Add(jueves);

            var viernes = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Viernes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(viernes);

            var sabado = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Sabado",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(sabado);

            var domingo = new Dominio.Entidades.Dia()
            {
                Descripcion = @"Domingo",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(domingo);

            //var planAlimenticio = await Context.PlanesAlimenticios.FirstOrDefaultAsync(x=> x.Id == planId);
            //if (planAlimenticio == null) throw new ArgumentNullException();

            //var dias = Context.Dias.Where(x => x.PlanAlimenticioId == planId);

            //foreach (var dia in dias)
            //{
            //    planAlimenticio.Dias.Add(dia);
            //}

            await Context.SaveChangesAsync();
        }

        public async Task<long> Add(DiaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(DiaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DiaDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<DiaDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextCode()
        {
            throw new NotImplementedException();
        }
    }
}
