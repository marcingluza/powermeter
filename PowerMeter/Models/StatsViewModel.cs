using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerMeter.Models
{
    public class StatsViewModel
    {
        public DateTime lastRecordTime;

        Decimal kwhPrice;

        public Decimal currentPowerL1;
        public Decimal currentPowerL2;
        public Decimal currentPowerL3;
        public Decimal lastVoltage;
        public Decimal currentTotalPower;

        public Decimal last1hVoltage;
        public Decimal last1hConsumption;
        public Decimal last1hCost;

        public Decimal last24hVoltage;
        public Decimal last24hConsumption;
        public Decimal last24hCost;

        public Decimal last7dVoltage;
        public Decimal last7dConsumption;
        public Decimal last7dCost;

        public Decimal last30dVoltage;
        public Decimal last30dConsumption;
        public Decimal last30dCost;

        public StatsViewModel()
        {
        }

        public StatsViewModel(device _device, double kwhprice)
        {
            DateTime lastRecordTime = Startup.db.Database.SqlQuery<DateTime>(
                @"SELECT TOP 1 stamp " +
                "FROM record WHERE id_dev = " + _device.id +
                " ORDER BY stamp DESC"
                ).Single();      //data + godzina ostatniego odczytu

            Decimal lastVoltage = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT TOP 1 voltage 
                FROM record WHERE id_dev = " + _device.id +
                " ORDER BY stamp DESC"
                ).Single();         //ostatni odzyt napiecia

            Decimal lastCurrentL1 = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT TOP 1 current_l1 
                FROM record WHERE id_dev = " + _device.id +
                " ORDER BY stamp DESC"
                ).Single();    //ostatni odzyt amperazu L1

            Decimal lastCurrentL2 = Startup.db.Database.SqlQuery<Decimal>(
                 @"SELECT TOP 1 current_l2
                FROM record WHERE id_dev = " + _device.id +
                 " ORDER BY stamp DESC"
                 ).Single();    //ostatni odzyt amperazu L2

            Decimal lastCurrentL3 = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT TOP 1 current_l3
                FROM record WHERE id_dev = " + _device.id +
                " ORDER BY stamp DESC"
                ).Single();    //ostatni odzyt amperazu L3

            Decimal last1hAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
    @"SELECT AVG(voltage) 
                AS avgVoltage 
                FROM record 
                WHERE id_dev = " + _device.id +
    "AND stamp BETWEEN DATEADD(hh, -1, getdate()) AND GETDATE()").Single(); //średnie napiecie 1h

            Decimal last1hConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(hh, -1, getdate()) AND GETDATE()").Single();  //zuzycie prądu 1h

            Decimal last24hAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT AVG(voltage) 
                AS avgVoltage 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()").Single(); //średnie napiecie 24h

            Decimal last24hConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()").Single();  //zuzycie prądu 24h

            Decimal last7dAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT AVG(voltage) 
                AS avgVoltage 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(dd, -7, getdate()) AND GETDATE()").Single(); //średnie napiecie 7d

            Decimal last7dConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(dd, -7, getdate()) AND GETDATE()").Single();  //zuzycie prądu 7d

            Decimal last30dAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
               @"SELECT AVG(voltage) 
                AS avgVoltage 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(dd, -30, getdate()) AND GETDATE()").Single(); //średnie napiecie 30d

            Decimal last30dConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalwh 
                FROM record 
                WHERE id_dev = " + _device.id +
                "AND stamp BETWEEN DATEADD(dd, -30, getdate()) AND GETDATE()").Single();  //zuzycie prądu 30d


            this.lastRecordTime = lastRecordTime;
            this.lastVoltage = Math.Round(lastVoltage);
            this.currentPowerL1 = Math.Round((lastCurrentL1 * lastVoltage)/1000, 2);
            this.currentPowerL2 = Math.Round((lastCurrentL2 * lastVoltage) / 1000, 2);
            this.currentPowerL3 = Math.Round((lastCurrentL3 * lastVoltage) / 1000, 2);
            this.currentTotalPower = Math.Round((this.currentPowerL1 + this.currentPowerL2 + this.currentPowerL3), 2);

            this.last1hVoltage = Math.Round(last1hAvgVoltage);
            this.last1hConsumption = Math.Round(last1hConsumption / 1000, 2);
            this.last1hCost = Math.Round(this.last1hConsumption * (Decimal)kwhprice, 2);

            this.last24hVoltage = Math.Round(last24hAvgVoltage);
            this.last24hConsumption = Math.Round(last24hConsumption / 1000, 2);
            this.last24hCost = Math.Round(this.last24hConsumption * (Decimal)kwhprice, 2);

            this.last7dVoltage = Math.Round(last7dAvgVoltage);
            this.last7dConsumption = Math.Round(last7dConsumption / 1000, 2);
            this.last7dCost = Math.Round(this.last7dConsumption * (Decimal)kwhprice, 2);

            this.last30dVoltage = Math.Round(last30dAvgVoltage);
            this.last30dConsumption = Math.Round(last30dConsumption / 1000, 2);
            this.last30dCost = Math.Round(this.last30dConsumption * (Decimal)kwhprice, 2);

            this.kwhPrice = (Decimal)kwhprice;

        }

        class StatsViewModelHelper
        {
            DateTime stamp;
            Decimal voltage;
            Decimal totalCurrent;

            public StatsViewModelHelper()
            {
            }

            public StatsViewModelHelper(DateTime stmap, decimal voltage, decimal current)
            {
                this.Stmap = stmap;
                this.Voltage = voltage;
                this.Current = current;
            }

            public DateTime Stmap { get => stamp; set => stamp = value; }
            public decimal Voltage { get => voltage; set => voltage = value; }
            public decimal Current { get => totalCurrent; set => totalCurrent = value; }
        }

    }
}