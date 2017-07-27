using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;
using RevisoChallenge.Models;

namespace RevisoChallenge.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ProjectsController : ApiController
    {
        private readonly DalServices _services;

        public ProjectsController() : this(new DalServices())
        {
        }

        public ProjectsController(DalServices services)
        {
            _services = services;
        }

        public string GetClientName(int clientId)
        {
            return _services.GetClient(clientId).Name;
        }

        public float GetProjectCost(int projectId, decimal costPerHour)
        {
            float cost = 0;
            // cost for all tasks depending on cost per hour and hours spent
            foreach (var task in _services.GetTasksByProjectId(projectId))
            {
                float y = task.ActualHours.GetValueOrDefault();
                if (Math.Abs(y) > 0.000001)
                    cost += y * (float) costPerHour;
            }
            return cost;
        }


        public bool IsProjectCompleted(int projectId)
        {
            foreach (var task in _services.GetTasksByProjectId(projectId))
                // needs a more clever implementation 
                if (!task.End.HasValue || task.End == default(DateTime))
                    return false;
            return true;
        }

        // GET api/projects
        public HttpResponseMessage Get()
        {
            var projectViewModels = new List<ProjectViewModel>();
            foreach (var project in _services.GetProjects())
            {
                var clientName = _services.GetClient(project.ClientId).Name;
                var cost = GetProjectCost(project.Id, project.CostPerHour);
                var isProjectCompleted = IsProjectCompleted(project.Id);
                projectViewModels.Add(new ProjectViewModel(project, clientName, (decimal) cost, isProjectCompleted));
            }

            var jsonString = JsonConvert.SerializeObject(projectViewModels);

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        // GET api/projects/5
        public HttpResponseMessage Get(int id)
        {
            var project = _services.GetProject(id);
            var projectViewModel = new ProjectViewModel(project, GetClientName(id),
                (decimal) GetProjectCost(id, project.CostPerHour), IsProjectCompleted(id));
            var jsonString = JsonConvert.SerializeObject(projectViewModel);

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        // POST api/projects/add
        [HttpPost,Route("Api/Projects/Add")]
        public void Add([FromBody] ProjectViewModel project)
        {
            if (project != null && project.Name != null)
            {
                var newProject = new Project
                {
                    Name = project.Name,
                    Description = project.Description,

                    Start = project.Start,
                    End = project.End,

                    //todo populate client with value from request 
                    ClientId = 1,
                    CostPerHour = project.Cost
                };
                _services.AddProject(newProject);
            }
        }

        // POST: /Projects/Edit/5
        [HttpPost, Route("Api/Projects/Edit")]
        public void Edit(ProjectViewModel projectToUpdate)
        {
            if (projectToUpdate != null)
            {
                var updatedProject = new Project
                {
                    Name = projectToUpdate.Name,
                    Description = projectToUpdate.Description,

                    Start = projectToUpdate.Start,
                    End = projectToUpdate.End,

                    ClientId = projectToUpdate.ClientId,
                    CostPerHour = projectToUpdate.Cost
                };
                _services.UpdateProject(updatedProject);
            }
        }

        // DELETE api/projects/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}