using AutoMapper;
using Moq;
using NUnit.Framework;
using PriceCalc.Basket;
using PriceCalc.Discount;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalc.Tests.BasketTests
{
    /// <summary>
    /// This test is an integration test ( testing the flow from start to finish, to ensure we get the expected results
    /// </summary>
    public class BasketT
    {
        private IBasket basketService;
        private IDiscountService discountService;

        [SetUp]
        public void Setup()
        {
            discountService = new DiscountService();
            basketService = new Basket.Basket(discountService);
        }

        [Test]
        public async System.Threading.Tasks.Task BasketHasOneOfEachProduct_ReturnOk()
        {
            //Arrange
            List<BasketProductModel> expectedProducts = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Milk",
                    ProductPrice = 1.15,
                    Quantity = 1,
                    Total = 1.15
                },
                new BasketProductModel() {
                    ProductName = "Bread",
                    ProductPrice = 1.00,
                    Quantity = 1,
                    Total = 1.00
                },
                new BasketProductModel() {
                    ProductName = "Butter",
                    ProductPrice = 0.80,
                    Quantity = 1,
                    Total = 0.80
                }
            };
            var expectedBasket = new BasketModel { Products = expectedProducts };

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
            var butter = new BasketProductModel()
            {
                ProductName = "Butter",
                ProductPrice = 0.80,
                Quantity = 1
            };
            var bread = new BasketProductModel()
            {
                ProductName = "Bread",
                ProductPrice = 1.00,
                Quantity = 1,
                OfferForThisProduct = new OfferModel
                {
                    DiscountAmount = 50,
                    IsFree = false,
                    OfferName = "Bread",
                    OfferTriggerFrom = "Butter"
                }
            };

            //Act
            await basketService.AddProductToBasket(milk);
            await basketService.AddProductToBasket(bread);
            await basketService.AddProductToBasket(butter);

            basketService.CalculateBasketTotal();
            //Assert
            Assert.IsNotNull(basketService.Baskett);
            Assert.AreEqual(expectedBasket.Products, basketService.Baskett.Products);
            Assert.That(basketService.TotalCost, Is.EqualTo(2.95));

        }

        [Test]
        public async System.Threading.Tasks.Task BasketHasTwoBreadAndTwoButter_ReturnOk()
        {
            //Arrange
            List<BasketProductModel> expectedProducts = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Butter",
                    ProductPrice = 0.80,
                    Quantity = 2,
                    Total = 1.60
                },
                new BasketProductModel() {
                    ProductName = "Bread",
                    ProductPrice = 1.00,
                    Quantity = 2,
                    Total = 1.5
                }
            };
            var expectedBasket = new BasketModel { Products = expectedProducts };

            var butter = new BasketProductModel()
            {
                ProductName = "Butter",
                ProductPrice = 0.80,
                Quantity = 2
            };
            var bread = new BasketProductModel()
            {
                ProductName = "Bread",
                ProductPrice = 1.00,
                Quantity = 2,
                OfferForThisProduct = new OfferModel
                {
                    DiscountAmount = 50,
                    IsFree = false,
                    OfferName = "Bread",
                    OfferTriggerFrom = "Butter"
                }
            };

            //Act
            await basketService.AddProductToBasket(butter);
            await basketService.AddProductToBasket(bread);

            basketService.CalculateBasketTotal();
            //Assert
            Assert.IsNotNull(basketService.Baskett);
            Assert.AreEqual(expectedBasket.Products, basketService.Baskett.Products);
            Assert.That(basketService.TotalCost, Is.EqualTo(3.10));

        }

        [Test]
        public async System.Threading.Tasks.Task BasketHasOneFreeMilk_ReturnOk()
        {
            //Arrange
            List<BasketProductModel> productsModel = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Milk",
                    ProductPrice = 1.15,
                    Quantity = 3,
                    OfferForThisProduct = new OfferModel
                        {
                            IsFree = true,
                            OfferName = "Milk",
                            OfferTriggerFrom = "Milk"
                        }
                }
            };

            basketService.Baskett.Products = productsModel;

            var milk = new BasketProductModel()
            {
                ProductName = "Milk",
                ProductPrice = 1.00,
                Quantity = 1,
                OfferForThisProduct = new OfferModel
                {
                    IsFree = true,
                    OfferName = "Milk",
                    OfferTriggerFrom = "Milk"
                }
            };

            //Act
            await basketService.AddProductToBasket(milk);

            basketService.CalculateBasketTotal();
            //Assert
            var totalBasket = Math.Round(basketService.Baskett.Products[0].Total, 2);
            Assert.IsNotNull(basketService.Baskett);
            Assert.That(totalBasket, Is.EqualTo(3.45));

        }

        [Test]
        public async System.Threading.Tasks.Task BasketHasTwoButterOneBreadEightMilk_ReturnOk()
        {
            //Arrange
            var butter = new BasketProductModel()
            {
                ProductName = "Butter",
                ProductPrice = 0.80,
                Quantity = 2
            };
            var bread = new BasketProductModel()
            {
                ProductName = "Bread",
                ProductPrice = 1.00,
                Quantity = 1,
                OfferForThisProduct = new OfferModel
                {
                    DiscountAmount = 50,
                    IsFree = false,
                    OfferName = "Bread",
                    OfferTriggerFrom = "Butter"
                }
            };

            var milk = new BasketProductModel()
            {
                ProductName = "Milk",
                ProductPrice = 1.15,
                Quantity = 8,
                OfferForThisProduct = new OfferModel
                {
                    IsFree = true,
                    OfferName = "Milk",
                    OfferTriggerFrom = "Milk"
                }
            };

            //Act
            await basketService.AddProductToBasket(butter);
            await basketService.AddProductToBasket(bread);
            await basketService.AddProductToBasket(milk);

            basketService.CalculateBasketTotal();

            //Assert
            var totalBasket = Math.Round(basketService.TotalCost, 2);

            Assert.IsNotNull(basketService.Baskett);
            Assert.That(totalBasket, Is.EqualTo(9.00));

        }
    }
}
