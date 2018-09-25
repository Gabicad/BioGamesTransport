using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BioGamesTransport.Data.SQL
{
    public partial class InvoiceAddresses
    {
        public InvoiceAddresses()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? OutId { get; set; }
        [Display(Name = "Vezetéknév")]
        public string FirstName { get; set; }
        [Display(Name = "Keresztnév")]
        public string LastName { get; set; }
        [Display(Name = "Ország")]
        public string Country { get; set; }
        [Display(Name = "Város")]
        public string City { get; set; }
        [Display(Name = "Irányítószám")]
        public string Zipcode { get; set; }
        [Display(Name = "Cím")]
        public string Address { get; set; }
        [Display(Name = "Cég")]
        public string Company { get; set; }
        [Display(Name = "Telefonszám")]
        public string Phone { get; set; }
        [Display(Name = "Adószám")]
        public string TaxNumber { get; set; }
        [Display(Name = "Megjegyzés")]
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; }
        public bool Default { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
