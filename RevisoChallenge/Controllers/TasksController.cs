using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using RevisoChallenge.DAL.Services.Implementation;
using RevisoChallenge.Models;

namespace RevisoChallenge.Controllers
{
    [EnableCors("*", "*", "*")]
    public class TasksController : ApiController
    {
        private readonly DalServices _services;

        public TasksController() : this(new DalServices())
        {
        }

        public TasksController(DalServices services)
        {
            _services = services;
        }

        // GET api/tasks/projectId (api/tasks/3)
        public HttpResponseMessage Get(int id)
        {
            var taskList = new List<TaskViewModel>();

            foreach (var task in _services.GetTasksByProjectId(id))
            {
                var projectName = _services.GetProject(task.ProjectId).Name;
                var cost = _services.GetProject(task.ProjectId).CostPerHour;
                var isTaskCompleted = (!task.End.HasValue || task.End == default(DateTime));
                taskList.Add(new TaskViewModel(task, projectName, cost, isTaskCompleted));
            }

            var jsonString = JsonConvert.SerializeObject(taskList);
            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        // GET api/tasks
        public HttpResponseMessage Get()
        {
            var services = new DalServices();

            var taskList = new List<TaskViewModel>();

            foreach (var task in services.GetTasks())
            {
                var projectName = _services.GetProject(task.ProjectId).Name;
                var cost = _services.GetProject(task.ProjectId).CostPerHour;
                var isTaskCompleted = (!task.End.HasValue || task.End == default(DateTime));
                taskList.Add(new TaskViewModel(task, projectName, cost, isTaskCompleted));
            }

            var jsonString = JsonConvert.SerializeObject(taskList);
            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }
    }
}