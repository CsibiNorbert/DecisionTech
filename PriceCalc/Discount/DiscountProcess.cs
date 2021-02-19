using PriceCalc.Basket;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalc.Discount
{
    /// <summary>
    /// An Abstract class for the discounted products, here we can put pre-defined code for discounts or freebies.
    /// Also, I created this class so that we can override methods in case each discount might behave in different way than other products etc.
    /// </summary>
    public abstract class DiscountProcess
    {
        private BasketProductModel _obj { get; set; }

        protected void Init(BasketProductModel bPModel)
        {
            _obj = bPModel;
        }

        /// <summary>
        /// Calculating discount
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="product"></param>
        public void CalculateDiscount(ref BasketModel basket, BasketProductModel product)
        {
            var productContained = NumberOfProductsInBasket(ref basket, _obj);
            var obj = basket.Products.FirstOrDefault(x => x.ProductName == _obj.ProductName);
            CalculateTotal(ref obj, false);

            var exists = basket.Products.Select(x => x.ProductName == obj?.OfferForThisProduct?.OfferTriggerFrom).FirstOrDefault();
            var productWithOfferQuanity = basket.Products.FirstOrDefault(prod => prod.ProductName == obj?.OfferForThisProduct?.OfferTriggerFrom);

            switch (_obj.ProductName)
            {
                case "Milk":
                     obj.Freebies = numberOfFreeItems(productWithOfferQuanity.Quantity);
                    CalculateTotal(ref obj, true);
                    break;
                case "Butter":

                    break;
                case "Bread":
                    if (exists && productWithOfferQuanity != null)
                    {
                        for (int i = 0; i < productWithOfferQuanity.Quantity; i++)
                        {
                            if (i % 2 == 0)
                            {
                                CalculateDiscountedPrice(obj.OfferForThisProduct.DiscountAmount, ref obj);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Calculating the numbers of freebies of a given product, based on how many products we have in the basket
        /// </summary>
        /// <param name="productContained"></param>
        /// <returns></returns>
        private int numberOfFreeItems(int productContained)
        {
            var freebies = 0;
            for (int i = 1; i <= productContained; i++)
            {
                if (i % 3 == 0)
                {
                    freebies++;
                }
            }

            return freebies;
        }

        /// <summary>
        /// Calculate the discounted price
        /// </summary>
        /// <param name="percentageToSubtract"></param>
        /// <param name="bModel"></param>
        private void CalculateDiscountedPrice(int percentageToSubtract, ref BasketProductModel bModel)
        {
            var discountedPrice = bModel.ProductPrice - (bModel.ProductPrice * percentageToSubtract / 100);
            bModel.Total -= discountedPrice; 
        }

        /// <summary>
        /// Calculate the totals if freebies available or not (we calculate the total of each product)
        /// </summary>
        /// <param name="bModel"></param>
        /// <param name="calculateFreebes"></param>
        private void CalculateTotal(ref BasketProductModel bModel, bool calculateFreebes = true)
        {
            if (bModel.Freebies != 0 && calculateFreebes)
            {
                var totalQuantity = bModel.Quantity - bModel.Freebies;
                bModel.Total = totalQuantity * bModel.ProductPrice;
            } else
            {
                bModel.Total = bModel.Quantity * bModel.ProductPrice;
            }
        }

        /// <summary>
        /// We calculate quantity of products
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="product"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public int NumberOfProductsInBasket(ref BasketModel basket, BasketProductModel product = null, string productName = null)
        {
            var productContained = 0;
            if (product != null)
                productContained = basket.Products.FirstOrDefault(x => x.ProductName == product.ProductName).Quantity;
            else
            {
                if (basket.Products.FirstOrDefault(x => x.ProductName == productName) != null)
                    productContained = basket.Products.FirstOrDefault(x => x.ProductName == productName).Quantity;
            }

            return productContained;
        }
    }
}
