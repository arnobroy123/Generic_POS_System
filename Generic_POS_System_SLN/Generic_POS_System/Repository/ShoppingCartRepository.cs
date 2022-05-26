using Generic_POS_System.Data;
using Generic_POS_System.Helper;
using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Generic_POS_System.Repository
{
    public class ShoppingCartRepository
    {
        private readonly UserHelper _userHelper;
        private PosContext _context;
        private IHttpContextAccessor _httpContext;

        public ShoppingCartRepository(UserHelper userHelper, PosContext context, IHttpContextAccessor httpContext)
        {
            _userHelper = userHelper;
            _context = context;
            _httpContext = httpContext;

        }
        public ShoppingCartRepository()
        {

        }

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";
        public string GetCart()
        {
            ShoppingCartId = GetCartId();

            return ShoppingCartId;
            
        }
        
        
        //Add to cart
        public async void AddToCart(Products products, int quantity)
        {

            var cartItem = _context.Cart.SingleOrDefault(
                c => c.CartId.Equals(ShoppingCartId)
                && c.productId.Equals(products.productId));

            /*var cart = _context.Cart.Where(c => c.CartId == ShoppingCartId).ToList();
            var product = _context.Product.ToList();

            var cartItem = (from c in cart
                        join p in product
                        on c.productId equals p.productId
                        select c).SingleOrDefault();*/




            if (cartItem == null)
            {

                if (quantity != 0)
                {
                    cartItem = new Cart
                    {
                        productId = products.productId,
                        CartId = ShoppingCartId,
                        Count = quantity,
                        GenDate = DateTime.UtcNow.AddHours(6)
                    };
                }
                else
                {
                    cartItem = new Cart
                    {
                        productId = products.productId,
                        CartId = ShoppingCartId,
                        Count = 1,
                        GenDate = DateTime.UtcNow.AddHours(6)
                    };
                }

                    
                    
                    

                _context.Cart.Add(cartItem);
            }
            else
            {
                if (quantity == 0)
                {
                    cartItem.Count++;
                }
                else
                {
                    cartItem.Count += quantity;
                }

            }
                

            _context.SaveChanges();
        }

        //Remove from cart

        public int RemoveFromCart(int id)
        {
            var cartItem = _context.Cart.Single(
                c => c.CartId.Equals(ShoppingCartId)
                && c.RecordId.Equals(id));

            //var cartItem = _context.Cart.Where(c => c.CartId == ShoppingCartId && c.RecordId == id).Single();



            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Cart.Remove(cartItem);
                }

                _context.SaveChanges();
            }

            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _context.Cart.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Cart.Remove(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }

        public List<CartModel> GetCartItems()
        {
            var prodTable = _context.Product.ToList();
            var cartTable = _context.Cart.Where(cart => cart.CartId == ShoppingCartId).ToList();
            var cart = (from p in prodTable
                       join c in cartTable on p.productId equals c.productId
                       select new CartModel() 
                       { 
                            RecordId = c.RecordId,
                            CartId = c.CartId,
                            productName = p.productName,
                            productId = p.productId,
                            Count = c.Count,
                            GenDate = c.GenDate,
                            unitPrice = p.unitPrice,
                            productDiscount = p.productDiscount
                            
                       }).ToList();

            
            return cart;
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.Cart
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _context.Cart
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Products.unitPrice).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Orders order)
        {
            decimal orderTotal = 0;

            var cartItems = _context.Cart.Where(cart => cart.CartId == ShoppingCartId).ToList(); ;
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    productId = item.productId,
                    orderId = order.orderId,
                    UnitPrice = item.Products.unitPrice,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Products.unitPrice);

                _context.OrderDetails.Add(orderDetail);
            }
            /*// Set the order's total to the orderTotal count
            order.Total = orderTotal;*/

            // Save the order
            _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.orderId;
            

        }
        public string GetCartId()
        {
            
            var user = _httpContext.HttpContext.User.Identity.Name;
            //var user = _userHelper.GetUserId();
            //if (context.Session[CartSessionKey] == null)
            var contain = _httpContext.HttpContext.Session.Keys.Contains(CartSessionKey);

            if (!contain)
            {
                if (!string.IsNullOrEmpty(user))
                {
                    _httpContext.HttpContext.Session.SetString(CartSessionKey, user);

                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    var tempoCartId = tempCartId.ToString();
                    _httpContext.HttpContext.Session.SetString(CartSessionKey, tempoCartId);
                    
                }
            }
            //var key = context.Session.Get(CartSessionKey);
            var key = _httpContext.HttpContext.Session.GetString(CartSessionKey).ToString();

            return key;
        }


        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Cart.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
        }



        
        



        /*public async Task<int> CheckOut()
        {
            var userId = _userHelper.GetUserId();

            *//*var product = await _context.Product.FindAsync(id);*/

        /*var order = await _context.Orders.ToListAsync();*//*


            var order = new Orders()
            {
                userId = userId,
                genDate = DateTime.UtcNow.AddHours(6)
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

        return order.orderId ;

    }


    public async void FirstOrder()
    {
        var userId = _userHelper.GetUserId();

        var order = new Orders()
        {
            userId = userId,
            genDate = DateTime.UtcNow.AddHours(6)
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }




    public async Task<List<OrderDetails>> AddToCart(int id)
    {
        var product = await _context.Product.FindAsync(id);

        var order = await _context.Orders.FindAsync();

        if (order != null)
        {
            var orderDetailsModel = new OrderDetailsModel
            {
                orderId = order.orderId,
                productId = product.productId,
                unitPrice = product.unitPrice,
                prodDiscount = (decimal)product.productDiscount,
                Discount = 0,
                Total = 0,
                DiscountedTotal = 0
            };

        }
        else
        {
            FirstOrder();
            await AddToCart(id);
        }






        return null;
    }*/

    }





}
