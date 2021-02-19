using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PriceCalc.Models
{
    /// <summary>
    /// I used the IEquatable<T> for asserting objects in the tests. By default, when we say something like Assert.IsEqual(x,y),
    /// we basically asserting the same reference, but in this case I was asserting 2 different objects
    /// </summary>
    public class BasketModel : IEquatable<BasketModel>
    {
        public List<BasketProductModel> Products;

        public bool Equals(BasketModel basketM)
        {
            if (Products == basketM.Products)
            {
                return true;
            }
            return false;
        }
    }
}
