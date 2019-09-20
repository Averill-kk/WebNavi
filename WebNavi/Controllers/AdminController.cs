using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNavi.Models;

namespace WebNavi.Controllers
{
    public class AdminController : Controller
    {
        private readonly GpsDataContext _context;

        public AdminController(GpsDataContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.GpsDatas.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsData = await _context.GpsDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsData == null)
            {
                return NotFound();
            }

            return View(gpsData);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Latitude,Lontitude,Satellite,Battery")] GpsData gpsData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gpsData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gpsData);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsData = await _context.GpsDatas.FindAsync(id);
            if (gpsData == null)
            {
                return NotFound();
            }
            return View(gpsData);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Latitude,Lontitude,Satellite,Battery")] GpsData gpsData)
        {
            if (id != gpsData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gpsData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GpsDataExists(gpsData.Id))
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
            return View(gpsData);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gpsData = await _context.GpsDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gpsData == null)
            {
                return NotFound();
            }

            return View(gpsData);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gpsData = await _context.GpsDatas.FindAsync(id);
            _context.GpsDatas.Remove(gpsData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GpsDataExists(string id)
        {
            return _context.GpsDatas.Any(e => e.Id == id);
        }
    }
}
