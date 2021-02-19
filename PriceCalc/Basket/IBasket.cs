using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriceCalc.Basket
{
    public interface IBasket
    {
        double TotalCost { get; }
        BasketModel Baskett { get; }
        Task AddProductToBasket(BasketProductModel product);
        void CalculateBasketTotal();
    }
}
