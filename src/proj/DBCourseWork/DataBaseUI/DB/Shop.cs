using System;
using System.Collections.Generic;

namespace DataBaseUI
{
    public partial class Shop
    {
        public Shop()
        {
            Availabilities = new HashSet<Availability>();
            Salereceipts = new HashSet<SaleReceipt>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Availability> Availabilities { get; set; }
        public virtual ICollection<SaleReceipt> Salereceipts { get; set; }
    }
}
