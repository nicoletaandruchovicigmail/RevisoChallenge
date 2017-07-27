using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Implementation.Database;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Services.Implementation
{
    public class DalServices
    {
        private readonly IProjectDbRepository _projectDbRepository;
        private readonly IClientDbRepository _clientDbRepository;        

        /// <summary>
        /// Default constructor
        /// </summary>
        public DalServices() : this(new ProjectDbRepository(new RevisoChallengeEntities()),new ClientDbRepository(new RevisoChallengeEntities()))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectDbRepository"></param>
        /// <param name="clientDbRepository"></param>
        public DalServices(IProjectDbRepository projectDbRepository,IClientDbRepository clientDbRepository)
        {
            _projectDbRepository = projectDbRepository;
            _clientDbRepository = clientDbRepository;            
        }

        //Methods for Project repository
        public Project GetProject(int id)
        {
            return _projectDbRepository.Get(id);
        }

        public IList<Project> GetProjects()
        {          
            return _projectDbRepository.GetAll();
        }

        public void AddProject(Project project)
        {
            _projectDbRepository.Create(project);            
        }

        public void UpdateProject(Project project)
        {
            _projectDbRepository.Update(project);
        }

        //Methods for Client repository
        public Client GetClient(int id)
        {
            return _clientDbRepository.Get(id);
        }

        public IList<Client> GetClients()
        {        
            return _clientDbRepository.GetAll();
        }

        public void AddClient(Client client)
        {
            _clientDbRepository.Create(client);
        }

        public void UpdateClient(Client client)
        {
            _clientDbRepository.Update(client);
        }


        // Methods for Task repository

        public Task GetTask(int id)
        {
            var context = new RevisoChallengeEntities();
            ITaskDbRepository clientRepository = new TaskDbRepository(context);

            return clientRepository.Get(id);
        }

        public IList<Task> GetTasks()
        {
            var context = new RevisoChallengeEntities();
            ITaskDbRepository taskRepository = new TaskDbRepository(context);

            return taskRepository.GetAll();
        }

        public IList<Task> GetTasksByProjectId(int projectId)
        {
            var context = new RevisoChallengeEntities();
            ITaskDbRepository taskRepository = new TaskDbRepository(context);

            return (taskRepository.GetAll().Where(x => x.ProjectId == projectId)).ToList();
        }

        private void AddTask(Task task)
        {
            DbContext context = new RevisoChallengeEntities();
            ITaskDbRepository projectRepository = new TaskDbRepository(context);
            projectRepository.Create(task);
        }

    }
}
