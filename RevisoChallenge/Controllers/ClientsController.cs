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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        // GET api/clients/5
        public HttpResponseMessage Get(int id)
        {
            var services = new DalServices();

            var ceva = new ClientViewModel(services.GetClient(id));
            var jsonString = JsonConvert.SerializeObject(ceva);

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }


        // GET api/clients
        public HttpResponseMessage Get()
        {
            var services = new DalServices();

            var ceva = new List<ClientViewModel>();
            foreach (var client in services.GetClients())
            {
                ceva.Add(new ClientViewModel(client));
            }
            var jsonString = JsonConvert.SerializeObject(ceva);

            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }
    }
}
