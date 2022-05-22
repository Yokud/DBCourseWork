using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    public class SaleReceipt
    {
        public SaleReceipt(int id, string fio, DateOnly dateOfPurchase, int shopId)
        {
            Id = id;
            Fio = fio;
            DateOfPurchase = dateOfPurchase;
            ShopId = shopId;
        }

        public SaleReceipt(string fio, DateOnly dateOfPurchase, int shopId)
        {
            Fio = fio;
            DateOfPurchase = dateOfPurchase;
            ShopId = shopId;
        }

        public int Id { get; set; }
        public string Fio { get; set; } = null!;
        public DateOnly DateOfPurchase { get; set; }
        public int ShopId { get; set; }
    }
}
