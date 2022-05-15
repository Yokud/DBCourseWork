using System;
using System.Collections.Generic;

namespace DataBaseUI
{
    public partial class Availability
    {
        public Availability()
        {
            Coststories = new HashSet<CostStory>();
            Salereceiptpositions = new HashSet<SaleReceiptPosition>();
        }

        public int Id { get; set; }
        public int Shopid { get; set; }
        public int Productid { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Shop Shop { get; set; } = null!;
        public virtual ICollection<CostStory> Coststories { get; set; }
        public virtual ICollection<SaleReceiptPosition> Salereceiptpositions { get; set; }
    }
}
