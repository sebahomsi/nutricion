using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Paciente;
using Servicio.Interface.Pago;

namespace Servicio.Pago
{
    public class PagoServicio: ServicioBase, IPagoServicio
    {
        public async Task<long> Add(PagoDto dto)
        {
            var codigo = await GetNextCode();
            var dato = new Dominio.Entidades.Pago()
            {
                Codigo = codigo,
                Concepto = dto.Concepto,
                Fecha = DateTime.Today,
                Monto = dto.Monto,
                PacienteId = dto.PacienteId,
                EstaEliminado = false,
            };

            Context.Pagos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(PagoDto dto)
        {
            var dato = await Context.Pagos.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if(dato==null) throw new ArgumentNullException();
            dato.PacienteId = dto.PacienteId;
            dato.Concepto = dto.Concepto;
            dato.Monto = dto.Monto;
            await Context.SaveChangesAsync();


        }

        public async Task Delete(long id)
        {
            var dato = await Context.Pagos.FirstOrDefaultAsync(x => x.Id == id);

            if (dato == null) throw new ArgumentNullException();
            dato.EstaEliminado = !dato.EstaEliminado;
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PagoDto>> Get(bool eliminado, string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Pago, bool>> expression = x => x.EstaEliminado == eliminado && (x.Concepto.Contains(cadenaBuscar));
            return await Context.Pagos.AsNoTracking()
                .Where(expression)
                .Select(x => new PagoDto()
                {
                    Id = x.Id,
                    PacienteId=x.PacienteId,
                    Codigo = x.Codigo,
                    Concepto = x.Concepto,
                    Fecha = x.Fecha,
                    Monto = x.Monto,
                    EstaEliminado = x.EstaEliminado,
                    
                }).ToListAsync();
        }

        public async Task<ICollection<PagoDto>> GetByDate(DateTime fecha,DateTime fechaH, bool eliminado, string cadenaBuscar)
        {

            Expression<Func<Dominio.Entidades.Pago, bool>> expression = x => (x.Fecha>=fecha&&x.Fecha<=fechaH) && x.EstaEliminado == eliminado 
            && (x.Concepto.Contains(cadenaBuscar)||(x.Paciente.Nombre.Contains(cadenaBuscar)||x.Paciente.Apellido.Contains(cadenaBuscar)));
            var das=await Context.Pagos.AsNoTracking().Include("Paciente")
                .Where(expression)
                .Select(x => new PagoDto()
                {
                    Id = x.Id,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Nombre+" "+x.Paciente.Apellido,
                    Codigo = x.Codigo,
                    Concepto = x.Concepto,
                    Fecha = x.Fecha,
                    Monto = x.Monto,
                    EstaEliminado = x.EstaEliminado,

                }).ToListAsync();
            return das;
        }

        public async Task<PagoDto> GetById(long id)
        {
            var dato = await Context.Pagos.Include("Paciente").FirstOrDefaultAsync(x => x.Id == id);
            if (dato == null) throw new ArgumentNullException();

            var dto=new PagoDto()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                EstaEliminado = dato.EstaEliminado,
                Concepto = dato.Concepto,
                Fecha = dato.Fecha,
                PacienteId = dato.PacienteId,
                Monto = dato.Monto,
                PacienteStr = dato.Paciente.Nombre + " " + dato.Paciente.Apellido
            };

            return dto;
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Pagos.AnyAsync()
                ? await Context.Pagos.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
