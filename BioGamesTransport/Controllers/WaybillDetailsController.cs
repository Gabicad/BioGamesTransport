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
    public class WaybillDetailsController : Controller
    {
        private readonly BiogamesTransContext _context;

        public WaybillDetailsController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: WaybillDetails
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.WaybillDetails.Include(w => w.OrderDetails).Include(w => w.Waybill);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: WaybillDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillDetails = await _context.WaybillDetails
                .Include(w => w.OrderDetails)
                .Include(w => w.Waybill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybillDetails == null)
            {
                return NotFound();
            }

            return View(waybillDetails);
        }

        // GET: WaybillDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderDetailsId"] = new SelectList(_context.OrderDetails, "Id", "ProductName");
            ViewData["WaybillId"] = new SelectList(_context.Waybill, "Id", "Id");
            return View();
        }

        // POST: WaybillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WaybillId,OrderDetailsId,Weighting")] WaybillDetails waybillDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waybillDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderDetailsId"] = new SelectList(_context.OrderDetails, "Id", "ProductName", waybillDetails.OrderDetailsId);
            ViewData["WaybillId"] = new SelectList(_context.Waybill, "Id", "Id", waybillDetails.WaybillId);
            return View(waybillDetails);
        }

        // GET: WaybillDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillDetails = await _context.WaybillDetails.FindAsync(id);
            if (waybillDetails == null)
            {
                return NotFound();
            }
            ViewData["OrderDetailsId"] = new SelectList(_context.OrderDetails, "Id", "ProductName", waybillDetails.OrderDetailsId);
            ViewData["WaybillId"] = new SelectList(_context.Waybill, "Id", "Id", waybillDetails.WaybillId);
            return View(waybillDetails);
        }

        // POST: WaybillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WaybillId,OrderDetailsId,Weighting")] WaybillDetails waybillDetails)
        {
            if (id != waybillDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waybillDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaybillDetailsExists(waybillDetails.Id))
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
            ViewData["OrderDetailsId"] = new SelectList(_context.OrderDetails, "Id", "ProductName", waybillDetails.OrderDetailsId);
            ViewData["WaybillId"] = new SelectList(_context.Waybill, "Id", "Id", waybillDetails.WaybillId);
            return View(waybillDetails);
        }

        // GET: WaybillDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waybillDetails = await _context.WaybillDetails
                .Include(w => w.OrderDetails)
                .Include(w => w.Waybill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (waybillDetails == null)
            {
                return NotFound();
            }

            return View(waybillDetails);
        }

        // POST: WaybillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waybillDetails = await _context.WaybillDetails.FindAsync(id);
            _context.WaybillDetails.Remove(waybillDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaybillDetailsExists(int id)
        {
            return _context.WaybillDetails.Any(e => e.Id == id);
        }
    }
}
