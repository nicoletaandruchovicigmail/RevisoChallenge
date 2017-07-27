using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    public class ProjectDbRepository :IProjectDbRepository
    {
        private RevisoChallengeEntities _context;

        public ProjectDbRepository(DbContext context)
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
            _context = (RevisoChallengeEntities) context;
        }

        public Project Get(int id)
        {
            return GetContext().Projects.FirstOrDefault(d => d.Id == id);
        }

        public IList<Project> GetAll()
        {
            return GetContext().Projects.ToList();
        }

        public bool Create(Project item)
        {
            try
            {
                GetContext().Projects.Add(item);
                GetContext().SaveChanges();
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
                var originalItem = GetContext().Projects.FirstOrDefault(x=>x.Id==item.Id);
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

        public bool Delete(Project item)
        {
            try
            {
                GetContext().Projects.Attach(item);
                GetContext().Projects.Remove(item);
                GetContext().SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }      
        }
    }
}