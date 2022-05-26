using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Mdoels
{
    public class CartModel
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public string productName { get; set; }
        public int productId { get; set; }
        public int Count { get; set; }
        public DateTime GenDate { get; set; }
        public decimal unitPrice { get; set; }
        public decimal? productDiscount { get; set; }

        

        



    }
}
