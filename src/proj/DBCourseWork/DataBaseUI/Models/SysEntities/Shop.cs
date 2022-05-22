using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    public class Shop
    {
        public Shop(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Shop(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
