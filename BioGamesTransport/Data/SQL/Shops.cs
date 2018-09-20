using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Shops
    {
        public Shops()
        {
            Customers = new HashSet<Customers>();
            Orders = new HashSet<Orders>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public int? LastSyncOrderId { get; set; }
        public DateTime? LastSync { get; set; }
        public int? LastSyncProdId { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
