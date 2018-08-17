using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAntropometrico;

namespace Servicio.DatoAntropometrico
{
    public class DatoAntropometricoServicio : ServicioBase, IDatoAntropometricoServicio
    {
        public async Task<long> Add(DatoAntropometricoDto dto)
        {
            var dato = new Dominio.Entidades.DatoAntropometrico()
            {
                Codigo = dto.Codigo,
                PacienteId = dto.PacienteId,
                Altura = dto.Altura,
                FechaMedicion = DateTime.Now,
                MasaGrasa = dto.MasaGrasa,
                MasaCorporal = dto.MasaCorporal,
                Peso = dto.Peso,
                PerimetroCintura = dto.PerimetroCintura,
                PerimetroCadera = dto.PerimetroCadera,
                Eliminado = false
            };

            Context.DatosAntropometricos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(DatoAntropometricoDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DatoAntropometricoDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<DatoAntropometricoDto> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
