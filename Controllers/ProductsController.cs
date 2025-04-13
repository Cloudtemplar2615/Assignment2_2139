using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment_1_G_92_2139.Data;
using Assignment_1_G_92_2139.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
namespace Assignment_1_G_92_2139.Controllers;


    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context,ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // GET: Products
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            try
            {
                var products = _context.Products.Include(p => p.Category).AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                    products = products.Where(p => p.Name.Contains(searchString));

                if (categoryId.HasValue && categoryId.Value > 0)
                    products = products.Where(p => p.CategoryId == categoryId);

                ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");

                return View(await products.ToListAsync());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        
    
        // GET: Products/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,CategoryId,Price,QuantityInStock,LowStockThreshold")] Product product)
        {
            Console.WriteLine($"CategoryId received: {product.CategoryId}");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"ModelState Error - Key: {entry.Key}, Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                Console.WriteLine("Product successfully saved.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Product NOT saved, reloading form.");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return NotFound();

                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }


        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,CategoryId,Price,QuantityInStock,LowStockThreshold")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(m => m.ProductId == id);

                if (product == null) return NotFound();

                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var products = string.IsNullOrEmpty(query)
                ? await _context.Products.ToListAsync()
                : await _context.Products
                    .Where(p => p.Name.ToLower().Contains(query.ToLower()))
                    .ToListAsync();

            return PartialView("_ProductListPartial", products);
        }

    }
