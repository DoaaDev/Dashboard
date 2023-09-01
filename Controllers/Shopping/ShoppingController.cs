using FirstProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
namespace Dashboard.Controllers.Shopping
{
	public class ShoppingController : Controller
    {
		private readonly ApplicationDbContext _context;

		public ShoppingController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
			var productDetails = _context.productDetails.ToList();
            var product = _context.product.ToList();
			ViewBag.product = product;
            return View(productDetails);
        }
        [Authorize]
        public IActionResult Checkout(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var productDetails = _context.productDetails.Where(p => p.Id == id).ToList();
            var productId = 0;
            foreach (var item in productDetails)
            {
                productId = item.ProductId;
            }
            var products = _context.product.Where(p => p.Id == productId).ToList();
            ViewBag.product = products;
            return View(productDetails);
        }

        public IActionResult ProductDeatils(int id)
        {
            var productDetails = _context.productDetails.Where(p => p.Id == id).ToList();
            var productId = 0;
            foreach (var item in productDetails)
            {
                productId = item.ProductId;
            }
            var products = _context.product.Where(p => p.Id == productId).ToList();
            ViewBag.product = products;
            return View(productDetails);
        }
    }
}
