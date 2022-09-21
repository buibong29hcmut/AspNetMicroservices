﻿namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }= new List<ShoppingCartItem>();        
        public ShoppingCart()
        {

        }
        public ShoppingCart(string username)
        {
            this.UserName = username;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                if (Items.Count() > 0)
                {
                    foreach (var item in Items)
                    {
                        totalprice += item.Price * item.Quantity;
                    }
                }
                return totalprice;
            }
        }
    }
}
