using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class OrderStatuses
    {
        public OrderStatuses()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AlertTime { get; set; }
        public int Priority { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
