using PriceCalc.Basket;
using PriceCalc.Discount;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PriceCalc.Models
{
    public class BasketProductModel : DiscountProcess, IEquatable<BasketProductModel>
    {
        public BasketProductModel()
        {
            Init(this);
        }

        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity;
        public int Freebies { get; set; }
        public double Total { get; set; }
        public OfferModel OfferForThisProduct { get; set; }

        public bool Equals(BasketProductModel other)
        {
            if (ProductName == other.ProductName
                && ProductPrice == other.ProductPrice
                && Quantity == other.Quantity
                && Freebies == other.Freebies
                && Total == other.Total)
            {
                return true;
            }
            return false;
        }
    }
}
