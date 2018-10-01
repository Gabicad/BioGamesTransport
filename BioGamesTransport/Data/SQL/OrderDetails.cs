using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioGamesTransport.Data.SQL
{
    public partial class OrderDetails
    {
        public OrderDetails()
        {
            WaybillDetails = new HashSet<WaybillDetails>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Szállítás állapot")]
        public int? ShipStatusId { get; set; }
        [Display(Name = "Gyártó")]
        public int? ManufacturerId { get; set; }
        [Display(Name = "Kép")]
        public int? ImagesId { get; set; }
        public int? ProductOutId { get; set; }
        [Display(Name = "Termék név")]
        public string ProductName { get; set; }
        [Display(Name = "Termék cikkszám")]
        public string ProductRef { get; set; }

        [Display(Name = "Darab")]
        [DisplayFormat(DataFormatString = "{0} db", ApplyFormatInEditMode = false)]
        public int Quantity { get; set; }

        [Display(Name = "Ár (brutto)")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double Price { get; set; }

        [Display(Name = "Előleg")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double? Deposit { get; set; }

        [Display(Name = "Beszerzési ár (netto)")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double? PurchasePrice { get; set; }

        [Display(Name = "Vállalt szállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipUndertakenDate { get; set; }
        [Display(Name = "Várható szállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipExpectedDate { get; set; }
        [Display(Name = "Valós kiszállítás")]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        public DateTime? ShipDeliveredDate { get; set; }

        [Display(Name = "Megjegyzés")]
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        [Display(Name = "Törölt")]
        public bool? Deleted { get; set; }

        [UIHint("YesNo")]
        [Display(Name = "Raktáron")]
        public bool InStock { get; set; }
        [Display(Name = "Kép")]
        public virtual Images Images { get; set; }
        [Display(Name = "Gyártó")]
        public virtual Manufacturers Manufacturer { get; set; }
        [Display(Name = "Megrendelés")]
        public virtual Orders Order { get; set; }
        public virtual ShipStatuses ShipStatus { get; set; }
        public virtual ICollection<WaybillDetails> WaybillDetails { get; set; }


        [NotMapped]
        [Display(Name = "Várható nyereség (netto)")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double? Profit { get => countProfitOrderDetails(); }


        private double? countProfitOrderDetails()
        {
            double tmpTotal = 0;
            double? tmpBeszar = 0;
     
                tmpTotal += (Price / 1.27) * Quantity;
                tmpBeszar += PurchasePrice * Quantity;
            
            return (tmpTotal - tmpBeszar);
        }




    }
}
