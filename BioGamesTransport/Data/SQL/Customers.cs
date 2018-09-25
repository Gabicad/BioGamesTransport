using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioGamesTransport.Data.SQL
{
    public partial class Customers
    {
        public Customers()
        {
            InvoiceAddresses = new HashSet<InvoiceAddresses>();
            Orders = new HashSet<Orders>();
            ShipAddresses = new HashSet<ShipAddresses>();
        }

        public InvoiceAddresses helperInvoiceAddresses = new InvoiceAddresses();
        public ShipAddresses helperShipAddresses = new ShipAddresses();

        public int Id { get; set; }
        [Display(Name = "Bolt")]
        public int? ShopId { get; set; }

        public int? OutCustomerId { get; set; }

        [Display(Name = "Vezetéknév")]
        public string FirstName { get; set; }

        [Display(Name = "Keresztnév")]
        public string LastName { get; set; }
        public bool? Newsletter { get; set; }
        [Display(Name = "Típus")]
        public string Type { get; set; }
        [Display(Name = "Telefonszám")]
        public string Phone { get; set; }
        [Display(Name = "E-mail cím")]
        public string Email { get; set; }
        [Display(Name = "Számlaszám")]
        public string BankAccount { get; set; }
        [Display(Name = "Cég")]
        public string Company { get; set; }
         [Display(Name = "Megjegyzés")]
        public string Comment { get; set; }

        [Display(Name = "Létrehozva")]
        public DateTime? Created { get; set; }
        [Display(Name = "Utolsó módosítás")]
        public DateTime? Modified { get; set; }
        [Display(Name = "Törölt")]
        public bool Deleted { get; set; }

        [NotMapped]
        [Display(Name = "Név")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [NotMapped]
        [Display(Name = "Info")]
        public string ShortDetails { get { return string.Format("{0} {1} | {2} | {3}", FirstName, LastName, Email, Company); } }


        [Display(Name = "Bolt")]
        public virtual Shops Shop { get; set; }
        public virtual ICollection<InvoiceAddresses> InvoiceAddresses { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ShipAddresses> ShipAddresses { get; set; }
    }
}
