using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CoreEntites = TaskManager.Core.Domain.Entities;

namespace TaskManager.Infrastructure
{
    public class DefaultDataContext : DbContext
    {
        public DefaultDataContext() : base("DefaultDatabase")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoreEntites.Task>().Property(p => p.DependentTaskId).IsOptional();
            base.OnModelCreating(modelBuilder);
        }
        
        public virtual DbSet<CoreEntites.Task> Tasks { get; set; }
    }
}