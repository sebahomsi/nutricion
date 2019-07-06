using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NutricionWeb.Models.DatoAntropometrico
{
    public class DatoAntropometricoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string PesoActual { get; set; }

        public string PesoHabitual { get; set; }

        public string PesoDeseado { get; set; }

        public string PesoIdeal { get; set; }

        public string PerimetroCuello { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Altura { get; set; }

        [Display(Name = "Perimetro de Cadera")]
        public string PerimetroCadera { get; set; }

        [Display(Name = "Perimetro de Cintura")]
        public string PerimetroCintura { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        [Display(Name = "Masa Grasa")]

        public string MasaGrasa { get; set; }

        [Display(Name = "Masa Corporal")]

        public string MasaCorporal { get; set; }

        [Display(Name = "Buscar Foto")]
        public HttpPostedFileBase FotoFrente { get; set; }

        [Display(Name = "Foto")]
        public string FotoFrenteStr { get; set; }

        [Display(Name = "Buscar Foto")]
        public HttpPostedFileBase FotoPerfil { get; set; }

        [Display(Name = "Foto")]
        public string FotoPerfilStr { get; set; }

        [Display(Name = "Pliegue Triceps")]
        public decimal PliegueTriceps { get; set; }

        [Display(Name = "Pliegue Subescapular")]
        public decimal PliegueSubescapular { get; set; }

        [Display(Name = "Pliegue Suprailiaco")]
        public decimal PliegueSuprailiaco { get; set; }

        [Display(Name = "Pliegue Abdominal")]
        public decimal PliegueAbdominal { get; set; }

        [Display(Name = "Pliegue Muslo")]
        public decimal PliegueMuslo { get; set; }

        [Display(Name = "Pliegue Pierna")]
        public decimal PlieguePierna { get; set; }

        public decimal TotalPliegues { get; set; }

        public bool Eliminado { get; set; }
    }
}