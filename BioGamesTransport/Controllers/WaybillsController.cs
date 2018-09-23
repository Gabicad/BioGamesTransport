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
    public class WaybillsController : Controller
    {
        private readonly BiogamesTransContext _context;

        public WaybillsController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Waybills
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.Waybill.Include(w => w.ShipMode).Include(w => w.WaybillStatus);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: Waybills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybill
                .Include(w => w.ShipMode)
                .Include(w => w.WaybillStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybill == null)
            {
                return NotFound();
            }

            return View(waybill);
        }

        // GET: Waybills/Create
        public IActionResult Create()
        {
            ViewData["ShipModeId"] = new SelectList(_context.ShipModes, "Id", "Id");
            ViewData["WaybillStatusId"] = new SelectList(_context.WaybillStatuses, "Id", "Id");
            return View();
        }

        // POST: Waybills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShipModeId,WaybillStatusId,From,To,Cost,DepartureTime,ArrivalTime,Comment,Created,Modified")] Waybill waybill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waybill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShipModeId"] = new SelectList(_context.ShipModes, "Id", "Id", waybill.ShipModeId);
            ViewData["WaybillStatusId"] = new SelectList(_context.WaybillStatuses, "Id", "Id", waybill.WaybillStatusId);
            return View(waybill);
        }

        // GET: Waybills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybill.FindAsync(id);
            if (waybill == null)
            {
                return NotFound();
            }
            ViewData["ShipModeId"] = new SelectList(_context.ShipModes, "Id", "Id", waybill.ShipModeId);
            ViewData["WaybillStatusId"] = new SelectList(_context.WaybillStatuses, "Id", "Id", waybill.WaybillStatusId);
            return View(waybill);
        }

        // POST: Waybills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShipModeId,WaybillStatusId,From,To,Cost,DepartureTime,ArrivalTime,Comment,Created,Modified")] Waybill waybill)
        {
            if (id != waybill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waybill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaybillExists(waybill.Id))
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
            ViewData["ShipModeId"] = new SelectList(_context.ShipModes, "Id", "Id", waybill.ShipModeId);
            ViewData["WaybillStatusId"] = new SelectList(_context.WaybillStatuses, "Id", "Id", waybill.WaybillStatusId);
            return View(waybill);
        }

        // GET: Waybills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybill = await _context.Waybill
                .Include(w => w.ShipMode)
                .Include(w => w.WaybillStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybill == null)
            {
                return NotFound();
            }

            return View(waybill);
        }

        // POST: Waybills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waybill = await _context.Waybill.FindAsync(id);
            _context.Waybill.Remove(waybill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaybillExists(int id)
        {
            return _context.Waybill.Any(e => e.Id == id);
        }
    }
}
