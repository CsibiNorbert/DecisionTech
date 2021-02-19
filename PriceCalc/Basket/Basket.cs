using AutoMapper;
using PriceCalc.Discount;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalc.Basket
{
    /// <summary>
    /// The basket class which holds the total of the basket and the products details
    /// </summary>
    public class Basket : IBasket
    {
        private BasketModel _basketProducts;
        private double _totalCost;
        public BasketModel Baskett { 
            get {
                if (_basketProducts == null)
                    throw new ArgumentNullException("xxxxxxxxxx");

                return _basketProducts;
            } 
        }

        public double TotalCost
        {
            get
            {
                return _totalCost;
            }
        }

        private readonly IDiscountService _discountService;

        public Basket(IDiscountService discountService)
        {
            _discountService = discountService;

            _basketProducts = new BasketModel();
            _basketProducts.Products = new List<BasketProductModel>();
        }

        /// <summary>
        /// Adds the product to the basket and then we apply discounts if any
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddProductToBasket(BasketProductModel product)
        {
            if (product == null)
                throw new ArgumentNullException();

            var obj = _basketProducts.Products.FirstOrDefault(x => x.ProductName == product.ProductName);
            if (obj == null)
                _basketProducts.Products.Add(product);
            else
            {
                obj.Quantity += product.Quantity;
            }
            _basketProducts =  await _discountService.ApplyDiscount(_basketProducts, product);

        }

        /// <summary>
        /// Calculate the basked total
        /// </summary>
        public void CalculateBasketTotal()
        {
            foreach (var item in Baskett.Products)
            {
                _totalCost += Math.Round(item.Total, 2);
            }
        }
    }
}
