using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Controllers
{
    [Authorize(Roles = "Admin, Salesman")]
    public class SalesmanController : Controller
    {
        private PosContext _context;
        private IAccountRepository _accountRepository;
        private UserManager<AppUser> _userManager;
        private ProductRepository _productRepository;
        private IWebHostEnvironment _webHostEnv;

        public SalesmanController(PosContext context, IAccountRepository accountRepository, UserManager<AppUser> userManager, ProductRepository product, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _productRepository = product;
            _webHostEnv = webHostEnvironment;
            

        }

        [ViewData]
        public string Title { get; set; }

        public IActionResult SalesmanIndex(string sortOrder)
        {
            Title = "SalesMan";
            ViewBag.Products = _context.Product;
            ViewBag.pIdSort = String.IsNullOrEmpty(sortOrder) ? "pId_desc" : "";
            ViewBag.NameSort = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.TotalProdSort = sortOrder == "totalProd" ? "totalProd_desc" : "totalProd";
            ViewBag.uniPriceSort = sortOrder == "unitPrice" ? "unitPrice_desc" : "unitPrice";




            switch (sortOrder)
            {
                
                case "pId_desc":
                    ViewBag.Products = _context.Product.OrderByDescending(p => p.productId);
                    break;
                case "name_desc":
                    ViewBag.Products = _context.Product.OrderByDescending(p => p.productName);
                    break;
                case "name":
                    ViewBag.Products = _context.Product.OrderBy(p => p.productName);
                    break;
                case "totalProd":
                    ViewBag.Products = _context.Product.OrderBy(p => p.totalProducts);
                    break;
                case "totalProd_desc":
                    ViewBag.Products = _context.Product.OrderByDescending(p => p.totalProducts);
                    break;
                case "unitPrice":
                    ViewBag.Products = _context.Product.OrderBy(p => p.unitPrice);
                    break;
                case "unitPrice_desc":
                    ViewBag.Products = _context.Product.OrderByDescending(p => p.unitPrice);
                    break;

                default:
                    ViewBag.Products = _context.Product.OrderBy(p => p.productId);
                    break;
            }
            

            



            return View();
        }

        
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            Title = "Edit Product";

            if (id == 0)
                return BadRequest();

            //ViewBag.ProductById = await _productRepository.GetProductById(id);
            var editProd = await _productRepository.GetProductById(id);

            var editModel = new EditProductModel()
            {
                productId = editProd.productId,
                productName = editProd.productName,
                productDiscount = editProd.productDiscount,
                productType = editProd.productType,
                totalProducts = editProd.totalProducts,
                unitPrice = editProd.unitPrice
            };


            return View(editModel);
        }



        
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductModel editModel)
        {
            Title = "Edit Product";
            if (ModelState.IsValid)
            {
                var result = await _productRepository.EditProduct(editModel);
                if (result > 0)
                {
                    return RedirectToAction("SalesmanIndex");
                }

            }
                

            return View(editModel);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (id != 0)
            {
                Delete(id);
                return RedirectToAction("SalesmanIndex");
            }
            
            
            return RedirectToAction("AccessDenied", "Account");


        }

        private void Delete(int id)
        {
            string path = _webHostEnv.WebRootPath;
            
            var product = _context.Product.Where(p => p.productId == id).Single();
            string coverUrl = path + product.coverPhotoUrl;
            var productArcade = _context.ProductArcade.Where(pA => pA.productId == id);

            if (product != null && productArcade != null)
            {
                string coverImgDel = Path.GetFullPath(coverUrl);
                FileInfo fi = new FileInfo(coverImgDel);
                if (fi != null)
                {
                    System.IO.File.Delete(coverImgDel);
                    fi.Delete();
                }
                foreach (var file in productArcade)
                {
                    string arcadeImgPath = path + file.URL;
                    string imgDel = Path.GetFullPath(arcadeImgPath);
                    FileInfo fiArcade = new FileInfo(imgDel);
                    if (fiArcade != null)
                    {
                        System.IO.File.Delete(imgDel);
                        fiArcade.Delete();
                    }
                }
                _context.ProductArcade.RemoveRange(productArcade);
                _context.Product.Remove(product);
                _context.SaveChanges();


            }
        }



    }
}


