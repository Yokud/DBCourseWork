using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    internal class SaleReceipt
    {
        public int Id { get; set; }
        public string Fio { get; set; } = null!;
        public DateOnly DateOfPurchase { get; set; }
        public Shop Shop { get; set; } = null!;
        public IEnumerable<SaleReceiptPosition> SaleReceiptPositions { get; set; } = null!;
    }
}
