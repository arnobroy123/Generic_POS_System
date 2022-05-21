using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Mdoels
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List <ProductModel> Products { get; set; }
    }
}
