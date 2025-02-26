using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0203.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mission08_Team0203.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskContext _context;

        public HomeController(TaskContext context)
        {
            _context = context;
        }

        // Display Quadrants View
        public async Task<IActionResult> Quadrant()
        {
            var tasks = await _context.Tasks.Include(t => t.Category).ToListAsync();
            return View(tasks);
        }

        // GET: Add Task View
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Add Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Quadrant");
            }

            // Reload categories if ModelState is invalid
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            return View(task);
        }

        // POST: Mark Task as Complete
        [HttpPost]
        public async Task<IActionResult> MarkComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Completed = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Quadrant");
        }

        // POST: Delete Task
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Quadrant");
        }

        // GET: Edit Task View
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            return View(task);
        }

        // POST: Edit Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Quadrant");
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            return View(task);
        }
    }
}
