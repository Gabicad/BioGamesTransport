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
    public class ExpendituresController : Controller
    {
        private readonly BiogamesTransContext _context;

        public ExpendituresController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Expenditures
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.Expenditures.Include(e => e.Order);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: Expenditures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures
                .Include(e => e.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditures == null)
            {
                return NotFound();
            }

            return View(expenditures);
        }

        // GET: Expenditures/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: Expenditures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,Name,Price,Comment")] Expenditures expenditures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenditures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", expenditures.OrderId);
            return View(expenditures);
        }

        // GET: Expenditures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures.FindAsync(id);
            if (expenditures == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", expenditures.OrderId);
            return View(expenditures);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,Name,Price,Comment")] Expenditures expenditures)
        {
            if (id != expenditures.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenditures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpendituresExists(expenditures.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", expenditures.OrderId);
            return View(expenditures);
        }

        // GET: Expenditures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditures = await _context.Expenditures
                .Include(e => e.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditures == null)
            {
                return NotFound();
            }

            return View(expenditures);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenditures = await _context.Expenditures.FindAsync(id);
            _context.Expenditures.Remove(expenditures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpendituresExists(int id)
        {
            return _context.Expenditures.Any(e => e.Id == id);
        }
    }
}
