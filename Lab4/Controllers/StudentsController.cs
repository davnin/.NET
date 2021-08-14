using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.Models.ViewModels;

namespace Lab4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;


        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string Id)
        {
            var viewModel = new CommunityViewModel();

            viewModel.Student = await _context.Student
                  .Include(i => i.Memberships)
                  .ThenInclude(i => i.Community)
                  .AsNoTracking()
                  .OrderBy(i => i.Id)
                  .ToListAsync();

            if (Id != null)
            {
                ViewData["StudentId"] = Id;
                viewModel.Memberships = viewModel.Student.Where(
                    x => x.Id.ToString() == Id).Single().Memberships;
                
            }

            return View(viewModel);

            //return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> EditMembership (int Id)
        {
            var viewModel = new CommunityViewModel();
            viewModel.Memberships = await _context.CommunityMemberships.Where(i => i.StudentId == Id)
                .ToListAsync();
            viewModel.Student = await _context.Student.Where(i => i.Id == Id)
                .ToListAsync();
            viewModel.Communities = await _context.Communities
                .ToListAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> AddMemberships(int studentId, string communityId) {
            var membership = new CommunityMembership();
            membership.CommunityId = communityId;
            membership.StudentId = studentId;
            _context.CommunityMemberships.Add(membership);
            await _context.SaveChangesAsync();

            return RedirectToAction("EditMembership", new { id = studentId });
        }

        public async Task<IActionResult> RemoveMemberships(int studentId, string communityId) {
            var membership = new CommunityMembership();
            membership.CommunityId = communityId;
            membership.StudentId = studentId;
            _context.CommunityMemberships.Remove(membership);
            await _context.SaveChangesAsync();

            return RedirectToAction("EditMembership", new { id = studentId });
        }

            // POST: Students/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
