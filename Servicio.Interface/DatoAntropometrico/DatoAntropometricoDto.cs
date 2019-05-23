using System;

namespace Servicio.Interface.DatoAntropometrico
{
    public class DatoAntropometricoDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public string PesoActual { get; set; }

        public string PesoHabitual { get; set; }
        public string Foto { get; set; }
        public string PesoDeseado { get; set; }

        public string PesoIdeal { get; set; }

        public string PerimetroCuello { get; set; }

        public string Altura { get; set; }

        public string PerimetroCadera { get; set; }

        public string PerimetroCintura { get; set; }

        public DateTime FechaMedicion { get; set; }

        public string MasaGrasa { get; set; }

        public string MasaCorporal { get; set; }

        public decimal PliegueTriceps { get; set; }

        public decimal PliegueSubescapular { get; set; }

        public decimal PliegueSuprailiaco { get; set; }

        public decimal PliegueAbdominal { get; set; }

        public decimal PliegueMuslo { get; set; }

        public decimal PlieguePierna { get; set; }

        public decimal TotalPliegues { get; set; }

        public bool Eliminado { get; set; }
    }
}
