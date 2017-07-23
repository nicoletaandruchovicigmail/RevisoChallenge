using System;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    [Serializable]
    public class TaskViewModel
    {
        public TaskViewModel() { }

        public TaskViewModel(Task task)
        {
            Id = task.Id;
            Name = task.Name;
            Description = task.Description;
            EstimatedHours = task.EstimatedHours;
            ActualHours = task.ActualHours;
            ProjectId = task.ProjectId;
            ProjectName = GetProjectName(ProjectId);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedHours { get; set; }
        public int? ActualHours { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string GetProjectName(int projectId)
        {
            var service = new DalServices();
            var project = service.GetProject(ProjectId);

            if (project != null)
                return project.Name;
            else
                return String.Empty;
        }
    }
}