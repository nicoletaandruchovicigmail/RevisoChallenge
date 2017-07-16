using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    public class ProjectDbRepository :IProjectDbRepository
    {
        private RevisoChallengeEntities _context;

        public ProjectDbRepository(DbContext context)
        {
            //_context = context;
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


        public bool Create(Project item)
        {
            try
            {
                GetContext().Projects.Add(item);
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
                GetContext().Projects.Attach(item);
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

        public Project Get(int id)
        {
            return GetContext().Projects.Find(id);
        }

        public IList<Project> GetAll()
        {
            return GetContext().Projects.ToList();
        }

    }
}