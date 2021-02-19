using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalc.Discount
{
    public interface IDiscountService
    {
        Task<BasketModel> ApplyDiscount(BasketModel basket, BasketProductModel product);
    }
}
