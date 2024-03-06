using E_Commerce.Models;
using System.Collections.Generic;

namespace E_Commerce.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}