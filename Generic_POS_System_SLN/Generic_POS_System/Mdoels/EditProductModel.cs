using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Mdoels
{
    public class EditProductModel
    {
        public int productId { get; set; }
        [Display(Name = "Product Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be of minimum 3 characters")]
        [Required(ErrorMessage = "Please enter product name")]
        public string productName { get; set; }


        [Display(Name = "Product Type")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Type should be of minimum 5 characters")]
        [Required(ErrorMessage = "Please enter product type")]
        public string productType { get; set; }


        [Display(Name = "Total Products")]
        [Required(ErrorMessage = "Please enter Total Products")]
        public int? totalProducts { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please enter Unit Price")]
        public decimal? unitPrice { get; set; }

        [Display(Name = "Product Discount")]
        public decimal productDiscount { get; set; }
    }
}
