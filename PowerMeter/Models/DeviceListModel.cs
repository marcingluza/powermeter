using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMeter.Models
{
    public class DeviceListModel
    {
        private List<device> _devices;
        private int _count;


        public List<device> Devices { get => _devices; set => _devices = value; }
        public int Count { get => _count; set => _count = value; }

        public DeviceListModel()
        {
            this._count = this.Count;
        }

        public DeviceListModel(List<device> devices, int count)
        {
            _devices = devices;
            _count = count;
        }

        public DeviceListModel Refresh() {
            return new DeviceListModel(Startup.db.device.ToList(), Startup.db.device.ToList().Count);
        }

        public bool checkExist(string devId)
        {
            bool exist;
            return exist = this._devices.Any(device => device.devID == devId);
        }

        public int getId(string devId)
        {
            device temp = _devices.Find(device => device.devID == devId);
            return temp.id;
        }



        
    }
}