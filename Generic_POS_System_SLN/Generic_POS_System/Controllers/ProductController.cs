using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Generic_POS_System.Controllers
{
    public class ProductController : Controller
    {
        //public iactionresult index()
        //{
        //    return view();
        //}

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PosContext _context;

        [ViewData]
        public string Title { get; set; }

        private readonly ProductRepository _productRepository = null;
        public ProductController(ProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment, PosContext context)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<ViewResult> GetAllProducts(string Search)
        {
            Title = "All Products";
            ViewData["Search"] = Search;
            
            var data = await _productRepository.GetAllProducts();
            if (!String.IsNullOrEmpty(Search))
            {
                var product = await _productRepository.SearchProducts(Search);
                
                
                return View(product);
                
                

            }
            return View(data);
        }

        

        [Route("prod-details/{id}")]
        public async Task<ViewResult> GetProductById(int id)
        {
            Title = "Details";
            var data = await _productRepository.GetProductById(id);


            return View(data);

        }



        [Route("prod-categories/{id}")]
        public async Task<ViewResult> GetProductByCategoryId(int id)
        {
            Title = "Details";

            ViewBag.ProdByCatId = await _productRepository.GetProductByCategoryId(id);
            ViewBag.CatName = _context.Category.Where(c => c.catId == id).Select(c => c.Name).Single();

            return View();

        }

        public string SearchProduct(string prodName, string prodType)
        {
            return $"Product with name = {prodName} & Type = {prodType}";
        }

        [Authorize(Roles = "Admin, Salesman")]
        public ViewResult AddNewProduct(bool value = false)
        {
            Title = "Add Product";

            /*List<SelectListItem> category = _context.Category.Select(c =>
                new SelectListItem()
                {
                    Value = c.Name,
                    Text = c.Name
                }).ToList();
            ProductModel product = new ProductModel()
            {
                Category = category
            };*/

            ViewBag.IsSuccess = value;
            return View();
        }

        /*[ModelStateValidate]*/
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
            //string path = "products/";


            destination += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverPath = Path.Combine(_webHostEnvironment.WebRootPath, destination);

            await file.CopyToAsync(new FileStream(serverPath, FileMode.Create));

            return "/" + destination;
            

        }

        


    }
}
