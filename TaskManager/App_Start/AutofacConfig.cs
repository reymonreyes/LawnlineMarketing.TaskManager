using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using TaskManager.Core.Business.Interfaces.Repositories;
using TaskManager.Core.Business.Interfaces.Services;
using TaskManager.Core.Business.Services;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Configurations
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterType<TasksRepository>().As<ITasksRepository>();
            builder.RegisterType<TasksService>().As<ITasksService>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var containter = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(containter);
        }
    }
}