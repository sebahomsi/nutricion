using NutricionWeb.Models.Mail;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Rotativa;
using Servicio.Interface.PlanAlimenticio;
using System.Web.Routing;
using System.Web;
using NutricionWeb.Controllers.PlanAlimenticio;
using NutricionWeb.Helpers;
using NutricionWeb.Helpers.SubGrupo;
using Servicio.Interface.Alimento;
using Servicio.Interface.Opcion;
using Servicio.Interface.Paciente;
using Servicio.Interface.Dia;
using AutoMapper;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Estrategia;
using NutricionWeb.Models.Objetivo;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Estrategia;
using Servicio.Interface.Objetivo;

namespace NutricionWeb.Controllers.Mail
{
    public class MailController : Controller
    {
        // GET: Mail
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
 
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;
        private readonly IObjetivoServicio _objetivoServicio;
        private readonly IEstrategiaServicio _estrategiaServicio;

        private readonly IDiaServicio _diaServicio;
        private readonly IOpcionServicio _opcionServicio;
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly IComboBoxSubGrupo _comboBoxSubGrupo;

        public MailController(IPlanAlimenticioServicio planAlimenticioServicio, IPacienteServicio pacienteServicio, IDatoAntropometricoServicio datoAntropometricoServicio, IObjetivoServicio objetivoServicio, IEstrategiaServicio estrategiaServicio, IDiaServicio diaServicio, IOpcionServicio opcionServicio, IAlimentoServicio alimentoServicio, IComboBoxSubGrupo comboBoxSubGrupo)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _pacienteServicio = pacienteServicio;
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _objetivoServicio = objetivoServicio;
            _estrategiaServicio = estrategiaServicio;
            _diaServicio = diaServicio;
            _opcionServicio = opcionServicio;
            _alimentoServicio = alimentoServicio;
            _comboBoxSubGrupo = comboBoxSubGrupo;
        }

        public async Task<ActionResult> Create(long? pacienteId)
        {
            if (pacienteId.HasValue)
            {
                var paciente = await _pacienteServicio.GetById(pacienteId.Value);
                return View(new MailViewModel()
                {
                    MailEmisor = User.Identity.Name,
                    MailDestino = paciente.Mail,
                    PacienteId = pacienteId.Value
                });
            }
            return View(new MailViewModel()
            {
                MailEmisor = User.Identity.Name
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MailViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var pdf = await GeneratePdfMail(vm.PacienteId);

                    var stream = new MemoryStream(pdf);
                    var mmsg = new MailMessage();
                    mmsg.To.Add(vm.MailDestino);
                    mmsg.Subject = vm.Asunto;
                    mmsg.SubjectEncoding = Encoding.UTF8;
                    mmsg.From = new MailAddress(vm.MailEmisor);
                    mmsg.Body = vm.CuerpoMensaje;
                    mmsg.BodyEncoding = Encoding.UTF8;
                    if (vm.Imagenes.Any(x=>x != null))
                    {
                        foreach (var imagen in vm.Imagenes)
                        {
                            var fileName = Path.GetFileName(imagen.FileName);
                            mmsg.Attachments.Add(new Attachment(imagen.InputStream, fileName));

                        }
                    }

                    if (vm.IncluirHistoriaClinica)
                    {
                        mmsg.Attachments.Add(new Attachment(stream, "Plan", "application/pdf"));
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

        public async Task<Byte[]> GeneratePdfMail(long pacienteId)
        {
            var paciente = await _pacienteServicio.GetById(pacienteId);

            var mailpdft = new ActionAsPdf("GenerarCuerpoMail", new { pacienteId })
            {
                FileName = "PlanAlimenticio_" + $"{paciente.Apellido} {paciente.Nombre}" + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
            };

          

            Byte[] PdfData = mailpdft.BuildFile(this.ControllerContext);
            return PdfData;
        }

        public async Task<ActionResult> GeneratePdf(long id)
        {
            var plan = await _planAlimenticioServicio.GetById(id);

            var comidas = await _planAlimenticioServicio.GetSortringComidas(id);

            ViewBag.PlanId = id;

            ViewBag.Calorias = plan.TotalCalorias;

            var comidasVm = Mapper.Map<PlanAlimenticioVistaViewModel>(comidas);

            return View(comidasVm);
        }


        public async Task<ActionResult> GenerarCuerpoMail(long pacienteId)
        {
            var paciente = Mapper.Map<PacienteViewModel>(await _pacienteServicio.GetById(pacienteId));

            var datosAntropometricos = await _datoAntropometricoServicio.GetByIdPaciente(pacienteId);

            var datoAntropometrico = Mapper.Map<DatoAntropometricoViewModel>(datosAntropometricos.OrderByDescending(x=>x.FechaMedicion).First());

            var objetivo = Mapper.Map<ObjetivoViewModel>(await _objetivoServicio.GetByPacienteId(pacienteId));

            var estrategia = Mapper.Map<EstrategiaViewModel>(await _estrategiaServicio.GetByPacienteId(pacienteId));

            var cuerpoMail = new CuerpoMailViewModel()
            {
                Paciente = paciente,
                DatoAntropometrico = datoAntropometrico,
                Estrategia = estrategia,
                Objetivo = objetivo
            };

            return View(cuerpoMail);
        }

    }
}