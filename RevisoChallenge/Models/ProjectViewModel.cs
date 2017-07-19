using System;
using RevisoChallenge.DAL.Entities;

namespace RevisoChallenge.Models
{
    [Serializable]
    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Name = project.Name;
            ClientId = project.ClientId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int ClientId { get; set; }
        public decimal CostPerHour { get; set; }
    }
}