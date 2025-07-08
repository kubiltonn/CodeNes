using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using code_MVC.Models;

namespace code_MVC.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Announcements.OrderByDescending(a => a.Date).ToListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.Date = DateTime.Now;
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Announcement announcement)
        {
            if (id != announcement.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }
    }
} 