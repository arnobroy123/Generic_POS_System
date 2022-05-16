using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class Products
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int productId { get; set; }

            [Column(TypeName = "nvarchar(100)")]
            public string productName { get; set; }

            [Column(TypeName = "nvarchar(100)")]
            public string productType { get; set; }

            public string coverPhotoUrl { get; set; }

            public int totalProducts { get; set; }
            
            [Column(TypeName = "decimal(7,2)")]
            public decimal unitPrice { get; set; }
            
            [Column(TypeName = "decimal(7,2)")]
            public decimal? productDiscount { get; set; }

            public ICollection<ProductArcade> productArcade { get; set; }

    }
}
