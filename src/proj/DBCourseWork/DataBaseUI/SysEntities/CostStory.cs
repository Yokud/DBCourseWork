using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    internal class CostStory
    {
        public CostStory(int id, int year, int month, int cost, int availabilityid)
        {
            Id = id;
            Year = year;
            Month = month;
            Cost = cost;
            AvailabilityId = availabilityid;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Cost { get; set; }
        public int AvailabilityId { get; set; }
    }
}
