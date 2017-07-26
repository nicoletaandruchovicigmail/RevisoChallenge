using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;
using RevisoChallenge.Models;

namespace RevisoChallenge.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectsController : ApiController
    {
        // GET api/projects
        public HttpResponseMessage Get()
        {
            var services = new DalServices();

            var ceva = new List<ProjectViewModel>();
            foreach (var project in services.GetProjects())
            {
                ceva.Add(new ProjectViewModel(project));
            }
            var jsonString = JsonConvert.SerializeObject(ceva);

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
            var services = new DalServices();

            var ceva = new ProjectViewModel(services.GetProject(id));
            var jsonString = JsonConvert.SerializeObject(ceva);

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        // POST api/projects
        public void Post([FromBody] ProjectViewModel project)
        {
            if (project != null)
            {
                if (project.Name != null)
                {
                    var newProject = new Project()
                    {
                        Name = project.Name,
                        Description = project.Description,
                        
                        Start = project.Start,
                        End = project.End,

                        //todo populate client with value from request 
                        ClientId = 1,
                        CostPerHour=(decimal) project.Cost
                    };
                    var services = new DalServices();
                    services.AddProject(newProject);
                }
            }
        }

        // PUT api/projects/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/projects/5
        public void Delete(int id)
        {
        }
    }
}