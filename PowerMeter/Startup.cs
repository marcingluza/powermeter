using Microsoft.Owin;
using Owin;
using PowerMeter.Models;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(PowerMeter.Startup))]
namespace PowerMeter
{
    public partial class Startup
    {

        
        public static powermeterEntities2 db = new powermeterEntities2();
        public static DeviceListModel DeviceList; 

        public void Configuration(IAppBuilder app)
        {
            DeviceList = new DeviceListModel(db.device.ToList(), db.device.ToList().Count);
            ConfigureAuth(app);
        }
    }
}
