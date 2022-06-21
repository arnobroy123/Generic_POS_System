using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class Orders
    {
        [Key]
        public int orderId { get; set; }


        public string userId { get; set; }

        public DateTime genDate { get; set; }

        [Column(TypeName = "decimal(7,2)")]
        public decimal? OrderTotal { get; set; }

        public IList<OrderDetails> OrderDetails { get; set; }

    }



}