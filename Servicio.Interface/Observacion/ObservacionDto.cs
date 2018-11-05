using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.Alimento;
using Servicio.Interface.Patologia;

namespace Servicio.Interface.Observacion
{
    public class ObservacionDto
    {
        public ObservacionDto()
        {
            Patologias = new List<PatologiaDto>();
            AlergiasIntolerancias = new List<AlergiaIntoleranciaDto>();
            Alimentos = new List<AlimentoDto>();
        }

        public int Codigo { get; set; }

        public long Id { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public bool Fumador { get; set; }

        public bool BebeAlcohol { get; set; }

        public string EstadoCivil { get; set; }

        public bool? TuvoHijo { get; set; }

        public string CantidadHijo { get; set; }

        public string CantidadSuenio { get; set; }

        public bool Eliminado { get; set; }
        
        public List<PatologiaDto> Patologias { get; set; }
        public List<AlergiaIntoleranciaDto> AlergiasIntolerancias { get; set; }
        public List<AlimentoDto> Alimentos { get; set; }
    }
}
