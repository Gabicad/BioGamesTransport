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
    public class ShipStatusesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public ShipStatusesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: ShipStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShipStatuses.ToListAsync());
        }

        // GET: ShipStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipStatuses = await _context.ShipStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipStatuses == null)
            {
                return NotFound();
            }

            return View(shipStatuses);
        }

        // GET: ShipStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AlertTime,Color,Priority")] ShipStatuses shipStatuses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipStatuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipStatuses);
        }

        // GET: ShipStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipStatuses = await _context.ShipStatuses.FindAsync(id);
            if (shipStatuses == null)
            {
                return NotFound();
            }
            return View(shipStatuses);
        }

        // POST: ShipStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AlertTime,Color,Priority")] ShipStatuses shipStatuses)
        {
            if (id != shipStatuses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipStatuses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipStatusesExists(shipStatuses.Id))
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
            return View(shipStatuses);
        }

        // GET: ShipStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipStatuses = await _context.ShipStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipStatuses == null)
            {
                return NotFound();
            }

            return View(shipStatuses);
        }

        // POST: ShipStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipStatuses = await _context.ShipStatuses.FindAsync(id);
            _context.ShipStatuses.Remove(shipStatuses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipStatusesExists(int id)
        {
            return _context.ShipStatuses.Any(e => e.Id == id);
        }
    }
}
