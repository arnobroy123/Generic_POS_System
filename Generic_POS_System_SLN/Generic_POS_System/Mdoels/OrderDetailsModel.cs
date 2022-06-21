using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Mdoels
{
    public class OrderDetailsModel
    {
        public int orderDetailsId { get; set; }

        public int orderId { get; set; }

        public int productId { get; set; }

        public int Quantity { get; set; }

        public string prodName { get; set; }

        public decimal unitPrice { get; set; }

        public decimal prodDiscount { get; set; }

        public decimal Discount { get; set; }

        public decimal? Total { get; set; }

        public decimal DiscountedTotal { get; set; }
    }
}
