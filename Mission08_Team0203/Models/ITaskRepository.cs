using System.Linq;

namespace Mission08_Team0203.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskModel> Tasks { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(int taskId);
    }
}