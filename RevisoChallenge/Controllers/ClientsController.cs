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
    public class ClientsController : ApiController
    {
        private readonly DalServices _services;

        public ClientsController() : this(new DalServices())
        {
        }

        public ClientsController(DalServices services)
        {
            _services = services;
        }

        // GET api/clients/5
        public HttpResponseMessage Get(int id)
        {
            var services = new DalServices();

            var clientViewModel = new ClientViewModel(services.GetClient(id));
            var jsonString = JsonConvert.SerializeObject(clientViewModel);

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

            var clientViewModel = new List<ClientViewModel>();
            foreach (var client in services.GetClients())
                clientViewModel.Add(new ClientViewModel(client));

            var jsonString = JsonConvert.SerializeObject(clientViewModel);
            var resp = new HttpResponseMessage
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }

        // POST api/clients
        public void Post([FromBody] ClientViewModel client)
        {
            if (client == null || client.Name == null) return;
            var newClient = new Client
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Company = client.Company
            };
            _services.AddClient(newClient);
        }

        // POST: api/clients/Edit/5
        public void Edit(ClientViewModel clientToUpdate)
        {
            if (clientToUpdate != null)
            {
                var updatedClient = new Client
                {
                    Id = clientToUpdate.Id,
                    Name = clientToUpdate.Name,
                    Email = clientToUpdate.Email,
                    Phone = clientToUpdate.Phone,
                    Company = clientToUpdate.Company
                };
                _services.UpdateClient(updatedClient);
            }
        }
    }
}
