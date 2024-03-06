//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using E_Commerce.Models;
//using E_Commerce.ViewModels;

//namespace E_Commerce.Controllers
//{
//    [AllowAnonymous]
//    public class ShoppingCartController : Controller
//    {
//        Entities3 db = new Entities3();
//        //
//        // GET: /ShoppingCart/
//        public ActionResult Index()
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);
//            // Set up our ViewModel
//            var viewModel = new ShoppingCartViewModel
//            {
//                CartItems = cart.GetCartItems(),
//                CartTotal = cart.GetTotal()
//            };
//            // Return the view
//            return View(viewModel);
//        }
//        //
//        // GET: /Store/AddToCart/5
//        public ActionResult AddToCart(int id)
//        {
//            // Retrieve the Product from the database
//            var addedadvert = db.Adverts
//                .Single(p => p.AdvertId == id);


//            if (addedadvert.Quantity > 0 && addedadvert.TempQuantity > 0 && (addedadvert.TempQuantity <= addedadvert.Quantity))
//            {
//                // Add it to the shopping cart
//                //var cart = ShoppingCart.GetCart(this.HttpContext);
//                //cart.AddToCart(addedadvert);
//                //////////////////////////////////
//                addedadvert.TempQuantity -= 1;
//                db.Entry(addedadvert).State = EntityState.Modified;
//                db.SaveChanges();
//                //////////////////////////////////
//            }
//            else
//            {
//                return RedirectToAction("QuantityOut");
//            }

//            // Go back to the main store page for more shopping
//            return RedirectToAction("Index", "Home");
//        }
//        //
//        // AJAX: /ShoppingCart/RemoveFromCart/5
//        [HttpPost]
//        public ActionResult RemoveFromCart(int id)
//        {
//            // Remove the item from the cart
//            var cart = ShoppingCart.GetCart(this.HttpContext);
//            // Get the name of the product to display confirmation
//            string adverttName = db.Carts
//                .Single(item => item.RecordId == id);
//            // Remove from cart
//            int itemCount = cart.RemoveFromCart(id);
//            // Display the confirmation message
//            var results = new ShoppingCartRemoveViewModel
//            {
//                Message = Server.HtmlEncode(productName) +
//                          " has been removed from your shopping cart.",
//                CartTotal = cart.GetTotal(),
//                CartCount = cart.GetCount(),
//                ItemCount = itemCount,
//                DeleteId = id
//            };

//            // increase quantity
//            return Json(results);
//        }
//        [ChildActionOnly]
//        public ActionResult QuantityOut()
//        {
//            return View();
//        }

//        //
//        // GET: /ShoppingCart/CartSummary
//        [ChildActionOnly]
//        public ActionResult CartSummary()
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);
//            ViewData["CartCount"] = cart.GetCount();
//            return PartialView("CartSummary");
//        }
//    }
//}