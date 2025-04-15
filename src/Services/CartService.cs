using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MadeByMe.src.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart GetUserCart(int userId)
        {
            return _context.Carts
                .Include(c => c.BuyerCarts)
                .ThenInclude(bc => bc.Post)
                .FirstOrDefault(c => c.BuyerId == userId);
        }

        public Cart AddToCart(AddToCartDto dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.BuyerId == dto.UserId);
            if (cart == null)
            {
                cart = new Cart { BuyerId = dto.UserId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var existingItem = _context.BuyerCarts
                .FirstOrDefault(bc => bc.CartId == cart.CartId && bc.PostId == dto.PostId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                var cartItem = new BuyerCart
                {
                    CartId = cart.CartId,
                    PostId = dto.PostId,
                    Quantity = dto.Quantity
                };
                _context.BuyerCarts.Add(cartItem);
            }

            _context.SaveChanges();
            return cart;
        }

        public bool RemoveFromCart(int cartItemId)
        {
            var item = _context.BuyerCarts.Find(cartItemId);
            if (item != null)
            {
                _context.BuyerCarts.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public decimal GetCartTotal(int cartId)
        {
            return _context.BuyerCarts
                .Where(bc => bc.CartId == cartId)
                .Sum(bc => bc.Quantity * bc.Post.Price);
        }

        public void ClearCart(int cartId)
        {
            var items = _context.BuyerCarts.Where(i => i.CartId == cartId);
            _context.BuyerCarts.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}