﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NutricionWeb.Models.Mail
{
    public class MailViewModel
    {
        public MailViewModel()
        {
            Imagenes = new List<HttpPostedFileBase>();
        }

        [Display(Name = "Destino")]
        [DataType(DataType.EmailAddress)]
        public string MailDestino { get; set; }

        [Display(Name = "Emisor")]
        [DataType(DataType.EmailAddress)]
        public string MailEmisor { get; set; }

        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Cuerpo")]
        [DataType(DataType.MultilineText)]
        public string CuerpoMensaje { get; set; }

        [DataType(DataType.Text)]
        public string Asunto { get; set; }

        [Display(Name = "Incluir Historia Clinica")]
        public bool IncluirHistoriaClinica { get; set; }

        [Display(Name = "Incluir Plan")]
        public bool IncluirPlan { get; set; }

        public List<HttpPostedFileBase> Imagenes { get; set; }

    }
}