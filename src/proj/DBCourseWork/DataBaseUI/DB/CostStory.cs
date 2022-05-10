using System;
using System.Collections.Generic;

namespace DataBaseUI
{
    public partial class CostStory
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Cost { get; set; }
        public int Availabilityid { get; set; }

        public virtual Availability Availability { get; set; } = null!;
    }
}
