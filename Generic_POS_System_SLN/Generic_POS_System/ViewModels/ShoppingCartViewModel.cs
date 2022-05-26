using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartModel> CartItems { get; set; }
        public decimal CartTotal { get; set; }

        public string ProductName { get; set; }
    }
}
