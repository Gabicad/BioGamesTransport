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
    public class CustomersController : Controller
    {
        private readonly BiogamesTransContext _context;

        public CustomersController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string q, string q1)
        {
            var biogamesMgrContext = _context.Customers.Include(c => c.Shop).Where(c => c.Deleted == false);
            if (!String.IsNullOrEmpty(q))
            {
                switch (q1)
            {
                case "fullName":
                        biogamesMgrContext = biogamesMgrContext.Where(s => s.FullName.Contains(q));
                        break;
                case "type":
                        biogamesMgrContext = biogamesMgrContext.Where(s => s.Type.Contains(q));
                        break;
                case "email":
                        biogamesMgrContext = biogamesMgrContext.Where(s => s.Email.Contains(q));
                        break;
                case "company":
                        biogamesMgrContext = biogamesMgrContext.Where(s => s.Company.Contains(q));
                        break;
                case "phone":
                        biogamesMgrContext = biogamesMgrContext.Where(s => s.Phone.Contains(q));
                        break;
                    default:
                        
                        break;
            }
                ViewData["q"] = q;
                ViewData["q1"] = q1;
            }

            dynamic tmp = await biogamesMgrContext.ToListAsync();
            return View(tmp);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var customers = await _context.Customers
                .Include(c => c.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);

            _context.Entry(customers).Collection("InvoiceAddresses").Load();
            _context.Entry(customers).Collection("ShipAddresses").Load();
            

            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customers Customers, InvoiceAddresses InvoiceAddresses, ShipAddresses ShipAddresses)
        {
            if (ModelState.IsValid)
            {
                DateTime cretaed_time = DateTime.Now;
                InvoiceAddresses.Created = cretaed_time;
                ShipAddresses.Created = cretaed_time;
                Customers.Created = cretaed_time;
                Customers.Deleted = false;

                Customers.InvoiceAddresses.Add(InvoiceAddresses);
                Customers.ShipAddresses.Add(ShipAddresses);
                _context.Add(Customers);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name", Customers.ShopId);
            

            return View(Customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name", customers.ShopId);

            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customers customers)
        {
            if (id != customers.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime cretaed_time = DateTime.Now;
                   
                    customers.Modified = cretaed_time;
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.Id))
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
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name", customers.ShopId);
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
