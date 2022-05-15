﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.Models
{
    internal interface IShopsRepository : IRepository<Shop>
    {
        IEnumerable<Shop> Shops { get; set; }
    }
}
