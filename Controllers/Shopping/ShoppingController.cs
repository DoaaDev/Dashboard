using FirstProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Drawing;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;

namespace Dashboard.Controllers.Shopping
{
	public class ShoppingController : Controller
    {
		private readonly ApplicationDbContext _context;

		public ShoppingController(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<IActionResult> SendMail(Invoice invoice)
        {
            var user = HttpContext.User.Identity.Name;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Doaa Store", "doaa.developer@gmail.com"));
            message.To.Add(MailboxAddress.Parse(user));
            message.Subject = "Invoice" + invoice.Id;
            message.Body = new TextPart("html")
            {
                Text = "<h2>شكرا على الشراء</h2>" +
                "<h4> رقم الفاتورة : " + invoice.Id.ToString() + "</h4>" +
                "<h4> التاريخ والوقت : " + DateTime.Now + "</h4>" +
                "<h4> المنتج : " + invoice.ProductName + "</h4>" +
                "<h4> المجموع :" + invoice.Total.ToString() + "</h4>"
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("doaa.developer@gmail.com", "runbwhaoycmmiolp");
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    client.Disconnect(true);
                }
            }
            //return RedirectToAction("InvoiceView", invoice);
            ViewBag.user = user;
            return View(invoice);
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
            var QTY = 1;
            var user = HttpContext.User.Identity.Name;
            var productDetails = _context.productDetails.SingleOrDefault(p => p.Id == id);
            var productId = productDetails.ProductId;
            var products = _context.product.SingleOrDefault(p => p.Id == productId);
            ViewBag.product = products;
            var cart = new Cart()
            {
                CostumerId = user,
                ProductId = productDetails.ProductId,
                ProductName = products.ProductName,
                Price = (productDetails.Price * QTY),
                Image = productDetails.Image,
                Color = productDetails.Color,
                Total = (productDetails.Price * QTY + ((productDetails.Price * QTY) * 15/100)),
                QTY = QTY,
                Tax = 0.15
            };
                _context.cart.Add(cart);
                _context.SaveChanges();

                int cartItemID = cart.Id; // cart id
                var cartItem = _context.cart.SingleOrDefault(p => p.Id == cartItemID);
               return View(cartItem);
        }

        public IActionResult Payment(int cartId)
        {
            var cartItem = _context.cart.SingleOrDefault(p => p.Id == cartId);
            return View(cartItem);
        }

        [HttpPost]
        public IActionResult Payment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.payment.Add(payment);
                _context.SaveChanges();
               return RedirectToAction("Invoice",payment.CartId);
            }
            return View();
        }
        public IActionResult InvoiceView(Invoice invoice)
        {
            var user = HttpContext.User.Identity.Name;
            ViewBag.user = user;
            return View(invoice);
        }
        public IActionResult Invoice(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var cartItem = _context.cart.SingleOrDefault(p => p.Id == id);
            var invoice = new Invoice()
            {
                CustomerId = user,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.ProductName,
                Price = (cartItem.Price * cartItem.QTY),
                QTY = cartItem.QTY,
                Tax = 0.15,
                Discount = 0.15,
                Total = (cartItem.Price * cartItem.QTY + ((cartItem.Price * cartItem.QTY) * 15 / 100))
            };
             ViewBag.user = user;
            _context.invoice.Add(invoice);
            _context.SaveChanges();
            return RedirectToAction("SendMail",invoice);
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
