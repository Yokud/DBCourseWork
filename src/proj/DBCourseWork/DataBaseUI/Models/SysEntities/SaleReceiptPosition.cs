using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    public class SaleReceiptPosition
    {
        public SaleReceiptPosition(int id, int availabilityid, int salereceiptid)
        {
            Id = id;
            AvailabilityId = availabilityid;
            SaleReceiptId = salereceiptid;
        }

        public SaleReceiptPosition(int availabilityid, int salereceiptid)
        {
            AvailabilityId = availabilityid;
            SaleReceiptId = salereceiptid;
        }

        public int Id { get; set; }
        public int AvailabilityId { get; set; }
        public int SaleReceiptId { get; set; }
    }
}
