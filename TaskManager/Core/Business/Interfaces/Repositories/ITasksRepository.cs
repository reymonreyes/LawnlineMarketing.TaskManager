using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreEntities = TaskManager.Core.Domain.Entities;

namespace TaskManager.Core.Business.Interfaces.Repositories
{
    public interface ITasksRepository
    {
        List<CoreEntities.Task> GetAll();
        CoreEntities.Task Get(Guid id);
        void Create(CoreEntities.Task task);
        void Update(CoreEntities.Task task);
    }
}
