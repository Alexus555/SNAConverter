using System;
using System.Collections.Generic;
using System.Text;

namespace SNAConverter.Data
{
    public abstract class AlcoBaseClass
    {
        public int BottleCount { get; set; }
        public double LitreCount { get; set; }
        public decimal Cost { get; set; }
        public string RegNumber { get; set; }
    }
}
