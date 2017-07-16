using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Implementation.Database;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Mock
{
    public class MockProjectDbRepository: IProjectDbRepository
    {
        readonly List<Project> _projectList = new List<Project>();

        public  bool Create(Project item)
        {
            try
            {
                _projectList.Add(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;               
            }            
        }

        public  bool Delete(Project item)
        {
            throw new NotImplementedException();
        }

        public  bool Update(Project item)
        {
            throw new NotImplementedException();
        }

        public  Project Get(int id)
        {
            throw new NotImplementedException();
        }

        public  IList<Project> GetAll()
        {
            return _projectList;
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
