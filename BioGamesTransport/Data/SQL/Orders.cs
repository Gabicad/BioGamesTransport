using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Orders
    {
        public Orders()
        {
            Expenditures = new HashSet<Expenditures>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public int ShopId { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public int OrderOutId { get; set; }
        public int? ShipModeId { get; set; }
        public int ShipAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        public int? ShipStatusId { get; set; }
        public double TotalPrice { get; set; }
        public double? Deposit { get; set; }
        public double? ExpensePrice { get; set; }
        public double? ShipPrice { get; set; }
        public DateTime OrderDatetime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Comment { get; set; }
        public DateTime? LastCheck { get; set; }
        public string OrderOutRef { get; set; }
        public string Payment { get; set; }
        public string Shipment { get; set; }
        public DateTime? ShipUndertakenDate { get; set; }
        public DateTime? ShipExpectedDate { get; set; }
        public DateTime? ShipDeliveredDate { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual InvoiceAddresses InvoiceAddress { get; set; }
        public virtual OrderStatuses OrderStatus { get; set; }
        public virtual ShipAddresses ShipAddress { get; set; }
        public virtual ShipModes ShipMode { get; set; }
        public virtual ShipStatuses ShipStatus { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Expenditures> Expenditures { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
