using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Repository
{
    public class ProductRepository
    {
        private readonly PosContext _context = null;

        public ProductRepository(PosContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewProduct(ProductModel model)
        {
            var newProduct = new Products()
            {
                productName = model.productName,
                totalProducts = model.totalProducts.HasValue ? model.totalProducts.Value : 0,
                unitPrice = model.unitPrice.HasValue ? model.unitPrice.Value : 0,
                productDiscount = model.productDiscount,
                productType = model.productType,
                coverPhotoUrl = model.coverPhotoUrl
                
                

            };

            newProduct.productArcade = new List<ProductArcade>();
            foreach (var file in model.Arcade)
            {
                newProduct.productArcade.Add(new ProductArcade()
                {
                    Name = file.Name,
                    URL = file.URL

                });
            }

            newProduct.Category = new Category()
            {
                Name = model.CategoryName
            };
            

            await _context.Product.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.productId;
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            var products = new List<ProductModel>();
            var cat = new Category();
            var allProducts = await _context.Product.ToListAsync();
            if (allProducts?.Any() == true)
            {
                foreach (var product in allProducts)
                {
                    products.Add(new ProductModel()
                    {
                        productName = product.productName,
                        totalProducts = product.totalProducts,
                        productId = product.productId,
                        unitPrice = product.unitPrice,
                        productDiscount = (decimal)product.productDiscount,
                        productType = product.productType,
                        coverPhotoUrl = product.coverPhotoUrl,
                        catId = product.catId

                    });
                }

            }
            return products;
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            /*var product = await _context.Product.FindAsync(id);

            if (product != null)
            {
                var productDetails = new ProductModel()
                {
                    productName = product.productName,
                    totalProducts = product.totalProducts,
                    unitPrice = product.unitPrice,
                    productDiscount = (decimal)product.productDiscount,
                    productType = product.productType,
                    productId = product.productId,
                    coverPhotoUrl = product.coverPhotoUrl,
                    Arcade = product.productArcade.Select(a => new ArcadeModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        URL = a.URL
                    }).ToList()

                };

                return productDetails;
            }

            return null;*/

            return await _context.Product.Where(x => x.productId == id)
                .Select(product => new ProductModel()
                {
                    productName = product.productName,
                    totalProducts = product.totalProducts,
                    unitPrice = product.unitPrice,
                    productDiscount = (decimal)product.productDiscount,
                    productType = product.productType,
                    productId = product.productId,
                    coverPhotoUrl = product.coverPhotoUrl,
                    Arcade = product.productArcade.Select(a => new ArcadeModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        URL = a.URL
                    }).ToList()


                }).FirstOrDefaultAsync();
            
        }

        /*public List<ProductModel> SearchProduct(string productName)
        {
            var a = "Arnob";
            return a;
        }*/

        public async Task<List<ProductByCategoryModel>> GetProductByCategory()
        {
            var prodTable = await _context.Product.ToListAsync();
            var catTable = await _context.Category.ToListAsync();
            
            var joinProdCat = from p in prodTable
                              join c in catTable on p.catId equals c.catId
                              select new ProductByCategoryModel() 
                              {
                                catId = c.catId,
                                categroyName = c.Name,
                                coverUrl = p.coverPhotoUrl
                              };
            
            return joinProdCat.ToList();
        }

        public async Task<List<ProductModel>> GetProductByDiscount()
        {
            var discountProd = await _context.Product.Where(x => x.productDiscount > 0).ToListAsync();
            var discountedProduct = new List<ProductModel>();


            if(discountProd != null)
            {
                discountedProduct = discountProd.Select(product => new ProductModel()
                                        {
                                            productId = product.productId,
                                            productName = product.productName,
                                            unitPrice = product.unitPrice,
                                            productDiscount = (decimal)product.productDiscount,
                                            coverPhotoUrl = product.coverPhotoUrl
                                            
                                        }).ToList();
            }


            return discountedProduct;
        }

        
        public async Task<List<ProductModel>> GetProductByCategoryId(int id)
        {
            var products = new List<ProductModel>();
            var allProducts = await _context.Product.Where(x => x.catId == id).ToListAsync();
            if (allProducts?.Any() == true)
            {
                foreach (var product in allProducts)
                {
                    products.Add(new ProductModel()
                    {
                        productName = product.productName,
                        totalProducts = product.totalProducts,
                        productId = product.productId,
                        unitPrice = product.unitPrice,
                        productDiscount = (decimal)product.productDiscount,
                        productType = product.productType,
                        coverPhotoUrl = product.coverPhotoUrl

                    });
                }

            }
            return products;
        }













        public async Task<List<CategoryModel>> GetCategory()
        {
            var categories = new List<CategoryModel>();
            var allCategories = await _context.Category.ToListAsync();
            if (allCategories?.Any() == true)
            {
                foreach (var category in allCategories)
                {
                    categories.Add(new CategoryModel()
                    {
                        Id = category.catId,
                        Name = category.Name

                    });

                }
            }
            return categories;
        }


    }
}
