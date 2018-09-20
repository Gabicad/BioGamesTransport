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
    public class SystemLogsController : Controller
    {
        private readonly BiogamesTransContext _context;

        public SystemLogsController(BiogamesTransContext context)
        {
            _context = context;
        }

        // GET: SystemLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemLog.ToListAsync());
        }

        // GET: SystemLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemLog == null)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // GET: SystemLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Logs")] SystemLog systemLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemLog);
        }

        // GET: SystemLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLog.FindAsync(id);
            if (systemLog == null)
            {
                return NotFound();
            }
            return View(systemLog);
        }

        // POST: SystemLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Logs")] SystemLog systemLog)
        {
            if (id != systemLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemLogExists(systemLog.Id))
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
            return View(systemLog);
        }

        // GET: SystemLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLog = await _context.SystemLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemLog == null)
            {
                return NotFound();
            }

            return View(systemLog);
        }

        // POST: SystemLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemLog = await _context.SystemLog.FindAsync(id);
            _context.SystemLog.Remove(systemLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemLogExists(int id)
        {
            return _context.SystemLog.Any(e => e.Id == id);
        }
    }
}
