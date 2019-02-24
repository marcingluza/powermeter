using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class ArduinoController : Controller
    {
        // 
        // GET: /Arduino

        public string Index()
        {
            return "This is my <b>Arduino</b> action...";
        }

        // 
        // GET: /Arduino/Add 
        public string Add(string id, int voltage, float l1_current, float l2_current, float l3_current)

        {
            return HttpUtility.HtmlEncode("ID " + id + ", Voltage: " + voltage);
        }

    }
}