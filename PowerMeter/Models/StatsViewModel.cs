using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMeter.Models
{
    public class StatsViewModel
    {
        DateTime lastRecordTime;
        Decimal lastVoltage;
        Decimal currentPowerL1;
        Decimal currentPowerL2;
        Decimal currentPowerL3;
        Decimal currentTotalPower;

        Decimal last24hVoltage;
        int last24hConsumption;

        Decimal last7dVoltage;
        int last7dConsumption;

        Decimal last30dVoltage;
        int last30dConsumption;

        public StatsViewModel(device _device)
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


            List<StatsViewModelHelper> test = Startup.db.Database.SqlQuery<StatsViewModelHelper>(
                @"SELECT stamp, voltage, current_l1 + current_l2 + current_l3 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()").ToList();

            Decimal last24hAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT AVG(voltage) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()").Single(); //średnie napiecie 24h

            int last24hConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()").Single();  //zuzycie prądu 24h

            Decimal last7dAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
                @"SELECT AVG(voltage) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(dd, -7, getdate()) AND GETDATE()").Single(); //średnie napiecie 7d

            int last7dConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(dd, -7, getdate()) AND GETDATE()").Single();  //zuzycie prądu 7d

            Decimal last30dAvgVoltage = Startup.db.Database.SqlQuery<Decimal>(
               @"SELECT AVG(voltage) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(dd, -30, getdate()) AND GETDATE()").Single(); //średnie napiecie 30d

            int last30dConsumption = Startup.db.Database.SqlQuery<int>(
                @"SELECT SUM(wh) 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(dd, -30, getdate()) AND GETDATE()").Single();  //zuzycie prądu 30d


            this.lastRecordTime = lastRecordTime;
            this.lastVoltage = lastVoltage;
            this.currentPowerL1 = lastCurrentL1 * lastVoltage;
            this.currentPowerL2 = lastCurrentL2 * lastVoltage;
            this.currentPowerL3 = lastCurrentL3 * lastVoltage;
            this.currentTotalPower = this.currentPowerL1 + this.currentPowerL2 + this.currentPowerL3;

            this.last24hVoltage = last24hAvgVoltage;
            this.last24hConsumption = last24hConsumption;

            this.last7dVoltage = last7dAvgVoltage;
            this.last7dConsumption = last7dConsumption;

            this.last30dVoltage = last30dAvgVoltage;
            this.last30dConsumption = last30dConsumption;

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