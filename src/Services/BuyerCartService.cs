using Humanizer;
using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Services
{
    public class BuyerCartService // CRUD операції з кошиком
    {
        private readonly ApplicationDbContext _context;

        public BuyerCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddToCart(string userId, AddToCartDto addToCartDto)
        {
            var post = _context.Posts.Find(addToCartDto.PostId);
            if (post == null) throw new ArgumentException("Товар не знайдено.");

            var cart = _context.Carts
                .FirstOrDefault(c => c.BuyerId == userId);

            if (cart == null)
            {
                cart = new Cart { BuyerId = userId };
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

        public bool RemoveFromCart(string buyerId, int postId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.BuyerId == buyerId);
            if (cart == null) return false;

            var item = _context.BuyerCarts
                .FirstOrDefault(bc => bc.CartId == cart.CartId && bc.PostId == postId);
            if (item != null)
            {
                _context.BuyerCarts.Remove(item);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

     
    }


}