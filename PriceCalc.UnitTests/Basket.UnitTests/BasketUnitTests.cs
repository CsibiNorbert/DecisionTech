using Moq;
using NUnit.Framework;
using PriceCalc.Discount;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalc.UnitTests.Basket.UnitTests
{
    /// <summary>
    /// This is used for Unit Testing each method individually. But for winning time, I've just created one class for the basket to test the method AddProductToBasket.
    /// </summary>
    
    public class BasketUnitTests
    {
        private Mock<IDiscountService> discountServiceMock;
        private PriceCalc.Basket.Basket subject;

        [SetUp]
        public void Setup()
        {
            discountServiceMock = new Mock<IDiscountService>();
            subject = new PriceCalc.Basket.Basket(discountServiceMock.Object);
        }

        [Test]
        public async Task AddToBasket_ReturnOk()
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

            BasketModel returnedFromApplyDiscount = new BasketModel { Products = expectedProducts };
            List<BasketProductModel> productsModel = new List<BasketProductModel> {
                new BasketProductModel() {
                    ProductName = "Milk",
                    ProductPrice = 1.15,
                    Quantity = 1
                }
            };

            subject.Baskett.Products = productsModel;

            discountServiceMock
                .Setup(x => x.ApplyDiscount(subject.Baskett, It.IsAny<BasketProductModel>())).Returns(Task.FromResult(returnedFromApplyDiscount));

            var milk = new BasketProductModel()
            {
                ProductName = "Milk",
                ProductPrice = 1.15,
                Quantity = 1
            };

            //Act
            await subject.AddProductToBasket(milk);
            subject.CalculateBasketTotal();

            //Assert
            Assert.AreEqual(2.30, subject.TotalCost);
            Assert.AreEqual(expectedProducts[0].Quantity, subject.Baskett.Products[0].Quantity);
        }

        [Test]
        public async Task CalculateBasketTotal_ReturnOk()
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
            BasketModel returnedFromApplyDiscount = new BasketModel { Products = expectedProducts };

            subject.Baskett.Products = expectedProducts;

            discountServiceMock
                .Setup(x => x.ApplyDiscount(subject.Baskett, It.IsAny<BasketProductModel>())).Returns(Task.FromResult(returnedFromApplyDiscount));

            // Act
            subject.CalculateBasketTotal();

            //Assert
            Assert.AreEqual(2.30, subject.TotalCost);
        }
    }
}
