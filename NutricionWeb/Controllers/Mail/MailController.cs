using AutoMapper;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Estrategia;
using NutricionWeb.Models.Mail;
using NutricionWeb.Models.Objetivo;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using Rotativa;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Estrategia;
using Servicio.Interface.Objetivo;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Turno;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        private readonly ITurnoServicio _turnoServicio;

        public MailController(IPlanAlimenticioServicio planAlimenticioServicio, IPacienteServicio pacienteServicio, IDatoAntropometricoServicio datoAntropometricoServicio, IObjetivoServicio objetivoServicio, IEstrategiaServicio estrategiaServicio, ITurnoServicio turnoServicio)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _pacienteServicio = pacienteServicio;
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _objetivoServicio = objetivoServicio;
            _estrategiaServicio = estrategiaServicio;
            _turnoServicio = turnoServicio;
        }

        public async Task<ActionResult> Create(long? pacienteId)
        {
            if (pacienteId.HasValue)
            {
                var paciente = await _pacienteServicio.GetById(pacienteId.Value);
                var turno = await _turnoServicio.GetLastByPacienteId(pacienteId.Value);
                if (turno != null)
                {
                    return View(new MailViewModel()
                    {
                        MailEmisor = User.Identity.Name,
                        MailDestino = paciente.Mail,
                        PacienteId = pacienteId.Value,
                        CuerpoMensaje = $"¡Hola {paciente.Nombre}! Te envío nuevo plan nutricional para realizar los proximos 15 días!\n\nPor cualquier duda o sugerencia que tengas no dudes en comunicarte por este medio.\n\nTu próxima consulta será el dia: {turno.HorarioEntrada:dd/MM/yyyy HH:mm} hs.\n\n" +
                        $" Éxitos!\n\n" +
                        $"Lic.Solana María Novillo \n" +
                        $"M.P N° 815 \n" +
                        $"Especializada en deportes. \n" +
                        $"Diplomada en diabetes y obesidad. \n" +
                        $"Antropometrista ISAK II. \n" +
                        $"Disertante. \n" +
                        $"Socia - gerente de Nutritucumán \n" +
                        $"Instagram: @lic.solnovillo\n ",
                        Asunto = "Plan Nutricional"
                    });


                }
                return View(new MailViewModel()
                {
                    MailEmisor = User.Identity.Name,
                    MailDestino = paciente.Mail,
                    PacienteId = pacienteId.Value,
                    CuerpoMensaje = $"¡Hola {paciente.Nombre}! Te envío nuevo plan nutricional para realizar los proximos 15 días!\n\n" +
                    $"Por cualquier duda o sugerencia que tengas no dudes en comunicarte por este medio.\n\n" +
                    $" Éxitos!\n\n" +
                    $"Lic.Solana María Novillo \n" +
                    $"M.P N° 815 \n" +
                    $"Especializada en deportes. \n" +
                    $"Diplomada en diabetes y obesidad \n" +
                    $"Antropometrista ISAK II \n" +
                    $"Disertante \n" +
                    $"Socia - gerente de Nutritucumán \n" +
                    $"Instagram: @lic.solnovillo\n ",
                    Asunto = "Plan Nutricional"
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
                    var mmsg = new MailMessage();
                    mmsg.To.Add(vm.MailDestino);
                    mmsg.Subject = vm.Asunto;
                    mmsg.SubjectEncoding = Encoding.UTF8;
                    mmsg.From = new MailAddress(vm.MailEmisor);
                    mmsg.Body = vm.CuerpoMensaje;
                    mmsg.BodyEncoding = Encoding.UTF8;
                    if (vm.Imagenes.Any(x => x != null))
                    {
                        foreach (var imagen in vm.Imagenes)
                        {
                            var fileName = Path.GetFileName(imagen.FileName);
                            mmsg.Attachments.Add(new Attachment(imagen.InputStream, fileName));

                        }
                    }

                    if (vm.IncluirHistoriaClinica)
                    {
                        var pdf = await GeneratePdfMail(vm.PacienteId);
                        var stream = new MemoryStream(pdf);
                        mmsg.Attachments.Add(new Attachment(stream, "Historia Clinica", "application/pdf"));
                    }

                    if (vm.IncluirPlan)
                    {
                        var pdf = await GeneratePdfMailPlan(vm.PacienteId);
                        var stream = new MemoryStream(pdf);
                        mmsg.Attachments.Add(new Attachment(stream, "Plan Alimenticio", "application/pdf"));
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

        public async Task<ActionResult> CreateParcial(long? pacienteId)
        {
            if (pacienteId.HasValue)
            {
                var paciente = await _pacienteServicio.GetById(pacienteId.Value);
                var turno = await _turnoServicio.GetLastByPacienteId(pacienteId.Value);
                if (turno != null)
                {
                    return PartialView(new MailViewModel()
                    {
                        MailEmisor = User.Identity.Name,
                        MailDestino = paciente.Mail,
                        PacienteId = pacienteId.Value,
                        CuerpoMensaje = $"¡Hola {paciente.Nombre}! Te envío nuevo plan nutricional para realizar los proximos 15 días!\n\nPor cualquier duda o sugerencia que tengas no dudes en comunicarte por este medio.\n\nTu próxima consulta será el dia: {turno.HorarioEntrada:dd/MM/yyyy HH:mm} hs.\n\n" +
                                        $" Éxitos!\n\n" +
                                        $"Lic.Solana María Novillo \n" +
                                        $"M.P N° 815 \n" +
                                        $"Especializada en deportes. \n" +
                                        $"Diplomada en diabetes y obesidad. \n" +
                                        $"Antropometrista ISAK II. \n" +
                                        $"Disertante. \n" +
                                        $"Socia - gerente de Nutritucumán \n" +
                                        $"Instagram: @lic.solnovillo\n ",
                        Asunto = "Plan Nutricional"
                    });
                }

                return PartialView(new MailViewModel()
                {
                    MailEmisor = User.Identity.Name,
                    MailDestino = paciente.Mail,
                    PacienteId = pacienteId.Value,
                    CuerpoMensaje = $"¡Hola {paciente.Nombre}! Te envío nuevo plan nutricional para realizar los proximos 15 días!\n\n" +
                                    $"Por cualquier duda o sugerencia que tengas no dudes en comunicarte por este medio.\n\n" +
                                    $" Éxitos!\n\n" +
                                    $"Lic.Solana María Novillo \n" +
                                    $"M.P N° 815 \n" +
                                    $"Especializada en deportes. \n" +
                                    $"Diplomada en diabetes y obesidad \n" +
                                    $"Antropometrista ISAK II \n" +
                                    $"Disertante \n" +
                                    $"Socia - gerente de Nutritucumán \n" +
                                    $"Instagram: @lic.solnovillo\n ",
                    Asunto = "Plan Nutricional"
                });
            }

            return PartialView(new MailViewModel()
            {
                MailEmisor = User.Identity.Name
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(MailViewModel vm)
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
                    if (vm.Imagenes.Any(x => x != null))
                    {
                        foreach (var imagen in vm.Imagenes)
                        {
                            var fileName = Path.GetFileName(imagen.FileName);
                            mmsg.Attachments.Add(new Attachment(imagen.InputStream, fileName));

                        }
                    }

                    if (vm.IncluirHistoriaClinica)
                    {
                        var pdf = await GeneratePdfMail(vm.PacienteId);
                        var stream = new MemoryStream(pdf);
                        mmsg.Attachments.Add(new Attachment(stream, "Historia Clinica", "application/pdf"));
                    }

                    if (vm.IncluirPlan)
                    {
                        var pdf = await GeneratePdfMailPlan(vm.PacienteId);
                        var stream = new MemoryStream(pdf);
                        mmsg.Attachments.Add(new Attachment(stream, "Plan Alimenticio", "application/pdf"));
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

        public async Task<byte[]> GeneratePdfMail(long pacienteId)
        {
            var paciente = await _pacienteServicio.GetById(pacienteId);

            var datosAntropometricos = await _datoAntropometricoServicio.GetByIdPaciente(pacienteId);

            var datoAntropometricoDtos = datosAntropometricos.ToList();

            if (!datoAntropometricoDtos.Any()) throw new ArgumentException("No tiene Datos Antropometricos cargados");

            var mailpdft = new ActionAsPdf("GenerarCuerpoMail", new { pacienteId })
            {
                FileName = "Historia Clinica_" + $"{paciente.Apellido} {paciente.Nombre}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
            };

            var pdfData = mailpdft.BuildFile(ControllerContext);
            return pdfData;
        }

        public async Task<byte[]> GeneratePdfMailPlan(long pacienteId)
        {
            var plan = await _planAlimenticioServicio.GetByIdPaciente(pacienteId);

            var ultimoPlan = plan.OrderByDescending(x => x.Fecha).FirstOrDefault();

            if (ultimoPlan == null) throw new ArgumentException("No tiene Plan Alimenticio cargado");

            var mailpdft = new ActionAsPdf("GeneratePdf", new { ultimoPlan.Id })
            {
                FileName = "Plan Alimenticio_" + $"{ultimoPlan.PacienteStr}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
            };

            var pdfData = mailpdft.BuildFile(ControllerContext);
            return pdfData;
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
            var datosAntropometricos = await _datoAntropometricoServicio.GetByIdPaciente(pacienteId);

            var datoAntropometricoDtos = datosAntropometricos.ToList();

            var paciente = Mapper.Map<PacienteViewModel>(await _pacienteServicio.GetById(pacienteId));

            var datoAntropometrico = Mapper.Map<DatoAntropometricoViewModel>(datoAntropometricoDtos.OrderByDescending(x => x.FechaMedicion).First());

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