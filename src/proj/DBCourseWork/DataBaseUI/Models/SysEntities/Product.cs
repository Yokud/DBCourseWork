using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    public class Product
    {
        public Product(int id, string name, string productType, int? cost = null)
        {
            Id = id;
            Name = name;
            ProductType = productType;
            Cost = cost;
        }

        public Product(string name, string productType, int? cost = null)
        {
            Name = name;
            ProductType = productType;
            Cost = cost;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public int? Cost { get; set; }
    }
}
