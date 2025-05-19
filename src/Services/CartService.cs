using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using MadeByMe.src.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MadeByMe.src.Services
{
    public class CartService //читання і підготовка даних для відображення кошика:
                             //список товарів, кількість, ціна за одиницю, загальна сума
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart GetUserCartEntity(string buyerId)
        {
            return _context.Carts
                .Include(c => c.BuyerCarts)
                .ThenInclude(bc => bc.Post)
                .ThenInclude(p => p.Seller)
                .FirstOrDefault(c => c.BuyerId == buyerId);
        }

        public CartViewModel GetUserCart(string buyerId)
        {
            var cart = _context.Carts
                .Include(c => c.BuyerCarts)
                .ThenInclude(bc => bc.Post)
                .ThenInclude(p => p.Seller)
                .FirstOrDefault(c => c.BuyerId == buyerId);

            if (cart == null || !cart.BuyerCarts.Any())
            {
                return new CartViewModel
                {
                    Items = new List<CartItem>(),
                    TotalPrice = 0
                };
            }

            var items = cart.BuyerCarts.Select(bc => new CartItem
            {
                Id = bc.CartItemId,
                ProductId = bc.PostId,
                Quantity = bc.Quantity,
                Product = new Product
                {
                    Id = bc.Post.Id,
                    Name = bc.Post.Title,
                    Price = bc.Post.Price,
                    ImageUrl = bc.Post.Photos.FirstOrDefault()?.FilePath ?? "/images/default.jpg",
                    Seller = bc.Post.Seller
                }
            }).ToList();

            var total = items.Sum(i => i.Product.Price * i.Quantity);

            return new CartViewModel
            {
                Items = items,
                TotalPrice = total
            };
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