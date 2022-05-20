using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    internal class Cost
    {
        public Cost(int availabilityId, int costValue)
        {
            AvailabilityId = availabilityId;
            CostValue = costValue;
        }

        public int AvailabilityId { get; set; }
        public int CostValue { get; set; }
    }
}
