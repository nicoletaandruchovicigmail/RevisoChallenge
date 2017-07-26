using System;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    public class ClientViewModel
    {
        public ClientViewModel() { }

        public ClientViewModel(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Email = client.Email;
            Phone = client.Phone;
            Company = client.Company;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public string Company { get; set; }
    }
}