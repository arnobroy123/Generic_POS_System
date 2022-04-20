using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Generic_POS_System.Controllers
{
    public class ProductController : Controller
    {
        //public iactionresult index()
        //{
        //    return view();
        //}

        public string GetAllProducts()
        {
            return "All products";
        }

        public string GetProduct()
        {
            return "Book";

        }

        public string SearchProduct(string prodName,  string prodType)
        {
            return $"Product with name = {prodName} & Type = {prodType}";
        }
    }
}
