using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    internal class ClientDbRepository : IClientDbRepository
    {
        private RevisoChallengeEntities _context;

        public ClientDbRepository(DbContext context)
        {
            _context = (RevisoChallengeEntities) context;
        }

        public RevisoChallengeEntities GetContext()
        {
            return _context;
        }

        DbContext IDbRepository.GetContext()
        {
            return _context;
        }

        public void SetContext(DbContext context)
        {
            _context = (RevisoChallengeEntities) context;
        }

        public Client Get(int id)
        {
            return GetContext().Clients.FirstOrDefault(d => d.Id == id);
        }

        public IList<Client> GetAll()
        {
            return GetContext().Clients.ToList();
        }

        public bool Create(Client item)
        {
            try
            {
                GetContext().Clients.Add(item);
                GetContext().SaveChanges();
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
                var originalItem = GetContext().Clients.FirstOrDefault(x => x.Id == item.Id);
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

        public bool Delete(Client item)
        {
            try
            {
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
    }
}