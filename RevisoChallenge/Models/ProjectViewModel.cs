using System;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    public class ProjectViewModel: IEquatable<ProjectViewModel>
    {
        public ProjectViewModel() { }

        public ProjectViewModel(Project project, string clientName, decimal cost, bool isProjectCompleted)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Start = project.Start;
            End = project.End;
            ClientId = project.ClientId;
            ClientName = clientName;
            Cost = cost;
            Completed = isProjectCompleted;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal Cost { get; set; }
        public bool Completed { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProjectViewModel);
        }

        public bool Equals(ProjectViewModel obj)
        {
            return (Id == obj.Id && Name==obj.Name && ClientId==obj.ClientId && Cost==obj.Cost && Completed==obj.Completed);   
        }
    }
}