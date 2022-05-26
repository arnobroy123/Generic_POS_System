using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class Category
    {
        [Key]
        public int catId { get; set; }

        public string Name { get; set; }

        
        public ICollection <Products> Product { get; set; }
    }
}
