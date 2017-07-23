using System;
using System.Collections.Generic;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    [Serializable]
    public class ProjectViewModel
    {
        public ProjectViewModel() { }

        public ProjectViewModel(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Start = project.Start;
            End = project.End;
            ClientName = GetClientName(project.ClientId);
            CostPerHour = project.CostPerHour;

            // cost for all tasks depending on cost per hour and hours spent
            Cost = GetProjectCost();

            //Completed -> when all tasks are completed
        }

        private string GetClientName(int clientId)
        {
            var service = new DalServices();
            var client  = service.GetClient(clientId);

            return client.Name;
        }

        public float GetProjectCost()
        {
            var services = new DalServices();

            float cost = 0;
            foreach (var task in services.GetTasks())
            {
                if (task.ProjectId == Id)
                {
                   float y = task.ActualHours.GetValueOrDefault();
                   if (Math.Abs(y) > 0.000001)
                   {
                       cost += y * (float)CostPerHour;
                   }
                 }
            }
         return cost;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ClientName { get; set; }
        public decimal CostPerHour { get; set; }
        public float Cost { get; set; }
    }
}