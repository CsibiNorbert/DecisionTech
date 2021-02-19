using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalc.Models
{
    public class OfferModel
    {
        public string OfferName { get; set; }
        public string OfferTriggerFrom { get; set; }
        public int DiscountAmount { get; set; }
        public bool IsFree { get; set; }

    }
}
