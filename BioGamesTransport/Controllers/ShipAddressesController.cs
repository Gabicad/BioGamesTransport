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
    public class ShipAddressesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public ShipAddressesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: ShipAddresses
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.ShipAddresses.Include(s => s.Customer);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: ShipAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddresses = await _context.ShipAddresses
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipAddresses == null)
            {
                return NotFound();
            }

            return View(shipAddresses);
        }

        // GET: ShipAddresses/Create
        public IActionResult Create(int? customer_id)
        {
            ViewData["CustomerId"] = customer_id;
            return View();
        }

        // POST: ShipAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? customer_id, ShipAddresses shipAddresses)
        {
            if (ModelState.IsValid)
            {
                DateTime cretaed_time = DateTime.Now;
                shipAddresses.Created = cretaed_time;
                _context.Add(shipAddresses);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Customers", new { id = shipAddresses.CustomerId });
            }
            ViewData["CustomerId"] = customer_id;
            return View(shipAddresses);
        }

        // GET: ShipAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddresses = await _context.ShipAddresses.FindAsync(id);
            if (shipAddresses == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", shipAddresses.CustomerId);
            return View(shipAddresses);
        }

        // POST: ShipAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OutId,FirstName,LastName,Country,City,Zipcode,Address,Company,Phone,Comment,Created,Modified,Deleted,Default")] ShipAddresses shipAddresses)
        {
            if (id != shipAddresses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime cretaed_time = DateTime.Now;
                    shipAddresses.Modified = cretaed_time;
                    _context.Update(shipAddresses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipAddressesExists(shipAddresses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Customers", new { id = shipAddresses.CustomerId });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", shipAddresses.CustomerId);
            return View(shipAddresses);
        }

        // GET: ShipAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddresses = await _context.ShipAddresses
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipAddresses == null)
            {
                return NotFound();
            }

            return View(shipAddresses);
        }

        // POST: ShipAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipAddresses = await _context.ShipAddresses.FindAsync(id);
            shipAddresses.Deleted = true;
            _context.Update(shipAddresses);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Customers", new { id = shipAddresses.CustomerId });
        }

        private bool ShipAddressesExists(int id)
        {
            return _context.ShipAddresses.Any(e => e.Id == id);
        }
    }
}
