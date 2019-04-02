using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PowerMeter.Models;
using System.Web;
using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
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

        [Authorize(Roles = "admin")]
        public ActionResult Panel()
        {
            ViewBag.Message = "Aktualne pomiary";
            StatsViewModel SVM = new StatsViewModel(Startup.DeviceList.Devices.Find(x => x.name == UserManager.FindById(User.Identity.GetUserId()).DeviceName), UserManager.FindById(User.Identity.GetUserId()).KwhPrice);

            return View(SVM);
        }

        
        public ActionResult Charts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}