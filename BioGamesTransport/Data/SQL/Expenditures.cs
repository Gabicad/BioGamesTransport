using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class Expenditures
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Comment { get; set; }

        public virtual Orders Order { get; set; }
    }
}
