using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Services
{
    public class BuyerCartService
    {
        private readonly ApplicationDbContext _context;

        public BuyerCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddToCart(AddToCartDto addToCartDto)
        {
            var cart = _context.Carts
                .FirstOrDefault(c => c.BuyerId == addToCartDto.UserId);

            if (cart == null)
            {
                cart = new Cart { BuyerId = addToCartDto.UserId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var existingItem = _context.BuyerCarts
                .FirstOrDefault(bc => bc.CartId == cart.CartId && bc.PostId == addToCartDto.PostId);

            if (existingItem != null)
            {
                existingItem.Quantity += addToCartDto.Quantity;
            }
            else
            {
                var cartItem = new BuyerCart
                {
                    CartId = cart.CartId,
                    PostId = addToCartDto.PostId,
                    Quantity = addToCartDto.Quantity
                };
                _context.BuyerCarts.Add(cartItem);
            }

            _context.SaveChanges();
            return true;
        }
    }
}