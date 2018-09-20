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
    public class ShipModesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public ShipModesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: ShipModes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShipModes.ToListAsync());
        }

        // GET: ShipModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipModes = await _context.ShipModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipModes == null)
            {
                return NotFound();
            }

            return View(shipModes);
        }

        // GET: ShipModes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipModes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ShipModes shipModes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipModes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipModes);
        }

        // GET: ShipModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipModes = await _context.ShipModes.FindAsync(id);
            if (shipModes == null)
            {
                return NotFound();
            }
            return View(shipModes);
        }

        // POST: ShipModes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ShipModes shipModes)
        {
            if (id != shipModes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipModes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipModesExists(shipModes.Id))
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
            return View(shipModes);
        }

        // GET: ShipModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipModes = await _context.ShipModes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipModes == null)
            {
                return NotFound();
            }

            return View(shipModes);
        }

        // POST: ShipModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipModes = await _context.ShipModes.FindAsync(id);
            _context.ShipModes.Remove(shipModes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipModesExists(int id)
        {
            return _context.ShipModes.Any(e => e.Id == id);
        }
    }
}
