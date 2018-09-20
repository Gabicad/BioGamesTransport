using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Manufacturers
    {
        public Manufacturers()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
