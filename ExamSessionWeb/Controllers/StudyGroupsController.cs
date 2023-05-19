using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamSessionWeb;
using ExamSessionWeb.Models;

namespace ExamSessionWeb.Controllers
{
    public class StudyGroupsController : Controller
    {
        private readonly ExamSessionContext _context;

        public StudyGroupsController()
        {
            _context = new ExamSessionContext();
        }

        // GET: StudyGroups
        public async Task<IActionResult> Index()
        {
            var examSessionContext = _context.StudyGroups.Include(s => s.ElderNavigation).Include(s => s.TutorNavigation);
            return View(await examSessionContext.ToListAsync());
        }

        // GET: StudyGroups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StudyGroups == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups
                .Include(s => s.ElderNavigation)
                .Include(s => s.TutorNavigation)
                .FirstOrDefaultAsync(m => m.NumberGroup == id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            return View(studyGroup);
        }

        // GET: StudyGroups/Create
        public IActionResult Create()
        {
            ViewData["Elder"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["Tutor"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return View();
        }

        // POST: StudyGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberGroup,Specialization,Elder,Tutor,NumberOfStudents")] StudyGroup studyGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Elder"] = new SelectList(_context.Students, "StudentId", "StudentId", studyGroup.Elder);
            ViewData["Tutor"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studyGroup.Tutor);
            return View(studyGroup);
        }

        // GET: StudyGroups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StudyGroups == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups.FindAsync(id);
            if (studyGroup == null)
            {
                return NotFound();
            }
            ViewData["Elder"] = new SelectList(_context.Students, "StudentId", "StudentId", studyGroup.Elder);
            ViewData["Tutor"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studyGroup.Tutor);
            return View(studyGroup);
        }

        // POST: StudyGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumberGroup,Specialization,Elder,Tutor,NumberOfStudents")] StudyGroup studyGroup)
        {
            if (id != studyGroup.NumberGroup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyGroupExists(studyGroup.NumberGroup))
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
            ViewData["Elder"] = new SelectList(_context.Students, "StudentId", "StudentId", studyGroup.Elder);
            ViewData["Tutor"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", studyGroup.Tutor);
            return View(studyGroup);
        }

        // GET: StudyGroups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StudyGroups == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups
                .Include(s => s.ElderNavigation)
                .Include(s => s.TutorNavigation)
                .FirstOrDefaultAsync(m => m.NumberGroup == id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            return View(studyGroup);
        }

        // POST: StudyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StudyGroups == null)
            {
                return Problem("Entity set 'ExamSessionContext.StudyGroups'  is null.");
            }
            var studyGroup = await _context.StudyGroups.FindAsync(id);
            if (studyGroup != null)
            {
                _context.StudyGroups.Remove(studyGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyGroupExists(string id)
        {
          return (_context.StudyGroups?.Any(e => e.NumberGroup == id)).GetValueOrDefault();
        }
    }
}
