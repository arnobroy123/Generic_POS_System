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

            await _context.Product.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.productId;
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            var products = new List<ProductModel>();
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
                        coverPhotoUrl = product.coverPhotoUrl

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

    }
}
