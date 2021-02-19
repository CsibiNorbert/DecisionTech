using Moq;
using NUnit.Framework;
using PriceCalc.Discount;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalc.UnitTests.Discount.UnitTests
{
    public class DiscountUnitTests
    {
        private DiscountService subject;

        [SetUp]
        public void Setup()
        {
            subject = new DiscountService();
        }

        [Test]
        public async Task ApplyDiscount_ReturnOk()
        {
            //Arrange
            List<BasketProductModel> expectedProducts = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Milk",
                    ProductPrice = 1.15,
                    Quantity = 2,
                    Total = 2.30,
                    OfferForThisProduct = new OfferModel
                    {
                        IsFree = true,
                        OfferName = "Milk",
                        OfferTriggerFrom = "Milk"
                    }
                }
            };

            BasketModel basketModel = new BasketModel { Products = expectedProducts };
            var milk = new BasketProductModel()
            {
                ProductName = "Milk",
                ProductPrice = 1.15,
                Quantity = 1,
                OfferForThisProduct = new OfferModel
                {
                    IsFree = true,
                    OfferName = "Milk",
                    OfferTriggerFrom = "Milk"
                }
            };

            //Act
            var resultBasket = await subject.ApplyDiscount(basketModel, milk);

            //Assert
            Assert.AreEqual(expectedProducts[0].Total, resultBasket.Products[0].Total);
            Assert.AreEqual(expectedProducts[0].Quantity, resultBasket.Products[0].Quantity);
        }

        /// <summary>
        /// Throws NullReferenceException if the offer is not passed alongside with the product
        /// Maybe this can be adjusted according to the future requirements etc..However, I wanted to test this as well
        /// </summary>
        /// <returns></returns>
        [Test]
        public void ApplyDiscount_Throws_NullReferenceException_Ok()
        {
            //Arrange
            List<BasketProductModel> expectedProducts = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Milk",
                    ProductPrice = 1.15,
                    Quantity = 2,
                    Total = 2.30
                }
            };

            BasketModel basketModel = new BasketModel { Products = expectedProducts };
            var milk = new BasketProductModel()
            {
                ProductName = "Milk",
                ProductPrice = 1.15,
                Quantity = 1
            };

            //Assert
            Assert.Throws<NullReferenceException>(() => subject.ApplyDiscount(basketModel, milk));
        }

    }
}
