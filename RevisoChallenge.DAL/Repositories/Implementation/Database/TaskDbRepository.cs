using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    class TaskDbRepository : ITaskDbRepository
    {
        private RevisoChallengeEntities _context;

        public TaskDbRepository(DbContext context)
        {
            _context = (RevisoChallengeEntities) context;
        }

        DbContext IDbRepository.GetContext()
        {
            return _context;
        }

        public RevisoChallengeEntities GetContext()
        {
            return _context;
        }

        public void SetContext(DbContext context)
        {
            _context = (RevisoChallengeEntities)context;
        }

        public bool Create(Task item)
        {
            GetContext().Tasks.Add(item);
            return true;
        }

        public bool Update(Task item)
        {
            try
            {
                GetContext().Tasks.Attach(item);
                var entry = GetContext().Entry(item);

                //entry.State = EntityState.Modified;
                entry.Property("Name").IsModified = true;
                //other properties

                GetContext().SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(Task item)
        {
            try
            {
                GetContext().Tasks.Attach(item);
                GetContext().Tasks.Remove(item);
                //GetContext().Entry(item).State=EntityState.Deleted;

                GetContext().SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Task Get(int id)
        {
            return GetContext().Tasks.Find(id);
        }

        public IList<Task> GetAll()
        {
            return GetContext().Tasks.ToList();
        }
    }
}
