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
    public class WaybillStatusesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public WaybillStatusesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: WaybillStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.WaybillStatuses.ToListAsync());
        }

        // GET: WaybillStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillStatuses = await _context.WaybillStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybillStatuses == null)
            {
                return NotFound();
            }

            return View(waybillStatuses);
        }

        // GET: WaybillStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WaybillStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color")] WaybillStatuses waybillStatuses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waybillStatuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waybillStatuses);
        }

        // GET: WaybillStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillStatuses = await _context.WaybillStatuses.FindAsync(id);
            if (waybillStatuses == null)
            {
                return NotFound();
            }
            return View(waybillStatuses);
        }

        // POST: WaybillStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color")] WaybillStatuses waybillStatuses)
        {
            if (id != waybillStatuses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waybillStatuses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaybillStatusesExists(waybillStatuses.Id))
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
            return View(waybillStatuses);
        }

        // GET: WaybillStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillStatuses = await _context.WaybillStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybillStatuses == null)
            {
                return NotFound();
            }

            return View(waybillStatuses);
        }

        // POST: WaybillStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waybillStatuses = await _context.WaybillStatuses.FindAsync(id);
            _context.WaybillStatuses.Remove(waybillStatuses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaybillStatusesExists(int id)
        {
            return _context.WaybillStatuses.Any(e => e.Id == id);
        }
    }
}
