using System;
using System.Collections.Generic;

namespace DataBaseUI.DB
{
    public partial class Product
    {
        public Product()
        {
            Availabilities = new HashSet<Availability>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Producttype { get; set; } = null!;

        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
