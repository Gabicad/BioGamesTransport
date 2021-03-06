﻿using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class ShipModes
    {
        public ShipModes()
        {
            Waybill = new HashSet<Waybill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Waybill> Waybill { get; set; }
    }
}
