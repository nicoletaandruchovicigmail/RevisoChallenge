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
        // GET api/tasks/projectId (api/tasks/3)
        public HttpResponseMessage Get(int id)
        {
            var services = new DalServices();

            var taskList = new List<TaskViewModel>();
            foreach (var task in services.GetTasks())
                if (task.ProjectId == id)
                    taskList.Add(new TaskViewModel(task));

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
                taskList.Add(new TaskViewModel(task));
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