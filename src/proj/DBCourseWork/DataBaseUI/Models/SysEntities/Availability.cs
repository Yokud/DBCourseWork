﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.SysEntities
{
    public class Availability
    {
        public Availability(int id, int shopid, int productid)
        {
            Id = id;
            ShopId = shopid;
            ProductId = productid;
        }

        public Availability(int shopid, int productid)
        {
            ShopId = shopid;
            ProductId = productid;
        }

        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
    }
}