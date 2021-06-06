using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManager.Core.Business.Interfaces.Repositories;
using CoreEntities = TaskManager.Core.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        public CoreEntities.Task Get(Guid id)
        {
            CoreEntities.Task result = null;
            using (var context = new DefaultDataContext())
            {
                result = context.Tasks.FirstOrDefault(f => f.Id == id);
            }

            return result;
        }

        public List<CoreEntities.Task> GetAll()
        {           
            List<CoreEntities.Task> tasks = new List<CoreEntities.Task>();
            using (var context = new DefaultDataContext())
            {
                tasks = context.Tasks.Where(w => (w.DependentTaskId.HasValue && w.DependentTask.Complete && !w.Complete) || (!w.DependentTaskId.HasValue && !w.Complete)).OrderByDescending(o => o.CreatedAt).ToList();
                //tasks = context.Tasks.Where(w => (w.DependentTaskId.HasValue && w.DependentTask.Complete) || (!w.Complete && !w.DependentTaskId.HasValue)).OrderByDescending(o => o.CreatedAt).ToList();
            }
            return tasks;
        }

        public void Create(CoreEntities.Task task)
        {
            using (var context = new DefaultDataContext())
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public void Update(CoreEntities.Task task)
        {
            using (var context = new DefaultDataContext())
            {
                context.Entry(task).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}