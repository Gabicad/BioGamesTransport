using System;
using System.Collections.Generic;

namespace BioGamesTransport.Data.SQL
{
    public partial class SystemLog
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Logs { get; set; }
    }
}
