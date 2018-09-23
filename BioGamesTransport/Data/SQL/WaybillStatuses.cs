using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class WaybillStatuses
    {
        public WaybillStatuses()
        {
            Waybill = new HashSet<Waybill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Waybill> Waybill { get; set; }
    }
}
