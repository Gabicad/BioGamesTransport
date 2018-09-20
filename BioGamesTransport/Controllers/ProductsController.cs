using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioGamesTransport.Data.SQL;

namespace BioGamesTransport.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BiogamesTransContext _context;

        public ProductsController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.Products.Include(p => p.Image).Include(p => p.Manufacturer).Include(p => p.Shop);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Image)
                .Include(p => p.Manufacturer)
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdOut,ShopId,ManufacturerId,ImageId,Quantity,Type,Reference,Width,Height,Depth,Weight,Ean13,Isbn,Upc,MinimalQuantity,Price,WolesalePrice,UnitPriceRatio,ShippingCost,Active,Created,Modified,Name,Description,DescriptionShort")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", products.ImageId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", products.ManufacturerId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", products.ShopId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", products.ImageId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", products.ManufacturerId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", products.ShopId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdOut,ShopId,ManufacturerId,ImageId,Quantity,Type,Reference,Width,Height,Depth,Weight,Ean13,Isbn,Upc,MinimalQuantity,Price,WolesalePrice,UnitPriceRatio,ShippingCost,Active,Created,Modified,Name,Description,DescriptionShort")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", products.ImageId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", products.ManufacturerId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", products.ShopId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Image)
                .Include(p => p.Manufacturer)
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
