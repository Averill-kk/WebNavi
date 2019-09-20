using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNavi.Models;
using Microsoft.Extensions.FileProviders;

namespace WebNavi.Controllers
{
    public class GpsDatasController : Controller
    {
        private readonly GpsDataContext _context;

        public GpsDatasController(GpsDataContext context)
        {
            _context = context;
        }

        // GET: GpsDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.GpsDatas.ToListAsync());
        }

        // GET: GpsDatas/Details/5
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

        // GET: GpsDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult View()
        {
            return View();
        }

        // POST: GpsDatas/Create
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

        // GET: GpsDatas/Edit/5
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

        // POST: GpsDatas/Edit/5
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

        // GET: GpsDatas/Delete/5
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

        // POST: GpsDatas/Delete/5
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
        [HttpPost]


        [HttpGet]
        [Route("testfile")]
        public ActionResult TestFile()
        {

            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            _context.GpsDatas.Load();
            foreach (var temp in _context.GpsDatas.Local)
            {
                string strContent = String.Format("RALLY	{0}	{1}	1	0	0	0", temp.Latitude, temp.Lontitude);
                tw.WriteLine(strContent);
                tw.Flush();
            }
            
       
            var length = memoryStream.Length;
            tw.Close();
            var toWrite = new byte[length];
            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

            return File(toWrite, "text/plain", "navdata.ral");
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".ral", "text/plain"}
            };
        }
    }

  
}

