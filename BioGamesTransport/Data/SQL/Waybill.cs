using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Waybill
    {
        public Waybill()
        {
            WaybillDetails = new HashSet<WaybillDetails>();
        }

        public int Id { get; set; }
        public int? ShipModeId { get; set; }
        public int? WaybillStatusId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double? Cost { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual ShipModes ShipMode { get; set; }
        public virtual WaybillStatuses WaybillStatus { get; set; }
        public virtual ICollection<WaybillDetails> WaybillDetails { get; set; }
    }
}
