using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Core.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Complete { get; set; }
        public Guid? DependentTaskId { get; set; }
        public virtual Task DependentTask { get; set; }
        public DateTime? DateDue { get; set; }
    }
}