using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_MVC.Models;

namespace code_MVC.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Classrooms.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null) return NotFound();
            return View(classroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom classroom)
        {
            if (id != classroom.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null) return NotFound();
            return View(classroom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null) return NotFound();
            return View(classroom);
        }
    }
} 