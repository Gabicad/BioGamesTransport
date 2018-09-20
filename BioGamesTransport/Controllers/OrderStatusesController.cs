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
    public class OrderStatusesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public OrderStatusesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: OrderStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatuses.ToListAsync());
        }

        // GET: OrderStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatuses = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatuses == null)
            {
                return NotFound();
            }

            return View(orderStatuses);
        }

        // GET: OrderStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AlertTime,Priority,Color")] OrderStatuses orderStatuses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatuses);
        }

        // GET: OrderStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatuses = await _context.OrderStatuses.FindAsync(id);
            if (orderStatuses == null)
            {
                return NotFound();
            }
            return View(orderStatuses);
        }

        // POST: OrderStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AlertTime,Priority,Color")] OrderStatuses orderStatuses)
        {
            if (id != orderStatuses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatuses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusesExists(orderStatuses.Id))
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
            return View(orderStatuses);
        }

        // GET: OrderStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatuses = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatuses == null)
            {
                return NotFound();
            }

            return View(orderStatuses);
        }

        // POST: OrderStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatuses = await _context.OrderStatuses.FindAsync(id);
            _context.OrderStatuses.Remove(orderStatuses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusesExists(int id)
        {
            return _context.OrderStatuses.Any(e => e.Id == id);
        }
    }
}
