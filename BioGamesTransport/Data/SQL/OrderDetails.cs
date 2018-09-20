using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ShipModeId { get; set; }
        public int? ShipStatusId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ImagesId { get; set; }
        public int? ProductOutId { get; set; }
        public string ProductName { get; set; }
        public string ProductRef { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double? Deposit { get; set; }
        public double? PurchasePrice { get; set; }
        public double? ShipPrice { get; set; }
        public double? ExpensePrice { get; set; }
        public DateTime? ShipUndertakenDate { get; set; }
        public DateTime? ShipExpectedDate { get; set; }
        public DateTime? ShipDeliveredDate { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public virtual Images Images { get; set; }
        public virtual Manufacturers Manufacturer { get; set; }
        public virtual Orders Order { get; set; }
        public virtual ShipModes ShipMode { get; set; }
        public virtual ShipStatuses ShipStatus { get; set; }
    }
}
