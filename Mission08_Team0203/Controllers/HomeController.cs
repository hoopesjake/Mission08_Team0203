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
        private readonly ITaskRepository _context;

        public HomeController(ITaskRepository repo)
        {
            _context = repo;
        }

        // Display Quadrants View
        public IActionResult Quadrant()
        {
            var tasks = _context.Tasks.Include(t => t.Category).ToList();
            return View(tasks);
        }

        // GET: Add Task View
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Add Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.AddTask(task);
                return RedirectToAction("Quadrant");
            }

            // Reload categories if ModelState is invalid
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(task);
        }

        // POST: Mark Task as Complete
        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task != null)
            {
                task.Completed = true;
                _context.UpdateTask(task);
            }
            return RedirectToAction("Quadrant");
        }

        // POST: Delete Task
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _context.DeleteTask(id);
            return RedirectToAction("Quadrant");
        }

        // GET: Edit Task View
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(task);
        }

        // POST: Edit Task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateTask(task);
                return RedirectToAction("Quadrant");
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(task);
        }
    }
}
