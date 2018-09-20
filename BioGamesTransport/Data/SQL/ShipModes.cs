using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class ShipModes
    {
        public ShipModes()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
