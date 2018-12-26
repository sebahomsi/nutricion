using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Comida;
using Servicio.Interface.Dia;

namespace Servicio.Dia
{
    public class DiaServicio: ServicioBase, IDiaServicio
    {
        private readonly IComidaServicio _comidaServicio;

        public DiaServicio(IComidaServicio comidaServicio)
        {
            _comidaServicio = comidaServicio;
        }

        public async Task GenerarDias(long planId)
        {
            var codigo = await GetNextCode();
            var lunes = new Dominio.Entidades.Dia()
            {
                Codigo = codigo,
                Descripcion = @"Lunes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(lunes);
            await Context.SaveChangesAsync();


            await _comidaServicio.GenerarComidas(lunes.Id);

            var martes = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 1,
                Descripcion = @"Martes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(martes);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(martes.Id);

            var miercoles = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 2,
                Descripcion = @"Miercoles",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(miercoles);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(miercoles.Id);

            var jueves = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 3,
                Descripcion = @"Jueves",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId,
            };
            Context.Dias.Add(jueves);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(jueves.Id);

            var viernes = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 4,
                Descripcion = @"Viernes",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(viernes);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(viernes.Id);

            var sabado = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 5,
                Descripcion = @"Sabado",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(sabado);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(sabado.Id);

            var domingo = new Dominio.Entidades.Dia()
            {
                Codigo = codigo + 6,
                Descripcion = @"Domingo",
                Comidas = new List<Dominio.Entidades.Comida>(),
                PlanAlimenticioId = planId
            };
            Context.Dias.Add(domingo);
            await Context.SaveChangesAsync();

            await _comidaServicio.GenerarComidas(domingo.Id);

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
            return await Context.Dias.AnyAsync()
                ? await Context.Dias.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
