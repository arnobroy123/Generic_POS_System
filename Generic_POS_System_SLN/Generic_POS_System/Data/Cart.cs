using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }

        [ForeignKey("Products")]
        public int productId { get; set; }

        public int Count { get; set; }

        public DateTime GenDate { get; set; }

        public Products Products { get; set; }
    }
}
