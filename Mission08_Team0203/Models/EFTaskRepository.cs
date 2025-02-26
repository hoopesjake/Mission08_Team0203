using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0203.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public EFTaskRepository(TaskContext context)
        {
            _context = context;
        }

        public IQueryable<TaskModel> Tasks => _context.Tasks;
        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskModel task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}