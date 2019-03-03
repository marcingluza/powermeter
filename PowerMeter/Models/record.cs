namespace PowerMeter.Models
{
    using System;

    public partial class record
    {
        private long _id;
        private int _id_dev;
        private DateTime _stamp;
        private decimal? _voltage;
        private decimal? _current_l1;
        private decimal? _current_l2;
        private decimal? _current_l3;

        public record(long id, int id_dev, DateTime stamp, decimal? voltage, decimal? current_l1, decimal? current_l2, decimal? current_l3)
        {
            _id = id;
            _id_dev = id_dev;
            _stamp = stamp;
            _voltage = voltage;
            _current_l1 = current_l1;
            _current_l2 = current_l2;
            _current_l3 = current_l3;
        }

        public long id { get => _id; set => _id = value; }
        public int id_dev { get => _id_dev; set => _id_dev = value; }
        public System.DateTime stamp { get => _stamp; set => _stamp = value; }
        public Nullable<decimal> voltage { get => _voltage; set => _voltage = value; }
        public Nullable<decimal> current_l1 { get => _current_l1; set => _current_l1 = value; }
        public Nullable<decimal> current_l2 { get => _current_l2; set => _current_l2 = value; }
        public Nullable<decimal> current_l3 { get => _current_l3; set => _current_l3 = value; }
    }
}
