using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElevenCourses.Data;
using ElevenCourses.Models;

namespace ElevenCourses.Controllers
{
    public class PdfFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PdfFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PdfFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Files.Include(p => p.Creator).Include(p => p.Week);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PdfFiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var pdfFile = await _context.Files
                .Include(p => p.Creator)
                .Include(p => p.Week)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfFile == null)
            {
                return NotFound();
            }

            return View(pdfFile);
        }

        // GET: PdfFiles/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["WeekId"] = new SelectList(_context.Weeks, "Id", "Id");
            return View();
        }

        // POST: PdfFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilePath,FileUrl,CreatorId,WeekId")] PdfFile pdfFile)
        {
            if (ModelState.IsValid)
            {
                pdfFile.Id = Guid.NewGuid();
                _context.Add(pdfFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", pdfFile.CreatorId);
            ViewData["WeekId"] = new SelectList(_context.Weeks, "Id", "Id", pdfFile.WeekId);
            return View(pdfFile);
        }

        // GET: PdfFiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var pdfFile = await _context.Files.FindAsync(id);
            if (pdfFile == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", pdfFile.CreatorId);
            ViewData["WeekId"] = new SelectList(_context.Weeks, "Id", "Id", pdfFile.WeekId);
            return View(pdfFile);
        }

        // POST: PdfFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FilePath,FileUrl,CreatorId,WeekId")] PdfFile pdfFile)
        {
            if (id != pdfFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pdfFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PdfFileExists(pdfFile.Id))
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
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", pdfFile.CreatorId);
            ViewData["WeekId"] = new SelectList(_context.Weeks, "Id", "Id", pdfFile.WeekId);
            return View(pdfFile);
        }

        // GET: PdfFiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var pdfFile = await _context.Files
                .Include(p => p.Creator)
                .Include(p => p.Week)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfFile == null)
            {
                return NotFound();
            }

            return View(pdfFile);
        }

        // POST: PdfFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Files == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Files'  is null.");
            }
            var pdfFile = await _context.Files.FindAsync(id);
            if (pdfFile != null)
            {
                _context.Files.Remove(pdfFile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PdfFileExists(Guid id)
        {
          return (_context.Files?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
