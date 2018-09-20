using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class ShipStatuses
    {
        public ShipStatuses()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AlertTime { get; set; }
        public string Color { get; set; }
        public int? Priority { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
