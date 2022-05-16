using System;
using System.Collections.Generic;

namespace DataBaseUI.DB
{
    public partial class SaleReceipt
    {
        public SaleReceipt()
        {
            Salereceiptpositions = new HashSet<SaleReceiptPosition>();
        }

        public int Id { get; set; }
        public string Fio { get; set; } = null!;
        public int Shopid { get; set; }
        public DateOnly Dateofpurchase { get; set; }

        public virtual Shop Shop { get; set; } = null!;
        public virtual ICollection<SaleReceiptPosition> Salereceiptpositions { get; set; }
    }
}
