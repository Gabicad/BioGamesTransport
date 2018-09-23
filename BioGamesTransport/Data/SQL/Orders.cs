using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioGamesTransport.Data.SQL
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }


        public int Id { get; set; }
        public int ShopId { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public int OrderOutId { get; set; }
        public int ShipAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        public int? ShipStatusId { get; set; }

        [Display(Name = "Fizetendő")]
        [DisplayFormat(DataFormatString = "{0:# ###} Ft", ApplyFormatInEditMode = false)]
        public double TotalPrice { get; set; }

        [Display(Name = "Előleg")]
        [DisplayFormat(DataFormatString = "{0:# ###} Ft", ApplyFormatInEditMode = false)]
        public double? Deposit { get; set; }

 
        [Display(Name = "Rendelés ideje")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime OrderDatetime { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Comment { get; set; }
        public DateTime? LastCheck { get; set; }

        [Display(Name = "Azonosító")]
        public string OrderOutRef { get; set; }
        public string Payment { get; set; }
        public string Shipment { get; set; }

        [Display(Name = "Válalt szállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipUndertakenDate { get; set; }

        [Display(Name = "Várható szállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipExpectedDate { get; set; }

        [Display(Name = "Valós kiszállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipDeliveredDate { get; set; }

        [Display(Name = "Előleg befizetés")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? DepositDate { get; set; }

        [Display(Name = "Ügyfél")]
        public virtual Customers Customer { get; set; }
        public virtual InvoiceAddresses InvoiceAddress { get; set; }

        [Display(Name = "Megrendelés állapot")]
        public virtual OrderStatuses OrderStatus { get; set; }
        public virtual ShipAddresses ShipAddress { get; set; }

        [Display(Name = "Szállíás állapot")]
        public virtual ShipStatuses ShipStatus { get; set; }
        public virtual Shops Shop { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }




        [NotMapped]
        [Display(Name = "Maradvány")]
        [DisplayFormat(DataFormatString = "{0:# ###} Ft", ApplyFormatInEditMode = false)]
        public double Residual { get => setAutoField(); }

        private double setAutoField()
        {
            if(Deposit != null)
            {
                return TotalPrice - (double)Deposit;
            }
            else
            {
                return TotalPrice;
            }
        }


    }
}
