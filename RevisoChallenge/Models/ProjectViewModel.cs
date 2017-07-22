using System;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    [Serializable]
    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Start = project.Start;
            End = project.End;
            ClientName = GetClientName(project.ClientId);

            // to be replaced with cost per project
            CostPerHour = project.CostPerHour;

            //Completed -> when all tasks are completed
        }

        private string GetClientName(int clientId)
        {
            var service = new DalServices();
            var client  = service.GetClient(clientId);

            return client.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ClientName { get; set; }
        public decimal CostPerHour { get; set; }
    }
}