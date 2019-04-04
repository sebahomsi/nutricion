using NutricionWeb.Models.Mail;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Mail
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Create()
        {
            return View(new MailViewModel()
            {
                MailEmisor = User.Identity.Name 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MailViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var mmsg = new MailMessage();
                    mmsg.To.Add(vm.MailDestino);
                    mmsg.Subject = vm.Asunto;
                    mmsg.SubjectEncoding = Encoding.UTF8;
                    mmsg.From = new MailAddress(vm.MailEmisor);
                    mmsg.Body = vm.CuerpoMensaje;
                    mmsg.BodyEncoding = Encoding.UTF8;
                    if (vm.Imagenes.Any())
                    {
                        foreach (var imagen in vm.Imagenes)
                        {
                            var fileName = Path.GetFileName(imagen.FileName);
                            mmsg.Attachments.Add(new Attachment(imagen.InputStream, fileName));

                        }
                    }

                    var cliente = new SmtpClient
                    {
                        Credentials = new NetworkCredential(vm.MailEmisor, vm.Contraseña),
                        Port = 587,
                        EnableSsl = true,
                        Host = "smtp.gmail.com"
                    };
                    
                    cliente.Send(mmsg);

                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json("Su Mail se envio correctamente");
        }

    }
}