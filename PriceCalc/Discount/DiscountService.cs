using PriceCalc.Basket;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalc.Discount
{
    public class DiscountService : IDiscountService
    {
        // This can be an array of OBJECTS with defined % discount or this can come from a DB table etc
        // What I did, is I created a class for Offers in which is attached to each product which has a specific offer either Freebie/Discount
        // and that can be extended in case we have more discounts when buy an specific product.
        public static readonly ReadOnlyCollection<string> discountedProducts = new List<String> {
                                                                                            "Milk",
                                                                                            "Butter",
                                                                                            "Bread",
        }.AsReadOnly();

        public Task<BasketModel> ApplyDiscount(BasketModel basket, BasketProductModel product)
        {
            if (discountedProducts.Contains(product.ProductName) && basket.Products.Count > 0)
                product.CalculateDiscount(ref basket, product);

            return Task.FromResult(basket);
        }

    }
}
