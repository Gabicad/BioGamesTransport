using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using BioGamesTransport.Data.SQL;

namespace BioGamesTransport.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly BiogamesTransContext _context;

        public OrderDetailsController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var biogamesTransContext = _context.OrderDetails.Include(o => o.Images).Include(o => o.Manufacturer).Include(o => o.Order).Include(o => o.ShipStatus);
            return View(await biogamesTransContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Images)
                .Include(o => o.Manufacturer)
                .Include(o => o.Order)
                .Include(o => o.ShipStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public IActionResult CreateSub(int? order_id)
        {

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["OrderId"] = order_id;
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSub(int? order_id, OrderDetails orderDetails, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
               
                if (Image != null)
                {
                    if (Image.Length > 0)
                    {
                        Images dbImages = new Images();
                        dbImages.Name = orderDetails.ProductName;

                        //Convert Image to byte and save to database
                        {
                            byte[] p1 = null;
                            using (var fs1 = Image.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            dbImages.Data = p1;
                        }
                        orderDetails.Images = dbImages;
                    }
                }


                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                await RecalculateTotalPrice(orderDetails.OrderId);

                return RedirectToAction("Details", "Orders", new { id = orderDetails.OrderId });
            }

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", orderDetails.ManufacturerId);
            ViewData["OrderId"] = orderDetails.OrderId;
            return View(orderDetails);
        }


        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ImagesId"] = new SelectList(_context.Images, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create(int? order_id, OrderDetails orderDetails, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                if (Image.Length > 0)
                {
                    Images dbImages = new Images();
                    dbImages.Name = orderDetails.ProductName;

                    //Convert Image to byte and save to database
                    {
                        byte[] p1 = null;
                        using (var fs1 = Image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        dbImages.Data = p1;
                    }
                    orderDetails.Images = dbImages;
                }


                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = orderDetails.OrderId });
            }

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", orderDetails.ManufacturerId);
            ViewData["OrderId"] = orderDetails.OrderId;
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }
            ViewData["ImagesId"] = new SelectList(_context.Images, "Id", "Name", orderDetails.ImagesId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", orderDetails.ManufacturerId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetails.OrderId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orderDetails.ShipStatusId);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ShipStatusId,ManufacturerId,ImagesId,ProductOutId,ProductName,ProductRef,Quantity,Price,Deposit,PurchasePrice,ShipUndertakenDate,ShipExpectedDate,ShipDeliveredDate,Comment,Created,Modified")] OrderDetails orderDetails)
        {
            if (id != orderDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                await RecalculateTotalPrice(orderDetails.OrderId);

                return RedirectToAction("Details", "Orders", new { id = orderDetails.OrderId });
            }
            ViewData["ImagesId"] = new SelectList(_context.Images, "Id", "Name", orderDetails.ImagesId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", orderDetails.ManufacturerId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetails.OrderId);
            ViewData["ShipStatusId"] = new SelectList(_context.ShipStatuses, "Id", "Name", orderDetails.ShipStatusId);
            return View(orderDetails);
        }



        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Images)
                .Include(o => o.Manufacturer)
                .Include(o => o.Order)
                .Include(o => o.ShipStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            int tmpID = orderDetails.OrderId;
            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();
            await RecalculateTotalPrice(tmpID);
            return RedirectToAction("Details", "Orders", new { id = tmpID });
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }

        private async Task RecalculateTotalPrice(int orderId)
        {
            var Orders = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(m => m.Id == orderId);

            Orders.TotalPrice = Orders.ReCalculateTotalPrice(Orders.OrderDetails);
            _context.Update(Orders);
            await _context.SaveChangesAsync();
        }



    }
}
