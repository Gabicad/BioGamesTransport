using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Images
    {
        public Images()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ContentType { get; set; }
        public DateTime? Created { get; set; }
        public int? ShopId { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
