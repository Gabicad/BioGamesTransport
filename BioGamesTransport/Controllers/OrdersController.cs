using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioGamesTransport.Data.SQL;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BioGamesTransport.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BiogamesTransContext _context;
        private Random random = new Random();

        public OrdersController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name");
            var biogamesTransContext = _context.Orders.Include(o => o.Customer).Include(o => o.InvoiceAddress).Include(o => o.OrderStatus).Include(o => o.ShipAddress).Include(o => o.ShipStatus).Include(o => o.Shop).Include(o => o.User).Where(o => o.OrderStatusId != 9);
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
                .Include(o => o.OrderDetails).ThenInclude(OrderDetail => OrderDetail.ShipStatus)
                .Include(o => o.InvoiceAddress)
                .Include(o => o.OrderStatus)
                .Include(o => o.ShipAddress)
                .Include(o => o.ShipStatus)
                .Include(o => o.Shop)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            orders.OrderDetails = orders.OrderDetails.Where(z => z.Deleted != true).ToList();
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orders.ShipStatusId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name", orders.OrderStatusId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name", orders.ShopId);
            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Deleted != true), "Id", "ShortDetails");
            // ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address");
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name");
            //  ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address");
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders orders,Customers Customer, InvoiceAddresses InvoiceAddresses, ShipAddresses ShipAddresses)
        {

            DateTime cretaed_time = DateTime.Now;
 
            int i = 0;
            double total=0;
            foreach ( var item in orders.OrderDetails)
            {
               
                foreach (IFormFile itemFile in HttpContext.Request.Form.Files)
                {
                    if (itemFile.Name == "Image" + i)
                    {
                        if (itemFile.Length > 0)
                        {
                            Images dbImages = new Images();
                            dbImages.Name = item.ProductName;

                            //Convert Image to byte and save to database
                            {
                                byte[] p1 = null;
                                using (var fs1 = itemFile.OpenReadStream())
                                using (var ms1 = new MemoryStream())
                                {
                                    fs1.CopyTo(ms1);
                                    p1 = ms1.ToArray();
                                }
                                dbImages.Data = p1;
                            }
                            item.Images = dbImages;
                        }
                    }
                    
                }
                i++;
                total += item.Price * item.Quantity;
                item.Created = cretaed_time;
                if(orders.ShipExpectedDate != null)
                {
                    item.ShipExpectedDate = orders.ShipExpectedDate;
                }
                if (orders.ShipUndertakenDate != null)
                {
                    item.ShipUndertakenDate = orders.ShipUndertakenDate;
                }
  
            }

            orders.TotalPrice = total;
            orders.OrderOutRef = RandomString(10);
            orders.OrderOutId = 0;
            orders.Created = cretaed_time;


            if (orders.CustomerId > 0)
            {
                Customers customers = _context.Customers.Where(c => c.Id == orders.CustomerId).FirstOrDefault();
                orders.Customer = customers;
            }
            else
            {
                InvoiceAddresses.Created = cretaed_time;
                ShipAddresses.Created = cretaed_time;
                Customer.Created = cretaed_time;
                Customer.Deleted = false;
                orders.Customer = Customer;
                InvoiceAddresses.Orders.Add(orders);
                ShipAddresses.Orders.Add(orders);
                orders.Customer.InvoiceAddresses.Add(InvoiceAddresses);
                orders.Customer.ShipAddresses.Add(ShipAddresses);
            }

            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Deleted != true), "Id", "ShortDetails", orders.CustomerId);
            //ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name", orders.OrderStatusId);
            //ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "Id", "Address", orders.ShipAddressId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orders.ShipStatusId);
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "BaseUrl", orders.ShopId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", orders.UserId);
           return View(orders);
        }

        public  string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int count = 0;
            string tmp ;
            do
            {
                tmp = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
                var tmporder = _context.Orders.Where(o => o.OrderOutRef == tmp).ToListAsync();
                count = tmporder.Result.Count();
            } while (count > 0);
            return "INT"+tmp;
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
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Deleted != true), "Id", "Email", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name", orders.OrderStatusId);
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
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Deleted != true), "Id", "Email", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses, "Id", "Address", orders.InvoiceAddressId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses.Where(n => n.Id != 9), "Id", "Name", orders.OrderStatusId);
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
                .Include(o => o.OrderDetails)
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
            var orders = await _context.Orders
                 .Include(o => o.Customer)
                 .Include(o => o.OrderDetails)
                 .Include(o => o.InvoiceAddress)
                 .Include(o => o.OrderStatus)
                 .Include(o => o.ShipAddress)
                 .Include(o => o.ShipStatus)
                 .Include(o => o.Shop)
                 .Include(o => o.User)
                 .FirstOrDefaultAsync(m => m.Id == id);

            orders.Deleted = true;
            orders.OrderStatusId = 9;
            foreach (OrderDetails Item in orders.OrderDetails)
            {
                Item.Deleted = true;
            }


            _context.Update(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeShip(Orders Orders, int? Index, bool? allprod)
        {
            if (ModelState.IsValid)
            {
                Orders orders = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(m => m.Id == Orders.Id);
                orders.ShipStatusId = Orders.ShipStatusId;
                orders.OrderDatetime = Orders.OrderDatetime;
                orders.ShipUndertakenDate = Orders.ShipUndertakenDate;
                orders.ShipExpectedDate = Orders.ShipExpectedDate;

                if (allprod == true)
                {
                    foreach(OrderDetails item in orders.OrderDetails)
                    {
                        if(item.ShipStatusId != 9)
                        {
                            item.ShipStatusId = Orders.ShipStatusId;
                            item.ShipUndertakenDate = Orders.ShipUndertakenDate;
                            item.ShipExpectedDate = Orders.ShipExpectedDate;
                        }
                    }
                }

                _context.Update(orders);
                await _context.SaveChangesAsync();
                if (Index == 1)
                {
                    return RedirectToAction("Index", "Orders");
                }
                return RedirectToAction("Details", "Orders", new { id = Orders.Id });
            }

            return RedirectToAction("Index", "Orders");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeFinance(Orders Orders, int? Index)
        {
            if (ModelState.IsValid)
            {
                Orders orders = _context.Orders.Find(Orders.Id);
                orders.Deposit = Orders.Deposit;
                orders.DepositDate = Orders.DepositDate;

                _context.Update(orders);
                await _context.SaveChangesAsync();
                if (Index == 1)
                {
                    return RedirectToAction("Index", "Orders");
                }
                return RedirectToAction("Details", "Orders", new { id = Orders.Id });
            }

            return RedirectToAction("Index", "Orders");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(Orders Orders, int? Index)
        {
            if (ModelState.IsValid)
            {
                Orders orders = _context.Orders.Find(Orders.Id);
                orders.OrderStatusId = Orders.OrderStatusId;

                _context.Update(orders);
                await _context.SaveChangesAsync();
                if(Index == 1)
                {
                    return RedirectToAction("Index", "Orders");
                }
                return RedirectToAction("Details", "Orders", new { id = Orders.Id });
            }

            return RedirectToAction("Index", "Orders");
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMin(Orders OutOrder)
        {
            if (ModelState.IsValid)
            {

                Orders orders = _context.Orders.Find(OutOrder.Id);
                orders.ShopId = OutOrder.ShopId;
                orders.Shipment = OutOrder.Shipment;
                orders.Payment = OutOrder.Payment;
                orders.OrderDatetime = OutOrder.OrderDatetime;
                orders.Comment = OutOrder.Comment;

                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(OutOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            }

            return RedirectToAction("Details", "Orders", new { id = OutOrder.Id });
        }





        // GET: Orders/CustomerEdit/5
        public async Task<IActionResult> CustomerEdit(int? id)
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
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Deleted != true), "Id", "ShortDetails", orders.CustomerId);
            ViewData["InvoiceAddressId"] = new SelectList(_context.InvoiceAddresses.Where(c => c.Deleted != true), "Id", "Address", orders.InvoiceAddressId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses.Where(c => c.Deleted != true), "Id", "Address", orders.ShipAddressId);
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerEdit(int id,Orders order, Customers Customer, InvoiceAddresses InvoiceAddresses, ShipAddresses ShipAddresses)
        {
            DateTime cretaed_time = DateTime.Now;
            Orders orders = _context.Orders.Where(c => c.Id == id).FirstOrDefault();
            if (order.CustomerId > 0)
            {
                Customers customers = _context.Customers.Where(c => c.Id == order.CustomerId).FirstOrDefault();
                orders.Customer = customers;
                orders.InvoiceAddressId = order.InvoiceAddressId;
                orders.ShipAddressId = order.ShipAddressId;
            }
            else
            {
                InvoiceAddresses.Created = cretaed_time;
                ShipAddresses.Created = cretaed_time;
                Customer.Created = cretaed_time;
                Customer.Deleted = false;
                orders.Customer = Customer;
                InvoiceAddresses.Orders.Add(orders);
                ShipAddresses.Orders.Add(orders);
                orders.Customer.InvoiceAddresses.Add(InvoiceAddresses);
                orders.Customer.ShipAddresses.Add(ShipAddresses);
            }


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
            return RedirectToAction("Details", "Orders", new { id = orders.Id });


        }

        


        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
