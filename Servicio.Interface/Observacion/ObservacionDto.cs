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

        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public string ActividadFisica { get; set; }

        public string AntecedentesFamiliares { get; set; }

        public string Tabaco { get; set; }

        public string Alcohol { get; set; }

        public string Medicacion { get; set; }

        public string HorasSuenio { get; set; }

        public string RitmoEvacuatorio { get; set; }

        public bool Eliminado { get; set; }

        public List<PatologiaDto> Patologias { get; set; }
        public List<AlergiaIntoleranciaDto> AlergiasIntolerancias { get; set; }
        public List<AlimentoDto> Alimentos { get; set; }
    }
}
