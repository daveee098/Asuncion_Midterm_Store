using Microsoft.AspNetCore.Mvc;
using Asuncion_Midterm_Store.Data;
using Asuncion_Midterm_Store.Models;

namespace Asuncion_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = _context.CartItems.ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            var existingItem = _context.CartItems
                .FirstOrDefault(c => c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };

                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _context.CartItems.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            if (quantity <= 0)
            {
                _context.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cartItems = _context.CartItems.ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var cartItems = _context.CartItems.ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return View("OrderConfirmation");
        }
    }
}