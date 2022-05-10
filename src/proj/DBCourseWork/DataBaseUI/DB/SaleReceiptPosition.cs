using System;
using System.Collections.Generic;

namespace DataBaseUI
{
    public partial class SaleReceiptPosition
    {
        public int Id { get; set; }
        public int Availabilityid { get; set; }
        public int Salereceiptid { get; set; }

        public virtual Availability Availability { get; set; } = null!;
        public virtual SaleReceipt Salereceipt { get; set; } = null!;
    }
}
