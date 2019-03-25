using System;
using Servicio.Interface.Paciente;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using NutricionWebApi.Models.Paciente;
using NutricionWebApi.Models.DatoAntropometrico;
using NutricionWebApi.Models.PlanAlimenticio;
using NutricionWebApi.Models.Turno;
using NutricionWebApi.Models.DatoAnalitico;

namespace NutricionWebApi.Controllers
{
    public class PacienteController : ApiController
    {
        private readonly IPacienteServicio _pacienteServicio;

        public PacienteController(IPacienteServicio pacienteServicio)
        {
            _pacienteServicio = pacienteServicio;
        }

        // GET api/values
        public async Task<List<PacienteViewModel>> Get()
        {
            var paceintes = await _pacienteServicio.Get(false,String.Empty);

            return paceintes.Select(x => new PacienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToList();
        }

        // GET api/values/5

        public async Task<IHttpActionResult> Get(long id)
        {
            var dato = await _pacienteServicio.GetById(id);

            if (dato == null) return NotFound();

            var paciente = new  PacienteViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                Apellido = dato.Apellido,
                Nombre = dato.Nombre,
                Celular = dato.Celular,
                Telefono = dato.Telefono,
                Direccion = dato.Direccion,
                Dni = dato.Dni,
                FechaNac = dato.FechaNac.Date,
                Sexo = dato.Sexo,
                Mail = dato.Mail,
                FotoStr = dato.Foto,
                Eliminado = dato.Eliminado,
                TieneObservacion = dato.TieneObservacion,
                DatosAntropometricos = dato.DatosAntropometricos.Select(p => new DatoAntropometricoViewModel()
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    PacienteId = p.PacienteId,
                    PacienteStr = p.PacienteStr,
                    Altura = p.Altura,
                    FechaMedicion = p.FechaMedicion,
                    MasaGrasa = p.MasaGrasa,
                    MasaCorporal = p.MasaCorporal,
                    Peso = p.PesoActual,
                    PerimetroCintura = p.PerimetroCintura,
                    PerimetroCadera = p.PerimetroCadera,
                    Eliminado = p.Eliminado
                }).ToList(),
                PlanesAlimenticios = dato.PlanesAlimenticios.OrderBy(q => q.Fecha).Select(q => new PlanAlimenticioViewModel()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Motivo = q.Motivo,
                    Fecha = q.Fecha,
                    PacienteId = q.PacienteId,
                    PacienteStr = q.PacienteStr,
                    Eliminado = q.Eliminado
                }).ToList(),
                Turnos = dato.Turnos.OrderBy(t => t.HorarioEntrada).Select(t => new TurnoViewModel()
                {
                    Id = t.Id,
                    Numero = t.Numero,
                    Motivo = t.Motivo,
                    PacienteId = t.PacienteId,
                    PacienteStr = t.PacienteStr,
                    HorarioEntrada = t.HorarioEntrada,
                    HorarioSalida = t.HorarioSalida,
                    Eliminado = t.Eliminado
                }).ToList(),
                DatosAnaliticos = dato.DatosAnaliticos.OrderBy(x => x.Codigo).Select(x => new DatoAnaliticoViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    ColesterolHdl = x.ColesterolHdl,
                    ColesterolLdl = x.ColesterolLdl,
                    ColesterolTotal = x.ColesterolTotal,
                    PresionDiastolica = x.PresionDiastolica,
                    PresionSistolica = x.PresionSistolica,
                    Trigliceridos = x.Trigliceridos,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.PacienteStr,
                    FechaMedicion = x.FechaMedicion,
                    Eliminado = x.Eliminado
                }).ToList()
            };

            return Ok(paciente);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}