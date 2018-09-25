using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Ár")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double Price { get; set; }

        [Display(Name = "Előleg")]
        [DisplayFormat(DataFormatString = "{0:# ### ###} Ft", ApplyFormatInEditMode = false)]
        public double? Deposit { get; set; }

        [Display(Name = "Beszerzési ár")]
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
        [Display(Name = "Kép")]
        public virtual Images Images { get; set; }
        [Display(Name = "Gyártó")]
        public virtual Manufacturers Manufacturer { get; set; }
        [Display(Name = "Megrendelés")]
        public virtual Orders Order { get; set; }
        public virtual ShipStatuses ShipStatus { get; set; }
        public virtual ICollection<WaybillDetails> WaybillDetails { get; set; }
    }
}
