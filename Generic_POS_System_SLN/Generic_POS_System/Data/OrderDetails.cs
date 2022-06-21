using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class OrderDetails
    {
        [Key]
        public int ordDetailsId { get; set; }

        [ForeignKey("Orders")]
        public int orderId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal UnitPrice { get; set; }

        /*[Column(TypeName = "decimal(7,2)")]
        public decimal Total { get; set; }*/

        /*[Column(TypeName = "decimal(7,2)")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal? DiscountedTotal { get; set; }*/

        [ForeignKey("Products")]
        public int productId { get; set; }

        public Products Products { get; set; }

        public Orders Orders { get; set; }


    }
}