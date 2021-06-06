using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Core.Business.Interfaces.Repositories;
using TaskManager.Core.Business.Interfaces.Services;
using TaskManager.Core.Domain.Entities;
using CoreDtos = TaskManager.Core.Business.Dtos;

namespace TaskManager.Core.Business.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public List<CoreDtos.Task> GetAll()
        {
            var tasks = _tasksRepository.GetAll();
            var result = new List<CoreDtos.Task>();
            if (tasks.Count > 0)
                result = tasks.Select(s => new CoreDtos.Task { Id = s.Id.ToString(), Name = s.Name, Complete = s.Complete, CreatedAt = DateTime.SpecifyKind(s.CreatedAt, DateTimeKind.Utc), DependentTaskId = s.DependentTaskId.ToString(), DateDue = (s.DateDue.HasValue ? DateTime.SpecifyKind(s.DateDue.Value, DateTimeKind.Utc) : new DateTime?()) }).ToList();

            return result;
        }
        
        public void Create(CoreDtos.Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Name))
                throw new Exception("Task Name is required.");

            Guid? dependentTaskGuidId = null;
            Guid dependendTaskGuidIdParse;
            if (!string.IsNullOrWhiteSpace(task.DependentTaskId))            
                if(Guid.TryParse(task.DependentTaskId, out dependendTaskGuidIdParse))                
                    dependentTaskGuidId = dependendTaskGuidIdParse;
                         
            var newTask = new Task() { Id = Guid.NewGuid(), Name = task.Name, Complete = false, CreatedAt = DateTime.UtcNow, DependentTaskId = dependentTaskGuidId, DateDue = (task.DateDue.HasValue ? task.DateDue.Value.ToUniversalTime() : new DateTime?()) };
            _tasksRepository.Create(newTask);
        }

        public CoreDtos.Task Get(string taskId)
        {
            if (string.IsNullOrWhiteSpace(taskId))
                throw new ArgumentNullException();
            Guid guidTaskId;
            CoreDtos.Task result = null;
            if (Guid.TryParse(taskId, out guidTaskId))
            {
                var task = _tasksRepository.Get(guidTaskId);
                if (task != null)
                    result = new CoreDtos.Task { Id = task.Id.ToString(), Name = task.Name, Complete = task.Complete, CreatedAt = task.CreatedAt, DependentTaskId = task.DependentTaskId.ToString(), DateDue = (task.DateDue.HasValue ? DateTime.SpecifyKind(task.DateDue.Value, DateTimeKind.Utc) : new DateTime?()) };
            }
            return result;
        }        

        public void Update(CoreDtos.Task task)
        {
            if (task == null)
                throw new ArgumentNullException();

            if(!string.IsNullOrWhiteSpace( task.Id))
            {
                Guid guidTaskId;
                if(Guid.TryParse(task.Id, out guidTaskId))
                {
                    var taskExist = _tasksRepository.Get(guidTaskId);
                    if(taskExist  != null)
                    {
                        taskExist.Name = task.Name;
                        taskExist.Complete = task.Complete;
                        taskExist.DateDue = (task.DateDue.HasValue ? task.DateDue.Value.ToUniversalTime() : new DateTime?());
                        if (!string.IsNullOrWhiteSpace(task.DependentTaskId))
                        {
                            Guid dependentTaskGuidId;
                            if (Guid.TryParse(task.DependentTaskId, out dependentTaskGuidId))
                                taskExist.DependentTaskId = dependentTaskGuidId;
                        }
                        _tasksRepository.Update(taskExist);
                    }
                }
            }
        }

        public void Completed(string taskId)
        {
            if (string.IsNullOrWhiteSpace(taskId))
                throw new ArgumentNullException();

            Guid guidTaskId;
            if (Guid.TryParse(taskId, out guidTaskId))
            {
                var taskExist = _tasksRepository.Get(guidTaskId);
                if (taskExist != null)
                {
                    taskExist.Complete = true;
                    _tasksRepository.Update(taskExist);
                }
            }
        }
    }
}