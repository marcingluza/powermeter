using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace PowerMeter.Models
{
    public class ChartsModel
    {
        public ArrayList x10m;
        public ArrayList y10m;

        public ArrayList x1h;
        public ArrayList y1h;

        public ArrayList x6h;
        public ArrayList y6h;

        public ArrayList x1d;
        public ArrayList y1d;

        public ArrayList x7d;
        public ArrayList y7d;

        public ChartsModel(device _device)
        {
            DateTime lastRecordTime = Startup.db.Database.SqlQuery<DateTime>(
                @"SELECT TOP 1 stamp " +
                "FROM record WHERE id_dev = " + _device.id +
                " ORDER BY stamp DESC"
                ).Single();      //data + godzina ostatniego odczytu

            this.x10m = new ArrayList();
            this.y10m = new ArrayList();
            this.x1h = new ArrayList();
            this.y1h = new ArrayList();
            this.x6h = new ArrayList();
            this.y6h = new ArrayList();
            this.x1d = new ArrayList();
            this.y1d = new ArrayList();
            this.x7d = new ArrayList();
            this.y7d = new ArrayList();

            for (int i =0; i<10; i++)    // i - dzielnik wykresu
            {
                Decimal tempConsumption10m = Startup.db.Database.SqlQuery<int?>(
              @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
                " AND stamp BETWEEN DATEADD(MINUTE," + (i * 10 + 10) * -1 + ", getdate()) AND DATEADD(MINUTE," + -i * 10 + ", getdate())").DefaultIfEmpty(0).Single() ?? 0;  //zuzycie prądu 10 min
                this.y10m.Add(tempConsumption10m);
                this.x10m.Add(lastRecordTime.AddMinutes(-i * 10).ToShortTimeString());

                Decimal tempConsumption1h = Startup.db.Database.SqlQuery<int?>(
             @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
               " AND stamp BETWEEN DATEADD(HOUR," + (i + 1) * -1 + ", getdate()) AND DATEADD(HOUR," + -i + ", getdate())").DefaultIfEmpty(0).Single() ?? 0;  //zuzycie prądu 1h
                this.y1h.Add(tempConsumption1h);
                this.x1h.Add(lastRecordTime.AddHours(-i).ToShortTimeString());

                Decimal tempConsumption6h = Startup.db.Database.SqlQuery<int?>(
             @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
               " AND stamp BETWEEN DATEADD(HOUR," + (i * 6 + 1) * -1 + ", getdate()) AND DATEADD(HOUR," + -i * 6 + ", getdate())").DefaultIfEmpty(0).Single() ?? 0;  //zuzycie prądu 6h
                this.y6h.Add(tempConsumption6h);
                this.x6h.Add(lastRecordTime.AddHours(-i * 6).ToString("HH:mm dd/MM"));

                Decimal tempConsumption1d = Startup.db.Database.SqlQuery<int?>(
            @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
              " AND stamp BETWEEN DATEADD(DAY," + (i + 1) * -1 + ", getdate()) AND DATEADD(DAY," + -i + ", getdate())").DefaultIfEmpty(0).Single() ?? 0;  //zuzycie prądu 6h
                this.y1d.Add(tempConsumption1d/1000);
                this.x1d.Add(lastRecordTime.AddHours(-i * 6).ToString("dd/MM"));

            }
            x10m.Reverse();
            y10m.Reverse();
            x1h.Reverse();
            y1h.Reverse();
            x6h.Reverse();
            y6h.Reverse();
            x1d.Reverse();
            y1d.Reverse();
        }
    }
}