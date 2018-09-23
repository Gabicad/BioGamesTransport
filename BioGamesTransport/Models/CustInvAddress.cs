using BioGamesTransport.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioGamesTransport.Models
{
    public class CustInvAddress
    {
        public Customers Customers = new Customers();
        public InvoiceAddresses InvoiceAddresses = new InvoiceAddresses();
        public ShipAddresses ShipAddresses = new ShipAddresses();

    }
}
