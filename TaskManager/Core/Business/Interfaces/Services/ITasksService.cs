using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreDtos = TaskManager.Core.Business.Dtos;

namespace TaskManager.Core.Business.Interfaces.Services
{
    public interface ITasksService
    {
        List<CoreDtos.Task> GetAll();
        void Create(CoreDtos.Task task);
        void Update(CoreDtos.Task task);
        CoreDtos.Task Get(string taskId);
        void Completed(string taskId);
    }
}