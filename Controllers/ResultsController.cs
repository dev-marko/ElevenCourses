using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElevenCourses.Data;
using ElevenCourses.Models;
using ClosedXML.Excel;
using GemBox.Document;

namespace ElevenCourses.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET: Results
        public async Task<IActionResult> Index(Guid id)
        {
            var applicationDbContext = _context.Results.Where(z => z.testid == id).Include(r => r.test).Include(r => r.user);
            return View(await applicationDbContext.ToListAsync());
        }



        public FileContentResult Export(Guid id)
        {

            var data = _context.Results.Where(z => z.testid == id).Include(r => r.test).Include(r => r.user).ToListAsync();
            string name = data.Result[0].testName;
            string fileName = name + " results.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("All Results");

               // worksheet.Cell(1, 1).Value = "First name";
               // worksheet.Cell(1, 2).Value = "Last name";
                worksheet.Cell(1, 1).Value = "Email";
                worksheet.Cell(1, 2).Value = "Correct answers";
                worksheet.Cell(1, 3).Value = "Percents %";


                for (int i = 1; i <= data.Result.Count; i++)
                {
                    var item = data.Result[i - 1];

                   // worksheet.Cell(i + 1, 1).Value = item.user.FirstName;
                   // worksheet.Cell(i + 1, 2).Value = item.user.LastName;
                    worksheet.Cell(i + 1, 1).Value = item.user.Email;
                    worksheet.Cell(i + 1, 2).Value = item.points;
                    worksheet.Cell(i + 1, 3).Value = item.procent + " %";
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }

        public FileContentResult GetCertificat(Guid id)
        {
            var data = _context.Results.Where(z => z.id == id).Include(r => r.test).Include(r => r.user).FirstOrDefaultAsync();

            var pateka = Path.Combine(Directory.GetCurrentDirectory(), "template.docx");

            var document = DocumentModel.Load(pateka);

            document.Content.Replace("{{name}}", data.Result.user.Email);

            document.Content.Replace("{{kurs}}", data.Result.testName);

            string name = "Certificate_" + data.Result.testName + ".pdf";

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, name);


        }






        // GET: Results/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var results = await _context.Results
                .Include(r => r.test)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (results == null)
            {
                return NotFound();
            }

            return View(results);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["testid"] = new SelectList(_context.Tests, "id", "id");
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,userId,points,testName,procent,testid")] Results results)
        {
            if (ModelState.IsValid)
            {
                results.id = Guid.NewGuid();
                _context.Add(results);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["testid"] = new SelectList(_context.Tests, "id", "id", results.testid);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", results.userId);
            return View(results);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var results = await _context.Results.FindAsync(id);
            if (results == null)
            {
                return NotFound();
            }
            ViewData["testid"] = new SelectList(_context.Tests, "id", "id", results.testid);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", results.userId);
            return View(results);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,userId,points,testName,procent,testid")] Results results)
        {
            if (id != results.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(results);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultsExists(results.id))
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
            ViewData["testid"] = new SelectList(_context.Tests, "id", "id", results.testid);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", results.userId);
            return View(results);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var results = await _context.Results
                .Include(r => r.test)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (results == null)
            {
                return NotFound();
            }

            return View(results);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var results = await _context.Results.FindAsync(id);
            _context.Results.Remove(results);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultsExists(Guid id)
        {
            return _context.Results.Any(e => e.id == id);
        }
    }
}
