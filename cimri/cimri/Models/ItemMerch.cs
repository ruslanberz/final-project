using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class ItemMerch
    {
        public int id { get; set; }
        public int ItemID { get; set; }
        public double PriceNormal { get; set; } 
        public bool IsSale { get; set; }
        public double? PriceSale { get; set; }
        public int ShippingTypeID { get; set; }
        public int UserID { get; set; }
        public int SalePercent { get; set; }
        public int MerchID { get; set; }

        public virtual Item Item { get; set; }
        public virtual ShippiingType ShippiingType{ get; set; }
        public virtual User User { get; set; }
        public virtual Merch Merch { get; set; }
    }
}