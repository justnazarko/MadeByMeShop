﻿namespace MadeByMe.src.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }

}
