using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class WaybillDetails
    {
        public int Id { get; set; }
        public int? WaybillId { get; set; }
        public int? OrderDetailsId { get; set; }
        public int? Weighting { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }
        public virtual Waybill Waybill { get; set; }
    }
}
