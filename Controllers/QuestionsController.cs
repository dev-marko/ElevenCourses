using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElevenCourses.Data;
using System.Security.Claims;
using ElevenCourses.Models;
using ElevenCourses.Models.Relations;

namespace ElevenCourses.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,question,correctAnswer,Answer1,Answer2,Answer3,Answer4")] Question question1)
        {
            //if (ModelState.IsValid)
            // {
            question1.id = Guid.NewGuid();
            _context.Add(question1);
            //problem so pronaoganje na testot
            var i = ViewData["id"];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pom = _context.Tests.Where(z => z.userId == userId).FirstOrDefaultAsync();

            QuestionInTest question = new QuestionInTest
            {
                testId = pom.Result.id,
                test = pom.Result,
                questionId = question1.id,
                question = question1

            };
            _context.Add(question);
            await _context.SaveChangesAsync();
            ViewData["test"] = pom.Result;
            return RedirectToAction("Create", "Questions");
            //}

        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,question,correctAnswer,Answer1,Answer2,Answer3,Answer4")] Question questio)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(questio.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Tests");
            }
            return View(questio);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tests");
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Questions.Any(e => e.id == id);
        }
    }
}
