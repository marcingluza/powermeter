using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Panel()
        {
            ViewBag.Message = "Aktualne pomiary";

            return View();
        }

        
        public ActionResult Charts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}