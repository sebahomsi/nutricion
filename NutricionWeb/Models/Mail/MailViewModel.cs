using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NutricionWeb.Models.Mail
{
    public class MailViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string MailDestino { get; set; }

        [DataType(DataType.EmailAddress)]
        public string MailEmisor { get; set; }

        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [DataType(DataType.MultilineText)]
        public string CuerpoMensaje { get; set; }

        [DataType(DataType.Text)]
        public string Asunto { get; set; }

        public HttpPostedFileBase Imagen { get; set; }

    }
}