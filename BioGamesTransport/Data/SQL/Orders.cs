﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioGamesTransport.Data.SQL;

namespace BioGamesTransport.Data.SQL
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public Customers Customers = new Customers();
        public InvoiceAddresses InvoiceAddresses = new InvoiceAddresses();
        public ShipAddresses ShipAddresses = new ShipAddresses();
        public OrderDetails OrderDetailsForm = new OrderDetails();


        public int Id { get; set; }

        [Display(Name = "Bolt")]
        public int ShopId { get; set; }

        [Display(Name = "Megrendelés Státusz")]
        public int OrderStatusId { get; set; }

        [Display(Name = "Vevő")]
        public int CustomerId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Azonosító")]
        public int OrderOutId { get; set; }
        [Display(Name = "Szállítási cím")]
        public int ShipAddressId { get; set; }
        [Display(Name = "Számlázási cím")]
        public int InvoiceAddressId { get; set; }
        [Display(Name = "Szállítási állapot")]
        public int? ShipStatusId { get; set; }

        [Display(Name = "Fizetendő")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double TotalPrice { get; set; }

        [Display(Name = "Előleg")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double? Deposit { get; set; }

 
        [Display(Name = "Rendelés ideje")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime OrderDatetime { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        [Display(Name = "Megjegyzés")]
        public string Comment { get; set; }
        public DateTime? LastCheck { get; set; }

        [Display(Name = "Azonosító")]
        public string OrderOutRef { get; set; }
        [Display(Name = "Fizetési mód")]
        public string Payment { get; set; }
        [Display(Name = "Szállítási mód")]
        public string Shipment { get; set; }

        [Display(Name = "Vállalt szállítás")]
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
        [Display(Name = "Számlázási cím")]
        public virtual InvoiceAddresses InvoiceAddress { get; set; }

        [Display(Name = "Megrendelés állapot")]
        public virtual OrderStatuses OrderStatus { get; set; }
        [Display(Name = "Szállítási cím")]
        public virtual ShipAddresses ShipAddress { get; set; }

        [Display(Name = "Szállíás állapot")]
        public virtual ShipStatuses ShipStatus { get; set; }
        [Display(Name = "Bolt")]
        public virtual Shops Shop { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }




        [NotMapped]
        [Display(Name = "Maradvány")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double Residual { get => setAutoField(); }

        [NotMapped]
        [Display(Name = "Összes termék")]
        [DisplayFormat(DataFormatString = "{0} db", ApplyFormatInEditMode = false)]
        public int ProdCount { get => countProductOrderDetails(); }

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


        private int countProductOrderDetails()
        {
           int i = 0;
            foreach(OrderDetails item in OrderDetails)
            {
                i = item.Quantity;
            }
            return i;
        }




    }
}
