using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Tests.Repositories.Implementation.InMemoryRepository
{
    public class MockProjectDbRepository: IProjectDbRepository
    {
        readonly List<Project> _projectList = new List<Project>();
        public Exception ExceptionToThrow { get; set; }
        

        public void SaveChanges(Project projectToUpdate)
        {

            foreach (Project project in _projectList)
            {
                if (project.Id == projectToUpdate.Id)
                {
                    _projectList.Remove(project);
                    _projectList.Add(projectToUpdate);
                    break;
                }
            }
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Add(Project projectToAdd)
        {
            _projectList.Add(projectToAdd);
        }

        public Project Get(int id)
        {
            return _projectList.FirstOrDefault(d => d.Id == id);
        }

        public IList<Project> GetAll()
        {
            return _projectList;
        }

        public bool Create(Project projectToCreate)
        {
            try
            {
                _projectList.Add(projectToCreate);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;               
            }            
        }

        public  bool Delete(Project projectToDelete)
        {
            try
            {
                _projectList.Remove(projectToDelete);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(Project item)
        {
            try
            {
                var projectToUpdate = _projectList.FirstOrDefault(x => x.Id == item.Id);
                if (projectToUpdate != null)
                {
                    _projectList.Remove(projectToUpdate);
                    _projectList.Add(projectToUpdate);
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public DbContext GetContext()
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
