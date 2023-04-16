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
    public class WeeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weeks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Weeks.Include(w => w.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Weeks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Weeks == null)
            {
                return NotFound();
            }

            var week = await _context.Weeks
                .Include(w => w.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (week == null)
            {
                return NotFound();
            }

            return View(week);
        }

        // GET: Weeks/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: Weeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CourseId")] Week week)
        {
            if (ModelState.IsValid)
            {
                week.Id = Guid.NewGuid();
                _context.Add(week);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", week.CourseId);
            return View(week);
        }

        // GET: Weeks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Weeks == null)
            {
                return NotFound();
            }

            var week = await _context.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", week.CourseId);
            return View(week);
        }

        // POST: Weeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,CourseId")] Week week)
        {
            if (id != week.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(week);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekExists(week.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", week.CourseId);
            return View(week);
        }

        // GET: Weeks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Weeks == null)
            {
                return NotFound();
            }

            var week = await _context.Weeks
                .Include(w => w.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (week == null)
            {
                return NotFound();
            }

            return View(week);
        }

        // POST: Weeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Weeks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Weeks'  is null.");
            }
            var week = await _context.Weeks.FindAsync(id);
            if (week != null)
            {
                _context.Weeks.Remove(week);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekExists(Guid id)
        {
          return (_context.Weeks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
