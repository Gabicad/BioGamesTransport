using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Products
    {
        public int Id { get; set; }
        public int? IdOut { get; set; }
        public int? ShopId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ImageId { get; set; }
        public int? Quantity { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Depth { get; set; }
        public double? Weight { get; set; }
        public string Ean13 { get; set; }
        public string Isbn { get; set; }
        public string Upc { get; set; }
        public int? MinimalQuantity { get; set; }
        public double? Price { get; set; }
        public double? WolesalePrice { get; set; }
        public double? UnitPriceRatio { get; set; }
        public double? ShippingCost { get; set; }
        public bool? Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }

        public virtual Images Image { get; set; }
        public virtual Manufacturers Manufacturer { get; set; }
        public virtual Shops Shop { get; set; }
    }
}
