using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_1_G_92_2139.Data;
using Assignment_1_G_92_2139.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace Assignment_1_G_92_2139.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ApplicationDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Orders/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.ProductList = _context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                }).ToList();

            // auto
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

                if (user != null)
                {
                    ViewBag.GuestName = user.FullName;
                    ViewBag.GuestEmail = user.Email;
                }
            }

            return View();
        }






        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string guestName, string guestEmail, int[] productIds, int[] quantities)
        {
            try
            {
                if (productIds == null || quantities == null || productIds.Length == 0 || quantities.Length == 0 || productIds.Any(id => id == 0))
                    return BadRequest("Please select at least one product.");


                var order = new Order
                {
                    GuestName = guestName,
                    GuestEmail = guestEmail
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                for (int i = 0; i < productIds.Length; i++)
                {
                    var product = await _context.Products.FindAsync(productIds[i]);
                    if (product != null)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = order.OrderId,
                            ProductId = productIds[i],
                            Quantity = quantities[i],
                            Price = product.Price
                        };
                        _context.OrderItems.Add(orderItem);
                    }
                }

                await _context.SaveChangesAsync();

               
                return Ok(new { message = "Order placed successfully", orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message); // helps you debug if anything goes wrong
            }
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Confirmation(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }
            catch (Exception ex)
            {
                
                return RedirectToAction("Error500", "Error");
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ToListAsync();

                return View(orders);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

    }
}
