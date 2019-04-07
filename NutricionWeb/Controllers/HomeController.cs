using System.Web.Mvc;

namespace NutricionWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ManageController.ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageController.ManageMessageId.ChangePasswordSuccess
                    ? "Su contraseña fue actualizada."

                    : "";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}