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
    public class OrdersController : Controller
    {
        private readonly BiogamesTransContext _context;

        public OrdersController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.Orders.Include(o => o.Customer).Include(o => o.InvoiceAddress).Include(o => o.OrderStatus).Include(o => o.ShipAddress).Include(o => o.ShipStatus).Include(o => o.Shop).Include(o => o.User);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.InvoiceAddress)
                .Include(o => o.OrderStatus)
                .Include(o => o.ShipAddress)
                .Include(o => o.ShipStatus)
                .Include(o => o.Shop)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address");
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name");
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address");
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name");
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShopId,OrderStatusId,CustomerId,UserId,OrderOutId,ShipAddressId,InvoiceAddressId,ShipStatusId,TotalPrice,Deposit,OrderDatetime,Created,Modified,Comment,LastCheck,OrderOutRef,Payment,Shipment,ShipUndertakenDate,ShipExpectedDate,ShipDeliveredDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", orders.OrderStatusId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address", orders.ShipAddressId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orders.ShipStatusId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", orders.ShopId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", orders.UserId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", orders.OrderStatusId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address", orders.ShipAddressId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orders.ShipStatusId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", orders.ShopId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", orders.UserId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopId,OrderStatusId,CustomerId,UserId,OrderOutId,ShipAddressId,InvoiceAddressId,ShipStatusId,TotalPrice,Deposit,OrderDatetime,Created,Modified,Comment,LastCheck,OrderOutRef,Payment,Shipment,ShipUndertakenDate,ShipExpectedDate,ShipDeliveredDate")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Name", orders.OrderStatusId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address", orders.ShipAddressId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orders.ShipStatusId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", orders.ShopId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", orders.UserId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.InvoiceAddress)
                .Include(o => o.OrderStatus)
                .Include(o => o.ShipAddress)
                .Include(o => o.ShipStatus)
                .Include(o => o.Shop)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
