using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Data
{
    public class ProductArcade
    {
        public int Id { get; set; }

        public int productId { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public Products Product { get; set; }

    }
}
