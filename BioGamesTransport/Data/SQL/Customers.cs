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
        public int? ShopId { get; set; }
        public int? OutCustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Newsletter { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string Company { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; }

        [NotMapped]
        [Display(Name = "Név")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [NotMapped]
        [Display(Name = "Info")]
        public string ShortDetails { get { return string.Format("{0} {1} | {2} | {3}", FirstName, LastName, Email, Company); } }



        public virtual Shops Shop { get; set; }
        public virtual ICollection<InvoiceAddresses> InvoiceAddresses { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ShipAddresses> ShipAddresses { get; set; }
    }
}
