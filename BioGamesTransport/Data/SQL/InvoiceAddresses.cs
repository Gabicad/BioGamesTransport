using System;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string TaxNumber { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; }
        public bool Default { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
