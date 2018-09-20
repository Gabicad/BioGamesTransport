using System;
using System.Collections.Generic;

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

        public virtual Shops Shop { get; set; }
        public virtual ICollection<InvoiceAddresses> InvoiceAddresses { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ShipAddresses> ShipAddresses { get; set; }
    }
}
