using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Database;

namespace OnlineShop.Models.Home
{
    public class Items
    {
        public Tbl_Product product { get; set; }
        public int quantity { get; set; }
    }
}