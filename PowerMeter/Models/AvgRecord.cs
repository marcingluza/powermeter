using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMeter.Models
{
    public class AvgRecord
    {
        private List<record> ListOfRecords;
        private int devID;
        private int count;

        public List<record> ListOfRecords1 { get => ListOfRecords; set => ListOfRecords = value; }
        public int DevID { get => devID; set => devID = value; }
        public int Count { get => count; set => count = value; }

        public AvgRecord(List<record> listOfRecords, int devID, int count)
        {
            ListOfRecords1 = listOfRecords;
            this.DevID = devID;
            this.Count = count;
        }

        public AvgRecord AddRecordToList(AvgRecord _avgRecord, record _record)
        {
            _avgRecord.ListOfRecords.Add(_record);
            _avgRecord.count++;

            return _avgRecord;
        }

        public record CalculateAvg(AvgRecord _avgRecord)
        {
            decimal? voltage = 0;
            decimal? l1 = 0;
            decimal? l2 = 0;
            decimal? l3 = 0;

            foreach (var record in _avgRecord.ListOfRecords)
            {
                voltage += record.voltage;
                l1 += record.current_l1;
                l2 += record.current_l2;
                l3 += record.current_l3;
            }
            voltage /= _avgRecord.count;
            l1 /= _avgRecord.count;
            l2 /= _avgRecord.count;
            l3 /= _avgRecord.count;

            record calculatedRecord = new record(1, this.devID, DateTime.Now, voltage, l1, l2, l3);
            return calculatedRecord;
        }

        
    }
}