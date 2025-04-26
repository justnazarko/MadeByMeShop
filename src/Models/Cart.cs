﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MadeByMe.src.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public ApplicationUser Buyer { get; set; }

        // Додати колекцію для зв'язку
        public ICollection<BuyerCart> BuyerCarts { get; set; } = new List<BuyerCart>();
    }
}