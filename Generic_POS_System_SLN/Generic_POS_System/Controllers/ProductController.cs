using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Generic_POS_System.Controllers
{
    public class ProductController : Controller
    {
        //public iactionresult index()
        //{
        //    return view();
        //}

        private readonly IWebHostEnvironment _webHostEnvironment;

        [ViewData]
        public string Title { get; set; }

        private readonly ProductRepository _productRepository = null;
        public ProductController(ProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ViewResult> GetAllProducts()
        {
            Title = "All Products";
            var data = await _productRepository.GetAllProducts();

            return View(data);
        }

        [Route("prod-details/{id}")]
        public async Task<ViewResult> GetProductById(int id)
        {
            Title = "Details";
            var data = await _productRepository.GetProductById(id);

            return View(data);

        }

        public string SearchProduct(string prodName,  string prodType)
        {
            return $"Product with name = {prodName} & Type = {prodType}";
        }

        public ViewResult AddNewProduct(bool value = false)
        {
            Title = "Add Product";

            ViewBag.IsSuccess = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                if (productModel.photoUrl != null)
                {
                    string path = "products/";
                    productModel.coverPhotoUrl =  await UploadImage(path, productModel.photoUrl);
                }

                if (productModel.arcadeFiles != null)
                {
                    string path = "products/arcade/";

                    productModel.Arcade = new List<ArcadeModel>(); 

                    foreach (var file in productModel.arcadeFiles)
                    {
                        var arcade = new ArcadeModel() 
                        { 
                            Name = file.FileName,
                            URL = await UploadImage(path, file)
                        };
                        productModel.Arcade.Add(arcade);
                        
                    }

                }

                int id = await _productRepository.AddNewProduct(productModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewProduct), new { value = true });
                }

            }

            
            return View();
        }

        private async Task<string> UploadImage(string destination, IFormFile file)
        {
            string path = "products/";


            destination += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverPath = Path.Combine(_webHostEnvironment.WebRootPath, destination);

            await file.CopyToAsync(new FileStream(serverPath, FileMode.Create));

            return "/" + destination;
        }
    }
}
