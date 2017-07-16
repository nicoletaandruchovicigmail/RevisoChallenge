using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    class ClientDbRepository : IClientDbRepository
    {
        private RevisoChallengeEntities _context;

        public ClientDbRepository(DbContext context)
        {
            _context = (RevisoChallengeEntities)context;
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

        public bool Create(Client item)
        {
            try
            {
                GetContext().Clients.Add(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(Client item)
        {
            try
            {
                GetContext().Clients.Attach(item);
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

        public bool Delete(Client item)
        {
            try
            {
                GetContext().Clients.Attach(item);
                GetContext().Clients.Remove(item);
                GetContext().SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Client Get(int id)
        {
            return GetContext().Clients.Find(id);
        }

        public IList<Client> GetAll()
        {
            return GetContext().Clients.ToList();
        }
    }
}
