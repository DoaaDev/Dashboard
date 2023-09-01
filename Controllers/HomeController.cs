using Dashboard.Models;
using FirstProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Delete(int id)
		{
			var Product = _context.product.SingleOrDefault(p => p.Id == id);
			if (Product != null)
			{
				_context.product.Remove(Product);
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}
		public IActionResult Update(Product product)
		{
			//Product p = _context.product.SingleOrDefault(x => x.Id == product.Id) ?? new Product();

			//p.ProductName = product.ProductName;
			_context.product.Update(product);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Edit(int id)
		{
			var product = _context.product.SingleOrDefault(x => x.Id == id);

			return View(product);
		}
		[HttpPost]
		public IActionResult AddProduct(Product product)
		{
			_context.product.Add(product);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult AddProductDetails(ProductDetails productDetails)
		{
			_context.productDetails.Add(productDetails);
			_context.SaveChanges();
			return RedirectToAction("ProductDetails");
		}
		public IActionResult UpdateProductDetails(ProductDetails productDetails)
		{
			_context.productDetails.Update(productDetails);
			_context.SaveChanges();
			return RedirectToAction("ProductDetails");
		}
		public IActionResult EditProductDetails(int id)
		{
			var productDetails = _context.productDetails.SingleOrDefault(x => x.Id == id);

			return View(productDetails);
		}
		public IActionResult DeleteProductDetails(int id)
		{
			var ProductDetails = _context.productDetails.SingleOrDefault(p => p.Id == id);
			if (ProductDetails != null)
			{
				_context.productDetails.Remove(ProductDetails);
				_context.SaveChanges();
			}
			return RedirectToAction("ProductDetails");
		}
		[HttpPost]
		public IActionResult ProductDetails(int id)
		{
			var productDetails = _context.productDetails.Where(p=> p.ProductId == id).ToList();
			var products = _context.product.ToList();
			ViewBag.ProductDetails = productDetails;
			return View(products);
		}
		public IActionResult PaymentAccept()
		{
            var paymentAcceptList = _context.paymentAccept.ToList();
            ViewBag.paymentAcceptList = paymentAcceptList;
            return View();
		}
		[HttpPost]
		public IActionResult PaymentAccept(PaymentAccept paymentAccept)
		{
            var paymentAcceptList = _context.paymentAccept.ToList();
            ViewBag.paymentAcceptList = paymentAcceptList;

            if (ModelState.IsValid)
			{
				_context.paymentAccept.Add(paymentAccept);
				_context.SaveChanges();
				return RedirectToAction("PaymentAccept");
			}
			return View();
		}
		public IActionResult ProductDetails()
        {
            var products = _context.product.ToList();
			var productDetails = _context.productDetails.ToList();
			ViewBag.ProductDetails = productDetails;
			//ViewBag.Name = Request.Cookies["Name"];
			ViewBag.Name = HttpContext.Session.GetString("Name");
            return View(products);
        }
		[Authorize]
        public IActionResult Index()
        {
			var products = _context.product.ToList();
			var Name = HttpContext.User.Identity.Name;
			//CookieOptions options = new CookieOptions();
			//options.Expires = DateTime.Now.AddMinutes(10);
			//Response.Cookies.Append("Name", Name, options);

			HttpContext.Session.SetString("Name", Name);
			ViewBag.Name = Name;
			return View(products);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}