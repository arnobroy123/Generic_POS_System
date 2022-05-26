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

        [ForeignKey("AppUser")]
        public string? userId { get; set; }

        public DateTime genDate { get; set; }

        public AppUser AppUser { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }


    }
}
