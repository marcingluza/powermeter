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
        Decimal last24hConsumption;

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


            var test = Startup.db.Database.SqlQuery<string>(
                @"SELECT stamp, voltage, current_l1 + current_l2 + current_l3 
                AS totalCurrent 
                FROM record 
                WHERE id_dev = 1 
                AND stamp BETWEEN DATEADD(hh, -24, getdate()) AND GETDATE()");



            this.lastRecordTime = lastRecordTime;
            this.lastVoltage = lastVoltage;
            this.currentPowerL1 = lastCurrentL1 * lastVoltage;
            this.currentPowerL2 = lastCurrentL2 * lastVoltage;
            this.currentPowerL3 = lastCurrentL3 * lastVoltage;
            this.currentTotalPower = this.currentPowerL1 + this.currentPowerL2 + this.currentPowerL3;
        }

    }
}