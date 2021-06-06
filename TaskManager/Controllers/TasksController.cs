using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Core.Business.Interfaces.Services;
using CoreDtos = TaskManager.Core.Business.Dtos;

namespace TaskManager.Controllers
{
    public class TasksController : ApiController
    {
        private readonly ITasksService _tasksService;
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        [Route("api/tasks/{taskId}")]
        public IHttpActionResult Get(string taskId)
        {
            var task = _tasksService.Get(taskId);
            return Json(task);
        }

        [HttpPost]
        [Route("api/tasks")]
        public IHttpActionResult Post(CoreDtos.Task task)
        {
            _tasksService.Create(task);
            return Ok();
        }

        [HttpPut]
        [Route("api/tasks")]
        public IHttpActionResult Put(CoreDtos.Task task)
        {
            _tasksService.Update(task);
            return Ok();
        }

        [HttpGet]
        [Route("api/tasks")]
        public IHttpActionResult All()
        {
            var tasks = _tasksService.GetAll();
            return Json(tasks);
        }

        [HttpPost]
        [Route("api/tasks/{taskId}/completed")]
        public IHttpActionResult Completed(string taskId)
        {
            _tasksService.Completed(taskId);
            return Ok();
        }
    }
}
