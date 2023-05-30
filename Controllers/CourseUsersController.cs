using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElevenCourses.Data;
using ElevenCourses.Models.Relations;
using Microsoft.AspNetCore.Authorization;

namespace ElevenCourses.Controllers
{
    [Authorize]
    public class CourseUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseUsers
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var applicationDbContext = _context.CourseUsers
                .Include(c => c.Course)
                .Include(c => c.User)
                .Where(c => c.UserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CourseUsers == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseUsers
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseUser == null)
            {
                return NotFound();
            }

            return View(courseUser);
        }

        // GET: CourseUsers/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CourseUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CourseId")] CourseUser courseUser)
        {
            if (ModelState.IsValid)
            {
                courseUser.Id = Guid.NewGuid();
                _context.Add(courseUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseUser.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", courseUser.UserId);
            return View(courseUser);
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(Guid courseId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var courseUser = new CourseUser
            {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                UserId = userId
            };

            var enrollmentExists = _context.CourseUsers.Any(cu => cu.CourseId == courseId && cu.UserId == userId);

            if (enrollmentExists) return RedirectToAction("Index", "Courses");

            _context.Add(courseUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Courses");
        }

        // GET: CourseUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CourseUsers == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseUsers.FindAsync(id);
            if (courseUser == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseUser.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", courseUser.UserId);
            return View(courseUser);
        }

        // POST: CourseUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CourseId")] CourseUser courseUser)
        {
            if (id != courseUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseUserExists(courseUser.Id))
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

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseUser.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", courseUser.UserId);
            return View(courseUser);
        }

        // GET: CourseUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CourseUsers == null)
            {
                return NotFound();
            }

            var courseUser = await _context.CourseUsers
                .Include(c => c.Course)
                .ThenInclude(c => c.Creator)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseUser == null)
            {
                return NotFound();
            }

            return View(courseUser);
        }

        // POST: CourseUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CourseUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CourseUsers'  is null.");
            }

            var courseUser = await _context.CourseUsers.FindAsync(id);
            if (courseUser != null)
            {
                _context.CourseUsers.Remove(courseUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseUserExists(Guid id)
        {
            return (_context.CourseUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}