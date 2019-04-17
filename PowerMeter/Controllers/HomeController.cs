using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PowerMeter.Models;
using System.Collections;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private static ChartsModel CM;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Panel()
        {
            ViewBag.Message = "Aktualne pomiary";
            StatsViewModel SVM = new StatsViewModel(Startup.DeviceList.Devices.Find(x => x.name == UserManager.FindById(User.Identity.GetUserId()).DeviceName), UserManager.FindById(User.Identity.GetUserId()).KwhPrice);
            return View(SVM);
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult Charts()
        {
            ViewBag.Message = "Your contact page.";
            CM = new ChartsModel(Startup.DeviceList.Devices.Find(x => x.name == UserManager.FindById(User.Identity.GetUserId()).DeviceName));
            return View();
        }

        public ActionResult Chart10m()
        {
            

            Chart chart10m = new Chart(width: 1000, height: 400)
             .AddTitle("10 minut [Wh]")
             .AddSeries("", chartType: "SplineArea", xValue: CM.x10m, yValues: CM.y10m)
             .Write("jpeg");

            return null;

        }

        public ActionResult Chart1h()
        {

            Chart chart1h = new Chart(width: 1000, height: 400)
             .AddTitle("1 godzina [Wh]")
             .AddSeries("", chartType: "SplineArea", xValue: CM.x1h, yValues: CM.y1h)
             .Write("jpeg");

            return null;

        }
        public ActionResult Chart6h()
        {

            Chart chart6h = new Chart(width: 1000, height: 400)
             .AddTitle("6 godzin [Wh]")
             .AddSeries("", chartType: "SplineArea", xValue: CM.x6h, yValues: CM.y6h)
             .Write("jpeg");

            return null;

        }

        public ActionResult Chart1d()
        {

            Chart chart1d = new Chart(width: 1000, height: 400)
             .AddTitle("1 dzień [kWh]")
             .AddSeries("", chartType: "SplineArea", xValue: CM.x1d, yValues: CM.y1d)
             .Write("jpeg");

            return null;

        }
    }
}