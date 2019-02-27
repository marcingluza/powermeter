using PowerMeter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class ArduinoController : Controller
    {

        // powermeterEntities db = new powermeterEntities();

        private powermeterEntities db = new powermeterEntities();
        List<AvgRecord> ListOfDevicesRecords = new List<AvgRecord>();

        // 
        // GET: /Arduino  
        public ActionResult Index()
        {
            return View(db.record.ToList());
        }

        // 
        // GET: /Arduino/Add 
        public string Add(string id, int voltage, float l1_current, float l2_current, float l3_current)

        {
            record temp = new record(0, 1, DateTime.Now, voltage, (decimal)l1_current, (decimal)l2_current, (decimal)l3_current);
            db.record.Add(temp);
            db.SaveChanges();
            return HttpUtility.HtmlEncode("ID " + id + ", Voltage: " + voltage);
        }

    }
}