using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalc.Models
{
    /// <summary>
    /// The reason for creating this class,
    /// is because I thought that maybe we can have a set of discounts for each product that is triggered by X product with X amount of % OFF
    /// </summary>
    public class Discounts
    {
        public string DiscountTriggeredBy { get; set; }
        public string DiscountTriggers { get; set; }
        public int MinimumProductsToTrigger { get; set; }
    }
}
