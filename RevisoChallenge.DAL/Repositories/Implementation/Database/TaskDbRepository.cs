using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            GetContext().SaveChanges();
            return true;
        }

        public bool Update(Task item)
        {
            try
            {
                var originalItem = GetContext().Tasks.FirstOrDefault(x => x.Id == item.Id);
                var entry = GetContext().Entry(originalItem);

                entry.CurrentValues.SetValues(item);
                entry.State = EntityState.Modified;
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
                GetContext().Tasks.Remove(item);
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
            return GetContext().Tasks.FirstOrDefault(d => d.Id == id);
        }

        public IList<Task> GetAll()
        {
            return GetContext().Tasks.ToList();
        }
    }
}
