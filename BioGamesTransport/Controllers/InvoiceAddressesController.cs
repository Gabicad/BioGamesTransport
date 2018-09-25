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
    public class InvoiceAddressesController : Controller
    {
        private readonly BiogamesTransContext _context;

        public InvoiceAddressesController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: InvoiceAddresses
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.InvoiceAddresses.Include(i => i.Customer);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: InvoiceAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceAddresses = await _context.InvoiceAddresses
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceAddresses == null)
            {
                return NotFound();
            }

            return View(invoiceAddresses);
        }

        // GET: InvoiceAddresses/Create
        public IActionResult Create(int? customer_id)
        {
            ViewData["CustomerId"] = customer_id;
            return View();
        }

        // POST: InvoiceAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? customer_id, InvoiceAddresses invoiceAddresses)
        {
            if (ModelState.IsValid)
            {
                DateTime cretaed_time = DateTime.Now;
                invoiceAddresses.Created = cretaed_time;
                _context.Add(invoiceAddresses);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Customers", new { id = invoiceAddresses.CustomerId });
            }
             ViewData["CustomerId"] = customer_id;
            return View(invoiceAddresses);
        }

        // GET: InvoiceAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceAddresses = await _context.InvoiceAddresses.FindAsync(id);
            if (invoiceAddresses == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", invoiceAddresses.CustomerId);
            return View(invoiceAddresses);
        }

        // POST: InvoiceAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,OutId,FirstName,LastName,Country,City,Zipcode,Address,Company,TaxNumber,Phone,Comment,Created,Modified,Deleted,Default")] InvoiceAddresses invoiceAddresses)
        {
            if (id != invoiceAddresses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime cretaed_time = DateTime.Now;
                    invoiceAddresses.Modified = cretaed_time;
                    _context.Update(invoiceAddresses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceAddressesExists(invoiceAddresses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Customers", new { id = invoiceAddresses.CustomerId });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", invoiceAddresses.CustomerId);
            return View(invoiceAddresses);
        }

        // GET: InvoiceAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceAddresses = await _context.InvoiceAddresses
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceAddresses == null)
            {
                return NotFound();
            }

            return View(invoiceAddresses);
        }

        // POST: InvoiceAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceAddresses = await _context.InvoiceAddresses.FindAsync(id);
            invoiceAddresses.Deleted = true;

            _context.Update(invoiceAddresses);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Customers", new { id = invoiceAddresses.CustomerId });
        }

        private bool InvoiceAddressesExists(int id)
        {
            return _context.InvoiceAddresses.Any(e => e.Id == id);
        }
    }
}
