using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Core.Business.Dtos
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Complete { get; set; }
        public string DependentTaskId { get; set; }
        public DateTime? DateDue { get; set; }
    }
}